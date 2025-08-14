using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddQuizSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TitleArabic = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescriptionArabic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subject = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Grade = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Chapter = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeLimit = table.Column<int>(type: "int", nullable: false, defaultValue: 30),
                    TotalQuestions = table.Column<int>(type: "int", nullable: false),
                    PassingScore = table.Column<int>(type: "int", nullable: false, defaultValue: 60),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    IsPublic = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)"),
                    CreatedBy = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quizzes_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    CompletedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TotalScore = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Percentage = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    IsPassed = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    TimeSpent = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizAttempts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizAttempts_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuizId = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuestionTextArabic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Explanation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExplanationArabic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Points = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    OrderIndex = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Type = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OptionTextArabic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_QuizQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AttemptAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AttemptId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    SelectedOptionId = table.Column<int>(type: "int", nullable: true),
                    TextAnswer = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BooleanAnswer = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    PointsEarned = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    AnsweredAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttemptAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttemptAnswers_QuestionOptions_SelectedOptionId",
                        column: x => x.SelectedOptionId,
                        principalTable: "QuestionOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AttemptAnswers_QuizAttempts_AttemptId",
                        column: x => x.AttemptId,
                        principalTable: "QuizAttempts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttemptAnswers_QuizQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Biography", "BiographyArabic", "BirthDate", "CreatedAt", "IsActive", "Name", "NameArabic", "Nationality", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Egyptian poet and playwright", "شاعر وكاتب مسرحي مصري", null, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4702), true, "Ahmed Shawqi", "أحمد شوقي", "Egyptian", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4702) },
                    { 2, "Egyptian writer and intellectual", "كاتب ومفكر مصري", null, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4705), true, "Taha Hussein", "طه حسين", "Egyptian", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4706) },
                    { 3, "Egyptian philosopher and writer", "فيلسوف وكاتب مصري", null, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4708), true, "Zaki Naguib Mahmoud", "زكي نجيب محمود", "Egyptian", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4708) },
                    { 4, "Nobel Prize-winning Egyptian writer", "كاتب مصري حائز على جائزة نوبل", null, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4710), true, "Naguib Mahfouz", "نجيب محفوظ", "Egyptian", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4711) },
                    { 5, "Lebanese-American writer and poet", "كاتب وشاعر لبناني أمريكي", null, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4712), true, "Khalil Gibran", "جبران خليل جبران", "Lebanese", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4713) }
                });

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

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "IsActive", "Name", "NameArabic", "Phone", "UpdatedAt", "Website" },
                values: new object[,]
                {
                    { 1, "Cairo, Egypt", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4963), "info@daralmaarif.com", true, "Dar Al-Ma'arif", "دار المعارف", "+20123456789", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4963), "www.daralmaarif.com" },
                    { 2, "Cairo, Egypt", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4967), "info@shorouk.com", true, "Dar Al-Shorouk", "دار الشروق", "+20123456790", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4967), "www.shorouk.com" },
                    { 3, "Beirut, Lebanon", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4969), "info@arabicbook.com", true, "Dar Al-Kitab Al-Arabi", "دار الكتاب العربي", "+96112345678", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4970), "www.arabicbook.com" },
                    { 4, "Damascus, Syria", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4972), "info@alfikr.com", true, "Dar Al-Fikr", "دار الفكر", "+96312345678", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(4972), "www.alfikr.com" }
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "Chapter", "CreatedAt", "CreatedBy", "Description", "DescriptionArabic", "Grade", "IsActive", "IsPublic", "PassingScore", "Subject", "TimeLimit", "Title", "TitleArabic", "TotalQuestions", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Algebra", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5143), null, "Basic mathematics quiz for 10th grade students", "كويز أساسيات الرياضيات لطلاب الصف العاشر", "Grade 10", true, true, 60, "Mathematics", 20, "Mathematics Quiz - Grade 10", "كويز الرياضيات - الصف العاشر", 5, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5143) },
                    { 2, "Mechanics", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5147), null, "Physics fundamentals for Tawjihi students", "أساسيات الفيزياء لطلاب التوجيهي", "Tawjihi", true, true, 70, "Physics", 25, "Physics Quiz - Tawjihi", "كويز الفيزياء - التوجيهي", 6, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5147) },
                    { 3, "Atomic Structure", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5150), null, "Chemistry basics for 11th grade students", "أساسيات الكيمياء لطلاب الصف الحادي عشر", "Grade 11", true, true, 75, "Chemistry", 15, "Chemistry Quiz - Grade 11", "كويز الكيمياء - الصف الحادي عشر", 4, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5151) }
                });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SettingValue", "UpdatedAt" },
                values: new object[] { "+962785462983", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5098) });

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
                columns: new[] { "SettingValue", "UpdatedAt" },
                values: new object[] { "ROYAL STUDY", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5102) });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "SettingValue", "UpdatedAt" },
                values: new object[] { "ROYAL STUDY", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5103) });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "SettingValue", "UpdatedAt" },
                values: new object[] { "info@royalstudy.com", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5104) });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "SettingValue", "UpdatedAt" },
                values: new object[] { "+962785462983", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5105) });

            migrationBuilder.InsertData(
                table: "SystemSettings",
                columns: new[] { "Id", "Description", "SettingKey", "SettingValue", "UpdatedAt" },
                values: new object[,]
                {
                    { 7, "Library address", "LibraryAddress", "إربد، الأردن", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5105) },
                    { 8, "Library address in English", "LibraryAddressEnglish", "Irbid, Jordan", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5106) },
                    { 9, "Library currency", "Currency", "د.أ", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5107) },
                    { 10, "Library currency in English", "CurrencyEnglish", "JOD", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5108) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "DescriptionArabic", "ISBN", "IsAvailable", "IsFeatured", "Language", "OriginalPrice", "Pages", "Price", "PublicationDate", "PublisherId", "Rating", "RatingCount", "StockQuantity", "Title", "TitleArabic", "UpdatedAt", "ViewCount" },
                values: new object[] { 1, 5, 6, null, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5046), "A book of 26 prose poetry essays written in English by the Lebanese-American poet and writer Kahlil Gibran", "كتاب يحتوي على 26 مقالة شعرية نثرية كتبها الشاعر والكاتب اللبناني الأمريكي جبران خليل جبران", "978-0-394-71585-9", true, true, "Arabic", 50.00m, null, 45.00m, null, 3, 4.8m, 150, 25, "The Prophet", "النبي", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5047), 500 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "DescriptionArabic", "ISBN", "IsAvailable", "IsNewRelease", "Language", "OriginalPrice", "Pages", "Price", "PublicationDate", "PublisherId", "Rating", "RatingCount", "StockQuantity", "Title", "TitleArabic", "UpdatedAt", "ViewCount" },
                values: new object[] { 2, 2, 1, null, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5053), "An autobiographical novel by Taha Hussein", "رواية سيرة ذاتية لطه حسين", "978-977-02-1234-5", true, true, "Arabic", 40.00m, null, 35.00m, null, 1, 4.5m, 89, 15, "The Days", "الأيام", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5053), 320 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "DescriptionArabic", "ISBN", "IsAvailable", "Language", "OriginalPrice", "Pages", "Price", "PublicationDate", "PublisherId", "Rating", "RatingCount", "StockQuantity", "Title", "TitleArabic", "UpdatedAt", "ViewCount" },
                values: new object[] { 3, 3, 6, null, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5062), "An introduction to the philosophy of science", "مقدمة في فلسفة العلوم", "978-977-09-5678-9", true, "Arabic", 60.00m, null, 55.00m, null, 2, 4.2m, 45, 8, "Philosophy of Science", "فلسفة العلوم", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5062), 180 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "DescriptionArabic", "ISBN", "IsAvailable", "IsFeatured", "Language", "OriginalPrice", "Pages", "Price", "PublicationDate", "PublisherId", "Rating", "RatingCount", "StockQuantity", "Title", "TitleArabic", "UpdatedAt", "ViewCount" },
                values: new object[] { 4, 4, 1, null, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5066), "A novel by Nobel Prize winner Naguib Mahfouz", "رواية للكاتب نجيب محفوظ الحائز على جائزة نوبل", "978-977-02-9012-3", true, true, "Arabic", 45.00m, null, 40.00m, null, 1, 4.7m, 120, 12, "Children of Gebelawi", "أولاد حارتنا", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5067), 450 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CategoryId", "CoverImageUrl", "CreatedAt", "Description", "DescriptionArabic", "ISBN", "IsAvailable", "Language", "OriginalPrice", "Pages", "Price", "PublicationDate", "PublisherId", "Rating", "RatingCount", "StockQuantity", "Title", "TitleArabic", "UpdatedAt", "ViewCount" },
                values: new object[] { 5, 1, 1, null, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5071), "Collection of poems by Ahmed Shawqi", "مجموعة شعرية لأحمد شوقي", "978-963-12-3456-7", true, "Arabic", 35.00m, null, 30.00m, null, 4, 4.3m, 67, 20, "Poetry Collection", "ديوان شعر", new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5071), 250 });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5171), null, null, 1, 1, "What is the value of x in the equation 2x + 5 = 13?", "ما قيمة س في المعادلة 2س + 5 = 13؟", 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5172) },
                    { 2, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5175), null, null, 2, 1, "Solve: 3x - 7 = 8", "حل المعادلة: 3س - 7 = 8", 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5175) }
                });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "Type", "UpdatedAt" },
                values: new object[] { 3, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5177), null, null, 3, 1, "The square root of 16 is 4", "الجذر التربيعي لـ 16 هو 4", 1, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5177) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "UpdatedAt" },
                values: new object[] { 4, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5179), null, null, 4, 1, "What is 15% of 200?", "ما هو 15% من 200؟", 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5180) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "Type", "UpdatedAt" },
                values: new object[] { 5, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5182), null, null, 5, 1, "The sum of angles in a triangle is 180 degrees", "مجموع زوايا المثلث 180 درجة", 1, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5182) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "UpdatedAt" },
                values: new object[] { 6, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5201), null, null, 1, 1, "What is the SI unit of force?", "ما هي وحدة القوة في النظام الدولي؟", 2, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5201) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "Type", "UpdatedAt" },
                values: new object[] { 7, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5203), null, null, 2, 1, "Newton's first law is also known as the law of inertia", "قانون نيوتن الأول يعرف أيضاً بقانون القصور الذاتي", 2, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5204) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "UpdatedAt" },
                values: new object[] { 8, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5205), null, null, 3, 1, "What is the formula for kinetic energy?", "ما هي معادلة الطاقة الحركية؟", 2, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5206) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "Type", "UpdatedAt" },
                values: new object[] { 9, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5208), null, null, 4, 1, "Gravity is a force that pulls objects toward the center of the Earth", "الجاذبية قوة تجذب الأجسام نحو مركز الأرض", 2, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5208) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "UpdatedAt" },
                values: new object[] { 10, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5253), null, null, 5, 1, "What is the unit of power?", "ما هي وحدة القدرة؟", 2, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5253) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "Type", "UpdatedAt" },
                values: new object[] { 11, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5255), null, null, 6, 1, "Velocity is a scalar quantity", "السرعة كمية قياسية", 2, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5256) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "UpdatedAt" },
                values: new object[] { 12, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5275), null, null, 1, 1, "What is the atomic number of hydrogen?", "ما هو العدد الذري للهيدروجين؟", 3, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5275) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "Type", "UpdatedAt" },
                values: new object[] { 13, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5277), null, null, 2, 1, "The nucleus contains protons and neutrons", "النواة تحتوي على البروتونات والنيوترونات", 3, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5277) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "UpdatedAt" },
                values: new object[] { 14, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5279), null, null, 3, 1, "What is the chemical symbol for gold?", "ما هو الرمز الكيميائي للذهب؟", 3, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5279) });

            migrationBuilder.InsertData(
                table: "QuizQuestions",
                columns: new[] { "Id", "CreatedAt", "Explanation", "ExplanationArabic", "OrderIndex", "Points", "QuestionText", "QuestionTextArabic", "QuizId", "Type", "UpdatedAt" },
                values: new object[] { 15, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5281), null, null, 4, 1, "Electrons have a positive charge", "الإلكترونات لها شحنة موجبة", 3, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5282) });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5302), true, "4", "4", 1, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5303) });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5305), "6", "6", 2, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5306) },
                    { 3, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5307), "8", "8", 3, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5308) },
                    { 4, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5310), "10", "10", 4, 1, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5310) }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[] { 5, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5312), true, "5", "5", 1, 2, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5312) });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { 6, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5314), "3", "3", 2, 2, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5314) },
                    { 7, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5316), "7", "7", 3, 2, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5316) },
                    { 8, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5318), "9", "9", 4, 2, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5318) }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[] { 9, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5320), true, "30", "30", 1, 4, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5320) });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { 10, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5322), "25", "25", 2, 4, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5322) },
                    { 11, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5324), "35", "35", 3, 4, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5324) },
                    { 12, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5326), "40", "40", 4, 4, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5326) }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[] { 13, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5345), true, "Newton", "نيوتن", 1, 6, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5346) });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { 14, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5347), "Joule", "جول", 2, 6, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5348) },
                    { 15, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5349), "Watt", "واط", 3, 6, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5350) },
                    { 16, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5352), "Pascal", "باسكال", 4, 6, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5352) }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[] { 17, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5354), true, "KE = 1/2 mv²", "ط ح = 1/2 ك ع²", 1, 8, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5354) });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { 18, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5356), "KE = mv", "ط ح = ك ع", 2, 8, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5356) },
                    { 19, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5358), "KE = mgh", "ط ح = ك ج ع", 3, 8, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5358) },
                    { 20, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5359), "KE = Fd", "ط ح = ق ف", 4, 8, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5360) }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[] { 21, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5361), true, "Watt", "واط", 1, 10, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5362) });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { 22, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5363), "Joule", "جول", 2, 10, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5364) },
                    { 23, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5365), "Newton", "نيوتن", 3, 10, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5366) },
                    { 24, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5367), "Meter", "متر", 4, 10, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5368) }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[] { 25, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5384), true, "1", "1", 1, 12, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5385) });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { 26, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5386), "2", "2", 2, 12, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5387) },
                    { 27, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5388), "3", "3", 3, 12, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5389) },
                    { 28, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5390), "4", "4", 4, 12, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5391) }
                });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "IsCorrect", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[] { 29, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5392), true, "Au", "Au", 1, 14, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5393) });

            migrationBuilder.InsertData(
                table: "QuestionOptions",
                columns: new[] { "Id", "CreatedAt", "OptionText", "OptionTextArabic", "OrderIndex", "QuestionId", "UpdatedAt" },
                values: new object[,]
                {
                    { 30, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5394), "Ag", "Ag", 2, 14, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5395) },
                    { 31, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5396), "Cu", "Cu", 3, 14, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5397) },
                    { 32, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5398), "Fe", "Fe", 4, 14, new DateTime(2025, 8, 7, 10, 43, 17, 764, DateTimeKind.Utc).AddTicks(5399) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswers_AttemptId",
                table: "AttemptAnswers",
                column: "AttemptId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswers_QuestionId",
                table: "AttemptAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttemptAnswers_SelectedOptionId",
                table: "AttemptAnswers",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_IsCorrect",
                table: "QuestionOptions",
                column: "IsCorrect");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_QuizId",
                table: "QuizAttempts",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_StartedAt",
                table: "QuizAttempts",
                column: "StartedAt");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_Status",
                table: "QuizAttempts",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_QuizAttempts_UserId",
                table: "QuizAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_OrderIndex",
                table: "QuizQuestions",
                column: "OrderIndex");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_QuizId",
                table: "QuizQuestions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_CreatedBy",
                table: "Quizzes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_Grade",
                table: "Quizzes",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_IsActive",
                table: "Quizzes",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_Subject",
                table: "Quizzes",
                column: "Subject");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttemptAnswers");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "QuizAttempts");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7153), new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7154) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7164), new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7165) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7167), new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7167) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7169), new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7170) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7171), new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7172) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7173), new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7174) });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "SettingValue", "UpdatedAt" },
                values: new object[] { "+962777279537", new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7331) });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7336));

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "SettingValue", "UpdatedAt" },
                values: new object[] { "مكتبة الكتب", new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7338) });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "SettingValue", "UpdatedAt" },
                values: new object[] { "Books Library", new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7340) });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "SettingValue", "UpdatedAt" },
                values: new object[] { "info@bookslibrary.com", new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7341) });

            migrationBuilder.UpdateData(
                table: "SystemSettings",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "SettingValue", "UpdatedAt" },
                values: new object[] { "+962777279537", new DateTime(2025, 7, 31, 9, 40, 41, 430, DateTimeKind.Utc).AddTicks(7342) });
        }
    }
}
