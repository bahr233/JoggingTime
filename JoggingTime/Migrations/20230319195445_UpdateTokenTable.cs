using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoggingTime.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "tokens");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "tokens",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
