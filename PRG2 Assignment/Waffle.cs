﻿//==========================================================
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

        public override double CalculatePrice(List<List<string>> flavourList, List<List<string>> toppingList, List<List<string>> optionList)
        {
            double totalPrice = 0.0;

            foreach (var flavourInfo in flavourList)
            {
                string flavourName = flavourInfo[0];
                double flavourPrice = Convert.ToDouble(flavourInfo[1]);

                foreach (var selectedFlavour in Flavours)
                    if (flavourName == selectedFlavour.Type)
                    {
                        totalPrice += flavourPrice * selectedFlavour.Quantity;
                    }
            }

            // Calculate price based on selected toppings
            foreach (var toppingInfo in toppingList)
            {
                string toppingName = toppingInfo[0];
                double toppingPrice = Convert.ToDouble(toppingInfo[1]);

                foreach (var selectedTopping in Toppings)
                {
                    if (toppingName == selectedTopping.Type)
                    {
                        totalPrice += toppingPrice;
                    }
                }
            }


            // Calculate price based on selected options
            foreach (var option in optionList)
            {
                //The option price is in the fifth column of optionList
                string optionType = option[0];
                int optionScoops = Convert.ToInt32(option[1]);
                double optionPrice = Convert.ToDouble(option[4]);
                string waffleFlavour = option[3];
                if (optionType == Option && optionScoops == Scoops && waffleFlavour == WaffleFlavour)
                {
                    totalPrice += optionPrice;
                }

            }

            return totalPrice;
        }
    
        public override string ToString()
        {
            string flavours = "";
            foreach (var flavour in Flavours)
            {
                flavours += flavour.ToString();
            }

            string toppings = "";
            foreach (var topping in Toppings)
            {
                toppings += topping.ToString();
            }

            return base.ToString() + " | Waffle Flavour: " + WaffleFlavour;
        }
    }
}
