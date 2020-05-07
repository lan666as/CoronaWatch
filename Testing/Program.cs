using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using CoronaWatchLibrary;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("https://api.covid19api.com/" + "summary");
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


            Console.ReadKey();
        }

        private void ParseWorldReports(JArray jArray, ref Region world)
        {
            Statistic statistic = new Statistic(confirmed : (int) jArray["TotalConfirmed"], recovered : (int) jArray["TotalRecovered"], death : (int) jArray["TotalDeaths"]);
            Report report = new Report(statistic);
            world.Report = report;
        }
    }
}
