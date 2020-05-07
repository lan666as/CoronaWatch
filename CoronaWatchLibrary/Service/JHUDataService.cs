using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CoronaWatchLibrary
{
    public class JHUDataService : BaseDataService
    {
        private static readonly string API = "https://api.covid19api.com/";

        public static Report FetchReports()
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
            Report report = new Report(statistic);
            return report;
        }


        public override void FetchTimeSeries()
        {
            throw new NotImplementedException();
        }
    }
}
