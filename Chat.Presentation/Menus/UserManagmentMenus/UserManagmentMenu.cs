using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;

namespace Chat.Menus.UserManagmentMenus;

public class UserManagmentMenu : Menu
{
    public static void Create(User user)
    {
        bool continueLoop = true;
        while (continueLoop)
        {
            Dictionary<string, Action?> optionList = new Dictionary<string, Action?>();
            var users = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig()).GetAll();
            foreach (var u in users)
            {
                if (u.UserId == user.UserId)
                {
                    users.Remove(user);
                }
                else
                {
                    optionList.Add(u.Email, () => UserManagmentOptionsMenu.Create(u));
                }
            }
            optionList.Add("Povratak", () => { continueLoop = false;});
            DisplayMenus(optionList);
        }
    }
}