using Chat.Actions;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Menus.GroupMenus;

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

        foreach (var group in groupsToDisplay)
        {
            int numberOfUsers = RepositoryFactory.Create<GroupUserRepository>(ConfigHelper.GetConfig())
                .CountUsersInGroup(group.GroupId);
            enterChat.Add($"{group.Name} ({numberOfUsers} korisnika)", () => EnterGroup.Create(user, group));
        }
        enterChat.Add("Povratak", () => {});

        DisplayMenus(enterChat);
    }
}