using System;
using System.Collections.Generic;
using System.IO;

namespace GetWeather
{
    class Cache
    {
        static readonly string filePath = Environment.CurrentDirectory + "\\cach.txt";

        //private Dictionary<string, DateTime> cacheTime;
        //private Dictionary<string, string> cacheOutput;

        public Cache()
        {
            CacheTime = new Dictionary<string, DateTime>();
            CacheOutput = new Dictionary<string, string>();
        }

        public Dictionary<string, DateTime> CacheTime { get; set; }
        public Dictionary<string, string> CacheOutput { get; set; }


        public void CreateCachFileIfNotExists()
        {
            if (!File.Exists(filePath))
            {
                var fileStream = File.Create(filePath);
                fileStream.Close();
            }
        }

        //public void ReadFromCacheFileToDictionaries(Dictionary<string, DateTime> cachTime, Dictionary<string, string> cachOutput)
        public void ReadFromCacheFileToDictionaries()
        {
            string[] readText = File.ReadAllLines(filePath);
            for (int i = 0; i < readText.Length; i++)
            {
                string key = readText[i].Substring(0, readText[i].IndexOf("<"));
                string timeText = readText[i].Substring(readText[i].IndexOf("<") + 1, readText[i].IndexOf(">") - readText[i].IndexOf("<") - 1);
                CacheTime[key] = DateTime.Parse(timeText);
                string outputText = readText[i].Substring(readText[i].IndexOf(">") + 1, readText[i].Length - readText[i].IndexOf(">") - 1);
                CacheOutput[key] = outputText;
            }
        }

        public static void WriteFromDictionariesToCacheFile(Dictionary<string, DateTime> cachTime, Dictionary<string, string> cachOutput)
        {
            File.WriteAllText(filePath, string.Empty);
            foreach (var key in cachTime.Keys)
            {
                File.AppendAllText(filePath, key + "<" + cachTime[key] + ">" + cachOutput[key] + Environment.NewLine);
            }
        }
    }
}
