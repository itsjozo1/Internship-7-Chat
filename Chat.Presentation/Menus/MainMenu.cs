using Chat.Actions;
using Chat.Data.Entities;

namespace Chat.Menus;

public class MainMenu : Menu
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
                { "Postavke profila", () => GroupMenu.Create(user) }
            };
            
            if (user.IsAdmin)
            {   
                optionsList.Add("User managment", null);
            }
            optionsList.Add("Odjava", () => { continueLoop = false;});
            DisplayMenus(optionsList);
        }
    }
    
}