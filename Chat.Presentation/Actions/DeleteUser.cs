using Chat.Data.Entities;
using Chat.Domain.Repositories;
using Chat.Helper;
using System.Linq;
using Chat.Domain.Factories;
using Chat.Menus;

namespace Chat.Actions
{
    public class DeleteUser
    {
        public static void Create(User user)
        {
            UserRepository userRepository = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig());
            GroupMessagesrepository groupMessagesrepository = RepositoryFactory.Create<GroupMessagesrepository>(ConfigHelper.GetConfig());
            PrivateMessageRepository privateMessageRepository = RepositoryFactory.Create<PrivateMessageRepository>(ConfigHelper.GetConfig());
            GroupUserRepository groupUserRepository = RepositoryFactory.Create<GroupUserRepository>(ConfigHelper.GetConfig());

            var userName = userRepository.TrimUsernameBeforeAtSign(user.UserId);
            Console.Clear();
            Console.WriteLine($"Želite li izbrisati korisnika {userName}? (da/ne)");
            if (Menu.Confirm())
            {
                groupMessagesrepository.DeleteGroupMessagesByUserId(user.UserId);
                privateMessageRepository.DeletePrivateMessagesByUserId(user.UserId);
                groupUserRepository.DeleteByUserId(user.UserId);
                userRepository.Delete(user.UserId);
                Console.Clear();
                Console.WriteLine($"Uspiješno izbrisan korisnik {user.TrimUserName()}, za nastavak pritisnite enter: ");
                Console.ReadKey();
            }
            
        }
    }
}
