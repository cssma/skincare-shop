using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoreanSkincareShop.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Details", "ImageUrl", "IsTrendingProduct", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Regenerująca esencja ze śluzem ślimaka, idealna do skóry wrażliwej.", "https://example.com/cosrx-snail.jpg", true, "COSRX Advanced Snail 96 Mucin Essence", 89.99m },
                    { 2, "Nocna maska intensywnie nawilżająca.", "https://example.com/laneige-mask.jpg", true, "Laneige Water Sleeping Mask", 129.99m },
                    { 3, "Serum z zieloną herbatą wzmacniające barierę skóry.", "https://example.com/innisfree-serum.jpg", false, "Innisfree Green Tea Seed Serum", 119.99m },
                    { 4, "Lekki krem przeciwsłoneczny z ekstraktami roślinnymi.", "https://example.com/joseon-sunscreen.jpg", true, "Beauty of Joseon Relief Sun SPF50+", 69.99m },
                    { 5, "Krem regenerujący dla skóry bardzo wrażliwej.", "https://example.com/soonjung-cream.jpg", false, "Etude House SoonJung 2x Barrier Cream", 79.99m },
                    { 6, "Krem kojący redukujący zaczerwienienia.", "https://example.com/drjart-cicapair.jpg", true, "Dr. Jart+ Cicapair Tiger Grass Cream", 159.99m },
                    { 7, "Esencja anti-aging poprawiająca strukturę skóry.", "https://example.com/missha-essence.jpg", false, "Missha Time Revolution First Treatment Essence", 139.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
