using Chat.Data.Entities;
using Chat.Domain;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;
using Chat.Menus;

namespace Chat.Actions;

public class EnterGroup
{
    public static void Create(User user, Group group)
    {
        Console.WriteLine("Želite li ući u grupu (da/ne)" + group.Name);
        if (IFunctionHelper.Confirm())
        {
            ResponseResultType groupUserRepository = RepositoryFactory
                .Create<GroupUserRepository>(ConfigHelper.GetConfig())
                .Add(new GroupUser(group.GroupId, user.UserId));
            if (groupUserRepository == ResponseResultType.Success)
            {
                Console.Clear();
                Console.WriteLine("Uspiješno ste ušli u grupu " + group.Name);
                Console.WriteLine("Za povratak unesite enter: ");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Greška: " + groupUserRepository + "za nastavak pritisnite enter: ");
            Console.ReadKey();
        }
    }


}