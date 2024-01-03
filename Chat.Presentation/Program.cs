using System;
using System.Collections.Generic;
using Chat;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;
using Chat.Menus;
using Microsoft.Extensions.Configuration;

UserRepository users = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig());

AutentificationMenu.Create();


