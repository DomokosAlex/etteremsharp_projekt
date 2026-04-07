using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Etterem
{
    public class Program
    {

        public static List<Ugyfel> ugyfelek = new List<Ugyfel>();
        public static List<Etel> etelek = new List<Etel>();
        public static List<Rendelesek> rendelesek = new List<Rendelesek>();
        public static List<Leadott> leadott = new List<Leadott>();
        public static string connstr = "server=localhost;user=root;password=;database=etteremsharp";

        public static void LoadUgyfel()
        {
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();
                string parancs = "SELECT * FROM ugyfel";
                MySqlCommand cmd = new MySqlCommand(parancs, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ugyfelek.Add(new Ugyfel
                    {

                        id = dr.GetInt32("id"),
                        veznev = dr.GetString("veznev"),
                        kernev = dr.GetString("kernev"),
                        email = dr.GetString("email")


                    });
                }
            }
        }


        public static void LoadEtelek()
        {
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();

                string parancs = "SELECT * FROM etel";

                MySqlCommand cmd = new MySqlCommand(parancs, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    etelek.Add(new Etel
                    {

                        id = dr.GetInt32("id"),
                        nev = dr.GetString("nev"),
                        allergenek = dr.GetString("allergenek"),
                        kaloria = dr.GetInt32("kaloria"),
                        ar = dr.GetInt32("ar"),
                        kategoria = dr.GetString("kategoria")
                        


                    });
                }


            }
        }

        public static void LoadRendelesek()
        {
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();

                string parancs = "SELECT * FROM rendelesek";

                MySqlCommand cmd = new MySqlCommand(parancs, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    rendelesek.Add(new Rendelesek
                    {

                        id = dr.GetInt32("id"),
                        ugyfelid = dr.GetInt32("ugyfelid"),

                    });
                }


            }
        }


        public static void LoadLeadott()
        {
            using (MySqlConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();

                string parancs = "SELECT * FROM leadott";

                MySqlCommand cmd = new MySqlCommand(parancs, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    leadott.Add(new Leadott
                    {

                        id = dr.GetInt32("id"),
                        rendelesid = dr.GetInt32("rendelesid"),
                        etelid = dr.GetInt32("etelid"),
                    });
                }


            }
        }

        public static void OsszesRendeles()
        {
            Console.WriteLine("Az összes rendelés: ");
            int osszbevetel = 0;
            foreach(var c in rendelesek)
            {
                var ugyfel = ugyfelek.Find(n => n.id == c.ugyfelid);

                var rendelt = leadott.Where(n => n.rendelesid == c.id).ToList();

                var rendeltetel = from r in rendelt join e in etelek on r.etelid equals e.id select e;

                Console.WriteLine($"{ugyfel.veznev} {ugyfel.kernev}: ");
                var ar = rendeltetel.Sum(n => n.ar);
                osszbevetel += ar;
                foreach(var v in rendeltetel)
                {
                    Console.WriteLine(v);
                }
                Console.WriteLine($"Ára: {ar}Ft");
                
                Console.WriteLine();
            }

            Console.WriteLine($"Összesen {rendelesek.Count()} rendelés volt leadva");
            Console.WriteLine($"Össz bevétel: {osszbevetel}");
            Console.WriteLine($"Átlag rendelési érték: {osszbevetel / rendelesek.Count()}");
        }

        public static void EmberkentRendeles()
        {
            Console.WriteLine("Személyenként rendelések száma: ");
            var csop = rendelesek.GroupBy(n => n.ugyfelid);

            foreach(var c in csop)
            {
                var nev = ugyfelek.Find(n => n.id == c.Key);
                int osszar = 0;
                foreach(var rend in c)
                {
                    var osszes = leadott.FindAll(n => n.rendelesid == rend.id);

                    foreach (var s in osszes) 
                    {

                        var ara = etelek.Find(n => n.id == s.etelid).ar;

                        osszar += ara;
                    }
                }

                Console.WriteLine($"{nev.veznev} {nev.kernev} - {c.Count()} - {osszar}ft");
            }
            
        }

        public static void AdottSzemely()
        {
            Console.Write("Adott személy vezetékneve: ");
            string veznev = Console.ReadLine();

            Console.Write("Adott személy keresztneve: ");
            string kernev = Console.ReadLine();

            var ember = ugyfelek.Find(n => n.veznev == veznev && n.kernev == kernev);
            if(ember == null)
            {
                Console.WriteLine("Nincs olyan ember");
            }
            else
            {
                var rendelid = rendelesek.Where(n => n.ugyfelid == ember.id).ToList();
                List<int> arak = new List<int>();
                Console.WriteLine("Eddigi rendelései: ");
                foreach(var r in rendelid)
                {
                    
                    var etel = leadott.FindAll(n => n.rendelesid == r.id);
                    Console.WriteLine();
                    int ossz = 0;
                    foreach(var c in etel)
                    {
                        var aktualisetel = etelek.Find(n => n.id == c.etelid);
                        Console.WriteLine($"{aktualisetel.nev} - {aktualisetel.ar}" );
                        ossz += aktualisetel.ar;
                    }

                    Console.WriteLine($"Rendelés értéke: {ossz}");
                    arak.Add(ossz);
                }
                Console.WriteLine($"A rendelései összértéke: {arak.Sum()}");
                arak.Clear();
                
            }
        }


        public static void KategoriankentEloszlas()
        {
            Console.WriteLine("Ételek rendelése kategoriánként: ");
            var leadottetelek = leadott.Select(n => n.etelid).ToList();

            List<Etel> osszesetel = new List<Etel>();

            foreach(var c in leadottetelek)
            {
                osszesetel.Add(etelek.Find(n => n.id == c));
            }

            var csop = osszesetel.GroupBy(n => n.kategoria);

            foreach(var c in csop)
            {
                Console.WriteLine($"{c.Key} - {c.Count()}");
            }

        }


        public static void RendelesTipus()
        {
            Console.WriteLine("Rendelés típusok: ");
            int husosrendeles = 0;
            int kevertrendeles = 0;
            int veganrendeles = 0;


            int husosertek = 0;
            int kevertertek = 0;
            int veganertek = 0;
            var csop = leadott.GroupBy(n => n.rendelesid);


            foreach (var c in csop) 
            {
                int husos = 0;
                int vegan = 0;

                int ar = 0;
                foreach (var etel in c)
                {
                    var et = etelek.Find(n => n.id == etel.etelid);
                    if (et.kategoria == "husos")
                    {
                        husos++;
                        
                    }
                    else
                    {
                        vegan++;
                        
                    }
                    ar += et.ar;
                }

                if(vegan > husos)
                {
                    veganrendeles++;
                    veganertek += ar;
                }
                else if(husos > vegan)
                {
                    husosrendeles++;
                    husosertek += ar;
                }
                else
                {
                    kevertrendeles++;
                    kevertertek += ar;
                }
            
            }

            Console.WriteLine($"Husos rendelés: {husosrendeles} - Bevétel: {husosertek}Ft");
            Console.WriteLine($"Vegán rendelés: {veganrendeles} - Bevétel: {veganertek}Ft");
            Console.WriteLine($"Kevert rendelés: {kevertrendeles} - Bevétel: {kevertertek}Ft");

        }


        public static void EtelNepszeruseg()
        {
            Console.WriteLine("Hányszor voltak rendelve az ételek: ");
            var etelnev = etelek.Select(n => n.nev).ToDictionary(g => g, g => 0);
            

            foreach(var c in leadott)
            {
                var keres = etelek.Find(n => n.id == c.etelid);
                etelnev[keres.nev]++;
            }

            foreach(var c in etelnev)
            {
                Console.WriteLine($"{c.Key} - {c.Value}");
            }

        }

        static void Main(string[] args)
        {

            LoadUgyfel();
            LoadEtelek();
            LoadRendelesek();
            LoadLeadott();


            
            OsszesRendeles();
            Console.WriteLine("----------");
            EmberkentRendeles();
            Console.WriteLine("----------");
            AdottSzemely();
            Console.WriteLine("----------");
            KategoriankentEloszlas();
            Console.WriteLine("----------");
            RendelesTipus();
            Console.WriteLine("----------");
            EtelNepszeruseg();

        }
    }

}
