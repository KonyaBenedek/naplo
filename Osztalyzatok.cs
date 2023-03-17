using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
        public class Osztalyzatok
        {
            String nev;
            String datum;
            String tantargy;
            int jegy;
            string csaladiNev;

            public Osztalyzatok(string nev, string datum, string tantargy, int jegy, string csaladiNev)
            {
                this.nev = nev;
                this.datum = datum;
                this.tantargy = tantargy;
                this.jegy = jegy;
                this.csaladiNev = csaladiNev;
            }

            public string Nev { get => nev; set => nev = value; }
            public string Datum { get => datum; }
            public string Tantargy { get => tantargy; }
            public int Jegy { get => jegy; }
            public string CsaladiNev { get => csaladiNev; }

        public string ForditottNev()
        {
            string[] reszek = this.nev.Split(' ');
            if (reszek.Length != 2)
            {
                return this.nev;
            }
            else
            {
                return reszek[1] + " " + reszek[0];
            }
        }
    }    
}

