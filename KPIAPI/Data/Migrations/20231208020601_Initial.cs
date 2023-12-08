using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KPIAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblDepartmants",
                columns: table => new
                {
                    DepNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDepartmants", x => x.DepNo);
                });

            migrationBuilder.CreateTable(
                name: "TblKPI",
                columns: table => new
                {
                    KPIIDNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KPIDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasurementUnit = table.Column<bool>(type: "bit", nullable: false),
                    TargetedValue = table.Column<int>(type: "int", nullable: false),
                    DepNo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblKPI", x => x.KPIIDNum);
                    table.ForeignKey(
                        name: "FK_TblKPI_TblDepartmants_DepNo",
                        column: x => x.DepNo,
                        principalTable: "TblDepartmants",
                        principalColumn: "DepNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblKPI_DepNo",
                table: "TblKPI",
                column: "DepNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblKPI");

            migrationBuilder.DropTable(
                name: "TblDepartmants");
        }
    }
}
