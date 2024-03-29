﻿using System;
using System.IO;
using System.Net;

namespace GetWeather
{
    static class WebServiceClient
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
    }
}
