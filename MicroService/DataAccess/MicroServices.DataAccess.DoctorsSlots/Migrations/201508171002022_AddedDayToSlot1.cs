using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MicroServices.DataAccess.DoctorsSlots.Migrations
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public partial class AddedDayToSlot1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Slots",
                         "Day_Id",
                         "DayId");
            RenameIndex("dbo.Slots",
                        "IX_Day_Id",
                        "IX_DayId");
        }

        public override void Down()
        {
            RenameIndex("dbo.Slots",
                        "IX_DayId",
                        "IX_Day_Id");
            RenameColumn("dbo.Slots",
                         "DayId",
                         "Day_Id");
        }
    }
}