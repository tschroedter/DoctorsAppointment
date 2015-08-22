using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MicroServices.DataAccess.DoctorsSlots.Migrations
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public partial class AddedDayToSlot : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Slots",
                           "Day_Id",
                           "dbo.Days");
            DropIndex("dbo.Slots",
                      new[]
                      {
                          "Day_Id"
                      });
            AlterColumn("dbo.Slots",
                        "Day_Id",
                        c => c.Int(false));
            CreateIndex("dbo.Slots",
                        "Day_Id");
            AddForeignKey("dbo.Slots",
                          "Day_Id",
                          "dbo.Days",
                          "Id",
                          true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Slots",
                           "Day_Id",
                           "dbo.Days");
            DropIndex("dbo.Slots",
                      new[]
                      {
                          "Day_Id"
                      });
            AlterColumn("dbo.Slots",
                        "Day_Id",
                        c => c.Int());
            CreateIndex("dbo.Slots",
                        "Day_Id");
            AddForeignKey("dbo.Slots",
                          "Day_Id",
                          "dbo.Days",
                          "Id");
        }
    }
}