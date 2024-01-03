using Chat.Actions;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Menus
{
    public class EnteredGroupsMenu : Menu
    {
        public static void Create(User user)
        {
            Dictionary<string, Action> enterChat = new Dictionary<string, Action>();
            List<GroupUser> groupUsers = RepositoryFactory.Create<GroupUserRepository>(ConfigHelper.GetConfig())
                .GetAllGroupUsersByUserId(user.UserId);

            List<Group?> enteredGroups = groupUsers
                .Select(gu => RepositoryFactory.Create<GroupRepository>(ConfigHelper.GetConfig()).GetById(gu.GroupId))
                .Where(group => group != null)
                .ToList();

            foreach (var group in enteredGroups)
            {
                if (group != null)
                {
                    enterChat.Add(group.Name, () => ChatAction.Create(user, group));
                }
            }

            if (enteredGroups.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Trenutačno niste u niti jednoj grupi, za nastavak pritisnite enter: ");
                Console.ReadLine();
                return;
            }

            enterChat.Add("Povratak", () => {});

            DisplayMenus(enterChat);
        }
    }
}