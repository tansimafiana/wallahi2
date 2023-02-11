using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace wallahi2
{
    internal class Program
    {
        // Arrays and Lists of appliances
        private static Appliance[] appliances;
        private static List<Refrigirator> refrigirators = new List<Refrigirator>();
        private static List<Vacuum> vacuums = new List<Vacuum>();
        private static List<Microwave> microwaves = new List<Microwave>();
        private static List<Dishwasher> dishwashers = new List<Dishwasher>();

        public static string pathToFile = "C:\\Users\\18253\\Downloads\\appliances.txt";
        //public static string pathToFile = "D:\\Downloads\\appliances.txt";

        static void Main(string[] args)
        {
            Initialize();
            Intro();

            Console.ReadLine();
        }

        static void Initialize()
        {
            // Reading from the file
            string[] linesOfText = File.ReadAllLines(pathToFile);
            appliances = new Appliance[linesOfText.Length];

            // Looping through each line and parsing the data into their respectful classes
            int index = 0;
            foreach (string line in linesOfText)
            {
                int deviceType = int.Parse(line[0] + "");
                string[] infoSplit = line.Split(';');

                // Main similar variables
                long itemNumber = long.Parse(infoSplit[0]);
                string brand    = infoSplit[1];
                int quantity    = int.Parse(infoSplit[2]);
                double wattage  = double.Parse(infoSplit[3]);
                string color    = infoSplit[4];
                double price    = double.Parse(infoSplit[5]);

                // Switch cases that add additional variables and initialize the
                //  variables into their appropriate lists.
                switch (deviceType)
                {
                    case 1:     // Refrigirator
                        int numberOfDoors = int.Parse(infoSplit[6]);
                        int height = int.Parse(infoSplit[7]);
                        int width = int.Parse(infoSplit[8]);

                        Refrigirator newRef = new Refrigirator(itemNumber, brand, quantity, wattage, color, price, numberOfDoors, height, width);
                        appliances[index] = newRef;
                        refrigirators.Add(newRef);
                        break;
                    case 2:     // Vacuum
                        string grade = infoSplit[6];
                        int batteryVoltage = int.Parse(infoSplit[7]);
                        
                        Vacuum newVac = new Vacuum(itemNumber, brand, quantity, wattage, color, price, grade, batteryVoltage);
                        appliances[index] = newVac;
                        vacuums.Add(newVac);
                        break;
                    case 3:     // Microwave
                        double capacity = double.Parse(infoSplit[6]);
                        char roomType = char.Parse(infoSplit[7]);
                        
                        Microwave newMic = new Microwave(itemNumber, brand, quantity, wattage, color, price, capacity, roomType);
                        appliances[index] = newMic;
                        microwaves.Add(newMic);
                        break;
                    case 4:     // Dishwasher (4)
                        string feature = infoSplit[6];
                        string soundRating = infoSplit[7];
                        
                        Dishwasher newDish = new Dishwasher(itemNumber, brand, quantity, wattage, color, price, feature, soundRating);
                        appliances[index] = newDish;
                        dishwashers.Add(newDish);
                        break;
                    case 5:     // Dishwasher (5)
                        string feature2 = infoSplit[6];
                        string soundRating2 = infoSplit[7];

                        Dishwasher newDish2 = new Dishwasher(itemNumber, brand, quantity, wattage, color, price, feature2, soundRating2);
                        appliances[index] = newDish2;
                        dishwashers.Add(newDish2);
                        break;
                    default:
                        Console.WriteLine("[Error] Invalid device reading from appliances.txt");
                        break;
                }
                index++;
            }
        }

        static void Intro()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("\n\nWelcome to Modern Appliances!\n" +
                                  "How May We Assist You?\n" +
                                  " 1 - Check out appliance\n" +
                                  " 2 - Find appliances by brand\n" +
                                  " 3 - Display appliances by type\n" +
                                  " 4 - Produce random appliance list\n" +
                                  " 5 - Save & exit\n");


                Console.Write("Enter option: ");
                string selection;
                int intSelection;
                selection = Console.ReadLine();

                if (isValid(selection, out intSelection))
                {
                    switch (intSelection)
                    {
                        case 1:
                            FirstSelection();
                            break;
                        case 2:
                            SecondSelection();
                            break;
                        case 3:
                            ThirdSelection();
                            break;
                        case 4:
                            FourthSelection();
                            break;
                        case 5:
                            loop = FifthSelection();
                            break;
                        default:
                            Console.WriteLine("[Error] How did you get here?");
                            break;
                    }
                }
            }
        }

        static void FirstSelection()    // TODO: Add check for availability/existance.
        {
            while (true)
            {
                Console.Write("\n\nEnter the item number of an appliance: ");
                string selection = Console.ReadLine();

                bool isValidSelection = false;
                foreach (Appliance app in appliances)
                {
                    if (app.itemNumber == long.Parse(selection))
                    {
                        if (app.isAvailable())
                        {
                            Console.WriteLine("\nAppliance \"" + selection + "\" has been checked out.");
                            app.quantity--;
                            isValidSelection = true;
                        }
                        else
                        {
                            Console.WriteLine("\nThe appliance is not available to be checked out.");
                            isValidSelection = true;
                        }
                    }
                }

                if (isValidSelection == false)
                {
                    Console.WriteLine("Invalid Number Selection");
                }

                break;
            }
        }

        static void SecondSelection()
        {
            Console.Write("\n\nEnter brand to search for: ");
            string selection = Console.ReadLine();
            
            while (true)
            {
                bool isValidSelection = false;
                foreach (Appliance app in appliances)
                {
                    if (app.brand == selection)
                    {
                        if (!isValidSelection) { Console.WriteLine("Matching Appliances:");  }

                        Console.WriteLine(app.toString() + "\n");
                        isValidSelection = true;
                    }
                }

                if (isValidSelection == false)
                {
                    Console.WriteLine("No matching appliances with the name \"" + selection + "\"");
                }

                break;
            }
        }

        static void ThirdSelection()
        {
            Console.WriteLine("\n\nAppliance Types\n" +
                              " 1 - Refrigerators\n" +
                              " 2 - Vacuums\n" +
                              " 3 - Microwaves\n" +
                              " 4 - Dishwashers");
            Console.Write("\nEnter type of appliance: ");
            string selection = Console.ReadLine();

            string query;
            switch (selection)
            {
                case "1":
                    query = Refrigirator.searchQuery();
                    if (query != null)
                    {
                        foreach (Refrigirator r in refrigirators)
                        {
                            if (r.numberOfDoors == int.Parse(query))
                            {
                                Console.WriteLine(r.toString() + "\n");
                            }
                        }
                    }
                    break;
                case "2":
                    query = Vacuum.searchQuery();

                    if (query != null)
                    {
                        foreach (Vacuum v in vacuums)
                        {
                            int voltage;
                            if (!int.TryParse(query, out voltage))
                            {
                                voltage = (query.ToLower() == "low") ? 18 : 24;
                            }

                            if (v.batteryVoltage == voltage)
                            {
                                Console.WriteLine(v.toString() + "\n");
                            }
                        }
                    }
                    break;
                case "3":
                    query = Microwave.searchQuery();

                    if (query != null)
                    {
                        foreach (Microwave m in microwaves)
                        {
                            Console.WriteLine(query);
                            if (m.roomType == char.Parse(query))
                            {
                                Console.WriteLine(m.toString() + "\n");
                            }
                        }
                    }
                    break;
                case "4":
                    query = Dishwasher.searchQuery();

                    if (query != null)
                    {
                        foreach (Dishwasher d in dishwashers)
                        {
                            if (d.soundRating == query)
                            {
                                Console.WriteLine(d.toString() + "\n");
                            }
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Type Selection");
                    break;
            }
        }

        static void FourthSelection()
        {
            Console.Write("\n\nEnter number of appliances: ");
            string selection = Console.ReadLine();

            int numOfAppliances;
            if (int.TryParse(selection, out numOfAppliances))
            {
                if (numOfAppliances <= 0)
                {
                    // Clamp up
                    Console.WriteLine("\"" + numOfAppliances + "\" is too low, clamping up to 1...\n");
                    numOfAppliances = 1;
                }
                else if (numOfAppliances > appliances.Length)
                {
                    // Clamp down
                    Console.WriteLine("\"" + numOfAppliances + "\" is too high, clamping to max value of " + appliances.Length + "...");
                    numOfAppliances = appliances.Length;
                }


                // Making an array where no two numbers are the same
                int[] usedSelections = new int[numOfAppliances];

                // Choose a random number...
                Random rand = new Random();
                for (int i = 0; i < numOfAppliances; i++) {
                    int randInt = rand.Next(numOfAppliances);
                    usedSelections[i] = RecursiveArrayAdder(usedSelections, randInt);       // Recursively add 1 to it until it's an available number
                }

                // Now we print the random selection
                foreach (int selectedIndex in usedSelections)
                {
                    Console.WriteLine(appliances[selectedIndex].toString() + "\n");
                }
            }
        }

        static bool FifthSelection()
        {
            Console.WriteLine("\n\nThank you for using our services."
                            + "\nPress 'Enter' to exit.");

            // Saving...
            using (StreamWriter writer = new StreamWriter(pathToFile))
            {
                foreach (Appliance app in appliances)
                {
                    writer.WriteLine(app.formatForFile() + ";");
                }
            }

            return false;
        }

        static int RecursiveArrayAdder(int[] arr, int num)
        {
            if (num > appliances.Length - 1)
            {
                num = 0;
            }
            if (arr.Contains(num) && num != 0)
            {
                return RecursiveArrayAdder(arr, num + 1);
            }
            return num;
        }

        static bool isValid(string selection, out int output)
        {
            try
            {
                int parsed = int.Parse(selection);
                if (parsed >= 1 && parsed <= 5)
                {
                    output = parsed;
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid selection");
                    output = 0;
                    return false;
                }
            }
            catch (FormatException e)
            {
                //Console.WriteLine(e.StackTrace);
                Console.WriteLine("\n[Error] Wrong Formatting Exception.\n");
            }

            output = 0;
            return false;
        }
    }

    public class Appliance      // Base Class
    {
        public long itemNumber;
        public string brand;
        public int quantity;
        public double wattage;
        public string color;
        public double price;

        public bool isAvailable()
        {
            if (quantity > 0)
            {
                return true;
            }
            return false;
        }

        public void checkout()
        {

        }

        public virtual string formatForFile()
        {
            return "Format";
        }

        public virtual string toString()
        {
            return "Normal toString";
        }
    }


    // --- Inherited Classes ---

    public class Refrigirator : Appliance
    {
        public int numberOfDoors;
        public int height;
        public int width;

        public Refrigirator(long itemNumber, string brand, int quantity, double wattage, string color, double price, int numberOfDoors, int height, int width)
        {
            this.itemNumber = itemNumber;
            this.brand = brand;
            this.quantity = quantity;
            this.wattage = wattage;
            this.color = color;
            this.price = price;
            this.numberOfDoors = numberOfDoors;
            this.height = height;
            this.width = width;
        }

        public override string formatForFile()
        {
            return itemNumber + ";" + brand + ";" + quantity + ";" + wattage + ";" + color + ";" + price + ";" + numberOfDoors + ";" + height + ";" + width;
        }

        public override string toString()
        {
            return "Item Number: " + itemNumber +
                   "\nBrand: " + brand +
                   "\nQuantity: " + quantity +
                   "\nWattage: " + wattage +
                   "\nColor: " + color +
                   "\nPrice: " + price +
                   "\nNumber of Doors: " + numberOfDoors +
                   "\nHeight: " + height +
                   "\nWidth: " + width;
        }

        public static string searchQuery()
        {
            Console.WriteLine("Enter number of doors:  2 (double door),  3 (three doors),  or 4 (four doors): ");
            string selection = Console.ReadLine();

            int value;
            if (int.TryParse(selection, out value)) {
                if (value >= 1 && value <= 4)
                {
                    return selection;
                }
            }

            Console.WriteLine("Invalid number of doors.");
            return null;
        }
    }

    public class Vacuum : Appliance
    {
        public string grade;
        public int batteryVoltage;

        public Vacuum(long itemNumber, string brand, int quantity, double wattage, string color, double price, string grade, int batteryVoltage)
        {
            this.itemNumber = itemNumber;
            this.brand = brand;
            this.quantity = quantity;
            this.wattage = wattage;
            this.color = color;
            this.price = price;
            this.grade = grade;
            this.batteryVoltage = batteryVoltage;
        }

        public override string formatForFile()
        {
            return itemNumber + ";" + brand + ";" + quantity + ";" + wattage + ";" + color + ";" + price + ";" + grade + ";" + batteryVoltage;
        }

        public override string toString()
        {
            return "Item Number: " + itemNumber +
                   "\nBrand: " + brand +
                   "\nQuantity: " + quantity +
                   "\nWattage: " + wattage +
                   "\nColor: " + color +
                   "\nPrice: " + price +
                   "\nGrade: " + grade +
                   "\nBattery Voltage: " + ((batteryVoltage == 18) ? "Low" : "High");
        }

        public static string searchQuery()
        {
            Console.WriteLine("Enter battery voltage value.  18 V (Low)  or  24 V (High): ");
            string selection = Console.ReadLine();

            if (selection == "18" || selection == "24")
            {
                return selection;
            }
            else if (selection.ToLower() == "low")
            {
                return "18";
            }
            else if (selection.ToLower() == "high")
            {
                return "24";
            }

            Console.WriteLine("Invalid voltage value.");
            return null;
        }
    }

    public class Microwave : Appliance
    {
        public double capacity;
        public char roomType;

        public Microwave(long itemNumber, string brand, int quantity, double wattage, string color, double price, double capacity, char roomType)
        {
            this.itemNumber = itemNumber;
            this.brand = brand;
            this.quantity = quantity;
            this.wattage = wattage;
            this.color = color;
            this.price = price;
            this.capacity = capacity;
            this.roomType = roomType;
        }

        public override string formatForFile()
        {
            return itemNumber + ";" + brand + ";" + quantity + ";" + wattage + ";" + color + ";" + price + ";" + capacity + ";" + roomType;
        }

        public override string toString()
        {
            return "Item Number: " + itemNumber +
                   "\nBrand: " + brand +
                   "\nQuantity: " + quantity +
                   "\nWattage: " + wattage +
                   "\nColor: " + color +
                   "\nPrice: " + price +
                   "\nCapacity: " + capacity +
                   "\nRoom Type: " + roomType;
        }

        public static string searchQuery()
        {
            Console.WriteLine("Room where the microwave will be installed:  K (kitchen)  or  W (worksite): ");
            string selection = Console.ReadLine();

            string checkSelection = selection.ToLower();
            if (checkSelection == "k" || checkSelection == "w")
            {
                return selection.ToUpper();
            }

            return null;
        }
    }

    public class Dishwasher : Appliance
    {
        public string feature;
        public string soundRating;

        public Dishwasher(long itemNumber, string brand, int quantity, double wattage, string color, double price, string feature, string soundRating)
        {
            this.itemNumber = itemNumber;
            this.brand = brand;
            this.quantity = quantity;
            this.wattage = wattage;
            this.color = color;
            this.price = price;
            this.feature = feature;
            this.soundRating = soundRating;
        }

        public override string formatForFile()
        {
            return itemNumber + ";" + brand + ";" + quantity + ";" + wattage + ";" + color + ";" + price + ";" + feature + ";" + soundRating;
        }

        public override string toString()
        {
            return "Item Number: " + itemNumber +
                   "\nBrand: " + brand +
                   "\nQuantity: " + quantity +
                   "\nWattage: " + wattage +
                   "\nColor: " + color +
                   "\nPrice: " + price +
                   "\nFeature: " + feature +
                   "\nSound Rating: " + soundRating;
        }

        public static string searchQuery()
        {
            Console.WriteLine("Enter the sound rating of the dishwasher:  Qt (Quietest),  Qr (Quieter),  Qu (Quiet),  or  M (Moderate): ");
            string selection = Console.ReadLine();

            string checkSelection = selection.ToLower();
            if (checkSelection == "qt" || checkSelection == "qr" || checkSelection == "qu")
            {
                return selection[0].ToString().ToUpper() + selection[1];
            }
            else if (checkSelection == "m")
            {
                return selection.ToString().ToUpper();
            }

            Console.WriteLine("Invalid Rating value.");
            return null;
        }
    }
}