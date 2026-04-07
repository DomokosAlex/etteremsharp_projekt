using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etterem
{
    public class Rendelesek
    {
        public int id { get; set; }
        public int ugyfelid { get; set; }
        public override string ToString()
        {
            return $"{id}; {ugyfelid}";
        }
    }

}
