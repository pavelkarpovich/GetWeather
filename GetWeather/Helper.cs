using System;
using System.IO;
using System.Net;

namespace GetWeather
{
    static class Helper
    {
        static readonly string url = "http://api.openweathermap.org/data/2.5/weather";
        static readonly string appid = "542ffd081e67f4512b705f89d2a611b2";

        public static string MakeRequest(string location)
        {
            string requestString = string.Format("{0}?q={1}&appid={2}", url, location, appid);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestString);

            request.Method = "GET";
            request.Accept = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string output = reader.ReadToEnd();

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
