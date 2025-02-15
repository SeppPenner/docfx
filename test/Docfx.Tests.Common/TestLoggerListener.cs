﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Docfx.Common;

namespace Docfx.Tests.Common;

public class TestLoggerListener : ILoggerListener
{
    public List<ILogItem> Items { get; } = new List<ILogItem>();

    private readonly Func<ILogItem, bool> _filter;

    public TestLoggerListener(Func<ILogItem, bool> filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        _filter = filter;
    }

    #region ILoggerListener

    public void WriteLine(ILogItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (_filter(item))
        {
            Items.Add(item);
        }
    }

    public void Flush()
    {
    }

    public void Dispose()
    {
    }

    #endregion

    #region Creators

    public static TestLoggerListener CreateLoggerListenerWithCodeFilter(string code, LogLevel logLevel = LogLevel.Warning)
        => new(i => i.LogLevel >= logLevel && i?.Code == code);

    public static TestLoggerListener CreateLoggerListenerWithCodesFilter(List<string> codes, LogLevel logLevel = LogLevel.Warning)
        => new(i => i.LogLevel >= logLevel && codes.Contains(i.Code));

    public static TestLoggerListener CreateLoggerListenerWithPhaseStartFilter(string phase, LogLevel logLevel = LogLevel.Warning)
    {
        return new TestLoggerListener(iLogItem =>
        {
            if (iLogItem.LogLevel < logLevel)
            {
                return false;
            }
            if (phase == null ||
               (iLogItem?.Phase != null && iLogItem.Phase.StartsWith(phase)))
            {
                return true;
            }
            return false;
        });
    }

    public static TestLoggerListener CreateLoggerListenerWithPhaseEndFilter(string phase, LogLevel logLevel = LogLevel.Warning)
    {
        return new TestLoggerListener(iLogItem =>
        {
            if (iLogItem.LogLevel < logLevel)
            {
                return false;
            }
            if (phase == null ||
               (iLogItem?.Phase != null && iLogItem.Phase.EndsWith(phase)))
            {
                return true;
            }
            return false;
        });
    }

    public static TestLoggerListener CreateLoggerListenerWithPhaseEqualFilter(string phase, LogLevel logLevel = LogLevel.Warning)
    {
        return new TestLoggerListener(iLogItem =>
        {
            if (iLogItem.LogLevel < logLevel)
            {
                return false;
            }
            if (phase == null ||
               (iLogItem?.Phase != null && iLogItem.Phase == phase))
            {
                return true;
            }
            return false;
        });
    }

    #endregion

    #region Helper methods

    public IEnumerable<ILogItem> GetItemsByLogLevel(LogLevel logLevel)
    {
        return Items.Where(i => i.LogLevel == logLevel);
    }

    #endregion
}
