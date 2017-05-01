namespace IpCamLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AverageMotions = c.Double(nullable: false),
                        TotalCount = c.Int(nullable: false),
                        TimeStart = c.DateTime(nullable: false),
                        TimeFinish = c.DateTime(nullable: false),
                        DataSource = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
