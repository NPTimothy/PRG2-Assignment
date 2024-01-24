// See https://aka.ms/new-console-template for more information

using PRG2_Assignment;


// Initialisation
Queue<Order> regularQueue = new();
Queue<Order> goldQueue = new();

Dictionary<int, Customer> customerDict = new();



List<List<string>> flavourList = new();
using (StreamReader sr = new("flavours.csv"))
{
    string headings = sr.ReadLine();
    string[] lines = sr.ReadToEnd().Split("\n");
    foreach(string line in lines)
    {
        flavourList.Add(new List<string> { line.Split(",")[0], line.Split(",")[1] });
    }
}

List<List<string>> toppingList = new();
using (StreamReader sr = new("toppings.csv"))
{
    string headings = sr.ReadLine();
    string[] lines = sr.ReadToEnd().Split("\n");
    foreach(string line in lines)
    {
        toppingList.Add(new List<string> { line.Split(",")[0], line.Split(",")[1] });
    }
}

List<List<string>> optionList = new();
using (StreamReader sr = new("options.csv"))
{
    string headings = sr.ReadLine();
    string[] lines = sr.ReadToEnd().Split("\n");
    foreach(string line in lines)
    {
        optionList.Add(new List<string> { line.Split(',')[0], line.Split(",")[1], line.Split(',')[2], line.Split(",")[3], line.Split(",")[4] });
    }
}

void InitializeCustomerDict() //Customer information in the dictionary
{
    string path = "customers.csv"; //Assign "path" to customer.csv file

    try
    {
        string[] lines = File.ReadAllLines(path); //Read all the contents from the customer.csv file

        for (int i = 1; i < lines.Length; i++) //Once again, starting from 1 to avoid format errors.
        {
            string[] data = lines[i].Split(','); //For each line in csv, the fields in each line are split by a comma ','

            string name = data[0]; //Name would be the first field
            int memberID = Convert.ToInt32(data[1]); //Member ID would be the second field
            DateTime dob = Convert.ToDateTime(data[2]); //Date of Birth would be the third field
            string membershipStatus = data[3]; //Membership Status would be the fourth field
            int membershipPoints = Convert.ToInt32(data[4]); //Membership Points would be the fifth field
            int punchCard = Convert.ToInt32(data[5]); //PunchCard would be the sixth field

            Customer customer = new Customer(name, memberID, dob); //Instantiated customer object

            customerDict.Add(memberID, customer); //Added customer information that was in the customers.csv file to the dictionary itself by having its key being memberID and customer object as its value
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Unable to initialise customerDict: {0}", ex.Message); //Should there be an issue reading from the csv file, this exception will show up
    }
}

// Feature 1
void ListAllCustomers()
{
    string path = "customers.csv"; //Assigned "path" to the file customers.csv
    string[] lines = File.ReadAllLines(path); //Reading the customers.csv file from "path"
    Console.WriteLine("{0, -10} {1, -10} {2, -15} {3, -15} {4, -15} {5, -15}", "Name", "MemberId", "DOB", "MembershipStatus", "MembershipPoints", "PunchCard"); //Printing out the headers for display
    for (int i = 1; i < lines.Length; i++) //Starting i from 1 since we want to skip the headers to avoid formatting errors.
    {
        string[] data = lines[i].Split(','); //For each line in the csv file, each content/field is divided by a comma ','
        string? name = data[0]; //Name would be the first field
        int memberID = Convert.ToInt32(data[1]); //The Member ID would be the second field
        DateTime dob = Convert.ToDateTime(data[2]); //The date of birth would be the third field
        string? membershipStatus = data[3]; //The Membership Status would be the fourth field
        //int? membershipPoints = Convert.ToInt32(data[4]); //The Membership Points would be the fifth field
        //int? punchCard = Convert.ToInt32(data[5]); //The PunchCard would be the sixth field
        Console.WriteLine("{0, -10} {1, -10} {2, -15} {3, -16} {4, -16} {5, -15}", data[0], data[1], data[2], data[3], data[4], data[5]); //Displaying the contents of the csv file below their respective headers.
    }
}

// Feature 2
void ListCurrentOrders()
{
    Console.WriteLine("Gold Queue:");
    //Console.WriteLine(goldQueue.Peek());
    foreach(Order goldOrder in goldQueue)
    {
        Console.WriteLine(goldOrder);
        Console.WriteLine("\t\t^");
    }

    Console.WriteLine("Regular Queue:");
    foreach(Order regularOrder in regularQueue)
    {
        Console.WriteLine(regularOrder);
    }
}

// Feature 3
void RegisterNewCustomer() //RegisterNewCustomer method
{
    try
    {
        Console.Write("Enter name of customer: "); //Prompt user to enter name of customer 
        string name = Console.ReadLine();

        Console.Write("Enter ID number of customer: "); //Prompt user to enter the member ID of customer
        int memberID;
        while (true)
        {
            memberID = Convert.ToInt32(Console.ReadLine());
            if (!customerDict.ContainsKey(memberID))
            {
                break; // Exit the loop if the member ID is unique
            }
            else
            {
                Console.WriteLine("Duplicate member ID. Please enter a unique ID: "); //Warn user if the member ID isn't unique and prompts user to put in another ID
            }

        }
        Console.Write("Enter date of birth of customer in (dd/MM/yyyy): "); //Prompt user to enter the date of birth of customer
        DateTime dob = Convert.ToDateTime(Console.ReadLine());

        Customer newCustomer = new Customer(name, memberID, dob);//Instantiated a customer object 

        PointCard pointCard = new PointCard();//Instantiated a Pointcard object
        newCustomer.Rewards = pointCard; //Assigned Pointcard object to customer

        string newCustomerLine = string.Format("\n{0},{1},{2},{3},{4},{5}", newCustomer.Name, newCustomer.MemberID, newCustomer.DOB.ToString("dd/MM/yyyy"), "Ordinary", "0", "0"); //Formatting the string of what will be appended to the csv file.

        customerDict.Add(memberID, newCustomer); //Add a new customer to customerDict once new customer registered.

        File.AppendAllLines("customers.csv", new[] { newCustomerLine }); //Appending the newly registered customer to the csv file
    }
    catch
    {
        Console.WriteLine("The input format is incorrect. Do enter the Customer ID/DOB of Customer correctly."); //Should there be any issues regarding the format of what the user input for the member ID & DOB prompts, this message will show up.
    }
}

// Feature 5
void DisplayOrderDetails()
{
    ListAllCustomers();
    int id;
    while (true)
    {
        try
        {
            Console.Write("Customer ID: ");
            id = Convert.ToInt32(Console.ReadLine());

            if(customerDict.ContainsKey(id) == false)
            {
                throw new FormatException();
            }

            break;
            
        } catch 
        { 
            Console.WriteLine("Invalid Customer ID: ID must be a whole number from the above list.");
        }

    }

    Customer customer = customerDict[id];
    Console.WriteLine("Current Order:");
    Console.WriteLine(customer.CurrentOrder);
    Console.WriteLine("_________________________________________");

    Console.WriteLine("Order History:");
    foreach(Order pastOrder in customer.OrderHistory)
    {
        Console.WriteLine(pastOrder + " Time Fulfilled: " + pastOrder.TimeFulfilled);
    }
}

void DisplayMenu() //Display Menu Method
{
    Console.WriteLine("\nWelcome to I.C.Treats Management System");
    Console.WriteLine("What would you like to do today?");
    Console.WriteLine("---------------------------------------");
    Console.WriteLine("[1] List all customers"); //Option 1
    Console.WriteLine("[2] List all current orders"); //Option 2
    Console.WriteLine("[3] Register a new customer"); //Option 3
    Console.WriteLine("[4] Create a customer's order"); //Option 4
    Console.WriteLine("[5] Display order details of a customer"); //Option 5
    Console.WriteLine("[6] Modify order details"); //Option 6
    Console.WriteLine("[0] Exit"); //Option 0 / Exit Option
    Console.WriteLine("---------------------------------------");

}

while (true) //Starting off the program with a while loop
{
    DisplayMenu(); //Calling the DisplayMenu method
    try
    {
        Console.Write("Enter your option: "); //Asking user to input in their option
        int option = Convert.ToInt32(Console.ReadLine());

        if (option == 1)
        {
            ListAllCustomers(); //Calling ListAllCustomers() method when option is 1
        }
        else if (option == 2)
        {
            ListCurrentOrders();
        }
        else if (option == 3)
        {
            RegisterNewCustomer(); //Calling RegisterNewCustomer method when option is 3
        }
        else if (option == 4)
        {
            //Console.WriteLine("Feature 4");
            //CreateCustomerOrder(); //Calling CreateCustomerOrder() method when option is 4
        }
        else if (option == 5)
        {
            DisplayOrderDetails();
        }
        else if (option == 6)
        {

        }
        else if (option == 0)
        {
            Console.WriteLine("Bye!"); //When option is 0, the program leaves a "Bye!" message before closing.
            break;
        }
        else
        {
            Console.WriteLine("Invalid option."); //When option is out of range, the program tells user that the option is invalid and displaying the menu once again.
        }
    }
    catch (FormatException ex)
    {
        //Console.WriteLine(ex);
        Console.WriteLine("Please input in an integer for the option you'd like to choose. Exception: {0}", ex.Message); //When there's a format exception, this message will show up.
    }
}

/* ------------------------------------------ WIP ------------------------------------------------
void ModifyOrderDetails() 
{
    ListAllCustomers();
    int id;
    while (true)
    {
        try
        {
            Console.Write("Customer ID: ");
            id = Convert.ToInt32(Console.ReadLine());

            if (customerDict.ContainsKey(id) == false)
            {
                throw new FormatException();
            }

            break;

        }
        catch
        {
            Console.WriteLine("Invalid Customer ID: ID must be a whole number from the above list.");
        }

    }

    Customer customer = customerDict[id];

    Console.WriteLine(@"_____________________________
[1] Modify existing ice cream
[2] Add new ice cream
[3] Delete existing ice cream
_____________________________");
    int choice;
    while (true)
    {
        try
        {
            Console.Write("Choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            int[] choices = { 1, 2, 3 };
            if (choices.Contains(choice) == false)
            {
                throw new FormatException();
            }

            break;

        } catch
        {
            Console.WriteLine("Invalid choice: Choice must be a whole number from 1 to 3.");
        }
    }

    if (choice == 1)
    {
        

        // Option
        Console.Write(@"Choose Ice Cream Option
-----------------------
[1] Cup
[2] Cone
[3] Waffle (+$3)
_______________________");
        string option = "";

        // Validation
        while (true)
        {
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
            }
            catch
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
                }
                catch
                {
                    Console.WriteLine("Invalid flavour: Flavour must be a whole number from 0 to 6.");
                }
            }

            if (flavour == 0)
            {
                break;
            }
            else
            {
                if (flavours.Contains(availableFlavours[flavour]))
                {
                    flavours[flavours.IndexOf(availableFlavours[flavour])].Quantity += 1;
                }
                else
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

            }
            catch
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


        }
        else if (option == "Waffle") // Waffle
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


        }
        else if (option == "Cup") // Cup
        {
            IceCreamList[index] = new Cup("Cup", scoops, flavours, toppings);
        }
    }

    
}
*/