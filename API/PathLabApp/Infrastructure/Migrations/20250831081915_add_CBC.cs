using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClarmindsApp.Migrations
{
    /// <inheritdoc />
    public partial class addCBC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CbcTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hemoglobin = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RBC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WBC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Platelets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hematocrit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MCV = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MCH = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MCHC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CbcTest", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CbcTest");
        }
    }
}
