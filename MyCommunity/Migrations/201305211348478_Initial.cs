namespace MyCommunity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("UserProfile","Description");
        }
        
        public override void Down()
        {
        }
    }
}
