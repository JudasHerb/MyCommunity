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
            context.Communities.AddOrUpdate(c => c.Name, new Community { Name = "Patcham" });

            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection",
                                                         "UserProfile",
                                                         "UserId",
                                                         "UserName",
                                                         autoCreateTables: true);

                var community = context.CommunitiesRepository.FindBy(c => c.Name == "Patcham").First();

                if (!WebSecurity.UserExists("JudasHerb"))
                {
                    WebSecurity.CreateUserAndAccount("JudasHerb", "B0110cks!", new {Community=community});
                }
                else
                {
                    var judas = context.UsersRepository.FindBy(m => m.UserName == "JudasHerb").First();
                    if (judas.Community == null)
                    {
                        judas.Community = community;
                        context.UsersRepository.Update(judas);
                    }
                }
            }

        }
 
    }
}
