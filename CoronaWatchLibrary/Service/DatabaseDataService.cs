using CoronaWatchDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CoronaWatchLibrary.Service
{
    public class DatabaseDataService : BaseDataService
    {
        private static readonly string API = "https://api.covid19api.com/";
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

                CoronaWatchDBEntities context = new CoronaWatchDBEntities();
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
                        context.Entry(regionDB).State = EntityState.Added;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nPlease Check Your Connection", "Error");
            }
            
        }

        public static void UpdateDatabase()
        {
            try
            {
                CoronaWatchDBEntities context = new CoronaWatchDBEntities();

                // Updating Region List, if exist
                InitializeDatabase();
                //Console.WriteLine("Database Initialized");

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
                    DateTime APIdate = Convert.ToDateTime(Regex.Match(jsonObject["Date"].ToString(), @"\d{4}-\d{2}-\d{2}").Value);
                    if (context.ReportDBs.Where(r => DbFunctions.TruncateTime(r.Date) == APIdate).FirstOrDefault() == null)
                    {
                        ReportDB reportDB = new ReportDB();
                        reportDB.ISOCode = jsonObject["CountryCode"].ToString();
                        reportDB.Confirmed = Convert.ToInt32(jsonObject["TotalConfirmed"].ToString());
                        reportDB.Recovered = Convert.ToInt32(jsonObject["TotalRecovered"].ToString());
                        reportDB.Death = Convert.ToInt32(jsonObject["TotalDeaths"].ToString());
                        reportDB.Active = reportDB.Confirmed - reportDB.Recovered - reportDB.Death;
                        reportDB.Date = Convert.ToDateTime(Regex.Match(jsonObject["Date"].ToString(), @"\d{4}-\d{2}-\d{2}").Value);
                        context.ReportDBs.Add(reportDB);
                        context.SaveChanges();
                    }
   
                }
                //Console.WriteLine("Done fetching");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\nPlease Check Your Connection", "Error");
            }
            
        }
    }
}
