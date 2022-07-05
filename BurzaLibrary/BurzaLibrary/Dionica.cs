using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurzaLibrary
{
    internal class Dionica
    {
        public string Ime { get; set; }
        public decimal Vrijednost { get; set; }

        public Dionica()
        {
            Ime = "Nedostaje";
            Vrijednost = 0.00M;
        }

        public Dionica(string a, decimal b)
        {
            Ime = a;
            Vrijednost = b;

        }


    }
}
