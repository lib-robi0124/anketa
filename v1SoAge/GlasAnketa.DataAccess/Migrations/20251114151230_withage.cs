using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlasAnketa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class withage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OU2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    WorkExperience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false),
                    QuestionFormId = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_QuestionForms_QuestionFormId",
                        column: x => x.QuestionFormId,
                        principalTable: "QuestionForms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Questions_QuestionTypes_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalTable: "QuestionTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Questions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    QuestionFormId = table.Column<int>(type: "int", nullable: false),
                    ScaleValue = table.Column<int>(type: "int", nullable: true),
                    TextValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnsweredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    WorkExperience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_QuestionForms_QuestionFormId",
                        column: x => x.QuestionFormId,
                        principalTable: "QuestionForms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Answers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "QuestionForms",
                columns: new[] { "Id", "CreatedDate", "Description", "IsActive", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2348), "Overall Satisfaction", true, "Општо задоволство" },
                    { 2, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2352), "Commitment to the Company", true, "Обврска кон компанијата" },
                    { 3, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2353), "Professional Development", true, "Професионален развој" },
                    { 4, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2354), "Work-Life Balance", true, "Рамнотежа помеѓу работата и животот" },
                    { 5, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2355), "Communication and Collaboration", true, "Комуникација и соработка" },
                    { 6, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2356), "Leadership", true, "Лидерство" },
                    { 7, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2357), "Organizational Culture", true, "Организациска култура" },
                    { 8, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2357), "Work Environment", true, "Работна средина" },
                    { 9, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2358), "Rewards and Recognition", true, "Награди и признанија" },
                    { 10, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2359), "Innovation and Changes", true, "Иновации и промени" },
                    { 11, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2360), "Final Evaluation", true, "Конечна евалуација" },
                    { 12, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2361), "Open Questions", true, "Отворени прашања" }
                });

            migrationBuilder.InsertData(
                table: "QuestionTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "1-10 Scale", "Scale" },
                    { 2, "Text Answer", "Text" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "Employee" },
                    { 3, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "CompanyId", "CreatedDate", "FullName", "OU", "OU2", "Password", "RoleId", "WorkExperience" },
                values: new object[,]
                {
                    { 1, 63, 16130, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1272), "Vasko Gjorgiev", "Production", "Rolling Unit", "16130", 2, 42 },
                    { 2, 63, 16684, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1278), "Zoran Stojanovski", "Production", "Rolling Unit", "16684", 2, 0 },
                    { 3, 62, 16695, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1280), "Pane Naskovski", "Production", "Rolling Unit", "16695", 2, 41 },
                    { 4, 62, 16720, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1281), "Tome Trajanov", "Projects and IT", "Crane Maintenance & Internal Transport", "16720", 2, 41 },
                    { 5, 61, 16827, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1283), "Zoran Boshkovski", "Production", "Rolling Unit", "16827", 2, 41 },
                    { 6, 61, 16984, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1284), "Dide Petrovski", "Projects and IT", "Central mechanical maintenance", "16984", 2, 40 },
                    { 7, 61, 17011, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1286), "Jovica Gjorgievski", "Projects and IT", "Crane Maintenance & Internal Transport", "17011", 2, 40 },
                    { 8, 61, 17055, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1288), "Blagica Besarovska", "Projects and IT", "Crane Maintenance & Internal Transport", "17055", 2, 41 },
                    { 9, 62, 17064, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1289), "Dragi Naskovski", "Production", "Coating Unit", "17064", 2, 40 },
                    { 10, 60, 17148, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1290), "Borche Anchevski", "Production", "Coating Unit", "17148", 2, 40 },
                    { 11, 59, 17772, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1292), "Toni Nacev", "HR", "Facility", "17772", 2, 39 },
                    { 12, 59, 17884, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1293), "Valentina Kostovska", "HR", "Human Resources", "17884", 2, 39 },
                    { 13, 61, 17893, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1294), "Zoran Tripunoski", "Production", "Rolling Unit", "17893", 2, 39 },
                    { 14, 61, 17896, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1296), "Zorancho Taseski", "Projects and IT", "Crane Maintenance & Internal Transport", "17896", 2, 39 },
                    { 15, 61, 18158, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1297), "Goran Despodovski", "Production", "Coating Unit", "18158", 2, 38 },
                    { 16, 59, 18162, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1298), "Ljupcho Krstevski", "Production", "Rolling Unit", "18162", 2, 38 },
                    { 17, 60, 18392, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1300), "Sabedin Ljura", "Production", "Coating Unit", "18392", 2, 38 },
                    { 18, 59, 18412, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1301), "Rade Milenkovski", "Production", "Coating Unit", "18412", 2, 38 },
                    { 19, 61, 18471, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1302), "Stojka Koneska", "Supply chain", "Customer service & Logistics", "18471", 2, 38 },
                    { 20, 62, 18529, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1304), "Zharko Nikolovski", "Production", "Rolling Unit", "18529", 2, 38 },
                    { 21, 60, 18533, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1308), "Radica Angelovska", "Projects and IT", "Crane Maintenance & Internal Transport", "18533", 2, 38 },
                    { 22, 58, 18874, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1310), "Borche Trifunovski", "Production", "Coating Unit", "18874", 2, 38 },
                    { 23, 58, 18876, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1311), "Pero Stojanovski", "Production", "Coating Unit", "18876", 2, 38 },
                    { 24, 60, 19370, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1312), "Dragi Petrovski", "Production", "Coating Unit", "19370", 2, 35 },
                    { 25, 59, 19379, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1314), "Ilo Risteski", "Supply chain", "Quality control", "19379", 2, 35 },
                    { 26, 48, 19767, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1315), "Aleksandar Iliev", "Production", "Coating Unit", "19767", 2, 27 },
                    { 27, 57, 19775, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1316), "Mile Popovski", "Production", "Rolling Unit", "19775", 2, 27 },
                    { 28, 45, 19776, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1317), "Dragan Hristovski", "Production", "Coating Unit", "19776", 2, 27 },
                    { 29, 50, 19777, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1319), "Aleksandar Jovchevski", "Production", "Coating Unit", "19777", 2, 27 },
                    { 30, 61, 19779, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1320), "Ljupcho Andovski", "Supply chain", "Quality control", "19779", 2, 27 },
                    { 31, 50, 19782, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1321), "Ivica Stanchevski", "Production", "Rolling Unit", "19782", 2, 27 },
                    { 32, 50, 19784, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1323), "Biljana Ilievska", "Supply chain", "Quality control", "19784", 2, 27 },
                    { 33, 55, 19787, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1324), "Goran Damjanoski", "Supply chain", "Quality control", "19787", 2, 27 },
                    { 34, 46, 19788, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1325), "Boban Neshovski", "Production", "Rolling Unit", "19788", 2, 27 },
                    { 35, 50, 19795, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1326), "Sashe Taparchevski", "Production", "Coating Unit", "19795", 2, 27 },
                    { 36, 49, 19796, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1329), "Igor Ristovski", "Production", "Coating Unit", "19796", 2, 27 },
                    { 37, 49, 19798, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1330), "Ivica Trajkovski", "Production", "Rolling Unit", "19798", 2, 27 },
                    { 38, 62, 19801, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1331), "Vlado Stojanovski", "Production", "Coating Unit", "19801", 2, 27 },
                    { 39, 49, 19804, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1332), "Goran Spirovski", "Production", "Coating Unit", "19804", 2, 27 },
                    { 40, 51, 19806, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1334), "Dejan Velkovski", "Production", "Coating Unit", "19806", 2, 27 },
                    { 41, 55, 19807, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1335), "Stojanche Stefkovski", "Production", "Coating Unit", "19807", 2, 27 },
                    { 42, 54, 19811, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1336), "Dancho Blazheski", "Production", "Rolling Unit", "19811", 2, 27 },
                    { 43, 60, 19813, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1337), "Ljupcho Lozanovski", "Production", "Coating Unit", "19813", 2, 27 },
                    { 44, 49, 19818, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1339), "Marjan Nedelkovski", "Projects and IT", "High voltage", "19818", 2, 27 },
                    { 45, 49, 19820, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1340), "Srgjan Stanojevikj", "Production", "Coating Unit", "19820", 2, 27 },
                    { 46, 47, 19822, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1341), "Dragan Spasevski", "Supply chain", "Quality control", "19822", 2, 27 },
                    { 47, 51, 19823, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1343), "Goran Andonovski", "Projects and IT", "Crane Maintenance & Internal Transport", "19823", 2, 27 },
                    { 48, 48, 19827, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1344), "Goran Anchovski", "Supply chain", "Planning & Strategy", "19827", 2, 27 },
                    { 49, 47, 19833, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1345), "Igor Mircheski", "Supply chain", "Stores", "19833", 2, 24 },
                    { 50, 51, 19834, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1374), "Goran Nikolovski", "HR", "Facility", "19834", 2, 27 },
                    { 51, 48, 19838, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1375), "Petar Moskov", "Production", "Rolling Unit", "19838", 2, 27 },
                    { 52, 52, 19840, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1377), "Goran Stojanovski", "Supply chain", "Planning & Strategy", "19840", 2, 27 },
                    { 53, 50, 19841, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1378), "Igor Petkovski", "Projects and IT", "Crane Maintenance & Internal Transport", "19841", 2, 27 },
                    { 54, 54, 19842, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1380), "Nenad Mitrovikj", "Production", "Coating Unit", "19842", 2, 27 },
                    { 55, 55, 19844, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1381), "Sashko Gjorgjievski", "Supply chain", "Quality control", "19844", 2, 27 },
                    { 56, 49, 19845, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1382), "Nikola Toshevski", "Production", "Coating Unit", "19845", 2, 27 },
                    { 57, 59, 19848, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1384), "Slobodan Velkovski", "Production", "Coating Unit", "19848", 2, 27 },
                    { 58, 48, 19849, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1386), "Goce Jankulovski", "Supply chain", "Planning & Strategy", "19849", 2, 27 },
                    { 59, 56, 19868, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1388), "Marjan Milovanovikj", "Production", "Rolling Unit", "19868", 2, 25 },
                    { 60, 55, 19877, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1389), "Goran Gavrilovski", "Sales", "Sales", "19877", 2, 27 },
                    { 61, 55, 19892, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1390), "Irfan Feratovski", "Production", "Rolling Unit", "19892", 2, 36 },
                    { 62, 54, 19899, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1392), "Igor Krpachovski", "Projects and IT", "Projects, progress and IT", "19899", 2, 27 },
                    { 63, 48, 19911, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1393), "Aleksandar Spasevski", "CEO office", "Health, Safety and Environment", "19911", 2, 26 },
                    { 64, 62, 19916, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1394), "Nevaip Bardi", "Production", "Rolling Unit", "19916", 2, 26 },
                    { 65, 52, 19917, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1397), "Biljana Stoshikj", "Supply chain", "Customer service & Logistics", "19917", 2, 26 },
                    { 66, 57, 19933, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1398), "Svetlana Jovanova", "Finance Department", "Financial Controlling and Reporting", "19933", 2, 26 },
                    { 67, 56, 19960, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1399), "Draganche Taleski", "Production", "Rolling Unit", "19960", 2, 26 },
                    { 68, 55, 19963, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1400), "Toni Naumovski", "Production", "Coating Unit", "19963", 2, 26 },
                    { 69, 54, 19992, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1402), "Metodi Gievski", "Projects and IT", "Projects", "19992", 2, 27 },
                    { 70, 59, 19993, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1403), "Jovica Velkovski", "Supply chain", "Customer service & Logistics", "19993", 2, 27 },
                    { 71, 54, 19997, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1404), "Gordana Astardjieva", "Projects and IT", "High voltage", "19997", 2, 27 },
                    { 72, 51, 20023, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1406), "Zharko Ivanovski", "Production", "Coating Unit", "20023", 2, 26 },
                    { 73, 51, 20024, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1407), "Igorche Janev", "Production", "Rolling Unit", "20024", 2, 26 },
                    { 74, 58, 20033, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1408), "Nikola Panov", "Supply chain", "Quality control", "20033", 2, 26 },
                    { 75, 47, 20034, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1410), "Sasho Mitkovski", "Production", "Coating Unit", "20034", 2, 26 },
                    { 76, 50, 20038, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1411), "Goran Ilievski", "Production", "Rolling Unit", "20038", 2, 27 },
                    { 77, 53, 20041, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1412), "Kircho Merdjanovski", "Projects and IT", "Automation", "20041", 2, 26 },
                    { 78, 47, 20052, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1413), "Davor Zdravkovski", "Supply chain", "Customer service & Logistics", "20052", 2, 26 },
                    { 79, 55, 20072, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1416), "Gorancho Petkovski", "Production", "Quality control", "20072", 2, 25 },
                    { 80, 49, 20076, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1417), "Sashko Cvetanovski", "Production", "Coating Unit", "20076", 2, 25 },
                    { 81, 45, 20095, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1418), "Ilija Tashevski", "Production", "Crane Maintenance & Internal Transport", "20095", 2, 26 },
                    { 82, 51, 20117, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1419), "Kire Stefanoski", "Production", "Rolling Unit", "20117", 2, 26 },
                    { 83, 46, 20125, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1421), "Aleksandar Evremov", "Supply chain", "Coating Unit", "20125", 2, 26 },
                    { 84, 51, 20127, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1422), "Ratko Trajkovski", "Supply chain", "Crane Maintenance & Internal Transport", "20127", 2, 26 },
                    { 85, 46, 20128, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1423), "Goran Miovski", "Projects and IT", "Quality control", "20128", 2, 23 },
                    { 86, 53, 20131, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1424), "Goran Trajkovski", "Production", "Rolling Unit", "20131", 2, 26 },
                    { 87, 55, 20137, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1426), "Gordana Shegmanovikj", "HR", "Facility", "20137", 2, 26 },
                    { 88, 48, 20144, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1428), "Igorche Bogdanovski", "Production", "Coating Unit", "20144", 2, 26 },
                    { 89, 51, 20152, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1429), "Miodrag Petkovikj", "Production", "Maintenance Progress", "20152", 2, 26 },
                    { 90, 54, 20159, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1430), "Gorancho Najdovski", "Projects and IT", "Coating Unit", "20159", 2, 26 },
                    { 91, 48, 20160, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1431), "Dejan Jazadjiev", "Supply chain", "Coating Unit", "20160", 2, 26 },
                    { 92, 53, 20162, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1433), "Sashko Peshov", "Production", "Crane Maintenance & Internal Transport", "20162", 2, 26 },
                    { 93, 61, 20163, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1434), "Kiro Radevski", "Production", "Rolling Unit", "20163", 2, 26 },
                    { 94, 51, 20167, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1436), "Sasho Beroski", "Production", "Coating Unit", "20167", 2, 25 },
                    { 95, 47, 20168, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1437), "Vlatko Changovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20168", 2, 25 },
                    { 96, 52, 20178, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1439), "Stojan Stavreski", "Projects and IT", "Crane Maintenance & Internal Transport", "20178", 2, 25 },
                    { 97, 46, 20182, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1440), "Kjemalj Abazi", "Production", "Coating Unit", "20182", 2, 25 },
                    { 98, 60, 20184, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1441), "Djevat Selimi", "Production", "Rolling Unit", "20184", 2, 25 },
                    { 99, 50, 20191, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1443), "Trajche Dimovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20191", 2, 25 },
                    { 100, 56, 20195, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1444), "Robert Shijakovski", "Production", "Rolling Unit", "20195", 2, 25 },
                    { 101, 49, 20203, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1471), "Slavisha Crnichin", "Production", "Coating Unit", "20203", 2, 25 },
                    { 102, 52, 20210, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1473), "Borche Cvetkovski", "Supply chain", "Customer service & Logistics", "20210", 2, 25 },
                    { 103, 57, 20212, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1474), "Sasha Stefanoski", "Supply chain", "Customer service & Logistics", "20212", 2, 25 },
                    { 104, 53, 20218, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1475), "Josif Slavkovski", "Production", "Central mechanical maintenance", "20218", 2, 25 },
                    { 105, 53, 20225, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1477), "Goce Stojchevski", "Projects and IT", "Customer service & Logistics", "20225", 2, 25 },
                    { 106, 61, 20226, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1478), "Donche Nedelkovski", "Production", "Quality control", "20226", 2, 25 },
                    { 107, 50, 20231, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1479), "Ljupcho Shegmanovikj", "Production", "Coating Unit", "20231", 2, 25 },
                    { 108, 47, 20232, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1482), "Goran Markovski", "Production", "Stores", "20232", 2, 25 },
                    { 109, 48, 20233, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1483), "Dragan Markovski", "Projects and IT", "Customer service & Logistics", "20233", 2, 25 },
                    { 110, 59, 20234, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1484), "Ljupcho Veljanovski", "Production", "Coating Unit", "20234", 2, 25 },
                    { 111, 45, 20235, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1486), "Nikola Angeleski", "Production", "Coating Unit", "20235", 2, 25 },
                    { 112, 49, 20236, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1487), "Aleksandar Bogoev", "Production", "Customer service & Logistics", "20236", 2, 25 },
                    { 113, 53, 20238, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1488), "Stevche Velkovski", "Projects and IT", "Facility", "20238", 2, 25 },
                    { 114, 48, 20245, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1490), "Ljubomir Kochovski", "Projects and IT", "Rolling Unit", "20245", 2, 9 },
                    { 115, 51, 20246, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1491), "Sashko Blazhevski", "Production", "Crane Maintenance & Internal Transport", "20246", 2, 25 },
                    { 116, 50, 20248, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1492), "Zoranche Borizovski", "Production", "Quality control", "20248", 2, 25 },
                    { 117, 49, 20253, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1494), "Nebojsha Stojmanovikj", "Projects and IT", "Rolling Unit", "20253", 2, 25 },
                    { 118, 46, 20255, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1495), "Ljupcho Pashoski", "Production", "Coating Unit", "20255", 2, 24 },
                    { 119, 47, 20261, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1496), "Aleksandar Karajanovski", "Production", "Crane Maintenance & Internal Transport", "20261", 2, 25 },
                    { 120, 48, 20262, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1498), "Dejan Stojanov", "Supply chain", "Rolling Unit", "20262", 2, 25 },
                    { 121, 51, 20263, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1499), "Vladimir Jakimov", "Projects and IT", "Coating Unit", "20263", 2, 25 },
                    { 122, 54, 20267, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1500), "Goranche Ginoski", "Production", "Coating Unit", "20267", 2, 25 },
                    { 123, 62, 20271, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1503), "Avdil Mustafa", "Production", "Customer service & Logistics", "20271", 2, 25 },
                    { 124, 59, 20272, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1504), "Beta Damevska", "Projects and IT", "Crane Maintenance & Internal Transport", "20272", 2, 25 },
                    { 125, 56, 20275, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1505), "Naser Ilazov", "Production", "Rolling Unit", "20275", 2, 25 },
                    { 126, 46, 20280, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1506), "Viktor Boshkovski", "Production", "Planning & Strategy", "20280", 2, 25 },
                    { 127, 56, 20283, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1508), "Zlatko Petrovski", "Supply chain", "Rolling Unit", "20283", 2, 25 },
                    { 128, 48, 20284, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1509), "Aleksandar Stoicev", "Production", "Coating Unit", "20284", 2, 25 },
                    { 129, 48, 20286, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1510), "Aleksandar Jovanovski", "Production", "Coating Unit", "20286", 2, 25 },
                    { 130, 53, 20296, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1512), "Goran Jovanovski", "Production", "Coating Unit", "20296", 2, 25 },
                    { 131, 47, 20297, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1513), "Ivan Maslov", "Projects and IT", "Coating Unit", "20297", 2, 25 },
                    { 132, 45, 20298, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1514), "Ivica Tripunovski", "Supply chain", "Coating Unit", "20298", 2, 25 },
                    { 133, 59, 20299, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1515), "Milisav Boshkovikj", "Production", "Coating Unit", "20299", 2, 25 },
                    { 134, 48, 20300, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1517), "Ilija Pandurski", "Production", "Coating Unit", "20300", 2, 25 },
                    { 135, 47, 20308, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1518), "Ljuben Trajkoski", "Supply chain", "Customer service & Logistics", "20308", 2, 25 },
                    { 136, 49, 20316, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1519), "Igor Petrovski", "Projects and IT", "Purchasing", "20316", 2, 25 },
                    { 137, 47, 20320, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1522), "Brankica Trajanoska", "Supply chain", "Crane Maintenance & Internal Transport", "20320", 2, 25 },
                    { 138, 51, 20323, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1523), "Blazhe Dimov", "Projects and IT", "Customer service & Logistics", "20323", 2, 25 },
                    { 139, 47, 20324, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1524), "Biljana Petrovska", "Supply chain", "Customer service & Logistics", "20324", 2, 25 },
                    { 140, 60, 20325, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1526), "Zoran Trajkov", "Projects and IT", "Central mechanical maintenance", "20325", 2, 25 },
                    { 141, 62, 20328, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1527), "Slavko Spasovski", "HR", "Union", "20328", 2, 25 },
                    { 142, 49, 20332, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1528), "Igorche Gjorgjievski", "Projects and IT", "Crane Maintenance & Internal Transport", "20332", 2, 25 },
                    { 143, 55, 20335, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1529), "Jovica Boshkovski", "Production", "Rolling Unit", "20335", 2, 25 },
                    { 144, 47, 20341, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1531), "Metodija Blazhevski", "Supply chain", "Customer service & Logistics", "20341", 2, 9 },
                    { 145, 56, 20350, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1532), "Sasho Petrushevski", "Supply chain", "Customer service & Logistics", "20350", 2, 25 },
                    { 146, 50, 20351, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1533), "Marjan Stojanovski", "Supply chain", "Customer service & Logistics", "20351", 2, 25 },
                    { 147, 52, 20357, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1535), "Dejan Petrushevski", "Sales", "Sales", "20357", 2, 25 },
                    { 148, 53, 20362, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1536), "Sasho Kiprijanovski", "Supply chain", "Quality control", "20362", 2, 25 },
                    { 149, 48, 20363, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1537), "Zoran Mitevski", "Production", "Coating Unit", "20363", 2, 25 },
                    { 150, 55, 20372, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1539), "Sasho Gjorgjievski", "Production", "Stores", "20372", 2, 25 },
                    { 151, 56, 20380, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1540), "Orce Angelovski", "Projects and IT", "Customer service & Logistics", "20380", 2, 25 },
                    { 152, 48, 20381, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1590), "Dejan Danilovski", "Supply chain", "Coating Unit", "20381", 2, 25 },
                    { 153, 50, 20382, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1591), "Robert Angelovski", "Projects and IT", "Coating Unit", "20382", 2, 25 },
                    { 154, 59, 20385, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1593), "Bratislav Mihajlovikj", "Supply chain", "Customer service & Logistics", "20385", 2, 25 },
                    { 155, 57, 20387, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1594), "Ace Mitevski", "Supply chain", "Facility", "20387", 2, 25 },
                    { 156, 51, 20389, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1596), "Igorche Markovski", "Production", "Rolling Unit", "20389", 2, 25 },
                    { 157, 52, 20390, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1597), "Jovan Markovski", "Supply chain", "Crane Maintenance & Internal Transport", "20390", 2, 25 },
                    { 158, 51, 20393, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1598), "Boban Hristovski", "Supply chain", "Quality control", "20393", 2, 25 },
                    { 159, 51, 20395, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1600), "Dejan Dimishkovski", "Production", "Rolling Unit", "20395", 2, 25 },
                    { 160, 53, 20397, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1601), "Marjan Simonovski", "Production", "Coating Unit", "20397", 2, 25 },
                    { 161, 60, 20402, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1602), "Slavica Mladenovska", "Supply chain", "High voltage", "20402", 2, 25 },
                    { 162, 61, 20431, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1604), "Angelina Rajovska", "HR", "Quality control", "20431", 2, 25 },
                    { 163, 46, 20439, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1605), "Dejan Dilevski", "Production", "Rolling Unit", "20439", 2, 25 },
                    { 164, 52, 20443, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1606), "Aleksandar Zotikj", "Supply chain", "Crane Maintenance & Internal Transport", "20443", 2, 24 },
                    { 165, 47, 20445, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1608), "Jovica Maznevski", "Production", "Quality control", "20445", 2, 25 },
                    { 166, 53, 20447, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1610), "Zvonko Neshkovikj", "Production", "Rolling Unit", "20447", 2, 25 },
                    { 167, 57, 20448, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1611), "Ljupcho Paunkov", "Production", "Coating Unit", "20448", 2, 25 },
                    { 168, 48, 20449, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1613), "Marjan Zdravkovski", "Production", "Crane Maintenance & Internal Transport", "20449", 2, 25 },
                    { 169, 49, 20451, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1614), "Igor Jordanovski", "Production", "Rolling Unit", "20451", 2, 25 },
                    { 170, 47, 20453, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1615), "Aleksandar Stamchevski", "Production", "Coating Unit", "20453", 2, 25 },
                    { 171, 51, 20454, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1617), "Dejan Vasilevski", "Production", "Crane Maintenance & Internal Transport", "20454", 2, 25 },
                    { 172, 44, 20459, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1618), "Zoran Stojanovski", "Production", "Rolling Unit", "20459", 2, 23 },
                    { 173, 63, 20466, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1619), "Iljaz Prekopuca", "Production", "Coating Unit", "20466", 2, 25 },
                    { 174, 56, 20468, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1621), "Fadil Tanalari", "Production", "Crane Maintenance & Internal Transport", "20468", 2, 25 },
                    { 175, 56, 20471, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1622), "Trajche Petrovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20471", 2, 23 },
                    { 176, 51, 20475, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1624), "Boban Mitrovikj", "Production", "Rolling Unit", "20475", 2, 23 },
                    { 177, 43, 20478, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1625), "Dragan Saveski", "Production", "Coating Unit", "20478", 2, 22 },
                    { 178, 52, 20489, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1626), "Ajdin Zulfiovski", "Production", "Coating Unit", "20489", 2, 8 },
                    { 179, 61, 20511, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1627), "Ivan Cibrev", "Supply chain", "Stores", "20511", 2, 25 },
                    { 180, 53, 20518, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1629), "Slavica Jovchevska", "Supply chain", "Customer service & Logistics", "20518", 2, 23 },
                    { 181, 60, 20521, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1631), "Elena Damchevska", "Finance Department", "Accounting & Treasury", "20521", 2, 21 },
                    { 182, 59, 20523, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1632), "Vesna Dimevska", "Supply chain", "Quality control", "20523", 2, 22 },
                    { 183, 53, 20527, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1634), "Kiril Simonoski", "Projects and IT", "Maintenance Progress", "20527", 2, 18 },
                    { 184, 49, 20530, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1635), "Goce Atanasoski", "Projects and IT", "Maintenance Progress", "20530", 2, 18 },
                    { 185, 50, 20603, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1636), "Goran Stojchevski", "Production", "Coating Unit", "20603", 2, 19 },
                    { 186, 56, 20621, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1637), "Todorka Ristovska", "CEO office", "CEO office", "20621", 2, 19 },
                    { 187, 45, 20623, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1639), "Elena Blazeva", "Finance Department", "Finance Department", "20623", 2, 18 },
                    { 188, 53, 20625, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1640), "Darko Najdenov", "Supply chain", "Purchasing", "20625", 2, 18 },
                    { 189, 46, 20632, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1641), "Zoran Mladenovski", "Projects and IT", "Projects, progress and IT", "20632", 2, 18 },
                    { 190, 52, 20636, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1643), "Natalija Nikoloska", "Supply chain", "Quality control", "20636", 2, 18 },
                    { 191, 49, 20637, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1644), "Aleksandar Krstev", "Supply chain", "Planning & Strategy", "20637", 2, 18 },
                    { 192, 49, 20638, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1645), "Elena Kocevska Peceva", "Supply chain", "Quality control", "20638", 2, 18 },
                    { 193, 48, 20640, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1646), "Kiro Risteski", "Production", "Production", "20640", 2, 16 },
                    { 194, 46, 20650, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1648), "Dejana Jovanova Krsteva", "Supply chain", "Supply chain", "20650", 2, 16 },
                    { 195, 48, 20652, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1649), "Toni Pandilovski", "Projects and IT", "Automation", "20652", 2, 18 },
                    { 196, 49, 20662, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1651), "Vladimir Shulevski", "Production", "Coating Unit", "20662", 2, 17 },
                    { 197, 41, 20675, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1652), "Dejan Trajkovski", "HR", "Internal Control", "20675", 2, 17 },
                    { 198, 43, 20678, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1654), "Kire Blagoeski", "Supply chain", "Planning & Strategy", "20678", 2, 17 },
                    { 199, 46, 20685, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1655), "Petar Brashnarov", "Production", "Coating Unit", "20685", 2, 17 },
                    { 200, 57, 20694, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1656), "Zvonimir Manchevski", "Production", "Rolling Unit", "20694", 2, 25 },
                    { 201, 46, 20695, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1658), "Aleksandar Dejanovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20695", 2, 25 },
                    { 202, 51, 20707, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1659), "Selaedin Feratovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20707", 2, 25 },
                    { 203, 45, 20708, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1686), "Slave Manevski", "Projects and IT", "Information technology", "20708", 1, 25 },
                    { 204, 47, 20723, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1687), "Djevat Saliovski", "Production", "Rolling Unit", "20723", 2, 11 },
                    { 205, 44, 20724, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1689), "Vesna Velichkovska", "HR", "Human Resources and Legal Affairs", "20724", 1, 11 },
                    { 206, 46, 20729, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1690), "Vlatko Dimishkovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20729", 2, 11 },
                    { 207, 58, 20734, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1691), "Blage Uroshevski", "Production", "Coating Unit", "20734", 2, 11 },
                    { 208, 62, 20735, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1693), "Stojadin Jankovski", "Production", "Rolling Unit", "20735", 2, 11 },
                    { 209, 53, 20737, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1694), "Zlatko Nikoloski", "Production", "Rolling Unit", "20737", 2, 11 },
                    { 210, 43, 20747, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1700), "Goce Gjorgjievski", "Production", "Rolling Unit", "20747", 2, 11 },
                    { 211, 34, 20751, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1701), "Stefan Tonevski", "Supply chain", "Quality control", "20751", 2, 11 },
                    { 212, 42, 20753, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1702), "Orce Dimovski", "Production", "Rolling Unit", "20753", 2, 11 },
                    { 213, 35, 20758, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1703), "Elena Valkancheva Najdenova", "Sales", "Sales", "20758", 2, 11 },
                    { 214, 44, 20776, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1705), "Igor Dushanovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20776", 2, 10 },
                    { 215, 38, 20779, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1706), "Jagoda Velevska", "CEO office", "Internal Audit", "20779", 2, 9 },
                    { 216, 45, 20781, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1707), "Vladimir Nikolikj", "Supply chain", "Quality control", "20781", 2, 9 },
                    { 217, 42, 20784, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1709), "Dejan Gocevski", "Production", "Coating Unit", "20784", 2, 9 },
                    { 218, 34, 20787, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1710), "Aleksandar Kostovski", "Production", "Rolling Unit", "20787", 2, 9 },
                    { 219, 41, 20797, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1711), "Cane Nikoloski", "Production", "Automation", "20797", 2, 9 },
                    { 220, 33, 20800, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1713), "Viktor Stamenkovski", "Projects and IT", "Quality control", "20800", 2, 9 },
                    { 221, 36, 20801, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1714), "Dragana Petrovikj", "Supply chain", "Quality control", "20801", 2, 9 },
                    { 222, 30, 20802, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1715), "Stefan Despodovski", "Supply chain", "Central mechanical maintenance", "20802", 2, 9 },
                    { 223, 50, 20803, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1716), "Marjan Milanovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20803", 2, 9 },
                    { 224, 63, 20804, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1718), "Dragan Koneski", "Projects and IT", "Coating Unit", "20804", 2, 9 },
                    { 225, 36, 20814, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1720), "Aleksandar Stojanovski", "Production", "Coating Unit", "20814", 2, 9 },
                    { 226, 48, 20815, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1721), "Sashko Miloshevski", "Production", "Crane Maintenance & Internal Transport", "20815", 2, 9 },
                    { 227, 53, 20822, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1723), "Elza Petrovska", "Sales", "Rolling Unit", "20822", 2, 6 },
                    { 228, 47, 20824, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1724), "Darko Zdravkovski", "Production", "Coating Unit", "20824", 2, 8 },
                    { 229, 49, 20825, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1725), "Kiril Chirkov", "Production", "Crane Maintenance & Internal Transport", "20825", 2, 8 },
                    { 230, 49, 20827, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1726), "Igor Cvetanoski", "Production", "Rolling Unit", "20827", 2, 8 },
                    { 231, 39, 20831, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1728), "Martin Nikolovski", "Production", "Coating Unit", "20831", 2, 8 },
                    { 232, 42, 20832, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1729), "Dushko Blazevski", "Supply chain", "Coating Unit", "20832", 2, 8 },
                    { 233, 29, 20834, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1730), "Muammet Sali", "Projects and IT", "Crane Maintenance & Internal Transport", "20834", 2, 8 },
                    { 234, 29, 20835, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1732), "Kristijan Janev", "Projects and IT", "Rolling Unit", "20835", 2, 8 },
                    { 235, 37, 20837, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1733), "Dilaver Sali", "Production", "Coating Unit", "20837", 2, 8 },
                    { 236, 42, 20838, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1734), "Sasho Neshkov", "Production", "Crane Maintenance & Internal Transport", "20838", 2, 8 },
                    { 237, 48, 20839, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1735), "Goran Ilikj", "Production", "Rolling Unit", "20839", 2, 8 },
                    { 238, 50, 20842, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1737), "Zoran Trendevski", "Production", "Customer service & Logistics", "20842", 2, 8 },
                    { 239, 51, 20844, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1739), "Igor Lazevski", "Production", "Coating Unit", "20844", 2, 8 },
                    { 240, 36, 20847, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1740), "Dragan Dragutinovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20847", 2, 8 },
                    { 241, 50, 20848, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1742), "Hristo Jovanovski", "Production", "Rolling Unit", "20848", 2, 8 },
                    { 242, 40, 20851, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1743), "Goran Moskov", "Production", "Coating Unit", "20851", 2, 8 },
                    { 243, 46, 20852, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1744), "Zoran Nikolovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20852", 2, 8 },
                    { 244, 38, 20855, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1746), "Goran Cvetanovski", "Production", "Rolling Unit", "20855", 2, 8 },
                    { 245, 45, 20866, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1747), "Pero Mangarov", "Supply chain", "Customer service & Logistics", "20866", 2, 7 },
                    { 246, 47, 20871, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1748), "Igor Blazevski", "Production", "Coating Unit", "20871", 2, 7 },
                    { 247, 36, 20872, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1750), "Spase Belinski", "Projects and IT", "Crane Maintenance & Internal Transport", "20872", 2, 7 },
                    { 248, 42, 20876, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1751), "Zhivorad Arsenovski", "Production", "Rolling Unit", "20876", 2, 7 },
                    { 249, 52, 20879, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1752), "Ljupcho Pijakovski", "Production", "Coating Unit", "20879", 2, 7 },
                    { 250, 37, 20883, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1753), "Ljupcho Dimitrijeski", "Projects and IT", "Crane Maintenance & Internal Transport", "20883", 2, 7 },
                    { 251, 43, 20889, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1755), "Dushan Jovanoski", "Sales", "Sales", "20889", 2, 11 },
                    { 252, 40, 20893, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1756), "Nikola Nikolovski", "Sales", "Sales", "20893", 2, 17 },
                    { 253, 42, 20894, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1757), "Dimitar Jankovski", "Production", "Rolling Unit", "20894", 2, 7 },
                    { 254, 54, 20896, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1789), "Imer Ljusjani", "Projects and IT", "Crane Maintenance & Internal Transport", "20896", 2, 7 },
                    { 255, 39, 20898, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1790), "Bobi Gjogjievski", "Projects and IT", "Crane Maintenance & Internal Transport", "20898", 2, 7 },
                    { 256, 26, 20899, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1792), "Hristijan Gjorgjevski", "Production", "Rolling Unit", "20899", 2, 7 },
                    { 257, 38, 20903, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1793), "Temelko Sarovski", "Production", "Quality control", "20903", 2, 7 },
                    { 258, 32, 20910, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1794), "Hristijan Simonovski", "Supply chain", "Rolling Unit", "20910", 2, 7 },
                    { 259, 33, 20911, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1796), "Dame Kekenovski", "Production", "Coating Unit", "20911", 2, 7 },
                    { 260, 44, 20914, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1797), "Afrim Jusufi", "Production", "Planning & Strategy", "20914", 2, 7 },
                    { 261, 47, 20915, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1798), "Igor Damjanovski", "Supply chain", "Crane Maintenance & Internal Transport", "20915", 2, 7 },
                    { 262, 41, 20916, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1800), "Besnik Ibraimi", "Projects and IT", "Quality control", "20916", 2, 7 },
                    { 263, 43, 20917, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1801), "Viktor Velichkovski", "Production", "Coating Unit", "20917", 2, 7 },
                    { 264, 40, 20919, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1802), "Robert Jovanovski", "Projects and IT", "Maintenance Progress", "20919", 2, 6 },
                    { 265, 29, 20920, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1804), "Adnan Feratovski", "Projects and IT", "Coating Unit", "20920", 2, 6 },
                    { 266, 52, 20924, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1805), "Biljana Chorobenska", "Supply chain", "Crane Maintenance & Internal Transport", "20924", 2, 6 },
                    { 267, 31, 20927, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1806), "Vladan Trajkovski", "Projects and IT", "Rolling Unit", "20927", 2, 4 },
                    { 268, 26, 20928, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1809), "Vlatko Mitevski", "Production", "Crane Maintenance & Internal Transport", "20928", 2, 6 },
                    { 269, 28, 20935, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1810), "Adis Nezirovski", "Projects and IT", "Coating Unit", "20935", 2, 4 },
                    { 270, 28, 20936, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1811), "Asim Nezirovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20936", 2, 6 },
                    { 271, 55, 20937, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1813), "Goce Spaseski", "Production", "Rolling Unit", "20937", 2, 6 },
                    { 272, 41, 20942, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1814), "Dragi Ickovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20942", 2, 6 },
                    { 273, 27, 20944, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1815), "Ibrahim Mujovikj", "Production", "Rolling Unit", "20944", 2, 6 },
                    { 274, 47, 20948, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1817), "Boban Grozdanovski", "Projects and IT", "Quality control", "20948", 2, 6 },
                    { 275, 27, 20951, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1818), "Robert Stojanovikj", "Projects and IT", "Rolling Unit", "20951", 2, 6 },
                    { 276, 33, 20953, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1819), "Mihajlo Zafirovikj", "Production", "Coating Unit", "20953", 2, 6 },
                    { 277, 41, 20955, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1821), "Aleksandra Trgachevska", "Supply chain", "Central mechanical maintenance", "20955", 2, 6 },
                    { 278, 55, 20958, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1822), "Marjanche Ristovski", "Production", "Customer service & Logistics", "20958", 2, 6 },
                    { 279, 35, 20963, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1823), "Dalibor Cvetkovski", "Production", "Automation", "20963", 2, 4 },
                    { 280, 40, 20964, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1825), "Ivica Stanoeski", "Projects and IT", "Rolling Unit", "20964", 2, 6 },
                    { 281, 47, 20967, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1826), "Gjorgji Velichkovski", "Supply chain", "Customer service & Logistics", "20967", 2, 6 },
                    { 282, 47, 20968, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1827), "Karanfilka Giceva", "Supply chain", "Rolling Unit", "20968", 2, 6 },
                    { 283, 56, 20971, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1829), "Djevat Feratovski", "Production", "Planning & Strategy", "20971", 2, 6 },
                    { 284, 26, 20973, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1831), "Ivan Mitodevski", "Production", "Coating Unit", "20973", 2, 6 },
                    { 285, 53, 20975, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1832), "Robert Ristovski", "Projects and IT", "Maintenance Progress", "20975", 1, 20 },
                    { 286, 34, 20977, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1833), "Vlatko Dimevski", "Supply chain", "Coating Unit", "20977", 2, 6 },
                    { 287, 53, 20979, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1835), "Violeta Vidinska", "HR", "Coating Unit", "20979", 2, 5 },
                    { 288, 52, 20981, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1836), "Aco Jovanovski", "Projects and IT", "Coating Unit", "20981", 2, 5 },
                    { 289, 47, 20982, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1837), "Rade Panovski", "Production", "Coating Unit", "20982", 2, 5 },
                    { 290, 45, 20983, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1838), "Slave Joshovikj", "Production", "Coating Unit", "20983", 2, 5 },
                    { 291, 48, 20988, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1840), "Nenad Petkovikj", "Projects and IT", "Maintenance Progress", "20988", 2, 5 },
                    { 292, 56, 20989, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1841), "Borche Livrinski", "Projects and IT", "Coating Unit", "20989", 2, 5 },
                    { 293, 33, 20994, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1842), "Sanja Lambrinidis", "Supply chain", "Rolling Unit", "20994", 2, 5 },
                    { 294, 52, 20998, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1844), "Ace Jovanovski", "Production", "Rolling Unit", "20998", 2, 5 },
                    { 295, 44, 20999, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1845), "Sashe Smilkovski", "Projects and IT", "Rolling Unit", "20999", 2, 5 },
                    { 296, 25, 21002, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1846), "Leon Danilovski", "Supply chain", "Automation", "21002", 2, 5 },
                    { 297, 37, 21003, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1849), "Enis Zekjiri", "Projects and IT", "Coating Unit", "21003", 2, 5 },
                    { 298, 57, 21006, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1850), "Metodija Malkov", "Production", "Rolling Unit", "21006", 2, 5 },
                    { 299, 33, 21010, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1851), "Stefan Risteski", "Projects and IT", "Customer service & Logistics", "21010", 2, 5 },
                    { 300, 50, 21012, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1852), "Igor Momchilovski", "Production", "Rolling Unit", "21012", 2, 5 },
                    { 301, 47, 21016, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1854), "Oliver Govedarovski", "Projects and IT", "Planning & Strategy", "21016", 2, 5 },
                    { 302, 52, 21017, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1856), "Bobi Nikolovski", "Projects and IT", "Coating Unit", "21017", 2, 5 },
                    { 303, 30, 21020, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1857), "Kristijan Stojanovski", "Supply chain", "Maintenance Progress", "21020", 2, 5 },
                    { 304, 58, 21082, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1859), "Rufat Rufati", "Production", "Coating Unit", "21082", 2, 4 },
                    { 305, 53, 21090, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1860), "Vase Pecevski", "Projects and IT", "Coating Unit", "21090", 2, 4 },
                    { 306, 32, 21094, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1887), "Kristina Karajanovska", "Sales", "Coating Unit", "21094", 2, 4 },
                    { 307, 57, 21095, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1888), "Srechko Vidinski", "Production", "Coating Unit", "21095", 2, 4 },
                    { 308, 42, 21096, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1890), "Nikola Spasevski", "Projects and IT", "Crane Maintenance & Internal Transport", "21096", 2, 4 },
                    { 309, 52, 21097, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1891), "Zvonko Miloshoski", "Supply chain", "Quality control", "21097", 2, 4 },
                    { 310, 45, 21100, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1892), "Ismail Redzepi", "Production", "Rolling Unit", "21100", 2, 4 },
                    { 311, 37, 21104, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1894), "Aleksandar Kekenovski", "Production", "Health, Safety and Environment", "21104", 2, 4 },
                    { 312, 24, 21112, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1896), "Nikola Nikolovski", "Production", "Financial Controlling and Reporting", "21112", 2, 4 },
                    { 313, 33, 21117, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1898), "Jovica Stojanovikj", "Production", "Coating Unit", "21117", 2, 4 },
                    { 314, 33, 21119, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1899), "Vasil Kocevski", "Production", "Rolling Unit", "21119", 2, 4 },
                    { 315, 52, 21121, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1900), "Petre Kushinovski", "Production", "Coating Unit", "21121", 2, 4 },
                    { 316, 56, 21125, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1901), "Mitko Lebamov", "Projects and IT", "Crane Maintenance & Internal Transport", "21125", 2, 4 },
                    { 317, 27, 21126, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1903), "Aleksandar Boshkovski", "Production", "Coating Unit", "21126", 2, 4 },
                    { 318, 32, 21128, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1904), "Gjuner Ismailovski", "Projects and IT", "Rolling Unit", "21128", 2, 9 },
                    { 319, 28, 21131, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1905), "Jovan Markovski", "Production", "Coating Unit", "21131", 2, 4 },
                    { 320, 47, 21133, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1907), "Kjamuran Muaremovski", "Production", "Coating Unit", "21133", 2, 4 },
                    { 321, 62, 21134, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1908), "Nikola Panovski", "Projects and IT", "Accounting & Treasury", "21134", 2, 4 },
                    { 322, 23, 21136, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1909), "Stefan Ristovski", "Production", "Coating Unit", "21136", 2, 4 },
                    { 323, 24, 21139, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1910), "Jasin Ismailovski", "Production", "Facility", "21139", 2, 4 },
                    { 324, 59, 21140, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1912), "Borko Sokolovikj", "Production", "Customer service & Logistics", "21140", 2, 4 },
                    { 325, 41, 21142, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1913), "Stojan Despotoski", "Production", "Coating Unit", "21142", 2, 5 },
                    { 326, 51, 21143, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1915), "Shezair Lazam", "Production", "High voltage", "21143", 2, 4 },
                    { 327, 24, 21149, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1916), "Jovan Stojanovski", "Projects and IT", "Crane Maintenance & Internal Transport", "21149", 2, 4 },
                    { 328, 34, 21151, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1918), "Kire Krusharski", "Production", "Stores", "21151", 2, 4 },
                    { 329, 45, 21152, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1919), "Igorche Kuzmanov", "Production", "Customer service & Logistics", "21152", 2, 4 },
                    { 330, 31, 21154, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1920), "Goce Zdravevski", "Supply chain", "Crane Maintenance & Internal Transport", "21154", 2, 3 },
                    { 331, 46, 21156, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1922), "Goran Vasilevski", "Production", "Crane Maintenance & Internal Transport", "21156", 2, 3 },
                    { 332, 28, 21160, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1923), "Deni Popovski", "Supply chain", "Coating Unit", "21160", 2, 3 },
                    { 333, 32, 21171, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1924), "Jovan Chankulovski", "Production", "High voltage", "21171", 2, 3 },
                    { 334, 53, 21174, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1925), "Dragi Risteski", "Projects and IT", "Quality control", "21174", 2, 3 },
                    { 335, 50, 21175, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1927), "Zoran Urdarevikj", "Production", "Rolling Unit", "21175", 2, 5 },
                    { 336, 41, 21178, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1928), "Miroslav Martinovski", "Production", "Quality control", "21178", 2, 3 },
                    { 337, 34, 21183, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1929), "Emran Iseinov", "Production", "Human Resources", "21183", 2, 3 },
                    { 338, 54, 21184, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1931), "Mirche Milkovski", "Production", "Crane Maintenance & Internal Transport", "21184", 2, 3 },
                    { 339, 33, 21188, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1932), "Aleksandar Kitanovski", "Production", "Rolling Unit", "21188", 2, 3 },
                    { 340, 31, 21189, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1934), "Dejan Stefanovski", "Production", "Coating Unit", "21189", 2, 3 },
                    { 341, 29, 21190, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1936), "Viktor Stojchevski", "Production", "Rolling Unit", "21190", 2, 3 },
                    { 342, 44, 21191, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1937), "Dragan Risteski", "Supply chain", "Crane Maintenance & Internal Transport", "21191", 2, 3 },
                    { 343, 38, 21193, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1938), "Dzemail Ljimani", "Production", "Coating Unit", "21193", 2, 3 },
                    { 344, 46, 21194, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1939), "Biljana Trajkovska", "Supply chain", "Accounting & Treasury", "21194", 2, 5 },
                    { 345, 36, 21196, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1941), "Miroslav Krstikj", "Production", "Planning & Strategy", "21196", 2, 3 },
                    { 346, 39, 21197, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1942), "Violeta Stojanovska", "CEO office", "Facility", "21197", 2, 3 },
                    { 347, 36, 21198, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1943), "Kristina Kolaroska", "Finance Department", "Crane Maintenance & Internal Transport", "21198", 2, 3 },
                    { 348, 23, 21200, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1944), "David Savevski", "Production", "Automation", "21200", 2, 3 },
                    { 349, 35, 21201, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1946), "Emrah Sali", "Production", "Coating Unit", "21201", 2, 3 },
                    { 350, 46, 21204, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1947), "Robert Ristovski", "Production", "Accounting & Treasury", "21204", 2, 3 },
                    { 351, 52, 21206, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1948), "Marjanche Milkovski", "Projects and IT", "Coating Unit", "21206", 2, 3 },
                    { 352, 62, 21209, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1949), "Ice Trajkoski", "Production", "Facility", "21209", 2, 3 },
                    { 353, 22, 21212, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1951), "Viktor Ilievski", "Production", "Customer service & Logistics", "21212", 2, 3 },
                    { 354, 21, 21218, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1952), "Daniel Slavkovski", "Production", "Coating Unit", "21218", 2, 3 },
                    { 355, 47, 21219, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1954), "Goce Peshevski", "Production", "High voltage", "21219", 2, 3 },
                    { 356, 51, 21224, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1956), "Natasha Mihova", "Finance Department", "Crane Maintenance & Internal Transport", "21224", 2, 2 },
                    { 357, 32, 21225, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1982), "Bujar Zenuli", "Production", "Stores", "21225", 2, 2 },
                    { 358, 28, 21227, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1983), "Tamara Stojchevska", "HR", "Coating Unit", "21227", 2, 2 },
                    { 359, 47, 21229, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1985), "Dragana Velkovikj-Krsteva", "Supply chain", "Customer service & Logistics", "21229", 2, 2 },
                    { 360, 48, 21231, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1986), "Jovica Stojanovski", "Production", "Crane Maintenance & Internal Transport", "21231", 2, 2 },
                    { 361, 23, 21233, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1987), "Mario Trajkovski", "Projects and IT", "Crane Maintenance & Internal Transport", "21233", 2, 2 },
                    { 362, 35, 21240, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1989), "Dancho Kostadinovski", "Projects and IT", "Coating Unit", "21240", 2, 2 },
                    { 363, 30, 21241, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1990), "Konstantin Koneski", "Supply chain", "High voltage", "21241", 2, 2 },
                    { 364, 42, 21243, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1991), "Nenad Mihajloski", "Production", "Quality control", "21243", 2, 2 },
                    { 365, 41, 21247, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1993), "Ilija Andonoski", "Supply chain", "Rolling Unit", "21247", 2, 2 },
                    { 366, 40, 21252, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1994), "Toni Karovchevikj", "Projects and IT", "Quality control", "21252", 2, 2 },
                    { 367, 29, 21254, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1995), "Hristijan Todorovski", "Projects and IT", "Coating Unit", "21254", 2, 2 },
                    { 368, 28, 21257, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1996), "Atanas Boshkov", "Production", "Coating Unit", "21257", 2, 2 },
                    { 369, 23, 21259, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(1998), "Damjan Petrovski", "Projects and IT", "Crane Maintenance & Internal Transport", "21259", 2, 2 },
                    { 370, 47, 21260, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2000), "Viktorija Karafiloska", "Supply chain", "Crane Maintenance & Internal Transport", "21260", 2, 1 },
                    { 371, 50, 21261, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2002), "Sashko Janevski", "Production", "Coating Unit", "21261", 2, 1 },
                    { 372, 52, 21262, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2003), "Maja Miloshoska", "Supply chain", "Crane Maintenance & Internal Transport", "21262", 2, 1 },
                    { 373, 44, 21263, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2004), "Elena Stoilkovska", "HR", "Coating Unit", "21263", 2, 1 },
                    { 374, 21, 21268, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2005), "Dragan Najdovski", "Projects and IT", "Crane Maintenance & Internal Transport", "21268", 2, 1 },
                    { 375, 23, 21269, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2007), "Luka Bostandzievski", "Production", "Coating Unit", "21269", 2, 1 },
                    { 376, 39, 21270, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2008), "Sinisha Voinoski", "Production", "Crane Maintenance & Internal Transport", "21270", 2, 2 },
                    { 377, 27, 21271, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2009), "Muhamed Mimin", "Production", "Coating Unit", "21271", 2, 1 },
                    { 378, 26, 21274, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2012), "Nuija Nuijovski", "Projects and IT", "Facility", "21274", 2, 1 },
                    { 379, 49, 21275, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2014), "Svetlana Davkovska", "Finance Department", "Facility", "21275", 2, 1 },
                    { 380, 51, 21277, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2015), "Isa Zenelji", "Production", "Coating Unit", "21277", 2, 1 },
                    { 381, 22, 21280, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2016), "Mario Nikolovski", "Projects and IT", "Quality control", "21280", 2, 1 },
                    { 382, 22, 21281, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2017), "Angel Kostovski", "Production", "Crane Maintenance & Internal Transport", "21281", 2, 1 },
                    { 383, 24, 21282, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2019), "Hristijan Stevkovski", "Supply chain", "Coating Unit", "21282", 2, 1 },
                    { 384, 44, 21283, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2021), "Naim Ajvazi", "Production", "Crane Maintenance & Internal Transport", "21283", 2, 1 },
                    { 385, 51, 21284, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2022), "Miodrag Achkovikj", "Production", "Coating Unit", "21284", 2, 1 },
                    { 386, 26, 21285, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2023), "Andrej Velichkovski", "Projects and IT", "Crane Maintenance & Internal Transport", "21285", 2, 1 },
                    { 387, 47, 21286, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2025), "Dejan Smilevski", "Projects and IT", "Coating Unit", "21286", 2, 1 },
                    { 388, 60, 21288, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2026), "Trajche Trajkovski", "Production", "Crane Maintenance & Internal Transport", "21288", 2, 1 },
                    { 389, 36, 21290, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2027), "Sashko Dimovski", "Projects and IT", "Coating Unit", "21290", 2, 1 },
                    { 390, 57, 21292, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2028), "Dushan Manojlovikj", "Production", "Quality control", "21292", 2, 1 },
                    { 391, 40, 21293, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2030), "Zoran Ilieski", "Projects and IT", "Coating Unit", "21293", 2, 1 },
                    { 392, 20, 21294, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2031), "Antonio Panovski", "Production", "Crane Maintenance & Internal Transport", "21294", 2, 1 },
                    { 393, 44, 21295, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2032), "Violeta Joshovikj", "HR", "Human Resources", "21295", 2, 1 },
                    { 394, 36, 21297, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2033), "Sashka Stojanovska", "HR", "Crane Maintenance & Internal Transport", "21297", 2, 1 },
                    { 395, 51, 21298, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2035), "Ljupcho Emsherijov", "Production", "Rolling Unit", "21298", 2, 1 },
                    { 396, 30, 21299, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2036), "Nikola Risteski", "Supply chain", "Coating Unit", "21299", 2, 1 },
                    { 397, 19, 21300, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2037), "Ljupcho Bogojev", "Projects and IT", "Crane Maintenance & Internal Transport", "21300", 2, 1 },
                    { 398, 32, 21302, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2038), "Erol Idriz", "Projects and IT", "Coating Unit", "21302", 2, 1 },
                    { 399, 59, 21303, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2040), "Blagoja Jovchevski", "Projects and IT", "Crane Maintenance & Internal Transport", "21303", 2, 1 },
                    { 400, 22, 21304, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2042), "Stefan Trajkovikj", "Production", "Coating Unit", "21304", 2, 1 },
                    { 401, 43, 21305, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2043), "Vesna Gjorgjevska", "HR", "Facility", "21305", 2, 1 },
                    { 402, 24, 21306, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2044), "Mihaela Gecheva", "HR", "Facility", "21306", 2, 1 },
                    { 403, 24, 21307, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2045), "Marija Malinova", "Supply chain", "Planning & Strategy", "21307", 2, 1 },
                    { 404, 24, 21308, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2047), "Viktorija Siljanoska", "Projects and IT", "Automation", "21308", 2, 1 },
                    { 405, 30, 21309, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2048), "Aleksandar Paunkovikj", "Projects and IT", "Crane Maintenance & Internal Transport", "21309", 2, 1 },
                    { 406, 20, 21310, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2049), "Stefan Cvetanovski", "Production", "Coating Unit", "21310", 2, 1 },
                    { 407, 51, 21311, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2050), "Valentina Cibreva", "Finance Department", "Accounting & Treasury", "21311", 2, 1 },
                    { 408, 27, 21312, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2068), "Milancho Uroshevski", "Supply chain", "Planning & Strategy", "21312", 2, 1 },
                    { 409, 61, 21313, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2069), "Jashar Ismaili", "HR", "Facility", "21313", 2, 1 },
                    { 410, 34, 21314, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2071), "Daniel Neshkovikj", "Daniel", "Crane Maintenance & Internal Transport", "21314", 2, 1 },
                    { 411, 26, 21315, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2072), "Hristina Jovanovska", "Projects and IT", "Automation", "21315", 2, 1 },
                    { 412, 46, 21316, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2073), "Marjan Georgiev", "Production", "Coating Unit", "21316", 2, 1 },
                    { 413, 56, 3320621, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2075), "Todorka Ristovska", "CEO office", "CEO office", "20621", 3, 0 },
                    { 414, 44, 3320724, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2076), "Vesna Velichkovska", "HR", "Human Resources and Legal Affairs", "20724", 3, 0 },
                    { 415, 48, 3320640, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2078), "Kiro Risteski", "Production", "Production", "20640", 3, 0 },
                    { 416, 46, 3320650, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2079), "Dejana Jovanova Krsteva", "Supply chain", "Supply chain", "20650", 3, 0 },
                    { 417, 45, 3320623, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2081), "Elena Blazeva", "Finance Department", "Finance Department", "20623", 3, 0 },
                    { 418, 43, 3320889, new DateTime(2025, 11, 14, 15, 12, 29, 613, DateTimeKind.Utc).AddTicks(2082), "Dushan Jovanoski", "Sales", "Sales", "20889", 3, 0 }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "IsRequired", "QuestionFormId", "QuestionTypeId", "Text", "UserId" },
                values: new object[,]
                {
                    { 1, true, 1, 1, "Задоволен сум од мојата моментална работа", 1 },
                    { 2, true, 1, 1, "Чувствувам дека мојата работа е ценета во рамките на компанијата", 1 },
                    { 3, true, 1, 1, "Се чувствувам мотивиран да одам на работа секој ден", 1 },
                    { 4, true, 2, 1, "Се чувствувам горд што работам за оваа компанија", 1 },
                    { 5, true, 2, 1, "Со задоволство ја препорачувам оваа компанија како работно место на пријателите и семејството", 1 },
                    { 6, true, 2, 1, "Се гледам себеси како долгорочно работам во оваа компанија", 1 },
                    { 7, true, 3, 1, "Имам можности за професионален развој и напредување", 1 },
                    { 8, true, 3, 1, "Добивам конструктивна повратна информација за мојата работа", 1 },
                    { 9, true, 3, 1, "Компанијата обезбедува соодветна обука и ресурси за мојот развој", 1 },
                    { 10, true, 4, 1, "Компанијата поддржува здрава рамнотежа помеѓу работата и личниот живот", 1 },
                    { 11, true, 4, 1, "Можам ефикасно да управувам со стресот поврзан со работата", 1 },
                    { 12, true, 4, 1, "Мојот работен распоред ми овозможува да ги исполнувам моите лични обврски", 1 },
                    { 13, true, 5, 1, "Комуникацијата во мојот тим е ефикасна", 1 },
                    { 14, true, 5, 1, "Се чувствувам удобно да ги искажувам моите идеи и мислења на работа", 1 },
                    { 15, true, 5, 1, "Соработката помеѓу одделенијата е ефикасна", 1 },
                    { 16, true, 6, 1, "Му верувам на раководството на компанијата", 1 },
                    { 17, true, 6, 1, "Мојот директен менаџер ме поддржува во остварувањето на моите цели", 1 },
                    { 18, true, 6, 1, "Важните одлуки на компанијата се пренесуваат транспарентно", 1 },
                    { 19, true, 7, 1, "Вредностите на компанијата се усогласуваат со моите лични вредности", 1 },
                    { 20, true, 7, 1, "Се чувствувам вклучено и почитувано на работа", 1 },
                    { 21, true, 7, 1, "Компанијата промовира различност и инклузија", 1 },
                    { 22, true, 8, 1, "Ги имам сите ресурси потребни за ефикасно извршување на моите задачи", 1 },
                    { 23, true, 8, 1, "Физичката работна средина е удобна и поволна за продуктивност", 1 },
                    { 24, true, 8, 1, "Се чувствувам безбедно на работа", 1 },
                    { 25, true, 9, 1, "Задоволен сум од мојот пакет компензации и бенефиции", 1 },
                    { 26, true, 9, 1, "Моите напори и достигнувања се препознаени и ценети", 1 },
                    { 27, true, 9, 1, "Постојат јасни можности за напредување во кариерата во рамките на компанијата", 1 },
                    { 28, true, 10, 1, "Компанијата ги поттикнува иновациите и креативното размислување", 1 },
                    { 29, true, 10, 1, "Подготвен сум да ги усвојам промените имплементирани во компанијата", 1 },
                    { 30, true, 10, 1, "Идеите и предлозите на вработените се разгледуваат и се спроведуваат кога е соодветно", 1 },
                    { 31, true, 11, 1, "Kолку е веројатно да ја препорачате оваа компанија како работно место на пријател или колега", 1 },
                    { 32, true, 11, 2, "Како ја гледате вашата иднина во оваа компанија во следните 2-3 години?", 1 },
                    { 33, true, 11, 2, "разно", 1 },
                    { 34, true, 12, 2, "Што најмногу ви се допаѓа на вашето сегашно работно место?", 1 },
                    { 35, true, 12, 2, "Кои се најголемите предизвици со кои се соочувате на работа?", 1 },
                    { 36, true, 12, 2, "Какви предлози имате за подобрување на работната средина или процесите на компанијата?", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionFormId",
                table: "Answers",
                column: "QuestionFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId_QuestionId_QuestionFormId",
                table: "Answers",
                columns: new[] { "UserId", "QuestionId", "QuestionFormId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionFormId",
                table: "Questions",
                column: "QuestionFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionTypeId",
                table: "Questions",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserId",
                table: "Questions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "QuestionForms");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
