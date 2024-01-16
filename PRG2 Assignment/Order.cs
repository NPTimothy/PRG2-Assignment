using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_Assignment
{
    internal class Order
    {
        public int ID { get; set; }  
        public DateTime TimeReceived { get; set; }
        public DateTime? TimeFulfilled { get; set; }
        public List<IceCream> IceCreamList;

        public Order() { }

        public Order(int i, DateTime t)
        {
            ID = i;
            TimeReceived = t;
            IceCreamList = new();
        }

        public void ModifyIceCream(int index)
        {
            // Ice cream to modify
            IceCream modify = IceCreamList[index];

            // Option
            Console.Write(@"Choose Ice Cream Option
-----------------------
[1] Cup
[2] Cone
[3] Waffle (+$3)
_______________________");
            string option = "";

            // Validation
            while (true) {
                try
                {
                    Console.Write("Option: ");
                    int optionNo = Convert.ToInt32(Console.ReadLine());
                    int[] options = { 1, 2, 3 };
                    if (options.Contains(optionNo) == false)
                    {
                        throw new FormatException();
                    }

                    
                    if (optionNo == 1) option = "Cup";
                    else if (optionNo == 2) option = "Cone";
                    else if (optionNo == 3) option = "Waffle";

                    break;
                } catch
                {
                    Console.WriteLine("Invalid option: Option must be a whole number from 1 to 3.");
                }
            }

            // Flavours
            int scoops = 0;
            Console.WriteLine(@"Choose flavours:
Regular
-----------------------
[1] Vanilla
[2] Chocolate
[3] Strawberry

Premium ($+2 per scoop)
-----------------------
[4] Durian
[5] Ube
[6] Sea salt

-----------------------
[0] Done
_______________________________________
Enter option numbers (Max. 3 scoops).");
            List<Flavour> flavours = new();
            List<Flavour> availableFlavours = new()
            {
                new Flavour("Vanilla", false, 1),
                new Flavour("Chocolate", false, 1),
                new Flavour("Strawberry", false, 1),
                new Flavour("Durian", true, 1),
                new Flavour("Ube", true, 1),
                new Flavour("Sea Salt", true, 1)
            };

            for (int i = 1; i < 4; i++)
            {
                int flavour = -1;

                // Validation
                while (true)
                {
                    try
                    {

                        Console.Write($"Flavour {i}: ");
                        flavour = Convert.ToInt32(Console.ReadLine());
                        int[] flavourOptions = { 0, 1, 2, 3, 4, 5, 6 };
                        if (flavourOptions.Contains(flavour) == false)
                        {
                            throw new FormatException();
                        }
                        break;
                    } catch
                    {
                        Console.WriteLine("Invalid flavour: Flavour must be a whole number from 0 to 6.");
                    }
                }

                if (flavour == 0)
                {
                    break;
                } else
                {
                    if (flavours.Contains(availableFlavours[flavour]))
                    {
                        flavours[flavours.IndexOf(availableFlavours[flavour])].Quantity += 1;
                    } else
                    {
                        flavours.Add(availableFlavours[flavour]);
                    }
                    
                }
                scoops = i;
                
            }

            // Toppings
            List<Topping> toppings = new();
            
            
            Console.WriteLine(@"Choose Toppings:
-------------------
[1] Sprinkles
[2] Mochi
[3] Sago
[4] Oreos

-------------------
[0] Done
___________________");
            // Validation
            while (true)
            { 
                try
                {
                    Console.Write("Enter option numbers: (+$1 per topping): ");
                    int topping = Convert.ToInt32(Console.ReadLine());
                    int[] toppingOptions = { 0, 1, 2, 3, 4 };
                    if (toppingOptions.Contains(topping) == false)
                    {
                        throw new FormatException();
                    }


                    if (topping == 1) toppings.Add(new Topping("Sprinkles"));
                    else if (topping == 2) toppings.Add(new Topping("Mochi"));
                    else if (topping == 3) toppings.Add(new Topping("Sago"));
                    else if (topping == 4) toppings.Add(new Topping("Oreos"));
                    else if (topping == 0) break;
                    
                } catch
                {
                    Console.WriteLine("Invalid topping: Topping must be a whole number from 0 to 4.");
                }
            }

            // Cone
            if (option == "Cone")
            {
                while (true)
                {

                    Console.Write("Dipped Cone [Y/N]: ");
                    string dipped = Console.ReadLine();
                    if (dipped.ToLower() != "y" || dipped.ToLower() != "n")
                    {
                        Console.WriteLine("Invalid option: Option needs to be Y or N.");
                    }
                    else
                    {
                        if (dipped.ToLower() == "y") IceCreamList[index] = new Cone("Cone", scoops, flavours, toppings, true);
                        else if (dipped.ToLower() == "n") IceCreamList[index] = new Cone("Cone", scoops, flavours, toppings, false);

                        break;
                    }
                }


            } else if (option == "Waffle") // Waffle
            {
                Console.WriteLine(@"Choose waffle flavour:
------------------------
[1] Plain
[2] Red velvet
[3] Charcoal
[4] Pandan
________________________");

                // Validation
                while (true)
                {
                    try
                    {
                        Console.Write("Enter waffle flavour: ");
                        int waffleFlavour = Convert.ToInt32(Console.ReadLine());
                        int[] waffleFlavourOptions = { 0, 1, 2, 3 };
                        if (waffleFlavourOptions.Contains(waffleFlavour) == false)
                        {
                            throw new FormatException();
                        }

                        if (waffleFlavour == 1) IceCreamList[index] = new Waffle("Waffle", scoops, flavours, toppings, "Plain");
                        else if (waffleFlavour == 2) IceCreamList[index] = new Waffle("Waffle", scoops, flavours, toppings, "Red velvet");
                        else if (waffleFlavour == 3) IceCreamList[index] = new Waffle("Waffle", scoops, flavours, toppings, "Charcoal");
                        else if (waffleFlavour == 4) IceCreamList[index] = new Waffle("Waffle", scoops, flavours, toppings, "Pandan");
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Invalid waffle flavour: Waffle flavour must be a whole number from 0 to 4.");
                    }
                }


            } else if (option == "Cup") // Cup
            {
                IceCreamList[index] = new Cup("Cup", scoops, flavours, toppings);
            }
            
        }

        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        public void DeleteIceCream(int index)
        {
            IceCreamList.RemoveAt(index);
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach(IceCream iceCream in IceCreamList)
            {
                total += iceCream.CalculatePrice();
            }
            return total;
        }

        public override string ToString()
        {
            return "";
        }
    }

}
