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
    public class EnterGroupMenu : Menu
    {
        public static void Create(User user)
        {
            Dictionary<string, Action> enterChat = new Dictionary<string, Action>();
            List<Group> allGroups = RepositoryFactory.Create<GroupRepository>(ConfigHelper.GetConfig()).GetAll();
            List<GroupUser> groupUsers = RepositoryFactory.Create<GroupUserRepository>(ConfigHelper.GetConfig())
                .GetAllGroupUsersByUserId(user.UserId);

            var groupsUserBelongsTo = groupUsers.Select(gu => gu.GroupId).ToList();

            var groupsToDisplay = allGroups.Where(g => !groupsUserBelongsTo.Contains(g.GroupId));

            foreach (var g in groupsToDisplay)
            {
                enterChat.Add(g.Name, () => EnterGroup.Create(user, g));
            }
            enterChat.Add("Povratak", () => {});

            DisplayMenus(enterChat);
        }
    }
}