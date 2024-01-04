using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;
using Chat.Domain;

namespace Chat.Actions
{
    public class PrivateChatAction
    {
        public static void Create(User user, User chatUser)
        {
            var privateMessageRepository = RepositoryFactory.Create<PrivateMessageRepository>(ConfigHelper.GetConfig());
            var chatMessages = privateMessageRepository.GetAllChatMessages(user.UserId, chatUser.UserId);
            string? newMessage;
            do
            {
                Console.Clear();
                PrintChat(user, chatMessages);
                newMessage = Console.ReadLine();
                if (newMessage == "")
                {
                    newMessage = null;
                }
                if (newMessage != "/exit" && newMessage != null)
                {
                    var addMessageResult = privateMessageRepository.Add(new PrivateMessage(user.UserId, chatUser.UserId, newMessage));

                    if (addMessageResult != ResponseResultType.Success)
                    {
                        Console.Clear();
                        Console.WriteLine("Pogreška: " + addMessageResult + ". Za nastavak pritisnite enter: ");
                        Console.ReadKey();
                    }
                    else
                    {
                        chatMessages = privateMessageRepository.GetAllChatMessages(user.UserId, chatUser.UserId);
                    }
                }
            } while (newMessage != "/exit");
        }

        private static void PrintChat(User user, List<PrivateMessage> messages)
        {
            var userRepository = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig());

            foreach (var pm in messages)
            {
                var sender = user.TrimUserName();
                var time = pm.SentTime.ToString("dd/MM/yyyy hh:mm:ss");
                if (user.UserId == pm.SentUserId)
                {
                    Console.WriteLine($"{string.Empty, -80}{sender} {time}");
                    Console.WriteLine($"{string.Empty, -80}{pm.Content}\n");
                }
                else
                {
                    Console.WriteLine($"{sender} {time}");
                    Console.WriteLine($"{pm.Content}\n");
                }
            }
        }
    }
}