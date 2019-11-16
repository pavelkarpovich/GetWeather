using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GetWeather
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Cache cache = new Cache();
            cache.CreateCachFileIfNotExists();
            cache.ReadFromCacheFileToDictionaries();

            Console.WriteLine("Getting weather data from https://openweathermap.org/");
            Console.WriteLine();



            string output = InputOutput.DataInput(cache);

            WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(output);
            InputOutput.ResultsOutput(weatherData);

            Console.ReadLine();
        }
    }
}
