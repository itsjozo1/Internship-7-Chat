using System;
using System.Collections.Generic;
using Chat;
using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Microsoft.Extensions.Configuration;


List<string> menuOptions = new List<string> { "Registracija", "Prijava", "Izlaz" };
var menu = new Menu(menuOptions);
int selectedOption = menu.DisplayMenus();
if (selectedOption == 1)
{
    Console.WriteLine("Registracija");
    var config = new ConfigurationBuilder()
        .AddJsonFile("/Users/rokoperisic/Internship-7-Chat/Chat.Data/appsettings.json") 
        .Build();
 
    var userRepository = RepositoryFactory.Create<UserRepository>(config);
    
    foreach (var VARIABLE in userRepository.GetAll())
    {
        Console.WriteLine(VARIABLE.UserId + VARIABLE.Email);
    }
}
else if (selectedOption == 2)
{ 
    Console.WriteLine("Prijava");
}

