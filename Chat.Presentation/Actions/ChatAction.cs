using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;

namespace Chat.Actions;

public class ChatAction
{
    public static void Create(User user, Group group)
    {
        List<GroupMessage> groupMessages = RepositoryFactory.Create<GroupMessagesrepository>(
            ConfigHelper.GetConfig()).GetAllSortedBySentTime();

        foreach (var gm in groupMessages)
        {
            var userName = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig()
            ).TrimUsernameBeforeAtSign(gm.UserId);
            Console.WriteLine(userName);
            Console.WriteLine(gm.Content + "\n");
        }

        Console.ReadLine();
    }
}