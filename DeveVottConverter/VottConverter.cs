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
        public static List<string> ConvertToCsvForYoloV4Tensorflow(string vottFilePath, bool useShortPath)
        {
            var vottFileContent = File.ReadAllText(vottFilePath);
            var vott = JsonConvert.DeserializeObject<VottFile>(vottFileContent);
            var vottDir = Path.GetDirectoryName(vottFilePath);

            var allTags = new Dictionary<string, int>();
            for (int i = 0; i < vott.Tags.Length; i++)
            {
                allTags.Add(vott.Tags[i].Name, i);
            }

            var stringOutput = new List<string>();

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
                if (useShortPath)
                {
                    imagePath = Path.GetFileName(imagePath);
                }


                var taggedBoxes = asset.regions.Select(t =>
                {
                    var x = (int)t.boundingBox.left;
                    var y = (int)t.boundingBox.top;
                    var width = (int)t.boundingBox.width;
                    var height = (int)t.boundingBox.height;

                    var centerX = x + (width / 2);
                    var centerY = y + (height / 2);

                    var cx = centerX.ToString(c);
                    var cy = centerY.ToString(c);
                    var w1 = width.ToString(c);
                    var h1 = height.ToString(c);

                    var tag = allTags[t.tags.First()];
                    return $"{tag},{cx},{cy},{w1},{h1}";
                });
                var taggedBoxesString = string.Join(' ', taggedBoxes);
                var line = $"{imagePath} {taggedBoxesString}";
                stringOutput.Add(line);
            }

            return stringOutput;
        }

        public static List<string> ConvertToCsvForYolo(string vottFilePath, bool useShortPath)
        {
            var vottFileContent = File.ReadAllText(vottFilePath);
            var vott = JsonConvert.DeserializeObject<VottFile>(vottFileContent);
            var vottDir = Path.GetDirectoryName(vottFilePath);

            var allTags = new Dictionary<string, int>();
            for (int i = 0; i < vott.Tags.Length; i++)
            {
                allTags.Add(vott.Tags[i].Name, i);
            }

            var stringOutput = new List<string>();

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
                if (useShortPath)
                {
                    imagePath = Path.GetFileName(imagePath);
                }


                var taggedBoxes = asset.regions.Select(t =>
                {
                    var x = (int)t.boundingBox.left;
                    var y = (int)t.boundingBox.top;
                    var width = (int)t.boundingBox.width;
                    var height = (int)t.boundingBox.height;

                    var x1 = x.ToString(c);
                    var y1 = y.ToString(c);
                    var x2 = (x + width).ToString(c);
                    var y2 = (y + height).ToString(c);
                    var tag = allTags[t.tags.First()];
                    return $"{x1},{y1},{x2},{y2},{tag}";
                });
                var taggedBoxesString = string.Join(' ', taggedBoxes);
                var line = $"{imagePath} {taggedBoxesString}";
                stringOutput.Add(line);
            }

            return stringOutput;
        }
    }
}
