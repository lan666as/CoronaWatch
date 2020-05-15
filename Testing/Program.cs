using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Text.RegularExpressions;
using System.Windows;
using Microsoft.EntityFrameworkCore.Sqlite;
using CoronaWatchDB;
using System.Security.Cryptography;

namespace Testing
{
    class Program
    {
        private static readonly string API = "https://api.covid19api.com/";

        static void Main()
        {
            #region Testing Aldo
            try
            {
                var client = new RestClient(API + "summary")
                {
                    Timeout = -1
                };
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                var json = SimpleJson.DeserializeObject(response.Content);
                JObject obj = JObject.Parse(json.ToString());
                JsonArray array = (JsonArray) SimpleJson.DeserializeObject(obj["Countries"].ToString());

                CoronaWatchContext context = new CoronaWatchContext();

                foreach (JsonObject jsonObject in array)
                {
                    string ISOCode = jsonObject["CountryCode"].ToString();
                    Console.WriteLine(ISOCode);
                    if (context.RegionDBs.Where(r => r.ISOCode == ISOCode).FirstOrDefault() == null)
                    {
                        RegionDB regionDB = new RegionDB
                        {
                            ISOCode = jsonObject["CountryCode"].ToString(),
                            Name = jsonObject["Country"].ToString(),
                            Slug = jsonObject["Slug"].ToString()
                        };
                        context.RegionDBs.Add(regionDB);
                        context.SaveChanges();
                    }
                    Console.WriteLine("ABC");
                }
            }
            catch (Exception e)
            {

                if (e.Message == "Invalid JSON string")
                {
                    Console.WriteLine(e.Message + "\nPlease Check Your Connection Error");
                }
                else
                {
                    Console.WriteLine(e.Message + "Error");
                }
            }
            #endregion

            Console.ReadLine();
        }
    }
}
