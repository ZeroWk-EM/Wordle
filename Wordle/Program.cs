using Wordle_Library;

namespace Wordle
{
    internal class Program
    {
        // DEBUG FUNCTION
        public static void PrintList(List<string> listToPrint)
        {
            foreach (string word in listToPrint)
            {
                Console.WriteLine(word);
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
                Console.WriteLine(winnerWord);
                Console.WriteLine($"Word have [{game.WordLength}] letter");
                while (turn > 0)
                {
                    Console.Write("Insert phrase: ");
                    string? toSend = Console.ReadLine();
                    if (toSend != null)
                    {
                        // SANIFICATE INPUT
                        toSend = toSend.Trim().ToLower().Replace(" ", "");
                        if (toSend.Length == game.WordLength)
                        {
                            game.InsertWord(toSend);
                            turn--;
                            Console.Clear();
                            PrintList(game.GameBoard);
                            // IF CHECK EXIST BUT NOT POSITION
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