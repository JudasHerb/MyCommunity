using MyCommunity.DataAccess;
using MyCommunity.Models;
using WebMatrix.WebData;

namespace MyCommunity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyCommunity.DataAccess.UnitOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MyCommunity.DataAccess.UnitOfWork context)
        {
            

            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                                                         "UserProfile",
                                                         "UserId",
                                                         "UserName",
                                                         autoCreateTables: true);

                if (!WebSecurity.UserExists("JudasHerb"))
                {
                    WebSecurity.CreateUserAndAccount("JudasHerb", "B0110cks!");
                }
            }
            var judas = context.UsersRepository.FindBy(m => m.UserName == "JudasHerb").First();
            var commnunity = new Community {Name = "Patcham", Administrator = judas};
            commnunity.Members.Add(judas);
            judas.Community = commnunity;
            context.Communities.AddOrUpdate(c => c.Name, commnunity);
            
            

        }
 
    }
}
