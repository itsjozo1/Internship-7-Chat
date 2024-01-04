using System.Text;
using System.Text.RegularExpressions;
using Chat.Actions;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;

namespace Chat.Menus;

public class AutentificationMenu : Menu
{
    public  static void Create()
    
    {
        
        var user1 = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig()).GetById(1);
        MainMenu.Create(user1); 
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

        }
    }

}