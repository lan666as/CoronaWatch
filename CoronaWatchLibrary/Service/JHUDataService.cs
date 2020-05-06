using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CoronaWatchLibrary.Service
{
    public class JHUDataService : BaseDataService
    {
        private static readonly string SUMMARY_API = "https://api.covid19api.com/summary";

        public override void FetchTimeSeries()
        {
            WebRequest request = WebRequest.Create(SUMMARY_API);
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                dynamic stuff = JObject.Parse(responseFromServer);
            }
            response.Close();
        }
    }
}
