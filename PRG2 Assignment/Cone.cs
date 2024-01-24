//==========================================================
// Student Number : S10262528
// Student Name : Joseph Wan
// Partner Name : Timothy Chai
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10262528
// Student Name : Joseph Wan
// Partner Name : Timothy Chai
//==========================================================

namespace PRG2_Assignment
{
    internal class Cone:IceCream 
    {
        public bool Dipped { get; set; }
        public Cone() { }
        public Cone(string o, int s, List<Flavour> f, List<Topping> t, bool d):base(o,s,f,t)
        {
            Dipped = d;
        }
        public override double CalculatePrice()
        {
            double price;
            if (Scoops == 1)
            {
                price = 4;

            }
            else if (Scoops == 2)
            {
                price = 5.50;

            }
            else if (Scoops == 3)
            {
                price = 6.50;
            }
            else
            {
                Console.WriteLine("You can only scoop a maximum of 3 scoops.");
                return 0;
            }

            bool hasPremiumFlavour = false;
            foreach (var flavour in Flavours)
            {
                if (flavour.Premium)
                {
                    hasPremiumFlavour = true;
                    break;
                }
            }

            if (hasPremiumFlavour)
            {
                price += 2 * Scoops;
            }

            if (Toppings.Count > 0)
            {
                price += (1 * Toppings.Count);
            }
            if (Dipped == true)
            {
                price += 2;
            }
            return price;
        }
        public override string ToString()
        {
            return "";
        }

        
    }
}
