namespace IpCamLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCameraTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cameras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Source = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Logs", "Camera_Id", c => c.Int());
            CreateIndex("dbo.Logs", "Camera_Id");
            AddForeignKey("dbo.Logs", "Camera_Id", "dbo.Cameras", "Id");
            DropColumn("dbo.Logs", "DataSource");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Logs", "DataSource", c => c.String());
            DropForeignKey("dbo.Logs", "Camera_Id", "dbo.Cameras");
            DropIndex("dbo.Logs", new[] { "Camera_Id" });
            DropColumn("dbo.Logs", "Camera_Id");
            DropTable("dbo.Cameras");
        }
    }
}
