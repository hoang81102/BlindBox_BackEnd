using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    /// <inheritdoc />
    public partial class Remove_CartDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Accounts_AccountId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Carts_CartId",
                table: "Packages");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CartDetail");

            migrationBuilder.DropIndex(
                name: "IX_Packages_CartId",
                table: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Carts_AccountId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Carts",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "BlindBoxId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Carts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_applicationUserId",
                table: "Carts",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_BlindBoxId",
                table: "Carts",
                column: "BlindBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_PackageId",
                table: "Carts",
                column: "PackageId",
                unique: true,
                filter: "[PackageId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_applicationUserId",
                table: "Carts",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_BlindBoxes_BlindBoxId",
                table: "Carts",
                column: "BlindBoxId",
                principalTable: "BlindBoxes",
                principalColumn: "BlindBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Packages_PackageId",
                table: "Carts",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "PackageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_applicationUserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_BlindBoxes_BlindBoxId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Packages_PackageId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_applicationUserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_BlindBoxId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_PackageId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "BlindBoxId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Carts",
                newName: "AccountId");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Packages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IsVerify = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "CartDetail",
                columns: table => new
                {
                    CartDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlindBoxId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetail", x => x.CartDetailId);
                    table.ForeignKey(
                        name: "FK_CartDetail_BlindBoxes_BlindBoxId",
                        column: x => x.BlindBoxId,
                        principalTable: "BlindBoxes",
                        principalColumn: "BlindBoxId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDetail_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDetail_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_CartId",
                table: "Packages",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_AccountId",
                table: "Carts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_BlindBoxId",
                table: "CartDetail",
                column: "BlindBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_CartId",
                table: "CartDetail",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetail_PackageId",
                table: "CartDetail",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Accounts_AccountId",
                table: "Carts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Carts_CartId",
                table: "Packages",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId");
        }
    }
}
