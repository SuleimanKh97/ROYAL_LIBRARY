using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddStudyContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyMaterials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitleArabic = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subject = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Teacher = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionArabic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0.0m),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "متوفر")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Delivery = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: "PDF / طباعة")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: false, defaultValue: 0.0m),
                    Downloads = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Duration = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Features = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyMaterials", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudySchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Day = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitleArabic = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionArabic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grade = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subjects = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Focus = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FocusArabic = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderIndex = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySchedules", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudyTips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitleArabic = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionArabic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grade = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subject = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tips = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderIndex = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyTips", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5322), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5322) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5325), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5326) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5328), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5328) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5330), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5331) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5333), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5333) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5609), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5610) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5617), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5617) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5621), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5622) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5626), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5626) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5630), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5631) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5538), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5538) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5542), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5543) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5544), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5544) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5546), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5546) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5547), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5548) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5569), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5570) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5503), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5503) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5506), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5507) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5509), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5509) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5511), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5512) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5850), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5850) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5853), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5853) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5855), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5855) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5857), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5857) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5859), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5859) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5861), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5861) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5863), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5863) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5865), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5865) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5867), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5867) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5869), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5869) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5871), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5871) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5873), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5873) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5894), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5895) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5896), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5897) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5899), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5899) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5901), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5901) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5903), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5903) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5905), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5905) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5906), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5907) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5908), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5909) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5910), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5911) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5912), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5913) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5914), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5914) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5916), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5916) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5934), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5934) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5936), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5936) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5938), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5938) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5940), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5940) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5942), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5942) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5944), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5944) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5946), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5946) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5948), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5948) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5732), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5733) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5735), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5736) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5738), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5738) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5740), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5740) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5742), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5743) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5760), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5760) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5762), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5762) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5764), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5764) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5766), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5767) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5769), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5769) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5771), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5771) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5787), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5788) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5789), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5790) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5792), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5792) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5794), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5794) });

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5696), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5697) });

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5701), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5701) });

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5704), new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5704) });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5656));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5659));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5660));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5661));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5661));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5662));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5663));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5664));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5665));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 20, 22, 10, 22, 929, DateTimeKind.Utc).AddTicks(5666));

            migrationBuilder.CreateIndex(
                name: "IX_StudyMaterials_Category",
                table: "StudyMaterials",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_StudyMaterials_IsActive",
                table: "StudyMaterials",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_StudyMaterials_Subject",
                table: "StudyMaterials",
                column: "Subject");

            migrationBuilder.CreateIndex(
                name: "IX_StudySchedules_Grade",
                table: "StudySchedules",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_StudySchedules_IsActive",
                table: "StudySchedules",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_StudySchedules_Type",
                table: "StudySchedules",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_StudyTips_Category",
                table: "StudyTips",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_StudyTips_Grade",
                table: "StudyTips",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_StudyTips_IsActive",
                table: "StudyTips",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_StudyTips_Subject",
                table: "StudyTips",
                column: "Subject");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyMaterials");

            migrationBuilder.DropTable(
                name: "StudySchedules");

            migrationBuilder.DropTable(
                name: "StudyTips");

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4702), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4702) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4705), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4706) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4708), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4708) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4710), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4711) });

            migrationBuilder.UpdateData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4712), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4713) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5046), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5047) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5053), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5053) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5062), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5062) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5066), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5067) });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5071), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5071) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4996), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4996) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5000), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5000) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5002), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5002) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5003), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5003) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5005), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5005) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5006), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5006) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4963), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4963) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4967), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4967) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4969), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4970) });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4972), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4972) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5302), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5303) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5305), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5306) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5307), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5308) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5310), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5310) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5312), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5312) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5314), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5314) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5316), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5316) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5318), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5318) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5320), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5320) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5322), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5322) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5324), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5324) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5326), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5326) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5345), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5346) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5347), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5348) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5349), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5352), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5352) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5354), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5354) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5356), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5356) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5358), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5358) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5359), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5360) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5361), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5362) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5363), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5364) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5365), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5366) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5367), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5368) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5384), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5385) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5386), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5387) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5388), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5389) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5390), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5391) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5392), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5393) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5394), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5395) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5396), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5397) });

            migrationBuilder.UpdateData(
                table: "QuestionOptions",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5398), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5399) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5171), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5172) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5175), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5175) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5177), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5177) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5179), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5182), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5182) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5201), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5201) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5203), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5204) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5205), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5206) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5208), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5208) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5253), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5253) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5255), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5256) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5275), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5275) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5277), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5277) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5279), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5279) });

            migrationBuilder.UpdateData(
                table: "QuizQuestions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5281), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5282) });

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5143), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5143) });

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5147), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5147) });

            migrationBuilder.UpdateData(
                table: "Quizzes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5150), new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5151) });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5098));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5102));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5103));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5104));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5105));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5105));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5106));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5107));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5108));
        }
    }
}
