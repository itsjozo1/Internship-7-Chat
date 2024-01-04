using Chat.Data.Entities;
using Chat.Domain.Factories;
using Chat.Domain.Repositories;
using Chat.Helper;
using Chat.Menus;
using System;
using Chat.Domain;

namespace Chat.Actions
{
    public class ChangeRole
    {
        public static void Create(User user)
        {
            var userRepository = RepositoryFactory.Create<UserRepository>(ConfigHelper.GetConfig());
            var userRole = user.IsAdmin;
            var userName = user.TrimUserName();

            Console.Clear();
            Console.WriteLine($"Role korisnika {userName} je {(userRole ? "Admin" : "običan korisnik")}\n" +
                              $"Želite li promijeniti u {(userRole ? "običan korisnik" : "Admin")} (da/ne):");

            if (Menu.Confirm())
            {
                var newRole = !userRole;
                var updatedUser = new User(user.Email, user.Password, newRole);
                
                var updateRole = userRepository.Update(updatedUser, user.UserId);
                
                if (updateRole == ResponseResultType.Success)
                {
                    Console.WriteLine($"Uspješno promijenjen role u {(newRole ? "Admin" : "običan korisnik")}");
                }
                else
                {
                    Console.WriteLine($"Pogreška: {updateRole}, za povratak pritisnite enter: ");
                    Console.ReadKey();
                }

                Console.ReadKey();
            }
        }
    }
}