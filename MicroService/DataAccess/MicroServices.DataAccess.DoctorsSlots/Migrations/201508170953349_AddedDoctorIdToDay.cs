using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MicroServices.DataAccess.DoctorsSlots.Migrations
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public partial class AddedDoctorIdToDay : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Days",
                         "Doctor_Id",
                         "DoctorId");
            RenameIndex("dbo.Days",
                        "IX_Doctor_Id",
                        "IX_DoctorId");
        }

        public override void Down()
        {
            RenameIndex("dbo.Days",
                        "IX_DoctorId",
                        "IX_Doctor_Id");
            RenameColumn("dbo.Days",
                         "DoctorId",
                         "Doctor_Id");
        }
    }
}