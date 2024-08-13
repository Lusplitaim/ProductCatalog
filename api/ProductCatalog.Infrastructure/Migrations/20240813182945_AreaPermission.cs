using Microsoft.EntityFrameworkCore.Migrations;
using ProductCatalog.Core.Models.Enums;

#nullable disable

namespace ProductCatalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AreaPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AreaAction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaAction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.Area, x.Action });
                    table.ForeignKey(
                        name: "FK_RolePermission_AreaAction_Action",
                        column: x => x.Action,
                        principalTable: "AreaAction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Area_Area",
                        column: x => x.Area,
                        principalTable: "Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_Action",
                table: "RolePermission",
                column: "Action");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_Area",
                table: "RolePermission",
                column: "Area");

            migrationBuilder.InsertData(
                "Area",
                new string[] { "Id", "Name" },
                new string[,] { { "1", "пользователи" }, { "2", "продукты" }, { "3", "категории продуктов" } });

            migrationBuilder.InsertData(
                "AreaAction",
                new string[] { "Id", "Name" },
                new string[,] { { "1", "создать" }, { "2", "читать" }, { "3", "обновить" }, { "4", "удалить" } });

            // Adding permissions for admin.
            migrationBuilder.InsertData(
                "RolePermission",
                new string[] { "RoleId", "Area", "Action" },
                new string[,] {
                    {
                        ((int)UserRoleType.Admin).ToString(),
                        ((int)Area.Users).ToString(),
                        ((int)AreaAction.Create).ToString()
                    },
                    {
                        ((int)UserRoleType.Admin).ToString(),
                        ((int)Area.Users).ToString(),
                        ((int)AreaAction.Read).ToString()
                    },
                    {
                        ((int)UserRoleType.Admin).ToString(),
                        ((int)Area.Users).ToString(),
                        ((int)AreaAction.Update).ToString()
                    },
                    {
                        ((int)UserRoleType.Admin).ToString(),
                        ((int)Area.Users).ToString(),
                        ((int)AreaAction.Delete).ToString()
                    },
                });

            // Adding permissions for user.
            migrationBuilder.InsertData(
                "RolePermission",
                new string[] { "RoleId", "Area", "Action" },
                new string[,] {
                    {
                        ((int)UserRoleType.User).ToString(),
                        ((int)Area.Products).ToString(),
                        ((int)AreaAction.Create).ToString()
                    },
                    {
                        ((int)UserRoleType.User).ToString(),
                        ((int)Area.Products).ToString(),
                        ((int)AreaAction.Read).ToString()
                    },
                    {
                        ((int)UserRoleType.User).ToString(),
                        ((int)Area.Products).ToString(),
                        ((int)AreaAction.Update).ToString()
                    },
                });

            // Adding permissions for advanced user.
            migrationBuilder.InsertData(
                "RolePermission",
                new string[] { "RoleId", "Area", "Action" },
                new string[,] {
                    {
                        ((int)UserRoleType.AdvancedUser).ToString(),
                        ((int)Area.Products).ToString(),
                        ((int)AreaAction.Create).ToString()
                    },
                    {
                        ((int)UserRoleType.AdvancedUser).ToString(),
                        ((int)Area.Products).ToString(),
                        ((int)AreaAction.Read).ToString()
                    },
                    {
                        ((int)UserRoleType.AdvancedUser).ToString(),
                        ((int)Area.Products).ToString(),
                        ((int)AreaAction.Update).ToString()
                    },
                    {
                        ((int)UserRoleType.AdvancedUser).ToString(),
                        ((int)Area.Products).ToString(),
                        ((int)AreaAction.Delete).ToString()
                    },

                    {
                        ((int)UserRoleType.AdvancedUser).ToString(),
                        ((int)Area.ProductCategories).ToString(),
                        ((int)AreaAction.Create).ToString()
                    },
                    {
                        ((int)UserRoleType.AdvancedUser).ToString(),
                        ((int)Area.ProductCategories).ToString(),
                        ((int)AreaAction.Read).ToString()
                    },
                    {
                        ((int)UserRoleType.AdvancedUser).ToString(),
                        ((int)Area.ProductCategories).ToString(),
                        ((int)AreaAction.Update).ToString()
                    },
                    {
                        ((int)UserRoleType.AdvancedUser).ToString(),
                        ((int)Area.ProductCategories).ToString(),
                        ((int)AreaAction.Delete).ToString()
                    },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "AreaAction");

            migrationBuilder.DropTable(
                name: "Area");
        }
    }
}
