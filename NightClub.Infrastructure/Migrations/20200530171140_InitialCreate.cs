using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NightClub.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blacklists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId1 = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    MemberId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blacklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blacklists_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blacklists_Members_MemberId1",
                        column: x => x.MemberId1,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    NationalRegisterNumber = table.Column<string>(nullable: true),
                    ValidityDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    CardNumber = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityCards_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    MemberId1 = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    MemberId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberCards_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberCards_Members_MemberId1",
                        column: x => x.MemberId1,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "IsActive", "PhoneNumber" },
                values: new object[] { 1, "john.doe@gmail.com", true, null });

            migrationBuilder.InsertData(
                table: "Blacklists",
                columns: new[] { "Id", "EndDate", "MemberId", "MemberId1", "StartDate" },
                values: new object[] { 1, new DateTime(2019, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "IdentityCards",
                columns: new[] { "Id", "BirthDate", "CardNumber", "ExpirationDate", "Firstname", "Lastname", "MemberId", "NationalRegisterNumber", "ValidityDate" },
                values: new object[] { 1, new DateTime(1994, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000000001, new DateTime(2022, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Doe", 1, "548.65.84-654-56", new DateTime(2017, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "MemberCards",
                columns: new[] { "Id", "Code", "IsActive", "MemberId", "MemberId1" },
                values: new object[,]
                {
                    { 1, "cb656a56-e038-448e-a509-541c549adf0c", false, 1, null },
                    { 2, "3da36c94-ee93-4736-b152-b4c8f7cd8006", true, 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blacklists_MemberId",
                table: "Blacklists",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Blacklists_MemberId1",
                table: "Blacklists",
                column: "MemberId1");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityCards_CardNumber",
                table: "IdentityCards",
                column: "CardNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IdentityCards_MemberId",
                table: "IdentityCards",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_MemberId",
                table: "MemberCards",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCards_MemberId1",
                table: "MemberCards",
                column: "MemberId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blacklists");

            migrationBuilder.DropTable(
                name: "IdentityCards");

            migrationBuilder.DropTable(
                name: "MemberCards");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
