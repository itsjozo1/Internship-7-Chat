using System.Text;
using System.Text.RegularExpressions;
using Chat.Actions;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;

namespace Chat.Menus;

public class AutentificationMenu : Menu
{
    public  static void Create()
    {
        bool continueLoop = true;
        while (continueLoop)
        {
            User? user = null;
            Dictionary<string, Action?> optionsList = new Dictionary<string, Action?>()
            {
                {"Registracija", () => user = Registration.Create()},
                {"Prijava", () => user = Login.Create()},
                {"Izlaz", () => { continueLoop = false;}},
            };
            DisplayMenus(optionsList);
            if (continueLoop == false) return;
            MainMenu.Create(user); 
        }
    }

}