using Wordle_Library;

namespace Wordle
{
    internal class Program
    {

        public static void PrintList(List<string> listToPrint)
        {
            foreach (string word in listToPrint)
            {
                Console.WriteLine(word);
            }
        }

        public static void PrintGameGrid(char[,] matrix, int wordLength)
        {
            Console.WriteLine("\x1b[1m\u001b[1;32mWO\u001b[1;31mRD\u001b[1;33mLE\u001b[1;0m\n");
            for (int i = 0; i < Logic.MaxTurn; i++)
            {
                for (int j = 0; j < wordLength; j++)
                {

                    if (matrix[i, j] != Logic.InitilizedMatrixSymbol && matrix[i, j] == Char.ToUpper(matrix[i, j]))
                    {
                        Console.Write($"[\x1b[1;32m{matrix[i, j]}\x1b[1;0m]");
                    }
                    else
                    {
                        Console.Write($"[{matrix[i, j]}]");
                    }
                }
                Console.WriteLine();
            }
        }

        static public void Main()
        {
            Console.Title = "Wordle Game";
            const string FilePath = @"C:\Users\EmanueleMoncada\Desktop\fakedb\wordTable.txt";
            int turn = Logic.MaxTurn;
            string winnerWord = "";
            bool winning = false;
            try
            {
                List<string> wordlist = File.ReadAllLines(FilePath).ToList();
                if (wordlist.Count == 0)
                {
                    Console.WriteLine("\u001b[1;31mError - The Collection is empty\x1b[1;0m");
                    Environment.Exit(1);
                }
                Logic game = new(wordlist);
                winnerWord = game.ChooseRandomWord();
                char[,] matrix = game.CreateGameMatrix();
                int attempt = 0;
                //Console.WriteLine($"DEBUG - CORRECT WORD [{winnerWord}]");
                Console.WriteLine($"\nWord have [{game.WordLength}] letter");
                while (turn > 0)
                {
                    Console.Write("Insert phrase: ");
                    string? toSend = Console.ReadLine();
                    if (toSend != null)
                    {
                        toSend = toSend.Trim().ToLower().Replace(" ", "");
                    }

                    if (toSend != null && toSend.Length == game.WordLength)
                    {
                        game.InserIntoMatrix(attempt, toSend, matrix);
                        turn--;
                        attempt++;
                        Console.Clear();
                        PrintGameGrid(matrix, winnerWord.Length);
                        // TODO: FIX THE BUG
                        if (game.ExistValue.Count > 0)
                        {
                            Console.Write("\nValue ");
                            foreach (char sentence in game.ExistValue)
                            {
                                Console.Write($"[\u001b[1;33m{sentence.ToString().ToUpper()}\u001b[1;0m]");

                            }
                            Console.WriteLine(" exist but the position is wrong");
                        }
                        if (game.IsWinner(toSend))
                        {
                            turn = 0;
                            winning = true;
                        }
                        else if (!game.IsWinner(toSend) && turn > 0)
                        {
                            Console.WriteLine($"\nWord have [{game.WordLength}] letter");
                        }
                    }
                    else
                    {
                        if (toSend != null)
                        {
                            Console.Write($"\u001b[1;31mLength Error - The word entered is too ");
                            Console.WriteLine(toSend.Length > winnerWord.Length ? "Long\u001b[1;0m" : "Short\u001b[1;0m");
                        }

                    }
                };
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found! IDIOT");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(winning ? "\n\u001b[1;32mYou Win\u001b[1;0m" : $"\n\u001b[1;31mI'm sorry but you lose!\nCorrect word is \u001b[1;0m\u001b[1;96m{winnerWord}\u001b[1;0m");
        }
    }
}