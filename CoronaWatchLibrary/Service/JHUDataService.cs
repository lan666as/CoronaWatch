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

namespace CoronaWatchLibrary
{
    public class JHUDataService : BaseDataService
    {
        private static readonly string API = "https://api.covid19api.com/";
        private static Report Report;

        public static Report GetReports()
        {
            if (Report == null)
            {
                Report = FetchReports();
            }
            return Report;
        }
        private static Report FetchReports()
        {
            WebRequest request = WebRequest.Create(API + "summary");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dynamic stuff;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                stuff = JObject.Parse(responseFromServer);
            }
            var global = stuff["Global"];
            //Console.WriteLine(Global.ToString());
            return ParseWorldReports(global);
        }
        private static Report ParseWorldReports(JObject jObject)
        {
            Statistic statistic = new Statistic(confirmed: (int)jObject["TotalConfirmed"], recovered: (int)jObject["TotalRecovered"], death: (int)jObject["TotalDeaths"]);
            Report report = new Report(statistic, "JHU");
            return report;
        }


        public static void FetchTimeSeries()
        {
            throw new NotImplementedException();
        }

        public static List<Region> FetchAllRegion()
        {
            List<Region> regions = new List<Region>();
            var client = new RestClient("https://api.covid19api.com/countries")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            JsonArray array = (JsonArray)SimpleJson.DeserializeObject(response.Content);
            foreach (JsonObject json in array)
            {
                Region region = new Region(json["Country"].ToString(), json["ISO2"].ToString(), Region.EnumLevel.Province, new Coordinate(), "World")
                {
                    Slug = json["Slug"].ToString()
                };
                regions.Add(region);
            }
            return regions;
        }

        public static List<Report> FetchRegionSummary()
        {
            List<Report> reports = new List<Report>();
            var client = new RestClient(API + "summary")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var json = SimpleJson.DeserializeObject(response.Content);
            JObject obj = JObject.Parse(json.ToString());
            JsonArray array = (JsonArray)SimpleJson.DeserializeObject(obj["Countries"].ToString());

            foreach (JsonObject jsonObject in array)
            {
                Statistic statistic = new Statistic(Convert.ToInt32(jsonObject["TotalConfirmed"].ToString()), Convert.ToInt32(jsonObject["TotalRecovered"].ToString()), Convert.ToInt32(jsonObject["TotalDeaths"].ToString()))
                {
                    StatisticID = jsonObject["CountryCode"].ToString()
                };
                DateTime date = Convert.ToDateTime(Regex.Match(jsonObject["Date"].ToString(), @"\d{4}-\d{2}-\d{2}").Value);
                Report report = new Report(date, statistic);
                reports.Add(report);
            }
            return reports;
        }

        public static TimeSeries FetchTimeSeriesByRegion(Region region)
        {
            if(region.TimeSeries != null)
            {
                return region.TimeSeries;
            }
            var client = new RestClient(API+ "/dayone/country/"+region.Slug)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            JsonArray array = (JsonArray)SimpleJson.DeserializeObject(response.Content);

            TimeSeries timeSeries = new TimeSeries();

            foreach (dynamic json in array)
            {
                Statistic statistic = new Statistic((int)json["Confirmed"], (int)json["Recovered"], (int)json["Deaths"], (int)json["Active"]);
                DateTime date = DateTime.Parse(json["Date"], null, System.Globalization.DateTimeStyles.RoundtripKind);
                timeSeries.Add(statistic, date);
            }

            return timeSeries;
        }

        public static List<Region> FetchSummary()
        {
            List<Region> regions = new List<Region>();
            var client = new RestClient(API + "summary")
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var json = SimpleJson.DeserializeObject(response.Content);
            JObject obj = JObject.Parse(json.ToString());
            JsonArray array = (JsonArray)SimpleJson.DeserializeObject(obj["Countries"].ToString());

            foreach (dynamic jsonObject in array)
            {
                Statistic statistic = new Statistic(Convert.ToInt32(jsonObject["TotalConfirmed"].ToString()), Convert.ToInt32(jsonObject["TotalRecovered"].ToString()), Convert.ToInt32(jsonObject["TotalDeaths"].ToString()))
                {
                    StatisticID = jsonObject["CountryCode"].ToString()
                };
                DateTime date = Convert.ToDateTime(Regex.Match(jsonObject["Date"].ToString(), @"\d{4}-\d{2}-\d{2}").Value);
                Report report = new Report(date, statistic);

                Region region = new Region(jsonObject["Country"], Region.EnumLevel.Country, jsonObject["Slug"], jsonObject["CountryCode"])
                {
                    Report = report
                };
                regions.Add(region);
            }
            return regions;
        }
    }
}
