using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HitchFix.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceTypeSvg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "DeviceTypes",
                newName: "ImageSvg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageSvg",
                table: "DeviceTypes",
                newName: "Url");
        }
    }
}
