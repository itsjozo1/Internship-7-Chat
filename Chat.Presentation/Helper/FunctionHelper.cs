using System.Text;

namespace Chat.Helper;

public interface IFunctionHelper
{
    public static bool Confirm()
    {
        var choice = Console.ReadLine().ToLower().Trim();
        while (!choice.Equals("da") && !choice.Equals("ne"))
        {
            Console.WriteLine("Unesite da ili ne: ");
            choice = Console.ReadLine().ToLower().Trim();
        }

        if (choice.Equals("da"))
        {
            return true;
        }

        return false;
    }
    public static string GetMaskedPassword()
    {
        StringBuilder password = new StringBuilder();
        ConsoleKeyInfo keyInfo;
        do
        {
            keyInfo = Console.ReadKey(true);

            if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter)
            {
                password.Append(keyInfo.KeyChar);
                Console.Write("*");
            }
            else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password.Remove(password.Length - 1, 1);
                Console.Write("\b \b");
            }
        } while (keyInfo.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password.ToString();
    }
}