

using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");
var config = new ConfigurationBuilder()
    .AddJsonFile("/Users/rokoperisic/Internship-7-Chat/Chat.Data/appsettings.json") 
    .Build();

var users = RepositoryFactory.Create<UserRepository>(config).GetAll();
var groups = RepositoryFactory.Create<GroupRepository>(config).GetAll();
foreach (var variable in groups)
{
    Console.WriteLine(variable.GroupId + " " + variable.Name);
}


