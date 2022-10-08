using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Feleves_feladat_Csikos_Benedek_FTEPXW
{
    class DicsosegTabla
    {
        public string[] Eredmenyek { get; set; }
        public Jatekos[] Ered { get; set; }       
        public DicsosegTabla()
        {
            
        }        
            
        public void Hozzaad(string uj)//Hozzáfüzi a file végéhez az új eredményt
        {
            File.AppendAllText("dicsosegtabla.txt",uj+"\r\n");
        }
        public void Behuz()//A txt file tartalmát egy string tömbbe behúzza, majd a Jatekos tömböt létrehozza.
        {           
            Eredmenyek = File.ReadAllLines("dicsosegtabla.txt");
            Ered = new Jatekos[Eredmenyek.Length];           
        }
        public void Rendezes()
        {
            int hossz = Eredmenyek.Length;
           
            //txt sorainak Jatékos típusú tömbben való elrtárolása a rendezéshez
            for (int i = 0; i < hossz; i++)
            {               
                string[] egy = Eredmenyek[i].Split('_');
                Ered[i] = new Jatekos(egy[0], int.Parse(egy[1]), TimeSpan.Parse(egy[2]));
            }           
            //idő szerint csökkenő
            for (int i = 0; i < hossz - 1; i++)
            {
                for (int j = i + 1; j < hossz; j++)
                {
                    if (Ered[i].Idotartam < Ered[j].Idotartam)
                    {
                        Jatekos jat = Ered[i];
                        Ered[i] = Ered[j];
                        Ered[j] = jat;
                    }
                }
            }
            //pont szerint növekvő
            for (int i = 0; i < hossz - 1; i++)
            {
                int j = i + 1;
               
                if (Ered[i].Idotartam == Ered[j].Idotartam)//ha az idő egyezik, nézi a pontokat
                {
                    int k = i;
                    int l = i;
                    int m = j;
                    while (k < hossz - 1 && Ered[l].Idotartam == Ered[m].Idotartam)//addig nézi a pontokat amíg az idő egyezik, 
                    {                        
                        if (Ered[l].Pontszam > Ered[m].Pontszam)//ha nagyobb csere --> növekvőbe lesz a végén
                        {
                            Jatekos jat = Ered[l];
                            Ered[l] = Ered[m];
                            Ered[m] = jat;                                                       
                            m++;
                            k++;
                        }
                        else
                        {
                            m++;
                            k++;
                        }
                    }
                }                                 
              }    
        }      
        public void FileIras()
        {
            StreamWriter w = new StreamWriter("dicsosegtabla.txt", false);
            for (int i = 0; i < Ered.Length; i++)
            {              
                w.WriteLine(Ered[i].Nev+"_"+ Ered[i].Pontszam+"_"+ Ered[i].Idotartam);
            }
            w.Close();
        }          
    }
}
