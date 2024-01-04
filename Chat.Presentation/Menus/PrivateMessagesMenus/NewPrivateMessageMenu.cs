using Chat.Actions;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;

namespace Chat.Menus.PrivateMessagesMenus;

public class NewPrivateMessageMenu : Menu
{
    public static void Create(User user)
    {
        Dictionary<string, Action> enterChat = new Dictionary<string, Action>();
            
        var existingChatUsers = RepositoryFactory.Create<PrivateMessageRepository>(ConfigHelper.GetConfig())
            .GetAllUsersPrivateChats(user.UserId);

        var allUsers = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig()).GetAll();
        
        var usersWithoutChat = new List<User>();
        
        foreach (var u in allUsers)
        {
            var userExistsInChat = existingChatUsers.Exists(ch => ch.UserId == u.UserId);
            if (!userExistsInChat && u.UserId != user.UserId)
            {
                usersWithoutChat.Add(u);
            }
        }
        
        foreach (var u in usersWithoutChat)
        {
            var userName = u.TrimUserName();
            enterChat.Add(userName, () => PrivateChatAction.Create(user, u));
        }
        enterChat.Add("Povratak", () => {});
        DisplayMenus(enterChat);

    }
}