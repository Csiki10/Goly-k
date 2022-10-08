using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feleves_feladat_Csikos_Benedek_FTEPXW
{
    class Tabla
    {
        public int Sor 
        {
            get;
            set;
        }
        public int Oszlop 
        {
            get;
            set;
        }
        public Golyo[,] Golyok { get; set; }

        static Random rnd;
       
        static Tabla()
        {
            rnd = new Random();
        }
        public Tabla()
        {

        }
        public Tabla(int sor, int oszlop)
        {
            Sor = sor;
            Oszlop = oszlop;
            this.Golyok = GolyoGeneralo(sor, oszlop);
            ;
        }

        public void Kiiratas(Golyo[,] golyok)
        {
            //Kiirja az aktuális matrix tömböt
            //táblázat fejléc 
            for (int i = 0; i < golyok.GetLength(1) + 1; i++)
            {
                if (i == 0)
                    Console.Write("    ");
                else if (i < 10)
                {
                    Console.Write(i + "  ");
                }
                else
                {
                    Console.Write(i + " ");
                }

            }
            Console.WriteLine();
            //kiiratás
            for (int i = 0; i < golyok.GetLength(0); i++)
            {
                for (int j = 0; j < golyok.GetLength(1); j++)
                {
                    if (j == 0)// ha az első oszlop akkor kiírja az oszlop számát
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (i > 8)
                        {
                            Console.Write(i + 1 + ". ");
                        }
                        else
                        {
                            Console.Write(" " + (i + 1) + ". ");
                        }

                    }
                    golyok[i, j].MilyenSzinuu();//Lekérdezi miylen színű az aktuális golyó, és beállítja szerint a színt.
                    Console.Write("0  ");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        public bool Vege(Golyo[,] golyok)
        {
            //Megnézi, hogy van-e 2 egyforma színű golyo. -> Ha igen mehet tovább.
            int i = 0;
            int j = 0;
            int sorSzam = golyok.GetLength(0) - 1;
            int oszSzam = golyok.GetLength(1) - 1;
            bool van = false;
            
            while (van == false && j < golyok.GetLength(1))//az oszlopokon fut es mézi egymás alatt van e egyező
            {
                i = 0;
                while (i < sorSzam && (golyok[i, j].Szin == 6 || golyok[i, j].Szin != golyok[i + 1, j].Szin))//utolsó előtti sorig nézi
                {
                    i++;
                }
                if (i < sorSzam)
                {
                    van = true;
                }
                j++;
            }  

            i = 0;
            j = 0;

            while (van == false && i < golyok.GetLength(0)) //sorokon fut mellette nézi
            {
                j = 0;
                while (j < oszSzam && (golyok[i, j].Szin == 6 || golyok[i, j].Szin != golyok[i, j + 1].Szin))//utolsó előtti oszlopig nézi
                {
                    j++;
                }
                if (j < oszSzam)
                {
                    van = true;
                }                                
                i++;
            }
                     
            return van;
        }
        public Golyo[,] Valtoztat(ref Golyo[,] golyok, int sor, int osz)
        {
            string[] egyezoIdx = EgyezoIdx(golyok, sor, osz).Item1;
            int db = EgyezoIdx(golyok, sor, osz).Item2;

            Mozgat(ref golyok, egyezoIdx, db);

            int[] uresOszlop = UresOszlop(ref golyok).Item1;
            int db1 = UresOszlop(ref golyok).Item2;
            if (db1 != 0)//Ha a db 0-a akkor nics üres oszlop, nem kell tolni.
            {
                Tolas(ref golyok, uresOszlop, db1);
            }

            return golyok;
        }
        public int Pontszam(Golyo[,] golyok)
        {   //Meghatározza a megmaradt nem fekete színű golyók száma alapján a pontszámot;  
            int pontszam = 0;
            for (int i = 0; i < golyok.GetLength(0); i++)
            {
                for (int j = 0; j < golyok.GetLength(1); j++)
                {
                    if (golyok[i, j].Szin != 6)
                    {
                        pontszam++;
                    }
                }
            }
            return pontszam;
        }

        private Golyo[,] GolyoGeneralo(int sor, int oszlop)
        {
            //legenerálja az alap golyokból álló matrixot.
            Golyo[,] golyok = new Golyo[sor, oszlop];
            int r = 0;
            for (int i = 0; i < golyok.GetLength(0); i++)
            {
                for (int j = 0; j < golyok.GetLength(1); j++)
                {
                    r = rnd.Next(1, 6);
                    if (r == 4)
                    {
                        golyok[i, j] = new Golyo(4);//rózsaszín
                    }
                    else if (r == 1)
                    {
                        golyok[i, j] = new Golyo(1);//piros
                    }
                    else if (r == 2)
                    {
                        golyok[i, j] = new Golyo(2);//zöld
                    }
                    else if (r == 3)
                    {
                        golyok[i, j] = new Golyo(3);//kék
                    }
                    else
                    {
                        golyok[i, j] = new Golyo(5);//sárga
                    }
                }
            }
            return golyok;
        }            
        private (string[],int db) EgyezoIdx(Golyo[,] golyok, int sor, int osz) 
        {
            //Megkeresi, hogy van-e azonos színű golyó a választottal, ha igen akkor vissza adja azok indexét.
            int utolsoSIdx = golyok.GetLength(0) - 1;
            int utolsoOIdx = golyok.GetLength(1) - 1;
                                           
            string[] idx = new string[5];//Fent,Középpen,Lent,Balra,Jobbra
            int db = 0;

            if (sor != 0 && sor != utolsoSIdx && osz != 0 && osz != utolsoOIdx) // nem első, utolső ; sor, oszlopban van
            {
                if (golyok[sor, osz].Szin == golyok[sor - 1, osz].Szin)//fel
                {
                    idx[db] = ((sor - 1) + "," + osz);
                    db++;
                    idx[db] = (sor + "," + osz);
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor + 1, osz].Szin)//le
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db+=2;
                    }
                    idx[db] = ((sor + 1) + "," + osz);
                    db++;
                }
                
                if (golyok[sor, osz].Szin == golyok[sor, osz - 1].Szin)//balra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz - 1));
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor, osz + 1].Szin)//jobbra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz + 1));
                    db++;
                }


            }
            else if (sor != 0 && sor != utolsoSIdx && osz == 0)//bal oszlopban van; de nem első, utlsó sor
            {
                if (golyok[sor, osz].Szin == golyok[sor - 1, osz].Szin)//fel
                {
                    idx[db] = ((sor - 1) + "," + osz);
                    db++;
                    idx[db] = (sor + "," + osz);
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor + 1, osz].Szin)//le
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = ((sor + 1) + "," + osz);
                    db++;
                }
                
                if (golyok[sor, osz].Szin == golyok[sor, osz + 1].Szin)//jobbra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz + 1));
                    db++;
                }
            }
            else if (sor != 0 && sor != utolsoSIdx && osz == utolsoOIdx)//jobb oszlopban van; de nem első, utlsó sor
            {
                if (golyok[sor, osz].Szin == golyok[sor - 1, osz].Szin)//fel
                {
                    idx[db] = ((sor - 1) + "," + osz);
                    db++;
                    idx[db] = (sor + "," + osz);
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor + 1, osz].Szin)//le
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = ((sor + 1) + "," + osz);
                    db++;
                }              
                if (golyok[sor, osz].Szin == golyok[sor, osz - 1].Szin)//balra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz - 1));
                    db++;
                }
            }
            else if (osz != 0 && osz != utolsoOIdx && sor == utolsoSIdx)//utolsó sorban van; de nem az első vagy utolsó oszlopban
            {
                if (golyok[sor, osz].Szin == golyok[sor - 1, osz].Szin)//fel
                {
                    idx[db] = ((sor - 1) + "," + osz);
                    db++;
                    idx[db] = (sor + "," + osz);
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor, osz - 1].Szin)//balra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz - 1));
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor, osz + 1].Szin)//jobbra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz + 1));
                    db++;
                }
            }
            else if (osz != 0 && osz != utolsoOIdx && sor == 0)//első sorban van; de nem az első vagy utolsó oszlopban
            {
                if (golyok[sor, osz].Szin == golyok[sor + 1, osz].Szin)//le
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = ((sor + 1) + "," + osz);
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor, osz - 1].Szin)//balra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz - 1));
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor, osz + 1].Szin)//jobbra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz + 1));
                    db++;
                }
            }
            else if (sor == 0 && osz ==0)//első sor első oszlop;
            {
                if (golyok[sor, osz].Szin == golyok[sor + 1, osz].Szin)//le
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = ((sor + 1) + "," + osz);
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor, osz + 1].Szin)//jobbra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz + 1));
                    db++;
                }
            }
            else if (sor == 0 && osz == utolsoOIdx)//első sor utolsó oszlop;
            {
                if (golyok[sor, osz].Szin == golyok[sor + 1, osz].Szin)//le
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = ((sor + 1) + "," + osz);
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor, osz - 1].Szin)//balra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz - 1));
                    db++;
                }
            }
            else if (sor == utolsoSIdx && osz == 0)//utolsó sor első oszlop;
            {
                if (golyok[sor, osz].Szin == golyok[sor - 1, osz].Szin)//fel
                {
                    idx[db] = ((sor - 1) + "," + osz);
                    db++;
                    idx[db] = (sor + "," + osz);
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor, osz + 1].Szin)//jobbra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz + 1));
                    db++;
                }
                
            }
            else if (sor == utolsoSIdx && osz == utolsoOIdx)//utolsó sor utolsó oszlop;
            {
                if (golyok[sor, osz].Szin == golyok[sor - 1, osz].Szin)//fel
                {
                    idx[db] = ((sor - 1) + "," + osz);//0.
                    db++;
                    idx[db] = (sor + "," + osz);//1.
                    db++;
                }
                if (golyok[sor, osz].Szin == golyok[sor, osz - 1].Szin)//balra
                {
                    if (idx[1] != sor + "," + osz)
                    {
                        idx[1] = (sor + "," + osz);
                        db += 2;
                    }
                    idx[db] = (sor + "," + (osz - 1));
                    db++;
                }
            }

            return (idx,db);
        }
        private Golyo[,] Mozgat(ref Golyo[,] golyok, string[] egyezoIdx, int db)
        {
            if (egyezoIdx[0] == null)//ha az elso elem null akkor nincs fölötte egyező színű
            {
                for (int i = 1; i < db; i++)//annyiszor fut le ahány darab elempár van a az egyező színek tömbben + ha az első hely üres.
                {
                    if (egyezoIdx[i] != null)
                    {
                        string[] egy = egyezoIdx[i].Split(',');
                        int sor = int.Parse(egy[0]);
                        int osz = int.Parse(egy[1]);

                        int k = 0;//lépteti azt hogy mennyi van vissza
                        int l = 1;//idexelő
                        int utolsoS = 0;
                        for (int j = 0; j < sor; j++)//felfele annyit megy ahányadik sorban van
                        {
                            int visz = sor - k;//mennyi van még vissza
                            golyok[visz, osz].Szin = golyok[visz - l, osz].Szin;
                            k++;
                            utolsoS = visz - l;
                        }
                        golyok[utolsoS, osz].Szin = 6;//üres, fekete lesz ami már nem kell
                    }                                      
                }
                return golyok;
            }
            else//ha az elso elem nem null akkor van fölotte egyező színű
            {
                for (int i = 0; i < db; i++)//annyiszor fut le ahány darab elempár van a az egyező színek tömbben
                {
                    if (egyezoIdx[i] != null)
                    {
                        string[] egy = egyezoIdx[i].Split(',');
                        int sor = int.Parse(egy[0]);
                        int osz = int.Parse(egy[1]);

                        int k = 0;
                        int l = 1;
                        int utolsoS = 0;
                        for (int j = 0; j < sor; j++)//felfele annyit megy ahányadik sorban van
                        {
                            int visz = sor - k;//mennyi van még vissza
                            golyok[visz, osz].Szin = golyok[visz - l, osz].Szin;
                            k++;
                            utolsoS = visz - l;
                        }
                        golyok[utolsoS, osz].Szin = 6;//üres, fekete lesz ami már nem kell

                    }
                }
                return golyok;
            }
            
        }
        private (int[],int) UresOszlop(ref Golyo[,] golyok)
        {
            int[] osz = new int[golyok.GetLength(1)];
            for (int i = 0; i < osz.Length; i++)
            {
                osz[i] = -1;
            }
            int db = 0;

            for (int j = 0; j < golyok.GetLength(1); j++)//végigmegy az oszlopokon
            {
                int i = 0;
                while (i < golyok.GetLength(0) && golyok[i, j].Szin == 6)//utolsó előtti sorig nézi
                {
                    i++;
                }
                if (i >= golyok.GetLength(0))//ha nagyobb akkor mind fekete, tehát üres
                {                  
                    osz[db] = j;
                    db++;
                }
            }
                
            return (osz,db);
        }
        private Golyo[,] Tolas(ref Golyo[,] golyok, int[] uresOszlop, int db)
        {           
            int utolsoOszlop = golyok.GetLength(1) - 1;
            int vissza = uresOszlop.Length;//mennyi van vissza

            while ( vissza > 0)//hátolról előre felé járja be a tömböt
            {
                int k = vissza - 1;
                int idx = uresOszlop[k];
                if (idx != -1)//valós index - e? ha nem -1 akkor jó
                {
                    int meddig = utolsoOszlop - idx;
                    int i = 0;
                    while (i < golyok.GetLength(0))//A sorokon fut végig lefelé
                    {
                        int j = 0;
                        idx = uresOszlop[k];
                        while (j < meddig)//oszlopokon fut jobbra
                        {
                            golyok[i, idx].Szin = golyok[i, idx + 1].Szin;//másolja át a jobra tőle lévő színét az övére
                            idx++;
                            j++;
                        }
                        golyok[i, idx].Szin = 6;//az utolsó színét feketére, üresre rakja
                        i++;
                    }
                    vissza--;
                }
                else//ha -1 továbbmegy
                {
                    vissza--;
                }
            }
           
            return golyok;
        }
        
    }
}
