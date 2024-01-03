using System.Text;
using System.Text.RegularExpressions;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Processes;

namespace Chat.Menus;

public class AutentificationMenu : Menu
{
    public  static User? Create()
    {
        User? user = null;
        Dictionary<string, Action?> optionsList = new Dictionary<string, Action?>()
        {
            {"Registracija", () => user = Registration.Create()},
            {"Prijava", () => user = Login.Create()},
            {"Izlaz", null},
        };
        new Menu().DisplayMenus(optionsList);
        return user;
    }

}