using System.Data.Entity.Migrations;
using MyCommunity.DataAccess;
using MyCommunity.Models;
using WebMatrix.WebData;

namespace MyCommunity.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DB context)
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                                                         "UserProfile",
                                                         "UserId",
                                                         "Email",
                                                         autoCreateTables: true);

                if (!WebSecurity.UserExists("david.garratt.little@gmail.com"))
                {
                    var commnunity = new Community {Name = "Patcham"};
                    context.Communities.AddOrUpdate(c => c.Name, commnunity);
                    context.SaveChanges();
                    WebSecurity.CreateUserAndAccount("david.garratt.little@gmail.com", "B0110cks!",
                                                     new
                                                         {
                                                             FirstName = "David",
                                                             LastName = "Little",
                                                             Address = "14 Craignair Avenue",
                                                             CommunityID = commnunity.CommunityId,
                                                             IsCommunityAdmin = true
                                                         }, false);
                }
            }
        }
    }
}