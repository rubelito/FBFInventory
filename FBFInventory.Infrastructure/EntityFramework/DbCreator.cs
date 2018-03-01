using System.Linq;
using FBFInventory.Domain.Entity;

namespace FBFInventory.Infrastructure.EntityFramework
{
    public class DbCreator
    {
        public void Create(string ipAddress)
        {
            var connection = new EfSqlServer("SQLServerDb", ipAddress);
            using (var ams = new FBFDBContext(connection))
            {
                if (ams.Database.Exists())
                {
                    ams.Database.Delete();
                }
                ams.Database.Create();
                ams.Roles.Add(new Role(){Name = "Administrator"});
                ams.Roles.Add(new Role() { Name = "Member" });
                ams.Roles.Add(new Role() { Name = "Spectator" });

                ams.SaveChanges();

                Role adminRole = ams.Roles.FirstOrDefault(r => r.Name == "Administrator");
                User adminUser = new User();
                adminUser.UserName = "admin";
                adminUser.Password = "admin";
                adminUser.IsActive = true;

                adminUser.Role = adminRole;
                adminRole.Users.Add(adminUser);

                ams.SaveChanges();
            }
        } 
    }
}