using Chat.Actions;
using Chat.Data.Entities;

namespace Chat.Menus.GroupMenus;

public class GroupMenu : Menu
{
    public static void Create(User user)
    {
        bool continueLoop = true;
        while (continueLoop)
        {
            Dictionary<string, Action?> groupOptionsList = new Dictionary<string, Action?>()
            {
                { "Kreiranje novog kanala", () => AddGroup.Create(user) },
                { "Ulazak u kanal", () => EnterGroupMenu.Create(user)},
                { "Ispis mojih kanala", () => EnteredGroupsMenu.Create(user)},
                { "Povratak", () => { continueLoop = false;}}
            };
            DisplayMenus(groupOptionsList);
        }
    }
}