using Chat.Actions;
using Chat.Data.Entities;

namespace Chat.Menus;

public class GroupMenu : Menu
{
    public static void Create(User user)
    {
        bool continueLoop = true;
        while (continueLoop)
        {
            Dictionary<string, Action?> groupOptionsList = new Dictionary<string, Action?>()
            {
                { "Kreiranje novog kanala", () => AddGroup.Create() },
                { "Ulazak u kanal", () => EnterGroup.Create()},
                { "Ispis mojih kanala", () => EnteredGroupsMenu.Create(user)},
                { "Povratak", () => { continueLoop = false;}}
            };
            DisplayMenus(groupOptionsList);
        }
    }
}