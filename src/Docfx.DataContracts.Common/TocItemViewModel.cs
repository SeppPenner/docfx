// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using System.Text.Json.Serialization;
using Docfx.Common;
using Docfx.YamlSerialization;
using Newtonsoft.Json;
using YamlDotNet.Serialization;

namespace Docfx.DataContracts.Common;

public class TocItemViewModel
{
    [YamlMember(Alias = Constants.PropertyName.Uid)]
    [JsonProperty(Constants.PropertyName.Uid)]
    [JsonPropertyName(Constants.PropertyName.Uid)]
    public string Uid { get; set; }

    [YamlMember(Alias = Constants.PropertyName.Name)]
    [JsonProperty(Constants.PropertyName.Name)]
    [JsonPropertyName(Constants.PropertyName.Name)]
    public string Name { get; set; }

    [YamlMember(Alias = Constants.PropertyName.DisplayName)]
    [JsonProperty(Constants.PropertyName.DisplayName)]
    [JsonPropertyName(Constants.PropertyName.DisplayName)]
    public string DisplayName { get; set; }

    [ExtensibleMember(Constants.ExtensionMemberPrefix.Name)]
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public SortedList<string, string> NameInDevLangs { get; } = new SortedList<string, string>();

    [YamlIgnore]
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public string NameForCSharp
    {
        get
        {
            NameInDevLangs.TryGetValue(Constants.DevLang.CSharp, out string result);
            return result;
        }
        set { NameInDevLangs[Constants.DevLang.CSharp] = value; }
    }

    [YamlIgnore]
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public string NameForVB
    {
        get
        {
            NameInDevLangs.TryGetValue(Constants.DevLang.VB, out string result);
            return result;
        }
        set { NameInDevLangs[Constants.DevLang.VB] = value; }
    }

    [YamlMember(Alias = Constants.PropertyName.Href)]
    [JsonProperty(Constants.PropertyName.Href)]
    [JsonPropertyName(Constants.PropertyName.Href)]
    public string Href { get; set; }

    [YamlMember(Alias = "originalHref")]
    [JsonProperty("originalHref")]
    [JsonPropertyName("originalHref")]
    public string OriginalHref { get; set; }

    [YamlMember(Alias = Constants.PropertyName.TocHref)]
    [JsonProperty(Constants.PropertyName.TocHref)]
    [JsonPropertyName(Constants.PropertyName.TocHref)]
    public string TocHref { get; set; }

    [YamlMember(Alias = "originalTocHref")]
    [JsonProperty("originalTocHref")]
    [JsonPropertyName("originalTocHref")]
    public string OriginalTocHref { get; set; }

    [YamlMember(Alias = Constants.PropertyName.TopicHref)]
    [JsonProperty(Constants.PropertyName.TopicHref)]
    [JsonPropertyName(Constants.PropertyName.TopicHref)]
    public string TopicHref { get; set; }

    [YamlMember(Alias = "originalTopicHref")]
    [JsonProperty("originalTopicHref")]
    [JsonPropertyName("originalTopicHref")]
    public string OriginalTopicHref { get; set; }

    [YamlIgnore]
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public string AggregatedHref { get; set; }

    [YamlMember(Alias = "includedFrom")]
    [JsonProperty("includedFrom")]
    [JsonPropertyName("includedFrom")]
    public string IncludedFrom { get; set; }

    [YamlMember(Alias = "homepage")]
    [JsonProperty("homepage")]
    [JsonPropertyName("homepage")]
    public string Homepage { get; set; }

    [YamlMember(Alias = "originalHomepage")]
    [JsonProperty("originalHomepage")]
    [JsonPropertyName("originalHomepage")]
    public string OriginalHomepage { get; set; }

    [YamlMember(Alias = "homepageUid")]
    [JsonProperty("homepageUid")]
    [JsonPropertyName("homepageUid")]
    public string HomepageUid { get; set; }

    [YamlMember(Alias = Constants.PropertyName.TopicUid)]
    [JsonProperty(Constants.PropertyName.TopicUid)]
    [JsonPropertyName(Constants.PropertyName.TopicUid)]
    public string TopicUid { get; set; }

    [YamlIgnore]
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public string AggregatedUid { get; set; }

    [YamlMember(Alias = "items")]
    [JsonProperty("items")]
    [JsonPropertyName("items")]
    public TocViewModel Items { get; set; }

    [YamlIgnore]
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public bool IsHrefUpdated { get; set; }

    [ExtensibleMember]
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();

    [EditorBrowsable(EditorBrowsableState.Never)]
    [YamlIgnore]
    [Newtonsoft.Json.JsonExtensionData]
    [System.Text.Json.Serialization.JsonExtensionData]
    public CompositeDictionary MetadataJson =>
        CompositeDictionary
            .CreateBuilder()
            .Add(Constants.ExtensionMemberPrefix.Name, NameInDevLangs, JTokenConverter.Convert<string>)
            .Add(string.Empty, Metadata)
            .Create();

    public TocItemViewModel Clone()
    {
        var cloned = (TocItemViewModel)MemberwiseClone();
        if (cloned.Items != null)
        {
            cloned.Items = Items.Clone();
        }
        return cloned;
    }

    public override string ToString()
    {
        var result = string.Empty;
        result += PropertyInfo(nameof(Name), Name);
        result += PropertyInfo(nameof(Href), Href);
        result += PropertyInfo(nameof(TopicHref), TopicHref);
        result += PropertyInfo(nameof(TocHref), TocHref);
        result += PropertyInfo(nameof(Uid), Uid);
        result += PropertyInfo(nameof(TopicUid), TopicUid);
        return result;

        static string PropertyInfo(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            return $"{name}:{value} ";
        }
    }
}
