using System;
using System.Text;

namespace _3._0Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                int tries = 10;
                //Return word
                string secretword = GetSecretWord();
                //Return word with '_'
                string secretlook = GetSecretLook(secretword);
                //Get program score
                bool score = GetProgram(secretword, secretlook, tries);
                if (score)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Victory!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failure!");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press enter to quit");
                Console.ReadLine();
                break;
            } while (true);
        }//end of Main

        static string GetSecretWord()
        {
            string[] word = { "VOLVO", "MAZDA", "DODGE" };
            Random random = new Random();
            int num = random.Next(word.Length);
            return word[num];
        }
        static string GetSecretLook(string secretword)
        {
            StringBuilder builder = new StringBuilder("");
            for (int j = 0; j < secretword.Length; ++j)
            {
                builder.Append(" _ ");
            }
            string result = builder.ToString();

            return result;
        }
        static bool GetProgram(string secretword, string secretlook, int tries)
        {
            StringBuilder all_inputs = new StringBuilder("");
            string user_input = "";
            string user_letter = "";
            for (int i = 1; i <= tries; ++i)
            {
                Console.WriteLine(string.Format("{0}    Try {1} of {2}. You have used: {3}", secretlook, i, tries, all_inputs));
                while (true)
                {
                    user_input = Console.ReadLine();
                    user_input = user_input.ToUpper();
                    //If input was a word
                    if (1 < user_input.Length)
                    {
                        if (string.Equals(user_input, secretword, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                        else
                            break;
                    }
                    user_letter = user_input.Substring(0);
                    //If input was a letter
                    if (all_inputs.ToString().Contains(user_letter) == false)
                    {
                        all_inputs.Append(user_letter);
                        break;
                    }
                    //If player want to quit
                    else if (user_input.Equals(""))
                    {
                        return false;
                    }
                    //Remind player have already used letter
                    else
                    {
                        Console.WriteLine($"You've already used {user_letter}");
                        continue;
                    }
                }

                //Replace every space with nothing
                secretlook = secretlook.Replace(" ", "");
                //Take user letter|secret word|secret look into a charArray
                char[] result_charArr = user_letter.ToCharArray();
                char[] name_charArr = secretword.ToCharArray();
                char[] look_charArr = secretlook.ToCharArray();

                for (int j = 0; j < name_charArr.Length; ++j)
                {
                    char c = name_charArr[j];
                    foreach (char u in result_charArr)
                    {
                        if (c == u)
                        {
                            look_charArr[j] = c;
                        }
                    }
                }
                secretlook = "";
                foreach(char item in look_charArr)
                {
                    secretlook = secretlook + string.Format($" {item} ");
                }

                if (secretlook.Contains("_") == false)
                {
                    Console.WriteLine(string.Format("{0}   Try {1} of {2}: ", secretlook, i, tries));
                    return true;
                }
                else if (secretlook.Contains("_") == true && i == tries)
                {
                    Console.WriteLine(string.Format("{0}   Try {1} of {2}: ", secretlook, i, tries));
                    return false;
                }
            }
            return false;
        }
    }
}