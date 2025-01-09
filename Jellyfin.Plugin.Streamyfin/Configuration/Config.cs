#pragma warning disable CS8604
#pragma warning disable CA2227
#pragma warning disable CS8600
#pragma warning disable CS8603
#pragma warning disable CS8714
#pragma warning disable CA1002
#pragma warning disable CA1819
#pragma warning disable CA1507

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Querying;
using Jellyfin.Data.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
//using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
//using Newtonsoft.Json.Schema;
using Newtonsoft.Json;

namespace Jellyfin.Plugin.Streamyfin.Configuration;

public class Config
{
  // public Home? home { get; set; }
  public Settings.Settings? settings { get; set; }
}

public class Home
{
  public SerializableDictionary<string, Section>? sections { get; set; }
}

public class Section
{
  public SectionStyle? style { get; set; }
  public SectionType? type { get; set; }
  public SectionItemResolver? items { get; set; }
  [YamlMember(DefaultValuesHandling = DefaultValuesHandling.OmitNull)]
  public SectionSuggestions? suggestions { get; set; } = null;
}

[JsonConverter(typeof(StringEnumConverter))]
public enum SectionStyle
{
  portrait,
  landscape
}

[JsonConverter(typeof(StringEnumConverter))]
public enum SectionType
{
  row,
  carousel,
}


public class SectionItemResolver
{
  public ItemArgs? args { get; set; }
}

public class ItemArgs
{
  [JsonConverter(typeof(StringEnumConverter))]
  public ItemSortBy[]? sortBy { get; set; }
  [JsonConverter(typeof(StringEnumConverter))]
  public SortOrder[]? sortOrder { get; set; }
  public List<string>? genres { get; set; }
  public string? parentId { get; set; }
  [JsonConverter(typeof(StringEnumConverter))]
  public ItemFilter[]? filters { get; set; }
  public bool? recursive { get; set; }
}

public class SectionSuggestions
{
  public SuggestionsArgs? args { get; set; }
}

public class SuggestionsArgs
{
  [JsonConverter(typeof(StringEnumConverter))]
  public BaseItemKind[]? type { get; set; }
}

[XmlRoot("dictionary")]
public class SerializableDictionary<TKey, TValue>
       : Dictionary<TKey, TValue>, IXmlSerializable
{
  #region IXmlSerializable Members
  public System.Xml.Schema.XmlSchema GetSchema()
  {
    return null;
  }

  public void ReadXml(System.Xml.XmlReader reader)
  {
    XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
    XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

    bool wasEmpty = reader.IsEmptyElement;
    reader.Read();

    if (wasEmpty)
      return;

    while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
    {
      reader.ReadStartElement("item");

      reader.ReadStartElement("key");
      TKey key = (TKey)keySerializer.Deserialize(reader);
      reader.ReadEndElement();

      reader.ReadStartElement("value");
      TValue value = (TValue)valueSerializer.Deserialize(reader);
      reader.ReadEndElement();

      this.Add(key, value);

      reader.ReadEndElement();
      reader.MoveToContent();
    }
    reader.ReadEndElement();
  }

  public void WriteXml(System.Xml.XmlWriter writer)
  {
    XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
    XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

    foreach (TKey key in this.Keys)
    {
      writer.WriteStartElement("item");

      writer.WriteStartElement("key");
      keySerializer.Serialize(writer, key);
      writer.WriteEndElement();

      writer.WriteStartElement("value");
      TValue value = this[key];
      valueSerializer.Serialize(writer, value);
      writer.WriteEndElement();

      writer.WriteEndElement();
    }
  }
  #endregion
}
/*
home:
  sections:
    Trending:
      style: portrait
      type: row 
      items:
        args: 
          sortBy: AddedDate
          sortOrder: Ascending
          filterByGenre: [""Anime"", ""Comics""]";
*/