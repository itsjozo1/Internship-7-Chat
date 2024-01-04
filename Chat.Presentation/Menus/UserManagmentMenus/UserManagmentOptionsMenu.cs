using Chat.Actions;
using Chat.Data.Entities;

namespace Chat.Menus.UserManagmentMenus;

public class UserManagmentOptionsMenu : Menu
{
    public static void Create(User user)
    {
        Dictionary<string, Action?> optionsList = new Dictionary<string, Action?>()
        {
            { "Izmjena emaila", () => ChangeEmail.Create(user) },
            { "Promijeni role", () => ChangeRole.Create(user) },
            { "Izbriši korisnika", () => DeleteUser.Create(user) },
            { "Povratak", () => {}}
        };
        
        DisplayMenus(optionsList);
    }
}