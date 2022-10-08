using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat_Csikos_Benedek_FTEPXW
{
    class Golyo
    {
        public int Szin { get; set; }
        public Golyo(int szin)
        {
            this.Szin = szin;
        }
               
        public void MilyenSzinuu()
        {
            switch (Szin)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 6:
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;               
            }            
        }
    }
}
