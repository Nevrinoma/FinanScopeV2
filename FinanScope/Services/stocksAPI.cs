using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace FinanScope.Services
{
    internal class stocksAPI
    {
        public static decimal GetStocks(string symbol)
        {
            string url = $"https://stock-prices2.p.rapidapi.com/api/v1/resources/stock-prices/1d?ticker={symbol}";

            WebRequest request = WebRequest.Create(url);
            request.Headers["X-RapidAPI-Host"] = "stock-prices2.p.rapidapi.com";
            request.Headers["X-RapidAPI-Key"] = "be43cea060mshc4dd49f4a6c7a6fp1f9a6ejsn5aa22bdcfd70";
            request.Method = "GET";
            request.ContentType = "application/json";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();

                        JObject stocksData = JObject.Parse(json);
                        JObject stock = (JObject)stocksData.First.First;


                        decimal kek = (decimal)stock["Close"];
                        

                        return kek;
                    }
                }
            }
        }
        
    }
}