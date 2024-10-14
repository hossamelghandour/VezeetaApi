using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VezeetaApi.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_AspNetUsers_applicationUserId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_applicationUserId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Doctor",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "Doctor", "IsDeleted", "PasswordHash", "RefreshToken", "SecurityStamp" },
                values: new object[] { "09121423-2351-4f56-8f84-c1f3906b522a", null, false, "AQAAAAIAAYagAAAAEKfcY9/fYQZZ+DMSOj37G7mJPwML5gc0oGVusciJCK3GlSRu9d4Me5X6jEndkFk49g==", null, "852c1142-7cac-47a0-803a-aedef39cb893" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Doctor",
                table: "AspNetUsers",
                column: "Doctor",
                unique: true,
                filter: "[Doctor] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Doctors_Doctor",
                table: "AspNetUsers",
                column: "Doctor",
                principalTable: "Doctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Doctors_Doctor",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Doctor",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Doctor",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fbe30f25-2fca-447e-afda-dae33aea78eb", "AQAAAAIAAYagAAAAEOSJinHbxXl2ESFwhbuazq7QhUXSQkWEdD20lFkJJQEGL/G/otW7Fn6Et1br11NnVg==", "6dbd2961-b7c8-48b8-8f9d-b8c7d58907e1" });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_applicationUserId",
                table: "Doctors",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_AspNetUsers_applicationUserId",
                table: "Doctors",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
