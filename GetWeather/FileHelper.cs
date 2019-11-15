using System;
using System.Collections.Generic;
using System.IO;

namespace GetWeather
{
    static class FileHelper
    {
        static string filePath = Environment.CurrentDirectory + "\\cach.txt";

        public static void CreateCachFileIfNotExists()
        {
            if (!File.Exists(filePath))
            {
                var fileStream = File.Create(filePath);
                fileStream.Close();
            }
        }

        public static void ReadFromFileToDirectories(Dictionary<string, DateTime> cachTime, Dictionary<string, string> cachOutput)
        {
            string[] readText = File.ReadAllLines(filePath);
            for (int i = 0; i < readText.Length; i++)
            {
                string key = readText[i].Substring(0, readText[i].IndexOf("<"));
                string timeText = readText[i].Substring(readText[i].IndexOf("<") + 1, readText[i].IndexOf(">") - readText[i].IndexOf("<") - 1);
                cachTime[key] = DateTime.Parse(timeText);
                string outputText = readText[i].Substring(readText[i].IndexOf(">") + 1, readText[i].Length - readText[i].IndexOf(">") - 1);
                cachOutput[key] = outputText;
            }
        }

        public static void WriteFromDictionariesToFile(Dictionary<string, DateTime> cachTime, Dictionary<string, string> cachOutput)
        {
            File.WriteAllText(filePath, string.Empty);
            foreach (var key in cachTime.Keys)
            {
                File.AppendAllText(filePath, key + "<" + cachTime[key] + ">" + cachOutput[key] + Environment.NewLine);
            }
        }

    }
}
