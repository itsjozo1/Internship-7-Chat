
using Chat.Data.Entities;
using Chat.Domain;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;

namespace Chat.Actions;

public class ChatAction
{
    public static void Create(User user, Group group)
    {
        var groupMessagesRepo = RepositoryFactory.Create<GroupMessagesrepository>(ConfigHelper.GetConfig());
        var messages = groupMessagesRepo.GetAllSortedBySentTime(group.GroupId);
        string? newMessage;

        do
        {
            Console.Clear();
            PrintChat(user, messages);
            newMessage = Console.ReadLine();
            if (newMessage == "")
            {
                newMessage = null;
            }
            if (newMessage != "/exit" && newMessage != null)
            {
                var addMessageResult = groupMessagesRepo.Add(new GroupMessage(group.GroupId, user.UserId, newMessage));

                if (addMessageResult != ResponseResultType.Success)
                {
                    Console.Clear();
                    Console.WriteLine("Pogreška: " + addMessageResult + ". Za nastavak pritisnite enter: ");
                    Console.ReadKey();
                }
                else
                {
                    messages = groupMessagesRepo.GetAllSortedBySentTime(group.GroupId);
                }
            }
        } while (newMessage != "/exit");
    }

    static void PrintChat(User user, List<GroupMessage> messages)
    {

        foreach (var gm in messages)
        {
            var userName = user.TrimUserName();
            var time = gm.SentTime.ToString("dd/MM/yyyy hh:mm:ss");

            if (user.UserId == gm.UserId)
            {
                Console.WriteLine($"{string.Empty, -80}{userName} {time}");
                Console.WriteLine($"{string.Empty, -80}{gm.Content}\n");
            }
            else
            {
                Console.WriteLine($"{userName} {time}");
                Console.WriteLine($"{gm.Content}\n");
            }
        }
    }
}