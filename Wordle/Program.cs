using Wordle_Library;

namespace Tic_Tac_Toe;

internal class Program
{

    public static void PrintList(List<string> listToPrint)
    {
        foreach (string word in listToPrint)
        {
            Console.WriteLine(word);
        }
    }

    static public void Main()
    {
        const string FilePath = @"C:\Users\EmanueleMoncada\Desktop\fakedb\wordTable.txt";
        try
        {
            List<string> wordlist = File.ReadAllLines(FilePath).ToList();
            Logic game = new(wordlist);
            PrintList(wordlist);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found! IDIOT");
        }
        catch (Exception e)
        {
            //Catch other Exception
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Finish");
        }

    }
}