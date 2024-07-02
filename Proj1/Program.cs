using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace ModernAppliances
{
    class Program
    {
        static List<Appliance> appliances = new List<Appliance>();

        static void Main(string[] args)
        {
            ParseAppliancesFile("appliances.txt");
            bool running = true;

            while (running)
            {
                Console.WriteLine("Welcome to Modern Appliances!\nHow may we assist you?\n1 – Check out appliance\n2 – Find appliances by brand\n3 – Display appliances by type\n4 – Produce random appliance list\n5 – Save & exit\nEnter option:");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        CheckoutAppliance();
                        break;
                    case 2:
                        FindAppliancesByBrand();
                        break;
                    case 3:
                        DisplayAppliancesByType();
                        break;
                    case 4:
                        ProduceRandomApplianceList();
                        break;
                    case 5:
                        SaveAppliancesFile("appliances.txt");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void ParseAppliancesFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(';');

                switch (parts[0][0])
                {
                    case '1':
                        appliances.Add(new Refrigerator(parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3]), parts[4], double.Parse(parts[5]), int.Parse(parts[6]), int.Parse(parts[7]), int.Parse(parts[8])));
                        break;
                    case '2':
                        appliances.Add(new Vacuum(parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3]), parts[4], double.Parse(parts[5]), parts[6], int.Parse(parts[7])));
                        break;
                    case '3':
                        appliances.Add(new Microwave(parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3]), parts[4], double.Parse(parts[5]), double.Parse(parts[6]), parts[7]));
                        break;
                    case '4':
                    case '5':
                        appliances.Add(new Dishwasher(parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3]), parts[4], double.Parse(parts[5]), parts[6], parts[7]));
                        break;
                }
            }
        }

        static void CheckoutAppliance()
        {
            Console.WriteLine("Enter the item number of an appliance:");
            string itemNumber = Console.ReadLine();

            var appliance = appliances.FirstOrDefault(a => a.ItemNumber == itemNumber);

            if (appliance == null)
            {
                Console.WriteLine($"No appliances found with that item number.");
            }
            else if (appliance.Quantity == 0)
            {
                Console.WriteLine("The appliance is not available to be checked out.");
            }
            else
            {
                appliance.Quantity--;
                Console.WriteLine($"Appliance \"{itemNumber}\" has been checked out.");
            }
        }

        static void FindAppliancesByBrand()
        {
            Console.WriteLine("Enter brand to search for:");
            string brand = Console.ReadLine().ToLower();

            var matchingAppliances = appliances.Where(a => a.Brand.ToLower() == brand).ToList();

            if (!matchingAppliances.Any())
            {
                Console.WriteLine("No appliances found with that brand.");
            }
            else
            {
                Console.WriteLine("Matching Appliances:");
                foreach (var appliance in matchingAppliances)
                {
                    Console.WriteLine(appliance);
                }
            }
        }









        static void DisplayAppliancesByType()
        {
            Console.WriteLine("Appliance Types\n1 – Refrigerators\n2 – Vacuums\n3 – Microwaves\n4 – Dishwashers\nEnter type of appliance:");
            int type = int.Parse(Console.ReadLine());

            List<Appliance> matchingAppliances = null;

            switch (type)
            {
                case 1:
                    Console.WriteLine("Enter number of doors:");
                    int doors = int.Parse(Console.ReadLine());
                    matchingAppliances = appliances.OfType<Refrigerator>().Where(r => r.NumberOfDoors == doors).ToList<Appliance>();
                    break;
                case 2:
                    Console.WriteLine("Enter grade:");
                    string grade = Console.ReadLine();
                    matchingAppliances = appliances.OfType<Vacuum>().Where(v => v.Grade == grade).ToList<Appliance>();
                    break;
                case 3:
                    Console.WriteLine("Room types\nK – Kitchen\nW – Work site\nB – Bathroom\nEnter room type:");
                    string roomType = Console.ReadLine();
                    matchingAppliances = appliances.OfType<Microwave>().Where(m => m.RoomType == roomType).ToList<Appliance>();
                    break;
                case 4:
                    Console.WriteLine("Sound Ratings\nQ – Quietest\nM – Moderate\nL – Loudest\nEnter sound rating:");
                    string soundRating = Console.ReadLine();
                    matchingAppliances = appliances.OfType<Dishwasher>().Where(d => d.SoundRating == soundRating).ToList<Appliance>();
                    break;
                default:
                    Console.WriteLine("Invalid type.");
                    break;
            }

            if (matchingAppliances == null || !matchingAppliances.Any())
            {
                Console.WriteLine("No matching appliances found.");
            }
            else
            {
                Console.WriteLine("Matching Appliances:");
                foreach (var appliance in matchingAppliances)
                {
                    Console.WriteLine(appliance);
                }
            }
        }






        static void ProduceRandomApplianceList()
        {
            Console.WriteLine("Enter number of appliances:");
            int number = int.Parse(Console.ReadLine());

            var random = new Random();
            var randomAppliances = appliances.OrderBy(a => random.Next()).Take(number).ToList();

            if (!randomAppliances.Any())
            {
                Console.WriteLine("No appliances found.");
            }
            else
            {
                Console.WriteLine("Random Appliances:");
                foreach (var appliance in randomAppliances)
                {
                    Console.WriteLine(appliance);
                }
            }
        }





        static void SaveAppliancesFile(string filePath)
        {
            var lines = appliances.Select(a => a.ToString());
            File.WriteAllLines(filePath, lines);
        }
    }
}
