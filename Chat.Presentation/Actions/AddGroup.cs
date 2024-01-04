using Chat.Data.Entities;
using Chat.Domain;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;

namespace Chat.Actions;

public class AddGroup
{
    public static void Create(User user)
    {
        Console.Clear();
        Console.WriteLine("STVARANJE NOVE GRUPE: \nUnesite ime grupe: ");
        string name = "";
        while (name == "")
        {
                Console.Clear();
                Console.WriteLine("Unesite ime grupe: ");
                name = Console.ReadLine();
        }
        Console.WriteLine("Potvrdite stvaranje grupe " + name + "(da/ne):");
        if (IFunctionHelper.Confirm())
        {
            ResponseResultType addGroup = RepositoryFactory.Create<GroupRepository>(ConfigHelper
                .GetConfig()).Add(new Group(name));

            if (addGroup == ResponseResultType.Success)
            {
                Group? group = RepositoryFactory.Create<GroupRepository>(ConfigHelper
                    .GetConfig()).GetByName(name);
                
                ResponseResultType groupUser = RepositoryFactory.Create<GroupUserRepository>(ConfigHelper
                    .GetConfig()).Add(new GroupUser(group.GroupId, user.UserId));
                
                Console.WriteLine("Uspiješno ste stvorili grupu " + name);
                Console.WriteLine("Za povratak pritisnite enter: ");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Pogreška " + addGroup + "\nZa povratak pritisnite enter");
            Console.ReadKey();
        }

    }
    
}