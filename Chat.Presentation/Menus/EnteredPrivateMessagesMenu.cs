using Chat.Actions;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;

namespace Chat.Menus;

public class EnteredPrivateMessagesMenu : Menu
{
    public static void Create(User user)
    {
        Dictionary<string, Action> enterChat = new Dictionary<string, Action>();
        var chatUsers = RepositoryFactory.Create<PrivateMessageRepository>(ConfigHelper.GetConfig())
            .GetAllUsersPrivateChats(user.UserId);

        foreach (var u in chatUsers)
        {
            var userName = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig())
                .TrimUsernameBeforeAtSign(u.UserId);
            enterChat.Add(userName, () => PrivateChatAction.Create(user, u));
        }
        enterChat.Add("Povratak", () => {});
        DisplayMenus(enterChat);
    }
}