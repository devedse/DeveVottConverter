using DeveCoolLib.OtherExtensions;
using DeveVottConverter.Poco;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DeveVottConverter
{
    public static class VottConverter
    {
        public static string ConvertToCsvForYolo(string vottFilePath)
        {
            var vottFileContent = File.ReadAllText(vottFilePath);
            var vott = JsonConvert.DeserializeObject<VottFile>(vottFileContent);
            var vottDir = Path.GetDirectoryName(vottFilePath);

            var allTags = new Dictionary<string, int>();
            for (int i = 0; i < vott.Tags.Length; i++)
            {
                allTags.Add(vott.Tags[i].Name, i);
            }

            var sb = new StringBuilder();

            foreach (var vottAsset in vott.Assets)
            {
                var expectedAssetJsonPath = Path.Combine(vottDir, $"{vottAsset.Key}-asset.json");

                if (!File.Exists(expectedAssetJsonPath))
                {
                    Console.WriteLine($"Skipping: {expectedAssetJsonPath}");
                    continue;
                }

                var assetContent = File.ReadAllText(expectedAssetJsonPath);
                var asset = JsonConvert.DeserializeObject<VottJsonAsset>(assetContent);

                var c = CultureInfo.InvariantCulture;

                var imagePath = asset.asset.path.TrimStartOnce("file:", StringComparison.OrdinalIgnoreCase);
                var taggedBoxes = asset.regions.Select(t => $"{t.boundingBox.left.ToString(c)},{t.boundingBox.top.ToString(c)},{(t.boundingBox.left + t.boundingBox.width).ToString(c)},{(t.boundingBox.top + t.boundingBox.height).ToString(c)},{allTags[t.tags.First()]}");
                var taggedBoxesString = string.Join(' ', taggedBoxes);
                var line = $"\"{imagePath}\" {taggedBoxesString}";
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}
