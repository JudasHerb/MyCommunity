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
                                                         "Email",
                                                         autoCreateTables: true);

                if (!WebSecurity.UserExists("david.garratt.little@gmail.com"))
                {
                    WebSecurity.CreateUserAndAccount("david.garratt.little@gmail.com", "B0110cks!", new {FirstName="David", LastName="Little", Address="14 Craignair Avenue"},false);

                    var judas = context.UsersRepository.FindBy(m => m.Email == "david.garratt.little@gmail.com").First();
                    var commnunity = new Community { Name = "Patcham", Administrator = judas };
                    commnunity.Members.Add(judas);
                    judas.Community = commnunity;
                    context.Communities.AddOrUpdate(c => c.Name, commnunity);
                }


            }
            
            
            

        }
 
    }
}
