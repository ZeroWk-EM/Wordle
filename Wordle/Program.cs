using NLog;
using System.Configuration;
using Wordle_Library;

namespace Wordle
{
    internal class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void PrintGameBoard(List<string> listToPrint)
        {
            Console.WriteLine("\x1b[1m\u001b[1;32mWO\u001b[1;31mRD\u001b[1;33mLE\u001b[1;0m\n");

            foreach (string word in listToPrint)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == Char.ToUpper(word[i]))
                    {
                        Console.Write($"[\x1b[1;32m{word[i]}\x1b[1;0m]");
                    }
                    else
                    {
                        Console.Write($"[{word[i]}]");
                    }

                }
                Console.WriteLine();
            }
        }
        static public void Main()
        {
            Console.Title = "Wordle Game";
            string? path = ConfigurationManager.AppSettings["FilePath"];
            int turn = Logic.MaxTurn;
            string winnerWord;
            bool winning = false;
            if (path != null)
            {

                try
                {
                    List<string> wordlist = File.ReadAllLines(path).ToList();

                    if (wordlist.Count == 0)
                    {
                        Console.WriteLine("\u001b[1;31mCould not start the game because of an unexpected error, please contact the technical support.\x1b[1;0m");
                        logger.Error("LOG - The collection is empty");
                        Environment.Exit(1);
                    }
                    Logic game = new(wordlist);
                    winnerWord = game.ChooseRandomWord();
                    Console.WriteLine(winnerWord);
                    Console.WriteLine($"Word have [{game.WordLength}] letter");
                    while (turn > 0)
                    {
                        Console.Write("Insert phrase: ");
                        string? toSend = Console.ReadLine();
                        if (toSend != null)
                        {
                            toSend = toSend.Trim().ToLower().Replace(" ", "");
                            if (toSend.Length == game.WordLength)
                            {
                                game.InsertWord(toSend);
                                turn--;
                                Console.Clear();
                                PrintGameBoard(game.GameBoard);
                                if (game.ExistValue.Count > 0)
                                {
                                    Console.Write("Value ");
                                    foreach (char sentence in game.ExistValue)
                                    {
                                        Console.Write($"[\u001b[1;33m{sentence.ToString().ToUpper()}\u001b[1;0m]");

                                    }
                                    Console.WriteLine(" exist but the position is wrong");
                                }
                            }
                            else
                            {
                                Console.Write($"\u001b[1;31mLength Error - The word entered is too ");
                                Console.WriteLine(toSend.Length > winnerWord.Length ? "Long\u001b[1;0m" : "Short\u001b[1;0m");
                            }
                            if (game.IsWinner(toSend))
                            {
                                turn = 0;
                                winning = true;
                            }
                        }
                    };
                    Console.WriteLine(winning ? "\n\u001b[1;32mYou Win\u001b[1;0m" : $"\n\u001b[1;31mI'm sorry but you lose!\nCorrect word is \u001b[1;0m\u001b[1;96m{winnerWord}\u001b[1;0m");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    logger.Error($"LOG - {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Specify path in app.setting");
                logger.Error($"LOG - Path in app.setting is null");
            }
        }
    }
}