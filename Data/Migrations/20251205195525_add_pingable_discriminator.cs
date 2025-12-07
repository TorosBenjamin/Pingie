using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class add_pingable_discriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "Pingable");

            migrationBuilder.AlterColumn<int>(
                name: "Port",
                table: "Pingable",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "PingableType",
                table: "Pingable",
                type: "TEXT",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pingable",
                table: "Pingable",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PingResults_PingableId",
                table: "PingResults",
                column: "PingableId");

            migrationBuilder.AddForeignKey(
                name: "FK_PingResults_Pingable_PingableId",
                table: "PingResults",
                column: "PingableId",
                principalTable: "Pingable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PingResults_Pingable_PingableId",
                table: "PingResults");

            migrationBuilder.DropIndex(
                name: "IX_PingResults_PingableId",
                table: "PingResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pingable",
                table: "Pingable");

            migrationBuilder.DropColumn(
                name: "PingableType",
                table: "Pingable");

            migrationBuilder.RenameTable(
                name: "Pingable",
                newName: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "Port",
                table: "Services",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Hostname = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    PingInterval = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });
        }
    }
}
