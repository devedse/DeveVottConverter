using System;
using System.IO;
using System.Linq;

namespace DeveVottConverter.ConsoleApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var vottFilePath = @"C:\XGitPrivate\DeveLicensePlateDataSet\Export\DeveLicensePlateDataSet.vott";
            var convertedContent = VottConverter.ConvertToCsvForYoloV4Tensorflow(vottFilePath, true);

            RandomListSorter.Shuffle(convertedContent, 1337);

            int testSetCount = (int)Math.Round(convertedContent.Count / 100.0 * 20.0);
            int trainSetCount = convertedContent.Count - testSetCount;

            var trainSet = convertedContent.Take(trainSetCount).OrderBy(t => t).ToList();
            var testSet = convertedContent.Skip(trainSetCount).OrderBy(t => t).ToList();

            File.WriteAllLines("train.txt", trainSet);
            File.WriteAllLines("val.txt", testSet);
        }
    }
}
