using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Menus;

public class Menu
{
    protected static void DisplayMenus(Dictionary<string, Action> optionsList)
    {
        ConsoleKeyInfo key;
        int selectedOption = 0;

        do
        {
            Console.Clear();
            Console.WriteLine("Odaberite opciju: ");

            int optionIndex = 0;
            foreach (var option in optionsList.Keys)
            {
                if (optionIndex == selectedOption)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.Write(">");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(option);
                Console.ResetColor();
                optionIndex++;
            }

            key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = (selectedOption == 0) ? optionsList.Count - 1 : selectedOption - 1;
                    break;
                case ConsoleKey.DownArrow:
                    selectedOption = (selectedOption == optionsList.Count - 1) ? 0 : selectedOption + 1;
                    break;
                case ConsoleKey.Enter:
                    string selectedAction = optionsList.Keys.ToArray()[selectedOption];
                    optionsList[selectedAction]?.Invoke();
                    return;
            }
        } while (key.Key != ConsoleKey.Enter);
            
    }
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
}