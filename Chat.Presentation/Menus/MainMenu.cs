namespace Chat.Menus;

public class MainMenu : Menu
{
    public static void Create()
    {
        while (true)
        {
            if (AutentificationMenu.Create() is null) return;
            
            Dictionary<string, Action?> optionsList = new Dictionary<string, Action?>()
            {
                { "Grupni kanali", () => MainMenu.Create() },
                { "Privatne poruke", () => MainMenu.Create() },
                { "Postavke profila", () => MainMenu.Create() },
                { "Odjava", null },
            };

            new MainMenu().DisplayMenus(optionsList);
        }
    }
    
}