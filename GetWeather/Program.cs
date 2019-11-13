using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GetWeather
{
    class Program
    {
        static Dictionary<string, DateTime> cachTime = new Dictionary<string, DateTime>();
        static Dictionary<string, string> cachOutput = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            FileHelper.ReadFromFileToDirectories(cachTime, cachOutput);

            Console.WriteLine("Getting weather data from https://openweathermap.org/");
            Console.WriteLine();

            string location, output = "";
            bool requestSuccess = false;
            do
            {
                Console.Write("Please, enter location name (e.g. London): ");
                location = Console.ReadLine();

                if (cachTime.ContainsKey(location.ToUpper()))
                {
                    DateTime timeFirst = cachTime[location.ToUpper()];
                    DateTime timeNow = DateTime.Now;
                    var diff = timeNow.Subtract(timeFirst).TotalMinutes;
                    if (diff < 60)
                    {
                        output = cachOutput[location.ToUpper()];

                        Console.WriteLine();
                        Console.WriteLine("Data from cach for time {0}", cachTime[location.ToUpper()]);

                        requestSuccess = true;
                    }
                }

                if (output == "")
                {
                    try
                    {
                        output = Helper.MakeRequest(location);

                        cachTime[location.ToUpper()] = DateTime.Now;
                        cachOutput[location.ToUpper()] = output;

                        FileHelper.WriteFromDictionariesToFile(cachTime, cachOutput);
                        requestSuccess = true;
                    }
                    catch
                    {
                        Console.WriteLine("Request error. Please, check location name");
                        requestSuccess = false;
                    }
                }
            } while (!requestSuccess);

            WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(output);
            Helper.ResultsOutput(weatherData);

            Console.ReadLine();
        }
    }
}
