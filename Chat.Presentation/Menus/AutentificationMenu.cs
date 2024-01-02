using System.Text;
using System.Text.RegularExpressions;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Processes;

namespace Chat.Menus;

public class AutentificationMenu : Menu
{
    public new static void Create()
    {
        Dictionary<string, Action> optionsList = new Dictionary<string, Action>()
        {
            {"Registracija", () => Registration.Create()},
            {"Prijava", () => Login.Create()},
            {"Izlaz", () => MainMenu.Create()},
        };
        new Menu().DisplayMenus(optionsList);
    }

}