using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EgyptianEscape.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Area", "ImageUrl", "Name", "Occupancy", "Price" },
                values: new object[] { 500, "https://placehold.co/600x400", "Royal Villa ", 4, 200.0 });

            migrationBuilder.InsertData(
                table: "Villa",
                columns: new[] { "Id", "Area", "CreatedDate", "Description", "ImageUrl", "Name", "Occupancy", "Price", "UpdatedDate" },
                values: new object[,]
                {
                    { 2, 7000, null, "Villa 1 Description", "https://placehold.co/600x400", "Premium Pool Villa ", 5, 300.0, null },
                    { 3, 1000, null, "Villa 1 Description", "https://placehold.co/600x400", "Luxury Pool Villa ", 6, 400.0, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Area", "ImageUrl", "Name", "Occupancy", "Price" },
                values: new object[] { 1000, null, "Villa 1", 0, 1000.0 });
        }
    }
}
