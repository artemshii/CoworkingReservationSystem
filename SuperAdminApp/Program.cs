using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace ConsoleAppForCoworking
{

    public class AuthUserData
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; } = null!;


    }

    public class AppDbContext : DbContext
    {
        public DbSet<AuthUserData> AuthUsersData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("ENTER Connection String here");
            
        }
    }

    public class inputUserData
    {
        
        public string UserName { get; set; }
        
        public string Password { get; set; }

        
    }

    

    public static class Coworking
    {
        static void Main()
        {
            AppDbContext _context = new AppDbContext();
            while (true)
            {
                Console.WriteLine("1 - Add new Admin");
                Console.WriteLine("2 - Delete existing Admin");
                Console.WriteLine("3 - Show all admins");
                int input;
                bool readLineSuccess;


                do
                {
                    readLineSuccess = int.TryParse(Console.ReadLine(), out input);
                } while (readLineSuccess != true && (input != 1 || input != 2 || input != 3));

                if (input == 1)
                {
                    string? userName = null;
                    string? password = null;
                    while (userName == null || password == null)
                    {
                        Console.WriteLine("Enter UserName");
                        userName = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        password = Console.ReadLine();


                    }

                    var user = new AuthUserData()
                    {
                        UserName = userName,
                        PasswordHash = ""
                    };

                    var passHash = new PasswordHasher<AuthUserData>().HashPassword(user, password);

                    user.PasswordHash = passHash;

                    _context.AuthUsersData.Add(user);
                    _context.SaveChanges();



                }
                else if (input == 2)
                {
                    Console.WriteLine("Enter Username");

                    string? userName = null;
                    while (userName == null || userName == "" || userName == " ")
                    {
                        userName = Console.ReadLine();
                    }
                    _context.AuthUsersData.Where(q => q.UserName == userName).ExecuteDelete();
                    _context.SaveChanges();



                }
                else if (input == 3)
                {
                    var rows = _context.AuthUsersData.ToList();
                    foreach (var row in rows)
                    {
                        Console.WriteLine($"ID - {row.Id}, Username - {row.UserName}");
                    }

                }

                



            }
        }
    }
}