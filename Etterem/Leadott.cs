using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etterem
{
    public class Leadott
    {
        public int id { get; set; }
        public int rendelesid { get; set; }
        public int etelid { get; set; }
        public override string ToString()
        {
            return $"{id}; {rendelesid}; {etelid}";
        }
    }
}
