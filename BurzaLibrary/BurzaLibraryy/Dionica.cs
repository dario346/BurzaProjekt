using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DionicaLibrary
{
    public class Dionica
    {

        public string Ime;
        public decimal Vrijednost;
            

            public Dionica(string a, decimal b)
            {
                Ime = a + "|";
                Vrijednost = b;

            }
 
        override public string ToString()
        {
            return  Ime  + Vrijednost;
        }
    }
    public class Datum
    {
        public string Ime;
        public string Date;
        public Datum(string a, string b)
        {
            Ime = a + "|";
            Date = b;

        }
        override public string ToString()
        {
            return Ime + Date;
        }

    }
}
