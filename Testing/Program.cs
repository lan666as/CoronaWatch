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
using CoronaWatchLibrary.Service;
using CoronaWatchDB;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Testing Aldo
            Console.WriteLine(Regex.Match(System.DateTime.UtcNow.Date.ToString(), @"\d{2}/\d{2}/\d{4}").Value);
            Console.WriteLine("Updating DB...");
                try
                {
                    JHUDataService.UpdateDatabase();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

            Console.ReadLine();
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
