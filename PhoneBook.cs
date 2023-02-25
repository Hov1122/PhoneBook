using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneBookConsole
{
    internal static class PhoneBook
         
    {
        private static List<string> verificationLines = new List<string>();
        private static List<UserInfo> userInfos = new List<UserInfo>();

        private const int WordsCountSurname = 4;
        private const int WordsCountNoSurname = 3;

        public static void Sort(Input getData)
        {
            (string filePath, Criteria criteria, Order ordering) = getData();

            // if the data is invalid add corresponding message to verifactionLines if not store the data in userInfos.

            int count = 1;
            foreach (string line in File.ReadLines(filePath))
            {
                VerifyLine(line, count++);
            }
            
            switch (criteria)
            {
                case Criteria.Name:
                    userInfos = userInfos.OrderByFlag(x => x.Name, ordering == Order.Descending).ToList();
                    break;
                case Criteria.Surname:
                    userInfos = userInfos.OrderBy(x => string.IsNullOrEmpty(x.Surname)).ThenByFlag(x => x.Surname, ordering == Order.Descending).ToList();
                    break;
                case Criteria.Number:
                    userInfos = userInfos.OrderByFlag(x => Convert.ToInt16(x.Number.Substring(0, 3)), ordering == Order.Descending).ToList();
                    break;
            }
        }

        private static void VerifyLine(string line, int count)
        {
            line = line.Trim();
            string[] tokens = line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            string verificationMessage = "";
            int sepIndex = -1;

            // surname can be empty
            if (tokens.Length == WordsCountSurname) sepIndex = 2;
            if (tokens.Length == WordsCountNoSurname) sepIndex = 1;

            if (sepIndex == -1)
            {
                verificationLines.Add($"Line {count}: Data structure must be {{Name}} [Surname] {{Separator(: or -)}} {{Number(9 digits long)}}");
                return;
            }

            if (tokens[sepIndex] != ":" && tokens[sepIndex] != "-")
            {
                verificationMessage += "The separator should be ':' or '-'. ";
            }

            // Number is always next to Seperator
            
            if (!tokens[sepIndex + 1].All(char.IsNumber))
            {
                verificationMessage += "The phone number should only contain dighits. ";
            }

            if (tokens[sepIndex + 1].Length != 9)
            {
                verificationMessage += "The phone number should be 9 digits long.";
            }

            if (verificationMessage != "")
            {
                verificationLines.Add($"Line {count}: " + verificationMessage);
                return;
            }

            // if the data is valid then add it to userInfos
           
            userInfos.Add(new UserInfo
                {
                    Name = tokens[0],
                    Surname = tokens.Length == 4 ? tokens[1] : "",
                    Separator = tokens[sepIndex],
                    Number = tokens[sepIndex + 1]
                });
            
        }
        public static void ShowRecords()
        {
            Console.WriteLine("\nSorted records");

            foreach (var userInfo in userInfos)
            {
                Console.WriteLine(userInfo.ToString());
            }

        }
        public static void ShowValidationLines()
        {
            if (verificationLines.Count != 0)
            {
                Console.WriteLine("\nVerifications");
                foreach (string line in verificationLines)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
