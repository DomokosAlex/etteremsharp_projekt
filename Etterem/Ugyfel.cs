using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etterem
{
    public class Ugyfel
    {
        public int id { get; set; }
        public string veznev { get; set; }
        public string kernev { get; set; }
        public string email { get; set; }

        public override string ToString()
        {
            return $"{veznev}; {kernev}; {email}";
        }
    }

}
