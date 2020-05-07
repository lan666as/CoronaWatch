using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary.SocialMedia
{
    class Twitter : ISocialMedia
    {
        private static Twitter _instance;
        private static readonly string API = "https://twitter.com/intent/tweet";

        protected Twitter()
        {

        }

        public static void Publish(string Data)
        {
            System.Diagnostics.Process.Start(API+$"?text={Data}");
        }
        void ISocialMedia.Publish(string data)
        {
            Publish(data);
        }

        public static Twitter Instance()
        {
            if(_instance == null)
            {
                _instance = new Twitter();
            }
            return _instance;
        }
    }
}
