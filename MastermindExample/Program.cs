using System;
using System.Linq;

namespace MastermindExample
{
    class Program
    {
        // Instantiate random number generator.  
        private static readonly Random _random = new Random();

        static string[] solution = new string[4];
        static int solutionLength = 4;
        static int minVal = 1;
        static int maxVal = 6;
        static int maxAttempts = 10;
        static int remainingAttempts = 10;
        static bool bSolutionSuccess = false;
        static string currentGuess = "";

        /// <summary>
        /// This program is a C# console application that is a simple version of Mastermind.
        /// The randomly generated answer should be four(4) digits in length.
        /// Each digit between the numbers 1 and 6.
        /// After the player enters a combination, 
        ///    *A minus (-) sign should be printed for every digit that is correct but in the wrong position, 
        ///    *A plus (+) sign should be printed for every digit that is both correct and in the correct position.  
        ///    *Nothing should be printed for incorrect digits.  
        /// The player has ten (10) attempts to guess the number correctly before receiving a message that they have lost.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            RandomizeSolution();
            //PrintSolution();
            PrintInstructions();

            while(remainingAttempts > 0)
            {
                Console.WriteLine(string.Format("Remaining Attempts {0}!!!", remainingAttempts));
                bSolutionSuccess = false;
                currentGuess = GetUserInput();

                if (!IsValidGuess(currentGuess))
                {
                    Console.WriteLine(string.Format("Please Enter a valid {0} digit guess!", solutionLength));
                    continue;
                }

                bSolutionSuccess = CheckSolution(currentGuess);

                if (!bSolutionSuccess)
                {
                    Console.WriteLine(GenerateHintString(currentGuess));
                }
                else
                {
                    bSolutionSuccess = true;
                    break;
                }

                remainingAttempts--;
            }

            if (bSolutionSuccess)
            {
                //Winning
                PrintWinningSolution();
            }
            else
            {
                //Losing Message
                PrintLosingSolution();
            }

            Console.ReadKey();
        }

        private static string GenerateHintString(string currentGuess)
        {
            char[] guess1 = currentGuess.ToCharArray();
            char[] sol1 = String.Join("", solution).ToCharArray();

            //A plus(+) sign for every digit that is both correct and in the correct position
            for (int i = 0; i < sol1.Length; i++)
            {
                if (sol1[i] == guess1[i])
                {
                    sol1[i] = '+';
                }
            }

            //A minus(-) sign for every digit that is correct but in the wrong position
            for (int i = 0; i < sol1.Length; i++)
            {
                if (sol1[i] == '+') continue;

                if (sol1.Contains(guess1[i]))
                {
                    sol1[i] = '-';
                }
            }

            //Nothing should be printed for incorrect digits
            for (int i = 0; i < sol1.Length; i++)
            {
                if (sol1[i] != '-' && sol1[i] != '+')
                {
                    sol1[i] = ' ';
                }
            }             

            return String.Join("", sol1);
        }

        private static bool CheckSolution(string currentGuess)
        {
            if (string.IsNullOrEmpty(currentGuess)) return false;
            string solutionText = String.Join("", solution);

            if (solutionText == currentGuess) return true;

            return false;
        }

        private static bool IsValidGuess(string guessText)
        {
            if (string.IsNullOrEmpty(guessText)) return false;
            if (guessText.Length != solutionLength) return false;

            return true;
        }

        private static string GetUserInput()
        {
            Console.Write("GUESS:");
            return Console.ReadLine();
        }

        private static void RandomizeSolution()
        {
            for (int i = 0; i < solutionLength; i++)
            {
                solution[i] = RandomNumber(minVal, maxVal).ToString();
            }
        }

        private static void PrintSolution()
        {
            Console.WriteLine(string.Format("The Solution Is {0}", String.Join("", solution.ToArray())));
        }

        private static void PrintWinningSolution()
        {
            Console.WriteLine("You Win!!!");
            Console.WriteLine(string.Format("The Solution Was {0}", String.Join("", solution.ToArray())));
        }

        private static void PrintLosingSolution()
        {
            Console.WriteLine("Sorry, You ran out of attempts. You did not win.");
            Console.WriteLine(string.Format("The Solution Was {0}", String.Join("", solution.ToArray())));
        }
        public static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        private static void PrintInstructions()
        {
            string sIntructions =
                string.Format(
                    "---Mastermind---" + Environment.NewLine +
                    "The randomly generated answer will be four({0}) digits in length." + Environment.NewLine +
                    "Each digit is between the numbers {1} and {2}." + Environment.NewLine +
                    "After the player enters a combination," + Environment.NewLine +
                    "   *A minus(-) sign will be printed for every digit that is correct but in the wrong position," + Environment.NewLine +
                    "   *A plus(+) sign will be printed for every digit that is both correct and in the correct position." + Environment.NewLine +
                    "   *Nothing will be printed for incorrect digits." + Environment.NewLine +
                    "The player has ten ({3}) attempts to guess the number correctly!" + Environment.NewLine + Environment.NewLine,
                    solutionLength, minVal, maxVal, maxAttempts);

            Console.WriteLine(sIntructions);
        }
    }
}
