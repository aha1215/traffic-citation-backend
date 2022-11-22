using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CitationWebAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citations",
                columns: table => new
                {
                    citation_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    driver_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: true),
                    time = table.Column<TimeSpan>(type: "time", nullable: true),
                    owner_fault = table.Column<bool>(type: "boolean", nullable: false),
                    desc = table.Column<string>(type: "text", nullable: false),
                    violation_loc = table.Column<string>(type: "text", nullable: false),
                    sign_date = table.Column<DateTime>(type: "date", nullable: true),
                    vin = table.Column<string>(type: "text", nullable: false),
                    vin_state = table.Column<string>(type: "text", nullable: false),
                    code_section = table.Column<string>(type: "text", nullable: false),
                    officer_name = table.Column<string>(type: "text", nullable: false),
                    officer_badge = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citations", x => x.citation_id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    driver_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    driver_name = table.Column<string>(type: "text", nullable: false),
                    date_birth = table.Column<DateTime>(type: "date", nullable: true),
                    sex = table.Column<char>(type: "character(1)", nullable: false),
                    hair = table.Column<string>(type: "text", nullable: false),
                    eyes = table.Column<string>(type: "text", nullable: false),
                    height = table.Column<string>(type: "text", nullable: false),
                    weight = table.Column<int>(type: "integer", nullable: false),
                    race = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false),
                    zip = table.Column<int>(type: "integer", nullable: false),
                    license_no = table.Column<string>(type: "text", nullable: false),
                    license_class = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.driver_id);
                });

            migrationBuilder.CreateTable(
                name: "Violations",
                columns: table => new
                {
                    violation_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    citation_id = table.Column<int>(type: "integer", nullable: false),
                    group = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    degree = table.Column<string>(type: "text", nullable: false),
                    desc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.violation_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citations");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Violations");
        }
    }
}
