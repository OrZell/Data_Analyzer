using Newtonsoft.Json;

namespace DataAnalyzer
{
    class Terrorist
    {
        public string name;
        public string weapon;
        public int age;
        public Dictionary<char, int> gps;
        public string affiliation;
        public Terrorist(string name, string weapon, int age, Dictionary<char, int> gps, string affiliation)
        {
            this.name = name;
            this.weapon = weapon;
            this.age = age;
            this.gps = gps;
            this.affiliation = affiliation;
        }
    }

    class Functions
    {
        public static List<Terrorist> craeteData(int num)
        {
            List<string> names = new List<string> { "Ahmed", "Ziad", "Salim", "Omar", "Hafez" };
            List<string> weapons = new List<string> { "Knife", "M4A1", "Handgun", "Rock" };
            List<string> affiliations = new List<string> { "Hamas", "Hizballa", "Daesh", "Ashaf" };
            List<Terrorist> Terrorists = new List<Terrorist>();
            Random random = new Random();
            for (int i = 0; i < num; i++)
            {
                string name = names.ElementAt(random.Next(names.Count));
                names.Remove(name);
                string weapon = weapons.ElementAt(random.Next(weapons.Count));
                string affiliation = affiliations.ElementAt(random.Next(affiliations.Count));
                int age = random.Next(16, 50);
                Dictionary<char, int> GPSs = new Dictionary<char, int>();
                GPSs.Add('x', random.Next(-10, 11)); GPSs.Add('y', random.Next(-10, 11));
                Terrorists.Add(new Terrorist(name, weapon, age, GPSs, affiliation));
            }
            return Terrorists;
        }
        public static void Menu()
        {
            Console.WriteLine("Plaese Choose From The Options Below:\n" +
                "1) Find The Most Common Weapon\n" +
                "2) Find The Least Common Weapon\n" +
                "3) Find The Organization With The Most Members\n" +
                "4) Find The Organization With The Least Members\n" +
                "5) Find The 2 Terrorists Who Are Closest To Each Other\n" +
                "6) Print The Data\n" + 
                "7) Print By Name\n" +
                "8) Exit");
        }
        public static void switchMenuChoices(List<Terrorist> Data)
        {
            bool parsed = false, exit = false;
            int userChoice;
            do
            {
                Functions.Menu();
                parsed = int.TryParse(Console.ReadLine()!, out userChoice);
                if (!parsed)
                {
                    Console.WriteLine("Please Enetr Only Numbers");
                    continue;
                }
                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine(Functions.commonWeapon(Data));
                        break;

                    case 2:
                        Console.WriteLine(Functions.leastWeapon(Data));
                        break;

                    case 3:
                        Console.WriteLine(Functions.commonAffiliation(Data));
                        break;

                    case 4:
                        Console.WriteLine(Functions.leastAffiliation(Data));
                        break;

                    case 5:
                        Console.WriteLine(Functions.checkTheTinyDis(Functions.dictOfTwoNamesAndDisBetweenThem(Functions.dictOfNamesAndGPS(Data))));
                        break;

                    case 6:
                        Console.WriteLine(Functions.toPrint(Data));
                        break;

                    case 7:
                        Console.WriteLine(Functions.toPrint(Functions.findByName(Data)));
                        break;

                    case 8:
                        Console.WriteLine("Bye Bye");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Please Enter Numbers Between 1 To 6");
                        break;
                }
            } while (!exit);
        }

        public static List<string> listWeapon(List<Terrorist> Data)
        {
            List<string> weapons = new List<string>();
            foreach (Terrorist trr in Data)
            {
                weapons.Add(trr.weapon);
            }
            return weapons;
        }

        public static List<string> listAffiliations(List<Terrorist> Data)
        {
            List<string> affiliations = new List<string>();
            foreach (Terrorist trr in Data)
            {
                affiliations.Add(trr.affiliation);
            }
            return affiliations;
        }

        public static List<string> mostWeapon(List<string> weapons, bool sign)
        {
            Dictionary<string, int> countWepons = new Dictionary<string, int>();
            foreach (string weapon in weapons)
            {
                if (countWepons.ContainsKey(weapon))
                {
                    countWepons[weapon] += 1;
                }
                else
                {
                    countWepons[weapon] = 1;
                }
            }

            List<string> leastWeapon=new List<string>(), commonWeapon=new List<string>();
            int least = countWepons.Count, common = 0;
            foreach (System.Collections.Generic.KeyValuePair<string, int> weapon in countWepons)
            {
                if (weapon.Value == common)
                {
                    commonWeapon.Add(weapon.Key);
                }
                if (weapon.Value > common)
                {
                    common = weapon.Value;
                    commonWeapon.Clear(); commonWeapon.Add(weapon.Key);
                }
                if (weapon.Value == least)
                {
                    leastWeapon.Add(weapon.Key);
                }
                if (weapon.Value < least)
                {
                    least = weapon.Value;
                    leastWeapon.Clear(); leastWeapon.Add(weapon.Key);
                }
            }
            if (sign)
            {
                return commonWeapon;
            }
            else
            {
                return leastWeapon;
            }
        }
        public static List<string> mostAffiliation(List<string> affiliations, bool sign)
        {
            Dictionary<string, int> countAffiliations = new Dictionary<string, int>();
            foreach (string affiliation in affiliations)
            {
                if (countAffiliations.ContainsKey(affiliation))
                {
                    countAffiliations[affiliation] += 1;
                }
                else
                {
                    countAffiliations[affiliation] = 1;
                }
            }
            List<string> commonAffiliation = new List<string>(), leapAffiliation = new List<string>();
            int common = 0, leap = countAffiliations.Count;
            foreach (System.Collections.Generic.KeyValuePair<string, int> affiliation in countAffiliations)
            {
                if (affiliation.Value == common)
                {
                    commonAffiliation.Add(affiliation.Key);
                }
                if (affiliation.Value > common)
                {
                    common = affiliation.Value;
                    commonAffiliation.Clear(); commonAffiliation.Add(affiliation.Key);
                }
                if (affiliation.Value == leap)
                {
                    leapAffiliation.Add(affiliation.Key);
                }
                if (affiliation.Value < leap)
                {
                    leap = affiliation.Value;
                    leapAffiliation.Clear();  leapAffiliation.Add(affiliation.Key);
                }
            }
            if (sign)
            {
                return commonAffiliation;
            }
            else
            {
                return leapAffiliation;
            }
        }
        public static string listStringToString(List<string> listString)
        {
            string result = "";
            for (int i = 0; i < listString.Count; i++)
            {
                result += listString.ElementAt(i);
                if (i < listString.Count - 1)
                {
                    result += "\n";
                }
            }
            return result;
        }
        public static string commonWeapon(List<Terrorist> Data)
        {
            return Functions.listStringToString(Functions.mostWeapon(Functions.listWeapon(Data), true));
        }
        public static string leastWeapon(List<Terrorist> Data)
        {
            return Functions.listStringToString(Functions.mostWeapon(Functions.listWeapon(Data), false));
        }
        public static string commonAffiliation(List<Terrorist> Data)
        {
            return Functions.listStringToString(Functions.mostAffiliation(Functions.listAffiliations(Data), true));
        }
        public static string leastAffiliation(List<Terrorist> Data)
        {
            return Functions.listStringToString(Functions.mostAffiliation(Functions.listAffiliations(Data), false));
        }
        public static Dictionary<string, Dictionary<char, int>> dictOfNamesAndGPS(List<Terrorist> Data)
        {
            Dictionary<string, Dictionary<char, int>> namesAndGPS = new Dictionary<string, Dictionary<char, int>>();
            foreach (Terrorist trr in Data)
            {
                namesAndGPS.Add(trr.name, trr.gps);
            }
            return namesAndGPS;
        }
        public static double calculaateDisTwoPoints(Dictionary<char, int> firstGPS, Dictionary<char, int> secondGPS)
        {
            int firstGPSX = firstGPS['x'], firstGPSY = firstGPS['y'], secondGPSX = secondGPS['x'], secondGPSY = secondGPS['y'];
            double distance = Math.Sqrt(Math.Pow(firstGPSX-secondGPSX,2) + Math.Pow(firstGPSY-secondGPSY,2));
            return distance;

        }
        public static Dictionary<List<string>, double> dictOfTwoNamesAndDisBetweenThem(Dictionary<string, Dictionary<char, int>> namesAndGPS)
        {
            Dictionary<List<string>, double> namesAndDisBetweenThem = new Dictionary<List<string>, double>();
            for (int i = 0; i < namesAndGPS.Count-1; i++)
            {
                for (int j = i+1; j < namesAndGPS.Count; j++)
                {
                    List<string> names = new List<string>();
                    string firstName = namesAndGPS.ElementAt(i).Key, secondName = namesAndGPS.ElementAt(j).Key;
                    Dictionary<char, int> firstGPS = namesAndGPS.ElementAt(i).Value, secondGPS = namesAndGPS.ElementAt(j).Value;
                    names.Add(firstName); names.Add(secondName);
                    namesAndDisBetweenThem.Add(names, Functions.calculaateDisTwoPoints(firstGPS, secondGPS));
                }
            }
            return namesAndDisBetweenThem;
        }
        public static string checkTheTinyDis(Dictionary<List<string>, double> namesAndDisBetweenThem)
        {
            double distance = namesAndDisBetweenThem.ElementAt(0).Value;
            List<string> names = namesAndDisBetweenThem.ElementAt(0).Key;
            foreach (System.Collections.Generic.KeyValuePair<System.Collections.Generic.List<string>, double> namesAndDis in namesAndDisBetweenThem)
            {
                if (namesAndDis.Value < distance)
                {
                    distance = namesAndDis.Value;
                    names = namesAndDis.Key;
                }
            }
            string firstName = names.ElementAt(0), secondName = names.ElementAt(1);
            return $"The Closest Terrorists Are {firstName} And {secondName}, And The Distance Between Them Is {distance}.";
        }
        public static string toPrint(List<Terrorist> terrorists)
        {
            string allData = "\n";
            foreach (Terrorist trr in terrorists)
            {
                string json = JsonConvert.SerializeObject(trr, Formatting.Indented);
                allData += json;
            }
            if (allData == "\n")
            {
                allData += "Not Name Found";
            }
            return allData + "\n";
        }
        public static List<Terrorist> findByName(List<Terrorist> Data)
        {
            Console.WriteLine("Plaese Enter The Name Of The Target");
            string name = Console.ReadLine()!;
            List<Terrorist> terrorist = new List<Terrorist>();
            foreach (Terrorist trr in Data)
            {
                if (trr.name.ToLower() == name.ToLower())
                {
                    terrorist.Add(trr);
                }
            }
            return terrorist;
        }
    }


    class Program
    {
        static void Main()
        {
            Functions.switchMenuChoices(Functions.craeteData(5));
        }
    }
}
