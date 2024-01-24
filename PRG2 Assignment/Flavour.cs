//==========================================================
// Student Number : S10256132
// Student Name : Timothy Chai
// Partner Name : Joseph Wan
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Flavour
    {
        public string Type { get; set; }
        public bool Premium { get; set; }
        public int Quantity { get; set; }

        public Flavour() { }

        public Flavour(string t, bool p, int q)
        {
            Type = t;
            Premium = p;
            Quantity = q;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
