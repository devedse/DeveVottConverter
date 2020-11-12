using System.IO;

namespace DeveVottConverter.ConsoleApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var vottFilePath = @"C:\XGitPrivate\DeveLicensePlateDataSet\Export\DeveLicensePlateDataSet.vott";
            var convertedContent = VottConverter.ConvertToCsvForYolo(vottFilePath);

            File.WriteAllText("annotations.csv", convertedContent);
        }
    }
}
