using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etterem
{
    public class Etel
    {
        public int id { get; set; }
        public string nev { get; set; }
        public string allergenek { get; set; }
        public int kaloria { get; set; }
        public int ar { get; set; }

        public string kategoria { get; set; }
        public override string ToString()
        {
            return $"{nev}; {allergenek}; {kaloria}; {ar}; {kategoria}";
        }
    }
}
