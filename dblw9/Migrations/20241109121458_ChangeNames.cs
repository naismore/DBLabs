using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace dblw9.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    contact_person_first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    contact_person = table.Column<string>(type: "nvarchar(max)", nullable: true, computedColumnSql: "[contact_person_first_name] + ' ' + [contact_person_last_name]"),
                    contact_person_last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    adress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "unspecified"),
                    cost = table.Column<double>(type: "float", nullable: false),
                    unit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    id_supplier = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.id);
                    table.ForeignKey(
                        name: "FK_Items_Suppliers_id_supplier",
                        column: x => x.id_supplier,
                        principalTable: "Suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsInStorages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_storage = table.Column<int>(type: "int", nullable: false),
                    id_item = table.Column<int>(type: "int", nullable: false),
                    arrial_date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsInStorages", x => x.id);
                    table.ForeignKey(
                        name: "FK_ItemsInStorages_Items_id_item",
                        column: x => x.id_item,
                        principalTable: "Items",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsInStorages_Storages_id_storage",
                        column: x => x.id_storage,
                        principalTable: "Storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "id", "adress", "name", "phone_number" },
                values: new object[,]
                {
                    { 1, "Moscow", "ST1", "123132131" },
                    { 2, "Vladimir", "ST2", "132313412" },
                    { 3, "Zelenograd", "ST3", "12441212" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "id", "adress", "contact_person_first_name", "contact_person_last_name", "email", "name", "phone_number" },
                values: new object[,]
                {
                    { 1, "New York City", "Name1", "Lastname1", null, "Apple", "123123123" },
                    { 2, "Tokyo", "Ryo", "Kudo", null, "Samsung", "3213321321" },
                    { 3, "Huyung", "Ching", "Chong", null, "Huawei", "31312312" },
                    { 4, "GungHuyung", "Chong", "Chang", null, "Xiaomi", "12341512" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "id", "cost", "name", "id_supplier", "unit" },
                values: new object[,]
                {
                    { 1, 109999.0, "Apple iPhone 15 Pro 128GB Dual Sim Blue Titanium", 1, "шт" },
                    { 2, 59999.0, "Apple iPhone 13 128GB nanoSim/eSim Midnight", 1, "шт" },
                    { 3, 14999.0, "Samsung Galaxy A15 LTE 4/128GB Dark Blue", 2, "шт" },
                    { 4, 27999.0, "HUAWEI nova 12s 8/256GB Black", 3, "шт" },
                    { 5, 20999.0, "Xiaomi Redmi Note 13 8/256GB Midnight Black", 4, "шт" },
                    { 6, 12999.0, "Xiaomi Redmi 12 8/256GB Midnight Black", 4, "шт" }
                });

            migrationBuilder.InsertData(
                table: "ItemsInStorages",
                columns: new[] { "id", "arrial_date", "id_item", "id_storage" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 9, 15, 14, 57, 416, DateTimeKind.Local).AddTicks(7526), 2, 1 },
                    { 2, new DateTime(2024, 11, 9, 15, 14, 57, 416, DateTimeKind.Local).AddTicks(7528), 3, 2 },
                    { 3, new DateTime(2024, 11, 9, 15, 14, 57, 416, DateTimeKind.Local).AddTicks(7529), 4, 2 },
                    { 4, new DateTime(2024, 11, 9, 15, 14, 57, 416, DateTimeKind.Local).AddTicks(7530), 5, 3 },
                    { 5, new DateTime(2024, 11, 9, 15, 14, 57, 416, DateTimeKind.Local).AddTicks(7532), 6, 3 },
                    { 6, new DateTime(2024, 11, 9, 15, 14, 57, 416, DateTimeKind.Local).AddTicks(7514), 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_id_supplier",
                table: "Items",
                column: "id_supplier");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsInStorages_id_item",
                table: "ItemsInStorages",
                column: "id_item");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsInStorages_id_storage",
                table: "ItemsInStorages",
                column: "id_storage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsInStorages");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
