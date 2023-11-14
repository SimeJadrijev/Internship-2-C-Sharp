using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2_C_Sharp
{
    internal class Program
    {
        static int PocetniIzbornik()
        {
            Console.WriteLine();
            string pocetniIzbornik = "1 - Artikli \n2 - Radnici \n3 - Računi \n4 - Statistika \n0 - Izlaz iz aplikacije"; ;
            Console.WriteLine(pocetniIzbornik);
            int odabirAkcije = -1;
            bool rezultat = false;
            while (rezultat == false || odabirAkcije > 4 || odabirAkcije < 0)
            {
                Console.Write("Odaberite jednu od ponuđenih opcija: ");
                rezultat = int.TryParse(Console.ReadLine(), out odabirAkcije);
            }
            return odabirAkcije;

        }

        // 1 - ARTIKL
        static int ArtikliIzbornik()
        {
            Console.WriteLine();
            string artikliIzbornik = "1 - Unos artikla \n2 - Brisanje artikla \n3 - Uređivanje artikla \n4 - Ispis \n0 - Povratak na početni izbornik";
            Console.WriteLine(artikliIzbornik);
            int odabirAkcije = -1;
            bool rezultat = false;
            while (rezultat == false || odabirAkcije > 4 || odabirAkcije < 0)
            {
                Console.Write("Odaberite jednu od ponuđenih opcija: ");
                rezultat = int.TryParse(Console.ReadLine(), out odabirAkcije);
            }
            return odabirAkcije;
        }

        //1.) Unos artikla
        static (string, Tuple<int, double, DateTime>) UnosArtikla()    
        {
            Console.WriteLine();
            Console.WriteLine("Dobrodošli u sekciju za unos novih artikala!");
            Console.WriteLine();

            Console.Write("Unesite naziv artikla: ");
            string naziv = Console.ReadLine();

            int kolicina = 0; bool rezultat = false;
            while (kolicina < 1 || rezultat == false)
            {
                Console.Write("Unesite količinu artikala (više od 0): ");
                rezultat = int.TryParse(Console.ReadLine(), out kolicina);
            }

            double cijena = 0.0; rezultat = false;
            while (cijena <= 0.0 || rezultat == false)
            {
                Console.Write("Unesite cijenu artikla (double format i više od 0): ");
                rezultat = double.TryParse(Console.ReadLine(), out cijena);
            }
            
            DateTime datum = DateTime.Now; rezultat = false;
            while (rezultat == false)
            {
                Console.Write("Unesite datum isteka artikla (godina,mjesec,dan): ");
                rezultat = DateTime.TryParse(Console.ReadLine(), out datum);
            }

            string kljuc = naziv;
            Tuple<int, double, DateTime> vrijednost = Tuple.Create(kolicina, cijena, datum);
            
            return (kljuc, vrijednost);
        }

        //2.) Brisanje artikla
        static void BrisanjeArtikla(Dictionary<string, Tuple<int, double, DateTime>> artikli)
        {
            Console.WriteLine();
            char unos = 'c'; bool rezultat = false;
            while (rezultat == false)
            {
                Console.WriteLine("Odaberite: \n 'a' - za brisanje artikla po imenu \n 'b' - za brisanje svih artikala kojima je istekao datum trajanja");
                rezultat = char.TryParse(Console.ReadLine(), out unos);
            }
            switch (unos)
            {
                case 'a':
                    Console.Write("Unesite ime artikla koji želite izbrisati: ");
                    var artiklZaBrisanje = Console.ReadLine();

                    Console.WriteLine("Ako ste sigurni da želite izbrisati artikl, napišite 'da': ");
                    var sigurnosnoPitanjeA = Console.ReadLine();
                    if (sigurnosnoPitanjeA == "da")
                    {
                        artikli.Remove(artiklZaBrisanje);
                    }                  
                    break;
                case 'b':
                    Console.WriteLine("Ako ste sigurni da želite izbrisati artikl, napišite 'da': ");
                    var sigurnosnoPitanjeB = Console.ReadLine();
                    if (sigurnosnoPitanjeB == "da")
                    {
                        Console.WriteLine("Brisanje artikala kojima je istekao datum trajanja...");
                        var keysToRemove = new List<string>();

                        foreach(var item in artikli)
                        {
                            if (item.Value.Item3 < DateTime.Now)
                                keysToRemove.Add(item.Key);
                        }
                        foreach(var key in keysToRemove)
                        {
                            artikli.Remove(key);
                        }
                    }

                    break;
            }
        }

        //3.)Uređivanje artikla
        static void UređivanjeArtikla(Dictionary<string, Tuple<int, double, DateTime>> artikli)
        {
            Console.WriteLine();
            char unos = 'c'; bool rezultat = false;
            while (rezultat == false)
            {
                Console.WriteLine("Odaberite: \n 'a' - za uređivanje artikla po imenu \n 'b' - popust / poskupljenje na sve proizvode unutar trgovine");
                rezultat = char.TryParse(Console.ReadLine(), out unos);
            }
            switch (unos)
            {
                case 'a':
                    Console.Write("Unesite ime artikla koji želite uređivati: ");
                    string artiklZaUredivanje = Console.ReadLine();

                    Console.WriteLine("Što želite uređivati? Upišite:  \n '1' za količinu artikla \n '2' za cijenu artikla \n '3' za datum isteka");
                    var atributZaUredivanje = Console.ReadLine();

                    Console.Write("Unesite izmjenu: ");
                    var promjena = Console.ReadLine();

                    Console.Write("Ako ste sigurni da želite izvršiti ovu izmjenu, unesite 'da'.");
                    var sigurnosnoPitanje = Console.ReadLine();
                    if (sigurnosnoPitanje == "da")
                    {
                        foreach (var item in artikli) //greška !!!!
                        {
                            if (item.Key == artiklZaUredivanje)
                            {
                                switch (atributZaUredivanje)
                                {
                                    case "1":
                                        try
                                        {
                                            int intPromjena;
                                            if (int.TryParse(promjena, out intPromjena) == true)
                                            {
                                                var trenutniTuple = item.Value;

                                                var noviTuple = Tuple.Create(intPromjena, trenutniTuple.Item2, trenutniTuple.Item3);

                                                artikli[item.Key] = noviTuple;
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Neispravan unos!");
                                        }                                     
                                        break;
                                    case "2":
                                        try
                                        {
                                            double doublePromjena;
                                            if(double.TryParse(promjena, out doublePromjena) == true)
                                            {
                                                var trenutiTuple = item.Value;

                                                var noviTuple = Tuple.Create(trenutiTuple.Item1, doublePromjena, trenutiTuple.Item3);

                                                artikli[item.Key] = noviTuple;
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Neispravan unos!");
                                        }
                                        break;
                                    case "3":
                                        try
                                        {
                                            DateTime datumPromjena;
                                            if (DateTime.TryParse(promjena, out datumPromjena) == true)
                                            {
                                                var trenutiTuple = item.Value;

                                                var noviTuple = Tuple.Create(trenutiTuple.Item1, trenutiTuple.Item2, datumPromjena);

                                                artikli[item.Key] = noviTuple;
                                            }
                                        }
                                        catch
                                        {
                                            Console.WriteLine("Neispravan unos!");
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    
                    break;
                case 'b':
                    Console.WriteLine("Odaberite: \n '1' - za popust na cijene \n '2' - za poskupljenje cijena");
                    var unos2 = Console.ReadLine();
                    if (unos2 == "1")
                    {
                        Console.Write("Odaberite iznos popusta u postotku (1-100)");
                        var iznosPopusta = Console.ReadLine();

                        Console.WriteLine(("Ako ste sigurni da želite unijeti ovu promjenu, unesite 'da'."));
                        var sigurnosnoPitanje2 = Console.ReadLine();
                        if(sigurnosnoPitanje2 == "da")
                        {
                            try
                            {
                                double doublePopust;
                                if (double.TryParse(iznosPopusta, out doublePopust))
                                {
                                    foreach (var item in artikli)
                                    {
                                        doublePopust = (100 - doublePopust) / 100;
                                        var trenutniTuple = item.Value;
                                        var novaCijena = item.Value.Item2 * doublePopust;
                                        var noviTuple = Tuple.Create(item.Value.Item1, novaCijena, item.Value.Item3);
                                        artikli[item.Key] = noviTuple;
                                    }
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Neispravan unos!");
                            }
                        }

                    }
                    else if (unos2 == "2")
                    {
                        Console.Write("Odaberite iznos poskupljenja u postotku (1-100)");
                        var iznosPopusta = Console.ReadLine();

                        Console.WriteLine(("Ako ste sigurni da želite unijeti ovu promjenu, unesite 'da'."));
                        var sigurnosnoPitanje2 = Console.ReadLine();
                        if(sigurnosnoPitanje2 == "da")
                        {
                            try
                            {
                                double doublePoskupljenje;
                                if (double.TryParse(iznosPopusta, out doublePoskupljenje))
                                {
                                    foreach (var item in artikli)
                                    {
                                        doublePoskupljenje = (100 + doublePoskupljenje) / 100;
                                        var trenutniTuple = item.Value;
                                        var novaCijena = item.Value.Item2 * doublePoskupljenje;
                                        var noviTuple = Tuple.Create(item.Value.Item1, novaCijena, item.Value.Item3);
                                        artikli[item.Key] = noviTuple;
                                    }
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Neispravan unos!");
                            }
                        }
                        
                    }

                    break;
            }
        }
        static void RadniciIzbornik()
        {
            Console.WriteLine("temp");
        }
        static void RacuniIzbornik()
        {
            Console.WriteLine("temp");
        }
        static void StatistikaIzbornik()
        {
            Console.WriteLine("temp");
        }

        static void Main(string[] args)
        {
            //Kreiranje dictionaryja za artikle u kojem je ključ string, a vrijednost tuple (int, double i DateTime)
            Dictionary<string, Tuple<int, double, DateTime>> artikli = new Dictionary<string, Tuple<int, double, DateTime>>()
            {
                {"voda", Tuple.Create(100, 1.5, new DateTime(2026,6,1) ) },
                {"sok", Tuple.Create(150, 2.5, new DateTime(2024,3,1) ) },
                {"pivo", Tuple.Create(200, 4.0, new DateTime(2024,3,1) ) },
                {"cokolada", Tuple.Create(25, 2.0, new DateTime(2026,6,1) ) },
                {"sendvic", Tuple.Create(20, 2.0, new DateTime(2023,12,1) ) },
                {"sir", Tuple.Create(50, 2.0, new DateTime(2022,12,1) ) }

            };

            int odabirAkcije = PocetniIzbornik(); // Ispisivanje početnog izbornika i odabir akcije
            while (odabirAkcije != 0)
            {
                switch (odabirAkcije)
                {
                    case 0:
                        Environment.Exit(0);
                        break;
                    case 1:
                        int temp1 = ArtikliIzbornik();
                        switch (temp1)
                        {
                            case 0:
                                PocetniIzbornik();
                                break;
                            case 1:
                                var rezultatUnosaArtikla = UnosArtikla();
                                artikli.Add(rezultatUnosaArtikla.Item1, rezultatUnosaArtikla.Item2);

                                Console.Write("Za povratak na početni izbornik, upišite '0': ");
                                var upitnikZaPovratak = Console.ReadLine();
                                if (upitnikZaPovratak == "0")
                                {
                                    odabirAkcije = PocetniIzbornik();
                                }
                                break;
                            case 2:
                                BrisanjeArtikla(artikli);
                                Console.Write("Za povratak na početni izbornik, upišite '0': ");

                                var upitnikZaPovratak1 = Console.ReadLine();
                                if (upitnikZaPovratak1 == "0")
                                {
                                    odabirAkcije = PocetniIzbornik();
                                }
                                break;
                            case 3:
                                UređivanjeArtikla(artikli);
                                Console.Write("Za povratak na početni izbornik, upišite '0': ");

                                var upitnikZaPovratak2 = Console.ReadLine();
                                if (upitnikZaPovratak2 == "0")
                                {
                                    odabirAkcije = PocetniIzbornik();
                                }
                                break;

                        }
                        break;
                    case 2:
                        RadniciIzbornik();
                        break;
                    case 3:
                        RacuniIzbornik();
                        break;
                    case 4:
                        StatistikaIzbornik();
                        break;


                }
            }
            

            foreach(var item in artikli)    //privremeni ispis
            {
                Console.WriteLine(item);
            }
            
            Console.ReadKey();

        }
    }
}
