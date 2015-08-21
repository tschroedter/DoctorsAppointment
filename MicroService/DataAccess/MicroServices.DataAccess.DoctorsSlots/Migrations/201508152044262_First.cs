using System.Data.Entity.Migrations;

namespace MicroServices.DataAccess.DoctorsSlots.Migrations
{
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                        "dbo.Days",
                        c => new
                             {
                                 Id = c.Int(false,
                                            true),
                                 Date = c.DateTime(false),
                                 Doctor_Id = c.Int()
                             })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors",
                            t => t.Doctor_Id)
                .Index(t => t.Doctor_Id);

            CreateTable(
                        "dbo.Slots",
                        c => new
                             {
                                 Id = c.Int(false,
                                            true),
                                 Status = c.Int(false),
                                 StartDateTime = c.DateTime(false),
                                 EndDateTime = c.DateTime(false),
                                 Day_Id = c.Int()
                             })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Days",
                            t => t.Day_Id)
                .Index(t => t.Day_Id);

            CreateTable(
                        "dbo.Doctors",
                        c => new
                             {
                                 Id = c.Int(false,
                                            true),
                                 FirstName = c.String(false),
                                 LastName = c.String(false)
                             })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Days",
                           "Doctor_Id",
                           "dbo.Doctors");
            DropForeignKey("dbo.Slots",
                           "Day_Id",
                           "dbo.Days");
            DropIndex("dbo.Slots",
                      new[]
                      {
                          "Day_Id"
                      });
            DropIndex("dbo.Days",
                      new[]
                      {
                          "Doctor_Id"
                      });
            DropTable("dbo.Doctors");
            DropTable("dbo.Slots");
            DropTable("dbo.Days");
        }
    }
}