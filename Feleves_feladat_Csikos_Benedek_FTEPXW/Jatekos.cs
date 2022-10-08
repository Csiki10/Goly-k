using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat_Csikos_Benedek_FTEPXW
{
    class Jatekos
    {         
        public string Nev
        {
            get;
            set;
        }
        public int Pontszam 
        {
            get;
            set;
        }              
        public TimeSpan Idotartam
        {
            get;
            set;
                
        }
        
        public DateTime KezdesIdo { get; set; }
       
        public Jatekos()
        {

        }
        public Jatekos(string nev, int pontszam, TimeSpan idotartam)
        {
            this.Nev = nev;
            this.Pontszam = pontszam;
            this.Idotartam = idotartam;
        }

        public string AdatokEgysorban(Jatekos j)
        {
            string adatokEgysorban = "";
            adatokEgysorban += j.Nev + "_";
            adatokEgysorban += j.Pontszam + "_";
            adatokEgysorban += j.Idotartam;

            return adatokEgysorban;
        }
    }
}
