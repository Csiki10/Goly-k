using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics.Tracing;
using System.Threading;


namespace Feleves_feladat_Csikos_Benedek_FTEPXW
{
    class Program
    {       
        static void Main(string[] args)
        {
            Console.WriteLine("Köszöntelek!");
            
            Tabla t = new Tabla();
            Jatekos j = new Jatekos();

            //------------------------------------------név bekérése--------------------------------           

            string nev = "";
            j.Nev = NevBek(nev);

            //------------------------------------------sor és oszlop bekérése---------------------------

            Console.Write("Add meg hány sorból álljon a tábla (2-79):");
            int sor = int.Parse(Console.ReadLine());
            t.Sor = SorBek(sor);

            Console.Write("Add meg hány oszlopból álljon a tábla (2-24):");
            int oszlop = int.Parse(Console.ReadLine());
            t.Oszlop = OszlopBek(oszlop);

            //------------------------------------------rnd és tábla létrehozása--------------------

            Random rnd = new Random();
            t = new Tabla(t.Sor, t.Oszlop);

            // -------------------------------------------program futása----------------------------                   
            Console.WriteLine("\nA játék kezdéséhez nyomj ENTERT!");
            string kezd = Console.ReadLine();
            j.KezdesIdo = DateTime.Now;

            Jatek(t, j);

            Console.WriteLine("\nA JÁTÉK VÉGET ÉRT!\n");
            Console.WriteLine("Az eredményt tartalmazó dicsőségtáblát a fileban tekintheted meg!");

            j.Idotartam = DateTime.Now - j.KezdesIdo;

            //-------------------------------------------eredmények, file kezelése------------------- 
            DicsosegTabla e = new DicsosegTabla();
            FileKezeles(e, j);

            Console.ReadLine();
        }

        static void Jatek(Tabla t, Jatekos j)
        {
            t.Kiiratas(t.Golyok);

            bool vege = t.Vege(t.Golyok);
            Golyo[,] g = t.Golyok;

            while (vege == true)// addig fut amíg van 2 egyező színű golyó egymás mellett  
            {
                int maxaSor = t.Sor;
                int maxOsz = t.Oszlop;

                Console.Write("\nAdd meg a választott golyó sorának a számát (1-" + t.Sor + "): ");
                int sor1 = int.Parse(Console.ReadLine());
                sor1 = SorVal(t.Sor, sor1);

                Console.Write("Add meg a választott golyó oszlopának a számát (1-" + t.Oszlop + "):");
                int osz1 = int.Parse(Console.ReadLine());
                osz1 = OszlopVal(t.Oszlop, osz1);

                Console.Clear();

                t.Valtoztat(ref g, sor1 - 1, osz1 - 1);
                t.Kiiratas(g); // ref kell
                vege = t.Vege(t.Golyok);
            }
            j.Pontszam = t.Pontszam(g);
        }
        static void FileKezeles(DicsosegTabla e, Jatekos j)
        {
            e.Hozzaad(j.AdatokEgysorban(j));
            e.Behuz();
            e.Rendezes();
            e.FileIras();           
        }
      
        static int SorBek(int sor)
        {
            bool jo = false;
            while ((sor < 2 || sor > 79) && jo == false)
            {
                Console.Write("Add meg hány sorból álljon a tábla (2-79):");
                sor = int.Parse(Console.ReadLine());
            }
            if (sor > 1 || sor < 80)
            {
                jo = true;
            }
            
            return sor;
        }
        static int OszlopBek(int oszlop)
        {
            bool jo = false;
            while ((oszlop < 2 || oszlop > 24) && jo == false)
            {
                Console.Write("Add meg hány oszlopból álljon a tábla (2-24):");
                oszlop = int.Parse(Console.ReadLine());
            }
            if (oszlop > 1 || oszlop < 25)
            {
                jo = true;
            }
           
            return oszlop;
        }
        static string NevBek(string nev)
        {
            //---nev bekérése---            
            int l = 0;
            do
            {
                Console.Write("Kérlek add meg a neved: ");
                nev = Console.ReadLine();
                l = nev.Length;
            } while (nev == "" || l < 3);
            return nev;
        }       

        static int SorVal(int sor,int valSor)
        {
            bool jo = false;
            while ((valSor < 1 || valSor > sor) && jo == false)
            {
                Console.Write("Add meg a választott golyó sorának a számát (1-" + sor + "): ");
                valSor = int.Parse(Console.ReadLine());
            }
            if (valSor > 0 || valSor <= sor)
            {
                jo = true;
            }
          
            return valSor;
        }
        static int OszlopVal(int osz, int valOsz)
        {
            bool jo = false;
            while ((valOsz < 1 || valOsz > osz) && jo == false)
            {
                Console.Write("Add meg a választott golyó oszlopának a számát: (1-" + osz + "): ");
                valOsz = int.Parse(Console.ReadLine());
            }
            if (valOsz > 0 || valOsz <= osz)
            {
                jo = true;
            }
           
            return valOsz;
        }
    }
}