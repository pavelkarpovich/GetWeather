using System;
using System.Collections.Generic;
using System.Text;

namespace GetWeather
{
    static class InputOutput
    {
        public static string DataInput(Cache cache)
        {
            string location, output = "";
            bool requestSuccess = false;
            do
            {
                Console.Write("Please, enter location name (e.g. London): ");
                location = Console.ReadLine();

                if (cache.CacheTime.ContainsKey(location.ToUpper()))
                {
                    DateTime timeFirst = cache.CacheTime[location.ToUpper()];
                    DateTime timeNow = DateTime.Now;
                    var diff = timeNow.Subtract(timeFirst).TotalMinutes;
                    if (diff < 60)
                    {
                        output = cache.CacheOutput[location.ToUpper()];

                        Console.WriteLine();
                        Console.WriteLine("Data from cach for time {0}", cache.CacheTime[location.ToUpper()]);

                        requestSuccess = true;
                    }
                }

                if (output == "")
                {
                    try
                    {
                        output = WebServiceClient.MakeRequest(location);

                        cache.CacheTime[location.ToUpper()] = DateTime.Now;
                        cache.CacheOutput[location.ToUpper()] = output;

                        Cache.WriteFromDictionariesToCacheFile(cache.CacheTime, cache.CacheOutput);
                        requestSuccess = true;
                    }
                    catch
                    {
                        Console.WriteLine("Request error. Please, check location name");
                        requestSuccess = false;
                    }
                }
            } while (!requestSuccess);
            return output;
        }

        public static void ResultsOutput(WeatherData weatherData)
        {
            Console.WriteLine();
            Console.WriteLine("Current weather in {0}, {1}", weatherData.Name, weatherData.Sys.Country);
            Console.WriteLine("Location coordinates: longitude={0}, latitude={1}", weatherData.Coordinates.Longitude, weatherData.Coordinates.Latitude);
            Console.WriteLine();
            Console.WriteLine("Main: {0}", weatherData.WeatherList[0].Main);
            Console.WriteLine("Description: {0}", weatherData.WeatherList[0].Description);
            Console.WriteLine("Actual temperature: {0}", weatherData.Main.Temp);
            Console.WriteLine("Minimal temperature: {0}", weatherData.Main.Temp_min);
            Console.WriteLine("Maximal temperature: {0}", weatherData.Main.Temp_max);
            Console.WriteLine("Pressure: {0}", weatherData.Main.Pressure);
            Console.WriteLine("Humidity: {0}", weatherData.Main.Humidity);
            Console.WriteLine("Wind speed: {0}", weatherData.Wind.Speed);
            Console.WriteLine("Wind degree: {0}", weatherData.Wind.Deg);
        }
    }
}
