using System;

namespace PhoneBookConsole
{
    public struct UserInfo
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Separator { get; set; }
        public string Number { get; set; }
        public override string ToString()
        {
            return $"{Name} {(Surname == "" ? "" : Surname + " ")}{Separator} {Number}";
        }
    }
    public enum Criteria { Name = 1, Surname, Number };
    public enum Order { Ascending = 1, Descending };

    public delegate (string, Criteria, Order) Input();
    class Program
    {
        static void Main(string[] args)
        {

            Input GetInputDel = GetInput;

            PhoneBook.Sort(GetInputDel);
            PhoneBook.ShowRecords();
            PhoneBook.ShowValidationLines();
        }

        public static (string, Criteria, Order) GetInput()
        {
            Console.WriteLine("Data structure should be {Name} [Surname] {Separator(: or -)} {Number(9 digits long)}\n");

            Order ordering;
            Criteria criteria;
            string filePath;

            while (true)
            {
                Console.WriteLine("Please enter a file path (Example data/data1.txt): ");
                string? path = Console.ReadLine();
                if (path == null) continue;

                filePath = Path.Combine(Directory.GetCurrentDirectory(), path);
                if (File.Exists(filePath))
                {
                    break;
                }

                PrintErrorMsg("Enter valid relative path");
            }

            while (true)
            {
                Console.WriteLine("Please choose an ordering to sort: 1: Ascending or 2: Descending (1|2): ");
                int ord = 0;
                if (int.TryParse(Console.ReadLine(), out ord) && (ord == 1 || ord == 2))
                {
                    ordering = (Order)ord;
                    break;
                }

                PrintErrorMsg("Enter valid Order number 1 or 2");
            }

            while (true)
            {

                Console.WriteLine("Please choose criteria: 1: Name, 2: Surname or 3: Number (1|2|3): ");
                int crit = 0;
                if (int.TryParse(Console.ReadLine(), out crit) && crit >= 1 && crit <= 3)
                {
                    criteria = (Criteria)crit;
                    break;
                }

                PrintErrorMsg("Enter valid Criteria number from 1 to 3");
            }

            return (filePath, criteria, ordering);
        }

        public static void PrintErrorMsg(string msg)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
