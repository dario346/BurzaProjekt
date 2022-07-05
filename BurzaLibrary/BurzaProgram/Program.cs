using DionicaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


namespace BurzaProgram
{
    class Program
    {
        public static List<Dionica> DionicaList = new List<Dionica>();
        public static List<Dionica> ShoppingList = new List<Dionica>();
        public static List<Datum> DatumDionica = new List<Datum>();
        public static List<string> forupdate = new List<string>();
        public static bool once2;

        public static bool once;
        public static string path = @"D:\Dionice.txt";
        public static string pathdatum = @"D:\Datum.txt";
        static void Main(string[] args)
        {
            NapraviDokument();

            int odabir = Biranje();

            while (odabir != 9)
            {

                switch (odabir)
                {
                    //Dodaj dionicu
                    case 1:
                        Console.WriteLine("Odlučili ste dodati novu dionicu");
                        string ime;
                        decimal vrijednost;
                        Console.WriteLine("Ime dionice;");
                        ime = Console.ReadLine();
                        var Dupli = DionicaList.SingleOrDefault(a => a.Ime == ime + "|");

                        if (Dupli == null && ime.Length>=1)
                        {
                            Console.WriteLine("Vrijednost dionice; u $");
                            try
                            {
                                vrijednost = decimal.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Upisite broj!");
                                goto case 1;
                            }
                            string Datum1 = DateTime.Now.ToString("yyyy/MM/dd");
                            Dionica novadionica = new Dionica(ime, vrijednost);
                            Datum datum = new Datum(ime, Datum1);
                            DatumDionica.Add(datum);
                            DionicaList.Add(novadionica);
                            Dodaj();
                            DodajDatum();
                        }
                        else
                        {
                            #region Skriven write line
                            Console.WriteLine("--------------------------------");
                            Console.WriteLine("Vec postoji dionica s tim imenom");
                            Console.WriteLine("--------------------------------");
                            #endregion

                        }
                        break;

                    //Košara
                    case 2:
                        Console.WriteLine("Koju dionicu zelite kupiti?");
                        PrintInventoryValue();
                        Console.WriteLine("Dodajte u kosaru tako da upisete ime dionice.");
                        string aaa = Console.ReadLine();
                        var Postoji = DionicaList.SingleOrDefault(a => a.Ime == aaa + "|");

                        if (Postoji != null)
                        {
                            ShoppingList.Add(Postoji);
                            PrintShoppingCart();
                        }
                        else
                        {
                            Console.WriteLine("Ta dionica nepostoji");
                        }
                      





                        break;
                    //Kupnja
                    case 3:
                        PrintShoppingCart();
                        Console.WriteLine("Ukupna cijena je; " + Checkout() + "$");
                       
                        break;
                    case 4:

                        PrintInventoryValue();  PrintInventoryDate();

                        break;
                    case 5:

                        Sort();
                        break;
                    case 6:

                        Makni();
                        break;

                    case 7:
                        Azuriraj();
                        break;
                    case 8:
                        Statdate();
                        break;

                    default:
                        Console.WriteLine("Upisite redni broj!");
                        break;
                }
                odabir = Biranje();
            }
        }

        private static void NapraviDokument()
        {
            if (!File.Exists(path))
            {
                string text = "HT|15";
                File.WriteAllText(path, text);
            }

            if (!File.Exists(pathdatum))
            {
                string text = "HT|2022.01.01";
                File.WriteAllText(pathdatum, text);
            }
            forupdate = File.ReadAllLines(path).ToList();
            if (!once)
            {
                for (int i = 0; i < forupdate.Count; i++)
                {
                    string text = forupdate[i];
                    string[] items = text.Split('|');
                    Dionica d = new Dionica (items[0],decimal.Parse (items[1]));
                    DionicaList.Add(d);
                }
                once = true;

            }
            forupdate.Clear();

            forupdate = File.ReadAllLines(pathdatum).ToList();
            if (!once2)
            {

                foreach (string line in forupdate)
                {
                    string[] items = line.Split('|');
                    Datum d = new Datum(items[0], items[1]);
                    DatumDionica.Add(d);

                }
                once2 = true;

            }
            forupdate.Clear();
        }

        //Košara
        private static void PrintShoppingCart()
        {
            Console.WriteLine("Dionice koje ste odlucili kupit:");
            for (int i = 0; i < ShoppingList.Count; i++)
            {
                Console.WriteLine(ShoppingList[i]+ "$");
            }
        }


        //Inventar
        public static void PrintInventoryValue()
        {
            Console.WriteLine("-----Vrijednost-----");
            foreach (Dionica d in DionicaList)
            {
                Console.WriteLine(d + "$");    
            }
            Console.WriteLine("------------------- \n");
        }
        public static void PrintInventoryDate()
        {
            Console.WriteLine("-------Datum-------");
            foreach (Datum b in DatumDionica)
            {
                Console.WriteLine(b);
            }
            Console.WriteLine("------------------- \n");
        }
        //Menu
        static public int Biranje()
        {

            int odabir = 0;
            Console.WriteLine("Odaberite radnju \n 1.Dodaj novu dionicu \n 2.Dodaj u košaru \n 3.Završi kupnju \n 4.Lista Dionica \n 5.Sortiranje dionica \n 6.Makni Dionicu \n 7.Azuriraj dionicu \n 8.Vremenski interval \n 9.Quit");
            try
            {
                odabir = int.Parse(Console.ReadLine());
                return odabir;
            }
            catch (Exception)
            {
                #region Skriven writeline
                Console.WriteLine("---------------------");
                Console.WriteLine("Upisite redni broj!!!");
                Console.WriteLine("---------------------");
                #endregion
                return odabir;
            }
            
        }


        //Upis u dokument
        public static void Dodaj()
        {                
            foreach  (Dionica d in DionicaList)
            {
    
                forupdate.Add(d.ToString());
            }
            string NoviDok = @"D:\Dionice.txt";
            File.WriteAllLines(NoviDok, forupdate);
            forupdate.Clear();




        }
        public static void DodajDatum()
        {
            foreach (Datum b in DatumDionica)
            {
       
                forupdate.Add(b.ToString());
            }
            string NoviDok = @"D:\Datum.txt";
            File.WriteAllLines(NoviDok, forupdate);
            forupdate.Clear();
        }
        public static void Makni()
        {
            PrintInventoryValue();
            Console.WriteLine("Unesite ime dionice");
            string a = Console.ReadLine()+"|";

            DionicaList.Remove(DionicaList.SingleOrDefault(d => d.Ime == a));
            DatumDionica.Remove(DatumDionica.SingleOrDefault(d => d.Ime == a));
            Dodaj();
            DodajDatum();
            PrintInventoryValue();


        }
        public static void Azuriraj() 
        {          
            PrintInventoryValue();
            Console.WriteLine("Odaberite koju dionicu zelite update-at tako da upisete njeno ime:");
            try
            {          
                string b = Console.ReadLine() + "|";
                int index = DionicaList.FindIndex(a => a.Ime == b);

                Console.WriteLine("Upisite novo ime;");
                DionicaList[index].Ime = Console.ReadLine() + "|";
                Console.WriteLine("Vrijednost");
                DionicaList[index].Vrijednost = decimal.Parse(Console.ReadLine());
                Dodaj();
                PrintInventoryValue();
            }
            catch (Exception)
            {
                #region Skriven writeline
                Console.WriteLine("---------------------");
                Console.WriteLine("Upisite redni broj!!!");
                Console.WriteLine("---------------------");
                #endregion
            }

        }        
        public static decimal Checkout()
        {
            decimal totalCost = 0;
            List<decimal> SamoVrijednost = ShoppingList.Select(x => x.Vrijednost).ToList();        
            for (int i = 0; i < SamoVrijednost.Count; i++)
            {
                decimal Vrijednost = SamoVrijednost[i];
                totalCost += Vrijednost;

            }
            ShoppingList.Clear();
            return totalCost;
        }
        
        //Sortiranje
        public static void Sort()
        {
            int odabir = 0;
            Console.WriteLine("Odaberite radnju \n 1. Sortiraj po Imenu(po abecedi) \n 2. Sortiraj po vrijednosti(Silazno) \n 3.Sortiraj po datumu (Silazno)  \n 4.Povratak na glavni menu");
            try
            {
                odabir = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                #region skriven cw
                Console.WriteLine("---------------------");
                Console.WriteLine("Upisite redni broj!!!");
                Console.WriteLine("---------------------");
                Sort();
                #endregion
            }

            switch (odabir)
            {
                case 1:
                    Sortbyname();
                    break;

                case 2:
                    Sortbynumber();
                    break;

                case 3:                   
                    Sortbydate();
                    return;

                case 4:
                    Sortbydate();
                    return;

                default:
                    #region Skriven writeline
                    Console.WriteLine("---------------------");
                    Console.WriteLine("Upisite redni broj!!!");
                    Console.WriteLine("---------------------");
                    #endregion 
                    Sort();
                    break;
            }



        }
        public static void Sortbynumber()
        {
            var SortDionica = DionicaList.OrderByDescending(x =>x.Vrijednost);

            foreach (var a in SortDionica)
            {
                
                Console.WriteLine($"Ime: {a.Ime.Remove(a.Ime.Length -1)}");
                Console.WriteLine($"Vrijednost: {a.Vrijednost}$");
                Console.WriteLine("--------------------------------------");
            }
        }
        public static void Sortbyname()
        {
            var SortDionica = DionicaList.OrderBy(x => x.Ime);

            foreach (var a in SortDionica)
            {

                Console.WriteLine($"Ime: {a.Ime.Remove(a.Ime.Length - 1)}");
                Console.WriteLine($"Vrijednost: {a.Vrijednost}$");
                Console.WriteLine("--------------------------------------");
            }
        }
        public static void Sortbydate()
        {
            var SortDionica = DatumDionica.OrderBy(x => x.Date);

            foreach (var a in SortDionica)
            {
                 
                Console.WriteLine($"Ime: {a.Ime.Remove(a.Ime.Length - 1)}");
                Console.WriteLine($"Datum: {a.Date}");
                Console.WriteLine("--------------------------------------");
            }
        }
        public static void Statdate()
        {
            PrintInventoryDate();
            Console.WriteLine("Upisite godinu:");
            string aaa = Console.ReadLine();
            forupdate = File.ReadAllLines(pathdatum).ToList();
            if (aaa.Length == 4)
            {
                foreach (string line in forupdate)
                {
                    string[] items = line.Split('|');
                    bool b = forupdate.Any(s => items[1].Substring(0,4).Contains(aaa));
                    if (b)
                    {
                        Console.WriteLine("\n"+"Ime: "+items[0] + "\n"+"Datum: "+ items[1]);
                    }

                }
            }
            else
            {
                Console.WriteLine("Upisite godinu!");
                Statdate();
            }          
            forupdate.Clear();           
        }
    }
}
