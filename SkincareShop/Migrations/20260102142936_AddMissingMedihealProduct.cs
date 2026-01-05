using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KoreanSkincareShop.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingMedihealProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Details", "ImageUrl", "Name", "Price" },
                values: new object[] { "Overnight calming mask with cica and niacinamide that helps reduce irritation and restore skin balance.", "/images/products/mediheal-cica-ac-calming-sleeping-mask.webp", "Mediheal Cica AC Calming Sleeping Mask", 12.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Details", "ImageUrl", "Name", "Price" },
                values: new object[] { "Firming toner pads infused with collagen to improve skin elasticity and hydration.", "/images/products/mediheal-collagen-ampoule-pad.jpg", "Mediheal Collagen Ampoule Pad", 119.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Details", "ImageUrl", "Name", "Price" },
                values: new object[] { "Targeted blemish patches with madecassoside to calm and protect irritated skin.", "/images/products/mediheal-derma-clear-madecassoside-patch.webp", "Mediheal Derma Clear Madecassoside Patch", 25.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Details", "ImageUrl", "IsTrendingProduct", "Name", "Price" },
                values: new object[] { "Alginate modeling mask with madecassoside for intensive soothing and regeneration.", "/images/products/mediheal-derma-modeling-madecassoside-mask.webp", false, "Mediheal Derma Modeling Madecassoside Mask", 31.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Details", "ImageUrl", "Name", "Price" },
                values: new object[] { "Purifying alginate mask with tea tree extract for oily and blemish-prone skin.", "/images/products/mediheal-derma-modeling-teatree-mask.webp", "Mediheal Derma Modeling Teatree Mask", 31.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Details", "ImageUrl", "Name", "Price" },
                values: new object[] { "Set of five essential Mediheal sheet masks for different skin needs.", "/images/products/mediheal-essential-mask-set.webp", "Mediheal Essential Mask Set", 53.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Details", "ImageUrl", "IsTrendingProduct", "Name", "Price" },
                values: new object[] { "Soothing toner pads with madecassoside to reduce redness and improve skin texture.", "/images/products/mediheal-madecassoside-blemish-pad.jpg", true, "Mediheal Madecassoside Blemish Pad", 119.99m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Details", "ImageUrl", "IsTrendingProduct", "Name", "Price" },
                values: new object[,]
                {
                    { 8, "Regenerating sheet mask with madecassoside for sensitive and irritated skin.", "/images/products/mediheal-madecassoside-essential-mask.webp", false, "Mediheal Madecassoside Essential Mask", 7.99m },
                    { 9, "Deeply moisturizing sheet mask with panthenol to strengthen the skin barrier.", "/images/products/mediheal-nmf-panthenol-mask.webp", false, "Mediheal N.M.F Panthenol Mask", 9.99m },
                    { 10, "Daily calming sheet mask designed to soothe sensitive and stressed skin.", "/images/products/mediheal-pure-calming-mask.jpg", true, "Mediheal Pure Calming Mask", 5.99m },
                    { 11, "Bamboo sheet mask with tea tree extract to calm and purify the skin.", "/images/products/mediheal-teatree-essential-mask.webp", true, "Mediheal Teatree Essential Mask", 5.95m },
                    { 12, "Hydrogel calming mask with tea tree for instant soothing effect.", "/images/products/mediheal-teatree-nude-gel-mask.jpg", false, "Mediheal Teatree Nude Gel Mask", 10.99m },
                    { 13, "Exfoliating and calming pads with tea tree extract for troubled skin.", "/images/products/mediheal-teatree-trouble-pad.jpg", true, "Mediheal Teatree Trouble Pad", 119.99m },
                    { 14, "Soothing sheet mask combining tea tree and cica for irritated skin.", "/images/products/mediheal-teatree-x-cica-mask.webp", false, "Mediheal Teatree X Cica Mask", 9.99m },
                    { 15, "Calming bamboo sheet mask with tea tree extract, ideal for sensitive and irritated skin.", "/images/products/mediheal-teatree-essential-mask.jpg", true, "Mediheal Teatree Essential Bamboo Mask", 7.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Details", "ImageUrl", "Name", "Price" },
                values: new object[] { "Regenerująca esencja ze śluzem ślimaka, idealna do skóry wrażliwej.", "https://example.com/cosrx-snail.jpg", "COSRX Advanced Snail 96 Mucin Essence", 89.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Details", "ImageUrl", "Name", "Price" },
                values: new object[] { "Nocna maska intensywnie nawilżająca.", "https://example.com/laneige-mask.jpg", "Laneige Water Sleeping Mask", 129.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Details", "ImageUrl", "Name", "Price" },
                values: new object[] { "Serum z zieloną herbatą wzmacniające barierę skóry.", "https://example.com/innisfree-serum.jpg", "Innisfree Green Tea Seed Serum", 119.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Details", "ImageUrl", "IsTrendingProduct", "Name", "Price" },
                values: new object[] { "Lekki krem przeciwsłoneczny z ekstraktami roślinnymi.", "https://example.com/joseon-sunscreen.jpg", true, "Beauty of Joseon Relief Sun SPF50+", 69.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Details", "ImageUrl", "Name", "Price" },
                values: new object[] { "Krem regenerujący dla skóry bardzo wrażliwej.", "https://example.com/soonjung-cream.jpg", "Etude House SoonJung 2x Barrier Cream", 79.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Details", "ImageUrl", "Name", "Price" },
                values: new object[] { "Krem kojący redukujący zaczerwienienia.", "https://example.com/drjart-cicapair.jpg", "Dr. Jart+ Cicapair Tiger Grass Cream", 159.99m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Details", "ImageUrl", "IsTrendingProduct", "Name", "Price" },
                values: new object[] { "Esencja anti-aging poprawiająca strukturę skóry.", "https://example.com/missha-essence.jpg", false, "Missha Time Revolution First Treatment Essence", 139.99m });
        }
    }
}
