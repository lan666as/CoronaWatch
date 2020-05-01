using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoronaWatchLibrary
{
    abstract class User
    {
        // Field
        public int ID { get; set; }
        public string Nama { get; set; }

        // Method
        public void Lihat(DataCorona data)
        {

        }
        public void Lihat(PetaPersebaran peta)
        {

        }
        public void Share(DataCorona data)
        {

        }

    }
}
