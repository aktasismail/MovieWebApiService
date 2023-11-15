using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieReviewAPP.Migrations
{
    public partial class addidentityrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "128f2df1-5f8d-45ff-ac96-c86e143b55a9", "8842a39e-68d9-4f35-8fb7-78e4a8faa968", "Admin", "ADMİN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f389902d-51e1-4dcc-8b93-0e7069cda7ec", "b7f0bb49-3093-43a1-a366-b1e57aee4581", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "128f2df1-5f8d-45ff-ac96-c86e143b55a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f389902d-51e1-4dcc-8b93-0e7069cda7ec");
        }
    }
}
