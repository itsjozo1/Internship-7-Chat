
using Chat.Actions;
using Chat.Data.Entities;

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