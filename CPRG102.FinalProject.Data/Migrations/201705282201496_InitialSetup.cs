namespace CPRG102.FinalProject.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagNumber = c.String(),
                        AssetTypeId = c.Int(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                        ModelId = c.Int(nullable: false),
                        Description = c.String(),
                        AssignedTo = c.String(),
                        SerialNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssetTypes", t => t.AssetTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .ForeignKey("dbo.Models", t => t.ModelId, cascadeDelete: true)
                .Index(t => t.AssetTypeId)
                .Index(t => t.ManufacturerId)
                .Index(t => t.ModelId);
            
            CreateTable(
                "dbo.AssetTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Models",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    ManufacturerId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Models", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Assets", "ModelId", "dbo.Models");
            DropForeignKey("dbo.Assets", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Assets", "AssetTypeId", "dbo.AssetTypes");
            DropIndex("dbo.Models", new[] { "ManufacturerId" });
            DropIndex("dbo.Assets", new[] { "ModelId" });
            DropIndex("dbo.Assets", new[] { "ManufacturerId" });
            DropIndex("dbo.Assets", new[] { "AssetTypeId" });
            DropTable("dbo.Models");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.AssetTypes");
            DropTable("dbo.Assets");
        }
    }
}
