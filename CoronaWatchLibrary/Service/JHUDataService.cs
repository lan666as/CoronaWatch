using CoronaWatchDB;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;

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
                //DateTime date = Convert.ToDateTime(Regex.Match(jsonObject["Date"].ToString(), @"\d{4}-\d{2}-\d{2}").Value);
                DateTime date = DateTime.Parse(jsonObject["Date"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
                Report report = new Report(date, statistic);
                reports.Add(report);
            }
            return reports;
        }

        public static TimeSeries FetchTimeSeriesByRegion(Region region)
        {
            if (region.TimeSeries != null)
            {
                return region.TimeSeries;
            }
            var client = new RestClient(API + "/country/" + region.Slug)
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
            try
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
                    //DateTime date = Convert.ToDateTime(Regex.Match(jsonObject["Date"].ToString(), @"\d{4}-\d{2}-\d{2}").Value);
                    DateTime date = DateTime.Parse(jsonObject["Date"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
                    Report report = new Report(date, statistic);

                    Region region = new Region(jsonObject["Country"], Region.EnumLevel.Country, jsonObject["Slug"], jsonObject["CountryCode"])
                    {
                        Report = report
                    };
                    regions.Add(region);
                }
                return regions;
            }
            catch (Exception e)
            {
                if (e.Message == "Invalid JSON string")
                {
                    MessageBox.Show(e.Message + "\nPlease Check Your Connection", "Error", MessageBoxButton.OK);
                    return null;
                }
                else
                {
                    MessageBox.Show(e.Message, "Error");
                    return null;
                }

            }

        }


        #region Database Services
        public static void InitializeDatabase()
        {
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
                JsonArray array = (JsonArray)SimpleJson.DeserializeObject(obj["Countries"].ToString());

                CoronaWatchContext context = new CoronaWatchContext();

                foreach (JsonObject jsonObject in array)
                {
                    string ISOCode = jsonObject["CountryCode"].ToString();
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
                }
            }
            catch (Exception e)
            {

                if (e.Message == "Invalid JSON string")
                {
                    MessageBox.Show(e.Message + "\nPlease Check Your Connection", "Error", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show(e.Message, "Error");
            }

        }

        public static void UpdateDatabase()
        {
            try
            {
                CoronaWatchContext context = new CoronaWatchContext();

                // Updating Region List, if exist
                InitializeDatabase();

                // Check if the current data for ReportDB is exist. Note that ReportDB is equivalent to Report.
                // Different Name used to avoid confusion and ambiguity
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
                    //DateTime APIdate = Convert.ToDateTime(Regex.Match(jsonObject["Date"].ToString(), @"\d{4}-\d{2}-\d{2}").Value);
                    DateTime APIdate = DateTime.Parse(jsonObject["Date"].ToString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
                    string CountryName = jsonObject["CountryCode"].ToString();
                    if (context.ReportDBs.Where(r => r.Date == APIdate && r.ISOCode == CountryName).FirstOrDefault() == null)
                    {
                        ReportDB reportDB = new ReportDB
                        {
                            ISOCode = jsonObject["CountryCode"].ToString(),
                            Confirmed = Convert.ToInt32(jsonObject["TotalConfirmed"].ToString()),
                            Recovered = Convert.ToInt32(jsonObject["TotalRecovered"].ToString()),
                            Death = Convert.ToInt32(jsonObject["TotalDeaths"].ToString())
                        };
                        reportDB.Active = reportDB.Confirmed - reportDB.Recovered - reportDB.Death;
                        reportDB.Date = APIdate;
                        context.ReportDBs.Add(reportDB);
                        context.SaveChanges();
                    }

                }
                MessageBox.Show("Successfully Updated DB");
            }
            catch (Exception e)
            {
                if (e.Message == "Invalid JSON string")
                {
                    MessageBox.Show(e.Message + "\nPlease Check Your Connection", "Error", MessageBoxButton.OK);
                }
                else
                    MessageBox.Show(e.Message, "Error");
            }

        }

        /// <summary>
        /// Get latest data available on Database.
        /// </summary>
        /// <returns></returns>
        public static List<Region> FetchDatabase()
        {
            List<Region> regions = new List<Region>();
            CoronaWatchContext context = new CoronaWatchContext();

            DateTime currentDate = System.DateTime.UtcNow.Date;
            DateTime latestDateOnDB = Convert.ToDateTime(context.ReportDBs.Max(r => r.Date)).Date;
            List<RegionDB> regionDBs = context.RegionDBs.ToList();
            List<ReportDB> reportDBs = context.ReportDBs.ToList();

            foreach (RegionDB regionDB in regionDBs)
            {
                ReportDB reportDB = reportDBs.Where(r => r.ISOCode == regionDB.ISOCode).OrderByDescending(r => r.Date).Select(r => r).FirstOrDefault();
                Statistic statistic = new Statistic((int)reportDB.Confirmed, (int)reportDB.Recovered, (int)reportDB.Death)
                {
                    StatisticID = reportDB.ISOCode
                };
                Report report = new Report(Convert.ToDateTime(reportDB.Date).Date, statistic);
                Region region = new Region(regionDB.Name, Region.EnumLevel.Country, regionDB.Slug, regionDB.ISOCode)
                {
                    Report = report
                };
                regions.Add(region);
            }

            // Sort by Name, actually unneccesary. Can be delete to improve performance
            regions.Sort((x, y) => x.Name.CompareTo(y.Name));
            return regions;
        }
        #endregion

    }
}
