using Chat.Actions;
using Chat.Data.Entities;
using Chat.Helper;
using Chat.Menus.GroupMenus;
using Chat.Menus.PrivateMessagesMenus;
using Chat.Menus.UserManagmentMenus;

namespace Chat.Menus;

public class MainMenu : Menu, IFunctionHelper
{
    public static void Create(User user)
    {
        bool continueLoop = true;
        while (continueLoop)
        {
            Dictionary<string, Action?> optionsList = new Dictionary<string, Action?>()
            {
                { "Grupni kanali", () => GroupMenu.Create(user) },
                { "Privatne poruke", () => PrivateMessagesMenu.Create(user) },
                { "Postavke profila", () => OptionsMenu.Create(user) }
            };
            
            if (user.IsAdmin)
            {   
                optionsList.Add("User managment", () => UserManagmentMenu.Create(user));
            }
            optionsList.Add("Odjava", () => { continueLoop = Logout();});
            DisplayMenus(optionsList);
        }
    }

    static bool Logout()
    {
        Console.WriteLine("Želite li se odjaviti (da/ne): ");
        if (IFunctionHelper.Confirm())
        {
            return false;
        }
        return true;
    }
}