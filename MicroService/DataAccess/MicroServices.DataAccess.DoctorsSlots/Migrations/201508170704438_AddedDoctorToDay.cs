using System.Data.Entity.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace MicroServices.DataAccess.DoctorsSlots.Migrations
{
    [ExcludeFromCodeCoverage]
    //ncrunch: no coverage start
    public partial class AddedDoctorToDay : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Days",
                           "Doctor_Id",
                           "dbo.Doctors");
            DropIndex("dbo.Days",
                      new[]
                      {
                          "Doctor_Id"
                      });
            AlterColumn("dbo.Days",
                        "Doctor_Id",
                        c => c.Int(false));
            CreateIndex("dbo.Days",
                        "Doctor_Id");
            AddForeignKey("dbo.Days",
                          "Doctor_Id",
                          "dbo.Doctors",
                          "Id",
                          true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Days",
                           "Doctor_Id",
                           "dbo.Doctors");
            DropIndex("dbo.Days",
                      new[]
                      {
                          "Doctor_Id"
                      });
            AlterColumn("dbo.Days",
                        "Doctor_Id",
                        c => c.Int());
            CreateIndex("dbo.Days",
                        "Doctor_Id");
            AddForeignKey("dbo.Days",
                          "Doctor_Id",
                          "dbo.Doctors",
                          "Id");
        }
    }
}