using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CitationWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Citations",
                columns: table => new
                {
                    citationid = table.Column<int>(name: "citation_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    driverid = table.Column<int>(name: "driver_id", type: "int", nullable: false),
                    userid = table.Column<int>(name: "user_id", type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: true),
                    time = table.Column<TimeSpan>(type: "time", nullable: true),
                    ownerfault = table.Column<bool>(name: "owner_fault", type: "bit", nullable: false),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    violationloc = table.Column<string>(name: "violation_loc", type: "nvarchar(max)", nullable: false),
                    signdate = table.Column<TimeSpan>(name: "sign_date", type: "time", nullable: true),
                    vin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vinstate = table.Column<string>(name: "vin_state", type: "nvarchar(max)", nullable: false),
                    codesection = table.Column<string>(name: "code_section", type: "nvarchar(max)", nullable: false),
                    officername = table.Column<string>(name: "officer_name", type: "nvarchar(max)", nullable: false),
                    officerbadge = table.Column<string>(name: "officer_badge", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citations", x => x.citationid);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    driverid = table.Column<int>(name: "driver_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    drivername = table.Column<string>(name: "driver_name", type: "nvarchar(max)", nullable: false),
                    datebirth = table.Column<DateTime>(name: "date_birth", type: "date", nullable: true),
                    sex = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    hair = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eyes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    height = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weight = table.Column<int>(type: "int", nullable: false),
                    race = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zip = table.Column<int>(type: "int", nullable: false),
                    licenseno = table.Column<string>(name: "license_no", type: "nvarchar(max)", nullable: false),
                    licenseclass = table.Column<string>(name: "license_class", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.driverid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userid = table.Column<int>(name: "user_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    officerbadge = table.Column<string>(name: "officer_badge", type: "nvarchar(max)", nullable: false),
                    officername = table.Column<string>(name: "officer_name", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Citations");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
