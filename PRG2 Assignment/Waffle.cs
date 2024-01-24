//==========================================================
// Student Number : S10262528
// Student Name : Joseph Wan
// Partner Name : Timothy Chai
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Waffle:IceCream
    {
        public string WaffleFlavour { get; set; }
        public Waffle() { }
        public Waffle(string o, int s, List<Flavour> f, List<Topping> t, string wf) : base(o, s, f, t)
        {
            WaffleFlavour = wf;
        }

        public override double CalculatePrice()
        {
            double price;
            if (Scoops == 1)
            {
                price = 7;

            }
            else if (Scoops == 2)
            {
                price = 8.50;

            }
            else if (Scoops == 3)
            {
                price = 9.50;
            }
            else
            {
                Console.WriteLine("You can only scoop a maximum of 3 scoops.");
                return 0;
            }

            if (Toppings.Count > 0)
            {
                price += (1 * Toppings.Count);
            }
            if (WaffleFlavour == "Red velvet" || WaffleFlavour == "Charcoal" || WaffleFlavour == "Pandan")
            {
                price += 3;
            }

            return price;
        }
    }
}
