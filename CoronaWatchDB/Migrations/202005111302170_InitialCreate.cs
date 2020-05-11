namespace CoronaWatchDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegionDBs",
                c => new
                    {
                        ISOCode = c.String(nullable: false, maxLength: 4, unicode: false),
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Slug = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ISOCode);
            
            CreateTable(
                "dbo.ReportDBs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ISOCode = c.String(maxLength: 4, unicode: false),
                        Confirmed = c.Int(),
                        Recovered = c.Int(),
                        Death = c.Int(),
                        Active = c.Int(),
                        Date = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RegionDBs", t => t.ISOCode)
                .Index(t => t.ISOCode);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportDBs", "ISOCode", "dbo.RegionDBs");
            DropIndex("dbo.ReportDBs", new[] { "ISOCode" });
            DropTable("dbo.ReportDBs");
            DropTable("dbo.RegionDBs");
        }
    }
}
