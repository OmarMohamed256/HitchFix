using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HitchFix.Migrations
{
    /// <inheritdoc />
    public partial class addDeviceIdToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeviceId",
                table: "Orders",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Devices_DeviceId",
                table: "Orders",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Devices_DeviceId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeviceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Orders");
        }
    }
}
