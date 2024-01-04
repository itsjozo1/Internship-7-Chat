using Chat.Actions;
using Chat.Data.Entities;

namespace Chat.Menus;

public class PrivateMessagesMenu : Menu
{
    public static void Create(User user)
    {
        bool continueLoop = true;
        while (continueLoop)
        {
            Dictionary<string, Action?> optionsList = new Dictionary<string, Action?>()
            {
                { "Nova poruka", () => NewPrivateMessageMenu.Create(user) },
                { "Moji razgovori", () => EnteredPrivateMessagesMenu.Create(user)},
                { "Povratak", () => { continueLoop = false;}}
            };
            DisplayMenus(optionsList);
        }
    }
}