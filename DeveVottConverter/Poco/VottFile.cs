using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Globalization;

namespace DeveVottConverter.Poco
{
    public partial class VottFile
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("securityToken")]
        public string SecurityToken { get; set; }

        [JsonProperty("sourceConnection")]
        public Connection SourceConnection { get; set; }

        [JsonProperty("targetConnection")]
        public Connection TargetConnection { get; set; }

        [JsonProperty("videoSettings")]
        public VideoSettings VideoSettings { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("exportFormat")]
        public ExportFormat ExportFormat { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lastVisitedAssetId")]
        public string LastVisitedAssetId { get; set; }

        [JsonProperty("assets")]
        public Dictionary<string, VottAsset> Assets { get; set; }

        [JsonProperty("activeLearningSettings")]
        public ActiveLearningSettings ActiveLearningSettings { get; set; }
    }

    public partial class ActiveLearningSettings
    {
        [JsonProperty("autoDetect")]
        public bool AutoDetect { get; set; }

        [JsonProperty("predictTag")]
        public bool PredictTag { get; set; }

        [JsonProperty("modelPathType")]
        public string ModelPathType { get; set; }
    }

    public partial class VottAsset
    {
        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("size")]
        public Size Size { get; set; }

        [JsonProperty("state")]
        public long State { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }
    }

    public partial class Size
    {
        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }
    }

    public partial class ExportFormat
    {
        [JsonProperty("providerType")]
        public string ProviderType { get; set; }

        [JsonProperty("providerOptions")]
        public ProviderOptions ProviderOptions { get; set; }
    }

    public partial class ProviderOptions
    {
        [JsonProperty("encrypted")]
        public string Encrypted { get; set; }
    }

    public partial class Connection
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("providerType")]
        public string ProviderType { get; set; }

        [JsonProperty("providerOptions")]
        public ProviderOptions ProviderOptions { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Tag
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }
    }

    public partial class VideoSettings
    {
        [JsonProperty("frameExtractionRate")]
        public long FrameExtractionRate { get; set; }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
