namespace ComputersFactory.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Computers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MemoryId = c.Int(nullable: false),
                        MotherboardId = c.Int(nullable: false),
                        ProcessorId = c.Int(nullable: false),
                        VideocardId = c.Int(nullable: false),
                        ComputerShopId = c.Int(nullable: false),
                        SalesReport_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ComputerShops", t => t.ComputerShopId, cascadeDelete: true)
                .ForeignKey("dbo.Memories", t => t.MemoryId, cascadeDelete: true)
                .ForeignKey("dbo.Motherboards", t => t.MotherboardId, cascadeDelete: true)
                .ForeignKey("dbo.Processors", t => t.ProcessorId, cascadeDelete: true)
                .ForeignKey("dbo.VideoCards", t => t.VideocardId, cascadeDelete: true)
                .ForeignKey("dbo.SalesReports", t => t.SalesReport_Id)
                .Index(t => t.MemoryId)
                .Index(t => t.MotherboardId)
                .Index(t => t.ProcessorId)
                .Index(t => t.VideocardId)
                .Index(t => t.ComputerShopId)
                .Index(t => t.SalesReport_Id);
            
            CreateTable(
                "dbo.ComputerShops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HardDrives",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CapacityInGb = c.Int(nullable: false),
                        Manufacturer = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Memories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CapacityInGb = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Manufacturer = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Motherboards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Manufacturer = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Processors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FrequencyInMhz = c.Int(nullable: false),
                        Manufacturer = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VideoCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Manufacturer = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SalesReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sales = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        ComputerShopId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ComputerShops", t => t.ComputerShopId, cascadeDelete: true)
                .Index(t => t.ComputerShopId);
            
            CreateTable(
                "dbo.HardDriveComputers",
                c => new
                    {
                        HardDrive_Id = c.Int(nullable: false),
                        Computer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.HardDrive_Id, t.Computer_Id })
                .ForeignKey("dbo.HardDrives", t => t.HardDrive_Id, cascadeDelete: true)
                .ForeignKey("dbo.Computers", t => t.Computer_Id, cascadeDelete: true)
                .Index(t => t.HardDrive_Id)
                .Index(t => t.Computer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SalesReports", "ComputerShopId", "dbo.ComputerShops");
            DropForeignKey("dbo.Computers", "SalesReport_Id", "dbo.SalesReports");
            DropForeignKey("dbo.Computers", "VideocardId", "dbo.VideoCards");
            DropForeignKey("dbo.Computers", "ProcessorId", "dbo.Processors");
            DropForeignKey("dbo.Computers", "MotherboardId", "dbo.Motherboards");
            DropForeignKey("dbo.Computers", "MemoryId", "dbo.Memories");
            DropForeignKey("dbo.HardDriveComputers", "Computer_Id", "dbo.Computers");
            DropForeignKey("dbo.HardDriveComputers", "HardDrive_Id", "dbo.HardDrives");
            DropForeignKey("dbo.Computers", "ComputerShopId", "dbo.ComputerShops");
            DropIndex("dbo.HardDriveComputers", new[] { "Computer_Id" });
            DropIndex("dbo.HardDriveComputers", new[] { "HardDrive_Id" });
            DropIndex("dbo.SalesReports", new[] { "ComputerShopId" });
            DropIndex("dbo.Computers", new[] { "SalesReport_Id" });
            DropIndex("dbo.Computers", new[] { "ComputerShopId" });
            DropIndex("dbo.Computers", new[] { "VideocardId" });
            DropIndex("dbo.Computers", new[] { "ProcessorId" });
            DropIndex("dbo.Computers", new[] { "MotherboardId" });
            DropIndex("dbo.Computers", new[] { "MemoryId" });
            DropTable("dbo.HardDriveComputers");
            DropTable("dbo.SalesReports");
            DropTable("dbo.VideoCards");
            DropTable("dbo.Processors");
            DropTable("dbo.Motherboards");
            DropTable("dbo.Memories");
            DropTable("dbo.HardDrives");
            DropTable("dbo.ComputerShops");
            DropTable("dbo.Computers");
        }
    }
}
