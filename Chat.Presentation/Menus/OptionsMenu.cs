using Chat.Actions;
using Chat.Data.Entities;

namespace Chat.Menus;

public class OptionsMenu : Menu
{
    public static void Create(User user)
    {
        bool continueLoop = true;
        while (continueLoop)
        {
            Dictionary<string, Action?> optionsList = new Dictionary<string, Action?>()
            {
                { "Promijeni email", () => ChangeEmail.Create(user) },
                { "Promijeni lozinku", () => ChangePassword.Create(user) },
                { "povratak", () => { continueLoop = false; }}
            };
            DisplayMenus(optionsList);
        }
    }
}