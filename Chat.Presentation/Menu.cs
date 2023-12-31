using System;
using System.Collections.Generic;

namespace Chat
{
    public class Menu
    {
        public Menu(List<string> optionsList)
        {
            OptionsList = optionsList;
        }

        public List<string> OptionsList { get; set; }

        public int DisplayMenus()
        {
            ConsoleKeyInfo key;
            int selectedOption = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Select an option:");

                for (int i = 0; i < OptionsList.Count; i++)
                {
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        Console.Write(">");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    Console.WriteLine(OptionsList[i]);
                    Console.ResetColor();
                }

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.W:
                        selectedOption = (selectedOption == 0) ? OptionsList.Count - 1 : selectedOption - 1;
                        break;
                    case ConsoleKey.S:
                        selectedOption = (selectedOption == OptionsList.Count - 1) ? 0 : selectedOption + 1;
                        break;
                    case ConsoleKey.Enter:
                        if (selectedOption != OptionsList.Count - 1)
                        {
                            return selectedOption + 1;
                        }
                        else
                        {
                            Console.WriteLine("Exiting...");
                            return selectedOption = 0;
                        }
                }
            } while (key.Key != ConsoleKey.Enter || selectedOption != OptionsList.Count - 1);

            return selectedOption;
        }
    }
}
