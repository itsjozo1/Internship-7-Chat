using Chat.Actions;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;

namespace Chat.Menus
{
    public class EnteredGroupsMenu : Menu
    {
        public static void Create(User user)
        {
            var continueLoop = true;
            Dictionary<string, Action> enterChat = new Dictionary<string, Action>();
            List<GroupUser> groupUsers = RepositoryFactory.Create<GroupUserRepository>(ConfigHelper.GetConfig())
                .GetAllGroupUsersByUserId(user.UserId);

            List<Group?> enteredGroups = new List<Group?>(); 

            foreach (var gu in groupUsers)
            {
                Group? group = RepositoryFactory.Create<GroupRepository>(ConfigHelper.GetConfig())
                    .GetById(gu.GroupId);

                enteredGroups.Add(group);
            }

            foreach (var group in enteredGroups)
            {
                if (group != null)
                {
                    enterChat.Add(group.Name, () => ChatAction.Create());
                }
            }
            if (enteredGroups.Capacity == 0)
            {
                Console.Clear();
                Console.WriteLine("Trenutačno niste u niti jednoj grupi, za nastavak pritisnite enter: ");
                Console.ReadLine();
                return;
            }
            enterChat.Add("Povratak", () => { continueLoop = false;});
            while (continueLoop)
            {
                DisplayMenus(enterChat);
            }

        }
    }
}