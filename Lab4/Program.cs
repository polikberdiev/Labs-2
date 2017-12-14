using System;
using System.IO;
using System.Linq;
using Lab4.BL;
using Lab4.Properties;

namespace Lab4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var xmls = Directory
                .GetFiles(Settings.Default.FolderPath)
                .Select(File.OpenRead)
                .ToArray();

            var searcher = new XmlFilesSearcher(Settings.Default.ThreadsCount);
            // ReSharper disable once CoVariantArrayConversion
            var result = searcher.Search(xmls, Settings.Default.SearchXPath);

            result
                .OrderBy(kv => kv.Key)
                .ToList()
                .ForEach(kv =>
                {
                    Console.WriteLine($"{(String.IsNullOrEmpty(kv.Key) ? "N/A" : kv.Key)}, {kv.Value}");
                });

            Console.ReadLine();
        }
    }
}