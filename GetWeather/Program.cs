using Newtonsoft.Json;
using System;

namespace GetWeather
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting weather data from https://openweathermap.org/");
            Console.WriteLine();

            string location, output = "";
            bool requestSuccess;
            do
            {
                try
                {
                    Console.Write("Please, enter location name (e.g. London): ");
                    location = Console.ReadLine();
                    output = Helper.MakeRequest(location);
                    requestSuccess = false;
                }
                catch
                {
                    Console.WriteLine("Request error. Please, check location name");
                    requestSuccess = true;
                }
            } while (requestSuccess);

            WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(output);
            Helper.ResultsOutput(weatherData);

            Console.ReadLine();
        }
    }
}
