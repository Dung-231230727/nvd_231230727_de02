using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nvd_231230727_de02.Migrations
{
    /// <inheritdoc />
    public partial class Fix_NvdId_Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NvdCatalog",
                columns: table => new
                {
                    nvdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nvdCateName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    nvdCatePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    nvdCateQty = table.Column<int>(type: "int", nullable: false),
                    nvdPicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nvdCateActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NvdCatal__CA45E3779A41E60A", x => x.nvdId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NvdCatalog");
        }
    }
}
