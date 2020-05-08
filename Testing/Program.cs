using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CoronaWatchLibrary;
using RestSharp;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Force.Crc32;
using System.Text.RegularExpressions;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Testing Zidan
            /*WebRequest request = WebRequest.Create("https://api.covid19api.com/" + "summary");
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
            var Global = stuff["Global"];
            Console.WriteLine(Global.ToString());


            Console.ReadKey();*/
            #endregion

            #region Testing Aldo

            List<Region> regions = new List<Region>();
            List<Report> reports = new List<Report>();

            regions = JHUDataService.FetchAllRegion();
            reports = JHUDataService.FetchRegionSummary();

            foreach(Report report in reports)
            {
                Console.WriteLine(report.Statistic.StatisticID);
                Region currentRegion = regions.Where(u => u.ISOCode == report.Statistic.StatisticID).FirstOrDefault();
                Console.WriteLine(currentRegion.Name);
                Console.WriteLine(report.Statistic.ConfirmedCases);
                Console.WriteLine(report.LastUpdate);
                Console.WriteLine("---------------------");
            }

            Console.Read();
            #endregion
        }

        private void ParseWorldReports(JArray jArray, ref Region world)
        {
            Statistic statistic = new Statistic(confirmed : (int) jArray["TotalConfirmed"], recovered : (int) jArray["TotalRecovered"], death : (int) jArray["TotalDeaths"]);
            Report report = new Report(statistic);
            world.Report = report;
        }
    }
}
