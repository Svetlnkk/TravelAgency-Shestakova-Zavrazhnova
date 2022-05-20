using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelAgencyDatabaseImplements.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "Tourists",
                columns: table => new
                {
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tourists", x => x.Login);
                });

            migrationBuilder.CreateTable(
                name: "Guides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuideName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperatorLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guides_Operators_OperatorLogin",
                        column: x => x.OperatorLogin,
                        principalTable: "Operators",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperatorLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_Operators_OperatorLogin",
                        column: x => x.OperatorLogin,
                        principalTable: "Operators",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Excursions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    TouristLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excursions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Excursions_Tourists_TouristLogin",
                        column: x => x.TouristLogin,
                        principalTable: "Tourists",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TouristLogin = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Tourists_TouristLogin",
                        column: x => x.TouristLogin,
                        principalTable: "Tourists",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StopName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckinDateStop = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateofDepatureStop = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperatorLogin = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TourId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stops_Operators_OperatorLogin",
                        column: x => x.OperatorLogin,
                        principalTable: "Operators",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stops_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourGuides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuideId = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    TourCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourGuides_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourGuides_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExcursionGuides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcursionId = table.Column<int>(type: "int", nullable: false),
                    GuideId = table.Column<int>(type: "int", nullable: false),
                    ExcursionCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcursionGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcursionGuides_Excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExcursionGuides_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfVisit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TouristLogin = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExcursionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_Excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Places_Tourists_TouristLogin",
                        column: x => x.TouristLogin,
                        principalTable: "Tourists",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TripExcursions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    ExcursionId = table.Column<int>(type: "int", nullable: false),
                    ExcursionCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripExcursions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripExcursions_Excursions_ExcursionId",
                        column: x => x.ExcursionId,
                        principalTable: "Excursions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripExcursions_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripTours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    TourCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripTours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripTours_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripTours_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionGuides_ExcursionId",
                table: "ExcursionGuides",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcursionGuides_GuideId",
                table: "ExcursionGuides",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_Excursions_TouristLogin",
                table: "Excursions",
                column: "TouristLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Guides_OperatorLogin",
                table: "Guides",
                column: "OperatorLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Places_ExcursionId",
                table: "Places",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_TouristLogin",
                table: "Places",
                column: "TouristLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_OperatorLogin",
                table: "Stops",
                column: "OperatorLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_TourId",
                table: "Stops",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_GuideId",
                table: "TourGuides",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_TourGuides_TourId",
                table: "TourGuides",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_OperatorLogin",
                table: "Tours",
                column: "OperatorLogin");

            migrationBuilder.CreateIndex(
                name: "IX_TripExcursions_ExcursionId",
                table: "TripExcursions",
                column: "ExcursionId");

            migrationBuilder.CreateIndex(
                name: "IX_TripExcursions_TripId",
                table: "TripExcursions",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TouristLogin",
                table: "Trips",
                column: "TouristLogin");

            migrationBuilder.CreateIndex(
                name: "IX_TripTours_TourId",
                table: "TripTours",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TripTours_TripId",
                table: "TripTours",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcursionGuides");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "TourGuides");

            migrationBuilder.DropTable(
                name: "TripExcursions");

            migrationBuilder.DropTable(
                name: "TripTours");

            migrationBuilder.DropTable(
                name: "Guides");

            migrationBuilder.DropTable(
                name: "Excursions");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropTable(
                name: "Tourists");
        }
    }
}
