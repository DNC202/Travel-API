using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tour_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "DestinationId", "Duration", "Price", "Rating", "Thumbnail", "Title" },
                values: new object[,]
                {
                    { 1, 3, "4 days 3 nights", 119.98999999999999, 4.7999999999999998, "tour-1.jpg", "Adventure in the Alps" },
                    { 2, 4, "6 days 5 nights", 89.989999999999995, 4.5, "tour-1.jpg", "Himalayan Escape" },
                    { 3, 2, "3 days 2 nights", 109.98999999999999, 4.9000000000000004, "tour-1.jpg", "Rocky Mountain Adventure" },
                    { 4, 1, "7 days 6 nights", 129.99000000000001, 4.5999999999999996, "tour-1.jpg", "Serene Mountain Journey" },
                    { 5, 2, "5 days 4 nights", 99.989999999999995, 4.7000000000000002, "tour-1.jpg", "Peak Exploration" },
                    { 6, 4, "6 days 5 nights", 139.99000000000001, 4.5, "tour-1.jpg", "Highland Trails" },
                    { 7, 1, "5 days 4 nights", 149.99000000000001, 4.7999999999999998, "tour-1.jpg", "Summit Challenge" },
                    { 8, 3, "7 days 6 nights", 119.98999999999999, 4.9000000000000004, "tour-1.jpg", "Alpine Discovery" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tours",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "The city of love", "paris.jpg", "Paris" },
                    { 2, "Vibrant and bustling metropolis", "tokyo.jpg", "Tokyo" },
                    { 3, "The Big Apple", "new-york.jpg", "New York" },
                    { 4, "Historical and cultural capital", "london.jpg", "London" }
                });
        }
    }
}
