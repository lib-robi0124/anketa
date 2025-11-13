using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlasAnketa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initprva : Migration
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
                    Age = table.Column<int>(type: "int", nullable: false)
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
                    AnsweredDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    { 1, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2590), "Overall Satisfaction", true, "Општо задоволство" },
                    { 2, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2593), "Commitment to the Company", true, "Обврска кон компанијата" },
                    { 3, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2594), "Professional Development", true, "Професионален развој" },
                    { 4, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2595), "Work-Life Balance", true, "Рамнотежа помеѓу работата и животот" },
                    { 5, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2595), "Communication and Collaboration", true, "Комуникација и соработка" },
                    { 6, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2596), "Leadership", true, "Лидерство" },
                    { 7, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2597), "Organizational Culture", true, "Организациска култура" },
                    { 8, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2598), "Work Environment", true, "Работна средина" },
                    { 9, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2599), "Rewards and Recognition", true, "Награди и признанија" },
                    { 10, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2617), "Innovation and Changes", true, "Иновации и промени" },
                    { 11, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2618), "Final Evaluation", true, "Конечна евалуација" },
                    { 12, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2619), "Open Questions", true, "Отворени прашања" }
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
                columns: new[] { "Id", "Age", "CompanyId", "CreatedDate", "FullName", "OU", "OU2", "Password", "RoleId" },
                values: new object[,]
                {
                    { 1, 63, 16130, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1497), "Vasko Gjorgiev", "Production", "Rolling Unit", "16130", 2 },
                    { 2, 63, 16684, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1505), "Zoran Stojanovski", "Production", "Rolling Unit", "16684", 2 },
                    { 3, 62, 16695, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1507), "Pane Naskovski", "Production", "Rolling Unit", "16695", 2 },
                    { 4, 62, 16720, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1508), "Tome Trajanov", "Projects and IT", "Crane Maintenance & Internal Transport", "16720", 2 },
                    { 5, 61, 16827, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1509), "Zoran Boshkovski", "Production", "Rolling Unit", "16827", 2 },
                    { 6, 61, 16984, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1511), "Dide Petrovski", "Projects and IT", "Central mechanical maintenance", "16984", 2 },
                    { 7, 61, 17011, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1513), "Jovica Gjorgievski", "Projects and IT", "Crane Maintenance & Internal Transport", "17011", 2 },
                    { 8, 61, 17055, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1514), "Blagica Besarovska", "Projects and IT", "Crane Maintenance & Internal Transport", "17055", 2 },
                    { 9, 62, 17064, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1515), "Dragi Naskovski", "Production", "Coating Unit", "17064", 2 },
                    { 10, 60, 17148, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1517), "Borche Anchevski", "Production", "Coating Unit", "17148", 2 },
                    { 11, 59, 17772, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1518), "Toni Nacev", "HR", "Facility", "17772", 2 },
                    { 12, 59, 17884, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1519), "Valentina Kostovska", "HR", "Human Resources", "17884", 2 },
                    { 13, 61, 17893, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1521), "Zoran Tripunoski", "Production", "Rolling Unit", "17893", 2 },
                    { 14, 61, 17896, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1527), "Zorancho Taseski", "Projects and IT", "Crane Maintenance & Internal Transport", "17896", 2 },
                    { 15, 61, 18158, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1528), "Goran Despodovski", "Production", "Coating Unit", "18158", 2 },
                    { 16, 59, 18162, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1530), "Ljupcho Krstevski", "Production", "Rolling Unit", "18162", 2 },
                    { 17, 60, 18392, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1590), "Sabedin Ljura", "Production", "Coating Unit", "18392", 2 },
                    { 18, 59, 18412, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1591), "Rade Milenkovski", "Production", "Coating Unit", "18412", 2 },
                    { 19, 61, 18471, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1592), "Stojka Koneska", "Supply chain", "Customer service & Logistics", "18471", 2 },
                    { 20, 62, 18529, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1594), "Zharko Nikolovski", "Production", "Rolling Unit", "18529", 2 },
                    { 21, 60, 18533, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1597), "Radica Angelovska", "Projects and IT", "Crane Maintenance & Internal Transport", "18533", 2 },
                    { 22, 58, 18874, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1599), "Borche Trifunovski", "Production", "Coating Unit", "18874", 2 },
                    { 23, 58, 18876, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1601), "Pero Stojanovski", "Production", "Coating Unit", "18876", 2 },
                    { 24, 60, 19370, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1602), "Dragi Petrovski", "Production", "Coating Unit", "19370", 2 },
                    { 25, 59, 19379, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1603), "Ilo Risteski", "Supply chain", "Quality control", "19379", 2 },
                    { 26, 48, 19767, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1605), "Aleksandar Iliev", "Production", "Coating Unit", "19767", 2 },
                    { 27, 57, 19775, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1607), "Mile Popovski", "Production", "Rolling Unit", "19775", 2 },
                    { 28, 45, 19776, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1608), "Dragan Hristovski", "Production", "Coating Unit", "19776", 2 },
                    { 29, 50, 19777, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1609), "Aleksandar Jovchevski", "Production", "Coating Unit", "19777", 2 },
                    { 30, 61, 19779, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1610), "Ljupcho Andovski", "Supply chain", "Quality control", "19779", 2 },
                    { 31, 50, 19782, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1612), "Ivica Stanchevski", "Production", "Rolling Unit", "19782", 2 },
                    { 32, 50, 19784, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1613), "Biljana Ilievska", "Supply chain", "Quality control", "19784", 2 },
                    { 33, 55, 19787, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1614), "Goran Damjanoski", "Supply chain", "Quality control", "19787", 2 },
                    { 34, 46, 19788, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1615), "Boban Neshovski", "Production", "Rolling Unit", "19788", 2 },
                    { 35, 50, 19795, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1617), "Sashe Taparchevski", "Production", "Coating Unit", "19795", 2 },
                    { 36, 49, 19796, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1618), "Igor Ristovski", "Production", "Coating Unit", "19796", 2 },
                    { 37, 49, 19798, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1619), "Ivica Trajkovski", "Production", "Rolling Unit", "19798", 2 },
                    { 38, 62, 19801, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1621), "Vlado Stojanovski", "Production", "Coating Unit", "19801", 2 },
                    { 39, 49, 19804, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1622), "Goran Spirovski", "Production", "Coating Unit", "19804", 2 },
                    { 40, 51, 19806, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1624), "Dejan Velkovski", "Production", "Coating Unit", "19806", 2 },
                    { 41, 55, 19807, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1625), "Stojanche Stefkovski", "Production", "Coating Unit", "19807", 2 },
                    { 42, 54, 19811, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1626), "Dancho Blazheski", "Production", "Rolling Unit", "19811", 2 },
                    { 43, 60, 19813, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1628), "Ljupcho Lozanovski", "Production", "Coating Unit", "19813", 2 },
                    { 44, 49, 19818, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1629), "Marjan Nedelkovski", "Projects and IT", "High voltage", "19818", 2 },
                    { 45, 49, 19820, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1630), "Srgjan Stanojevikj", "Production", "Coating Unit", "19820", 2 },
                    { 46, 47, 19822, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1631), "Dragan Spasevski", "Supply chain", "Quality control", "19822", 2 },
                    { 47, 51, 19823, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1632), "Goran Andonovski", "Projects and IT", "Crane Maintenance & Internal Transport", "19823", 2 },
                    { 48, 48, 19827, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1634), "Goran Anchovski", "Supply chain", "Planning & Strategy", "19827", 2 },
                    { 49, 47, 19833, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1635), "Igor Mircheski", "Supply chain", "Stores", "19833", 2 },
                    { 50, 51, 19834, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1636), "Goran Nikolovski", "HR", "Facility", "19834", 2 },
                    { 51, 48, 19838, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1637), "Petar Moskov", "Production", "Rolling Unit", "19838", 2 },
                    { 52, 52, 19840, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1638), "Goran Stojanovski", "Supply chain", "Planning & Strategy", "19840", 2 },
                    { 53, 50, 19841, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1640), "Igor Petkovski", "Projects and IT", "Crane Maintenance & Internal Transport", "19841", 2 },
                    { 54, 54, 19842, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1642), "Nenad Mitrovikj", "Production", "Coating Unit", "19842", 2 },
                    { 55, 55, 19844, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1643), "Sashko Gjorgjievski", "Supply chain", "Quality control", "19844", 2 },
                    { 56, 49, 19845, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1644), "Nikola Toshevski", "Production", "Coating Unit", "19845", 2 },
                    { 57, 59, 19848, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1646), "Slobodan Velkovski", "Production", "Coating Unit", "19848", 2 },
                    { 58, 48, 19849, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1647), "Goce Jankulovski", "Supply chain", "Planning & Strategy", "19849", 2 },
                    { 59, 56, 19868, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1648), "Marjan Milovanovikj", "Production", "Rolling Unit", "19868", 2 },
                    { 60, 55, 19877, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1649), "Goran Gavrilovski", "Sales", "Sales", "19877", 2 },
                    { 61, 55, 19892, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1650), "Irfan Feratovski", "Production", "Rolling Unit", "19892", 2 },
                    { 62, 54, 19899, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1652), "Igor Krpachovski", "Projects and IT", "Projects, progress and IT", "19899", 2 },
                    { 63, 48, 19911, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1653), "Aleksandar Spasevski", "CEO office", "Health, Safety and Environment", "19911", 2 },
                    { 64, 62, 19916, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1654), "Nevaip Bardi", "Production", "Rolling Unit", "19916", 2 },
                    { 65, 52, 19917, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1655), "Biljana Stoshikj", "Supply chain", "Customer service & Logistics", "19917", 2 },
                    { 66, 57, 19933, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1656), "Svetlana Jovanova", "Finance Department", "Financial Controlling and Reporting", "19933", 2 },
                    { 67, 56, 19960, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1657), "Draganche Taleski", "Production", "Rolling Unit", "19960", 2 },
                    { 68, 55, 19963, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1659), "Toni Naumovski", "Production", "Coating Unit", "19963", 2 },
                    { 69, 54, 19992, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1661), "Metodi Gievski", "Projects and IT", "Projects", "19992", 2 },
                    { 70, 59, 19993, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1662), "Jovica Velkovski", "Supply chain", "Customer service & Logistics", "19993", 2 },
                    { 71, 54, 19997, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1705), "Gordana Astardjieva", "Projects and IT", "High voltage", "19997", 2 },
                    { 72, 51, 20023, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1707), "Zharko Ivanovski", "Production", "Coating Unit", "20023", 2 },
                    { 73, 51, 20024, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1708), "Igorche Janev", "Production", "Rolling Unit", "20024", 2 },
                    { 74, 58, 20033, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1709), "Nikola Panov", "Supply chain", "Quality control", "20033", 2 },
                    { 75, 47, 20034, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1711), "Sasho Mitkovski", "Production", "Coating Unit", "20034", 2 },
                    { 76, 50, 20038, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1712), "Goran Ilievski", "Production", "Rolling Unit", "20038", 2 },
                    { 77, 53, 20041, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1713), "Kircho Merdjanovski", "Projects and IT", "Automation", "20041", 2 },
                    { 78, 47, 20052, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1714), "Davor Zdravkovski", "Supply chain", "Customer service & Logistics", "20052", 2 },
                    { 79, 55, 20072, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1715), "Gorancho Petkovski", "Production", "Quality control", "20072", 2 },
                    { 80, 49, 20076, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1717), "Sashko Cvetanovski", "Production", "Coating Unit", "20076", 2 },
                    { 81, 45, 20095, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1718), "Ilija Tashevski", "Production", "Crane Maintenance & Internal Transport", "20095", 2 },
                    { 82, 51, 20117, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1719), "Kire Stefanoski", "Production", "Rolling Unit", "20117", 2 },
                    { 83, 46, 20125, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1720), "Aleksandar Evremov", "Supply chain", "Coating Unit", "20125", 2 },
                    { 84, 51, 20127, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1722), "Ratko Trajkovski", "Supply chain", "Crane Maintenance & Internal Transport", "20127", 2 },
                    { 85, 46, 20128, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1724), "Goran Miovski", "Projects and IT", "Quality control", "20128", 2 },
                    { 86, 53, 20131, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1725), "Goran Trajkovski", "Production", "Rolling Unit", "20131", 2 },
                    { 87, 55, 20137, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1726), "Gordana Shegmanovikj", "HR", "Facility", "20137", 2 },
                    { 88, 48, 20144, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1728), "Igorche Bogdanovski", "Production", "Coating Unit", "20144", 2 },
                    { 89, 51, 20152, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1729), "Miodrag Petkovikj", "Production", "Maintenance Progress", "20152", 2 },
                    { 90, 54, 20159, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1730), "Gorancho Najdovski", "Projects and IT", "Coating Unit", "20159", 2 },
                    { 91, 48, 20160, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1731), "Dejan Jazadjiev", "Supply chain", "Coating Unit", "20160", 2 },
                    { 92, 53, 20162, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1732), "Sashko Peshov", "Production", "Crane Maintenance & Internal Transport", "20162", 2 },
                    { 93, 61, 20163, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1734), "Kiro Radevski", "Production", "Rolling Unit", "20163", 2 },
                    { 94, 51, 20167, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1735), "Sasho Beroski", "Production", "Coating Unit", "20167", 2 },
                    { 95, 47, 20168, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1736), "Vlatko Changovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20168", 2 },
                    { 96, 52, 20178, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1737), "Stojan Stavreski", "Projects and IT", "Crane Maintenance & Internal Transport", "20178", 2 },
                    { 97, 46, 20182, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1738), "Kjemalj Abazi", "Production", "Coating Unit", "20182", 2 },
                    { 98, 60, 20184, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1740), "Djevat Selimi", "Production", "Rolling Unit", "20184", 2 },
                    { 99, 50, 20191, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1741), "Trajche Dimovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20191", 2 },
                    { 100, 56, 20195, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1742), "Robert Shijakovski", "Production", "Rolling Unit", "20195", 2 },
                    { 101, 49, 20203, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1744), "Slavisha Crnichin", "Production", "Coating Unit", "20203", 2 },
                    { 102, 52, 20210, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1745), "Borche Cvetkovski", "Supply chain", "Customer service & Logistics", "20210", 2 },
                    { 103, 57, 20212, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1747), "Sasha Stefanoski", "Supply chain", "Customer service & Logistics", "20212", 2 },
                    { 104, 53, 20218, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1748), "Josif Slavkovski", "Production", "Central mechanical maintenance", "20218", 2 },
                    { 105, 53, 20225, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1749), "Goce Stojchevski", "Projects and IT", "Customer service & Logistics", "20225", 2 },
                    { 106, 61, 20226, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1750), "Donche Nedelkovski", "Production", "Quality control", "20226", 2 },
                    { 107, 50, 20231, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1752), "Ljupcho Shegmanovikj", "Production", "Coating Unit", "20231", 2 },
                    { 108, 47, 20232, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1753), "Goran Markovski", "Production", "Stores", "20232", 2 },
                    { 109, 48, 20233, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1754), "Dragan Markovski", "Projects and IT", "Customer service & Logistics", "20233", 2 },
                    { 110, 59, 20234, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1755), "Ljupcho Veljanovski", "Production", "Coating Unit", "20234", 2 },
                    { 111, 45, 20235, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1756), "Nikola Angeleski", "Production", "Coating Unit", "20235", 2 },
                    { 112, 49, 20236, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1757), "Aleksandar Bogoev", "Production", "Customer service & Logistics", "20236", 2 },
                    { 113, 53, 20238, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1759), "Stevche Velkovski", "Projects and IT", "Facility", "20238", 2 },
                    { 114, 48, 20245, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1761), "Ljubomir Kochovski", "Projects and IT", "Rolling Unit", "20245", 2 },
                    { 115, 51, 20246, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1762), "Sashko Blazhevski", "Production", "Crane Maintenance & Internal Transport", "20246", 2 },
                    { 116, 50, 20248, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1763), "Zoranche Borizovski", "Production", "Quality control", "20248", 2 },
                    { 117, 49, 20253, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1764), "Nebojsha Stojmanovikj", "Projects and IT", "Rolling Unit", "20253", 2 },
                    { 118, 46, 20255, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1766), "Ljupcho Pashoski", "Production", "Coating Unit", "20255", 2 },
                    { 119, 47, 20261, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1767), "Aleksandar Karajanovski", "Production", "Crane Maintenance & Internal Transport", "20261", 2 },
                    { 120, 48, 20262, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1768), "Dejan Stojanov", "Supply chain", "Rolling Unit", "20262", 2 },
                    { 121, 51, 20263, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1769), "Vladimir Jakimov", "Projects and IT", "Coating Unit", "20263", 2 },
                    { 122, 54, 20267, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1770), "Goranche Ginoski", "Production", "Coating Unit", "20267", 2 },
                    { 123, 62, 20271, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1772), "Avdil Mustafa", "Production", "Customer service & Logistics", "20271", 2 },
                    { 124, 59, 20272, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1773), "Beta Damevska", "Projects and IT", "Crane Maintenance & Internal Transport", "20272", 2 },
                    { 125, 56, 20275, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1804), "Naser Ilazov", "Production", "Rolling Unit", "20275", 2 },
                    { 126, 46, 20280, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1805), "Viktor Boshkovski", "Production", "Planning & Strategy", "20280", 2 },
                    { 127, 56, 20283, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1806), "Zlatko Petrovski", "Supply chain", "Rolling Unit", "20283", 2 },
                    { 128, 48, 20284, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1807), "Aleksandar Stoicev", "Production", "Coating Unit", "20284", 2 },
                    { 129, 48, 20286, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1809), "Aleksandar Jovanovski", "Production", "Coating Unit", "20286", 2 },
                    { 130, 53, 20296, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1810), "Goran Jovanovski", "Production", "Coating Unit", "20296", 2 },
                    { 131, 47, 20297, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1811), "Ivan Maslov", "Projects and IT", "Coating Unit", "20297", 2 },
                    { 132, 45, 20298, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1813), "Ivica Tripunovski", "Supply chain", "Coating Unit", "20298", 2 },
                    { 133, 59, 20299, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1815), "Milisav Boshkovikj", "Production", "Coating Unit", "20299", 2 },
                    { 134, 48, 20300, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1816), "Ilija Pandurski", "Production", "Coating Unit", "20300", 2 },
                    { 135, 47, 20308, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1817), "Ljuben Trajkoski", "Supply chain", "Customer service & Logistics", "20308", 2 },
                    { 136, 49, 20316, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1819), "Igor Petrovski", "Projects and IT", "Purchasing", "20316", 2 },
                    { 137, 47, 20320, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1820), "Brankica Trajanoska", "Supply chain", "Crane Maintenance & Internal Transport", "20320", 2 },
                    { 138, 51, 20323, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1821), "Blazhe Dimov", "Projects and IT", "Customer service & Logistics", "20323", 2 },
                    { 139, 47, 20324, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1822), "Biljana Petrovska", "Supply chain", "Customer service & Logistics", "20324", 2 },
                    { 140, 60, 20325, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1823), "Zoran Trajkov", "Projects and IT", "Central mechanical maintenance", "20325", 2 },
                    { 141, 62, 20328, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1825), "Slavko Spasovski", "HR", "Union", "20328", 2 },
                    { 142, 49, 20332, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1826), "Igorche Gjorgjievski", "Projects and IT", "Crane Maintenance & Internal Transport", "20332", 2 },
                    { 143, 55, 20335, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1827), "Jovica Boshkovski", "Production", "Rolling Unit", "20335", 2 },
                    { 144, 47, 20341, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1828), "Metodija Blazhevski", "Supply chain", "Customer service & Logistics", "20341", 2 },
                    { 145, 56, 20350, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1829), "Sasho Petrushevski", "Supply chain", "Customer service & Logistics", "20350", 2 },
                    { 146, 50, 20351, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1831), "Marjan Stojanovski", "Supply chain", "Customer service & Logistics", "20351", 2 },
                    { 147, 52, 20357, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1832), "Dejan Petrushevski", "Sales", "Sales", "20357", 2 },
                    { 148, 53, 20362, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1834), "Sasho Kiprijanovski", "Supply chain", "Quality control", "20362", 2 },
                    { 149, 48, 20363, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1835), "Zoran Mitevski", "Production", "Coating Unit", "20363", 2 },
                    { 150, 55, 20372, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1836), "Sasho Gjorgjievski", "Production", "Stores", "20372", 2 },
                    { 151, 56, 20380, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1840), "Orce Angelovski", "Projects and IT", "Customer service & Logistics", "20380", 2 },
                    { 152, 48, 20381, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1841), "Dejan Danilovski", "Supply chain", "Coating Unit", "20381", 2 },
                    { 153, 50, 20382, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1842), "Robert Angelovski", "Projects and IT", "Coating Unit", "20382", 2 },
                    { 154, 59, 20385, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1843), "Bratislav Mihajlovikj", "Supply chain", "Customer service & Logistics", "20385", 2 },
                    { 155, 57, 20387, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1844), "Ace Mitevski", "Supply chain", "Facility", "20387", 2 },
                    { 156, 51, 20389, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1846), "Igorche Markovski", "Production", "Rolling Unit", "20389", 2 },
                    { 157, 52, 20390, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1847), "Jovan Markovski", "Supply chain", "Crane Maintenance & Internal Transport", "20390", 2 },
                    { 158, 51, 20393, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1848), "Boban Hristovski", "Supply chain", "Quality control", "20393", 2 },
                    { 159, 51, 20395, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1849), "Dejan Dimishkovski", "Production", "Rolling Unit", "20395", 2 },
                    { 160, 53, 20397, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1850), "Marjan Simonovski", "Production", "Coating Unit", "20397", 2 },
                    { 161, 60, 20402, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1852), "Slavica Mladenovska", "Supply chain", "High voltage", "20402", 2 },
                    { 162, 61, 20431, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1853), "Angelina Rajovska", "HR", "Quality control", "20431", 2 },
                    { 163, 46, 20439, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1854), "Dejan Dilevski", "Production", "Rolling Unit", "20439", 2 },
                    { 164, 52, 20443, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1856), "Aleksandar Zotikj", "Supply chain", "Crane Maintenance & Internal Transport", "20443", 2 },
                    { 165, 47, 20445, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1857), "Jovica Maznevski", "Production", "Quality control", "20445", 2 },
                    { 166, 53, 20447, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1859), "Zvonko Neshkovikj", "Production", "Rolling Unit", "20447", 2 },
                    { 167, 57, 20448, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1860), "Ljupcho Paunkov", "Production", "Coating Unit", "20448", 2 },
                    { 168, 48, 20449, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1861), "Marjan Zdravkovski", "Production", "Crane Maintenance & Internal Transport", "20449", 2 },
                    { 169, 49, 20451, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1862), "Igor Jordanovski", "Production", "Rolling Unit", "20451", 2 },
                    { 170, 47, 20453, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1863), "Aleksandar Stamchevski", "Production", "Coating Unit", "20453", 2 },
                    { 171, 51, 20454, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1865), "Dejan Vasilevski", "Production", "Crane Maintenance & Internal Transport", "20454", 2 },
                    { 172, 44, 20459, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1866), "Zoran Stojanovski", "Production", "Rolling Unit", "20459", 2 },
                    { 173, 63, 20466, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1867), "Iljaz Prekopuca", "Production", "Coating Unit", "20466", 2 },
                    { 174, 56, 20468, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1868), "Fadil Tanalari", "Production", "Crane Maintenance & Internal Transport", "20468", 2 },
                    { 175, 56, 20471, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1869), "Trajche Petrovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20471", 2 },
                    { 176, 51, 20475, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1871), "Boban Mitrovikj", "Production", "Rolling Unit", "20475", 2 },
                    { 177, 43, 20478, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1872), "Dragan Saveski", "Production", "Coating Unit", "20478", 2 },
                    { 178, 52, 20489, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1873), "Ajdin Zulfiovski", "Production", "Coating Unit", "20489", 2 },
                    { 179, 61, 20511, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1905), "Ivan Cibrev", "Supply chain", "Stores", "20511", 2 },
                    { 180, 53, 20518, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1906), "Slavica Jovchevska", "Supply chain", "Customer service & Logistics", "20518", 2 },
                    { 181, 60, 20521, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1907), "Elena Damchevska", "Finance Department", "Accounting & Treasury", "20521", 2 },
                    { 182, 59, 20523, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1908), "Vesna Dimevska", "Supply chain", "Quality control", "20523", 2 },
                    { 183, 53, 20527, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1910), "Kiril Simonoski", "Projects and IT", "Maintenance Progress", "20527", 2 },
                    { 184, 49, 20530, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1911), "Goce Atanasoski", "Projects and IT", "Maintenance Progress", "20530", 2 },
                    { 185, 50, 20603, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1912), "Goran Stojchevski", "Production", "Coating Unit", "20603", 2 },
                    { 186, 56, 20621, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1913), "Todorka Ristovska", "CEO office", "CEO office", "20621", 2 },
                    { 187, 45, 20623, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1915), "Elena Blazeva", "Finance Department", "Finance Department", "20623", 2 },
                    { 188, 53, 20625, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1917), "Darko Najdenov", "Supply chain", "Purchasing", "20625", 2 },
                    { 189, 46, 20632, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1918), "Zoran Mladenovski", "Projects and IT", "Projects, progress and IT", "20632", 2 },
                    { 190, 52, 20636, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1919), "Natalija Nikoloska", "Supply chain", "Quality control", "20636", 2 },
                    { 191, 49, 20637, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1920), "Aleksandar Krstev", "Supply chain", "Planning & Strategy", "20637", 2 },
                    { 192, 49, 20638, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1922), "Elena Kocevska Peceva", "Supply chain", "Quality control", "20638", 2 },
                    { 193, 48, 20640, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1923), "Kiro Risteski", "Production", "Production", "20640", 2 },
                    { 194, 46, 20650, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1924), "Dejana Jovanova Krsteva", "Supply chain", "Supply chain", "20650", 2 },
                    { 195, 48, 20652, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1926), "Toni Pandilovski", "Projects and IT", "Automation", "20652", 2 },
                    { 196, 49, 20662, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1928), "Vladimir Shulevski", "Production", "Coating Unit", "20662", 2 },
                    { 197, 41, 20675, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1929), "Dejan Trajkovski", "HR", "Internal Control", "20675", 2 },
                    { 198, 43, 20678, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1930), "Kire Blagoeski", "Supply chain", "Planning & Strategy", "20678", 2 },
                    { 199, 46, 20685, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1931), "Petar Brashnarov", "Production", "Coating Unit", "20685", 2 },
                    { 200, 57, 20694, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1932), "Zvonimir Manchevski", "Production", "Rolling Unit", "20694", 2 },
                    { 201, 46, 20695, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1934), "Aleksandar Dejanovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20695", 2 },
                    { 202, 51, 20707, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1935), "Selaedin Feratovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20707", 2 },
                    { 203, 45, 20708, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1936), "Slave Manevski", "Projects and IT", "Information technology", "20708", 1 },
                    { 204, 47, 20723, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1937), "Djevat Saliovski", "Production", "Rolling Unit", "20723", 2 },
                    { 205, 44, 20724, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1938), "Vesna Velichkovska", "HR", "Human Resources and Legal Affairs", "20724", 1 },
                    { 206, 46, 20729, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1940), "Vlatko Dimishkovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20729", 2 },
                    { 207, 58, 20734, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1941), "Blage Uroshevski", "Production", "Coating Unit", "20734", 2 },
                    { 208, 62, 20735, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1942), "Stojadin Jankovski", "Production", "Rolling Unit", "20735", 2 },
                    { 209, 53, 20737, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1943), "Zlatko Nikoloski", "Production", "Rolling Unit", "20737", 2 },
                    { 210, 43, 20747, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1945), "Goce Gjorgjievski", "Production", "Rolling Unit", "20747", 2 },
                    { 211, 34, 20751, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1947), "Stefan Tonevski", "Supply chain", "Quality control", "20751", 2 },
                    { 212, 42, 20753, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1948), "Orce Dimovski", "Production", "Rolling Unit", "20753", 2 },
                    { 213, 35, 20758, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1949), "Elena Valkancheva Najdenova", "Sales", "Sales", "20758", 2 },
                    { 214, 44, 20776, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1950), "Igor Dushanovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20776", 2 },
                    { 215, 38, 20779, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1951), "Jagoda Velevska", "CEO office", "Internal Audit", "20779", 2 },
                    { 216, 45, 20781, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1953), "Vladimir Nikolikj", "Supply chain", "Quality control", "20781", 2 },
                    { 217, 42, 20784, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1954), "Dejan Gocevski", "Production", "Coating Unit", "20784", 2 },
                    { 218, 34, 20787, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1955), "Aleksandar Kostovski", "Production", "Rolling Unit", "20787", 2 },
                    { 219, 41, 20797, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1956), "Cane Nikoloski", "Production", "Automation", "20797", 2 },
                    { 220, 33, 20800, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1958), "Viktor Stamenkovski", "Projects and IT", "Quality control", "20800", 2 },
                    { 221, 36, 20801, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1959), "Dragana Petrovikj", "Supply chain", "Quality control", "20801", 2 },
                    { 222, 30, 20802, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1960), "Stefan Despodovski", "Supply chain", "Central mechanical maintenance", "20802", 2 },
                    { 223, 50, 20803, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1961), "Marjan Milanovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20803", 2 },
                    { 224, 63, 20804, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1962), "Dragan Koneski", "Projects and IT", "Coating Unit", "20804", 2 },
                    { 225, 36, 20814, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1963), "Aleksandar Stojanovski", "Production", "Coating Unit", "20814", 2 },
                    { 226, 48, 20815, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1966), "Sashko Miloshevski", "Production", "Crane Maintenance & Internal Transport", "20815", 2 },
                    { 227, 53, 20822, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1967), "Elza Petrovska", "Sales", "Rolling Unit", "20822", 2 },
                    { 228, 47, 20824, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1968), "Darko Zdravkovski", "Production", "Coating Unit", "20824", 2 },
                    { 229, 49, 20825, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1969), "Kiril Chirkov", "Production", "Crane Maintenance & Internal Transport", "20825", 2 },
                    { 230, 49, 20827, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1970), "Igor Cvetanoski", "Production", "Rolling Unit", "20827", 2 },
                    { 231, 39, 20831, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1972), "Martin Nikolovski", "Production", "Coating Unit", "20831", 2 },
                    { 232, 42, 20832, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(1973), "Dushko Blazevski", "Supply chain", "Coating Unit", "20832", 2 },
                    { 233, 29, 20834, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2010), "Muammet Sali", "Projects and IT", "Crane Maintenance & Internal Transport", "20834", 2 },
                    { 234, 29, 20835, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2011), "Kristijan Janev", "Projects and IT", "Rolling Unit", "20835", 2 },
                    { 235, 37, 20837, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2012), "Dilaver Sali", "Production", "Coating Unit", "20837", 2 },
                    { 236, 42, 20838, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2013), "Sasho Neshkov", "Production", "Crane Maintenance & Internal Transport", "20838", 2 },
                    { 237, 48, 20839, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2015), "Goran Ilikj", "Production", "Rolling Unit", "20839", 2 },
                    { 238, 50, 20842, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2016), "Zoran Trendevski", "Production", "Customer service & Logistics", "20842", 2 },
                    { 239, 51, 20844, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2017), "Igor Lazevski", "Production", "Coating Unit", "20844", 2 },
                    { 240, 36, 20847, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2018), "Dragan Dragutinovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20847", 2 },
                    { 241, 50, 20848, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2019), "Hristo Jovanovski", "Production", "Rolling Unit", "20848", 2 },
                    { 242, 40, 20851, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2022), "Goran Moskov", "Production", "Coating Unit", "20851", 2 },
                    { 243, 46, 20852, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2023), "Zoran Nikolovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20852", 2 },
                    { 244, 38, 20855, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2024), "Goran Cvetanovski", "Production", "Rolling Unit", "20855", 2 },
                    { 245, 45, 20866, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2025), "Pero Mangarov", "Supply chain", "Customer service & Logistics", "20866", 2 },
                    { 246, 47, 20871, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2027), "Igor Blazevski", "Production", "Coating Unit", "20871", 2 },
                    { 247, 36, 20872, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2028), "Spase Belinski", "Projects and IT", "Crane Maintenance & Internal Transport", "20872", 2 },
                    { 248, 42, 20876, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2029), "Zhivorad Arsenovski", "Production", "Rolling Unit", "20876", 2 },
                    { 249, 52, 20879, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2030), "Ljupcho Pijakovski", "Production", "Coating Unit", "20879", 2 },
                    { 250, 37, 20883, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2031), "Ljupcho Dimitrijeski", "Projects and IT", "Crane Maintenance & Internal Transport", "20883", 2 },
                    { 251, 43, 20889, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2033), "Dushan Jovanoski", "Sales", "Sales", "20889", 2 },
                    { 252, 40, 20893, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2034), "Nikola Nikolovski", "Sales", "Sales", "20893", 2 },
                    { 253, 42, 20894, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2035), "Dimitar Jankovski", "Production", "Rolling Unit", "20894", 2 },
                    { 254, 54, 20896, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2036), "Imer Ljusjani", "Projects and IT", "Crane Maintenance & Internal Transport", "20896", 2 },
                    { 255, 39, 20898, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2037), "Bobi Gjogjievski", "Projects and IT", "Crane Maintenance & Internal Transport", "20898", 2 },
                    { 256, 26, 20899, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2038), "Hristijan Gjorgjevski", "Production", "Rolling Unit", "20899", 2 },
                    { 257, 38, 20903, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2040), "Temelko Sarovski", "Production", "Quality control", "20903", 2 },
                    { 258, 32, 20910, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2042), "Hristijan Simonovski", "Supply chain", "Rolling Unit", "20910", 2 },
                    { 259, 33, 20911, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2043), "Dame Kekenovski", "Production", "Coating Unit", "20911", 2 },
                    { 260, 44, 20914, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2044), "Afrim Jusufi", "Production", "Planning & Strategy", "20914", 2 },
                    { 261, 47, 20915, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2045), "Igor Damjanovski", "Supply chain", "Crane Maintenance & Internal Transport", "20915", 2 },
                    { 262, 41, 20916, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2046), "Besnik Ibraimi", "Projects and IT", "Quality control", "20916", 2 },
                    { 263, 43, 20917, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2048), "Viktor Velichkovski", "Production", "Coating Unit", "20917", 2 },
                    { 264, 40, 20919, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2049), "Robert Jovanovski", "Projects and IT", "Maintenance Progress", "20919", 2 },
                    { 265, 29, 20920, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2050), "Adnan Feratovski", "Projects and IT", "Coating Unit", "20920", 2 },
                    { 266, 52, 20924, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2051), "Biljana Chorobenska", "Supply chain", "Crane Maintenance & Internal Transport", "20924", 2 },
                    { 267, 31, 20927, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2052), "Vladan Trajkovski", "Projects and IT", "Rolling Unit", "20927", 2 },
                    { 268, 26, 20928, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2053), "Vlatko Mitevski", "Production", "Crane Maintenance & Internal Transport", "20928", 2 },
                    { 269, 28, 20935, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2055), "Adis Nezirovski", "Projects and IT", "Coating Unit", "20935", 2 },
                    { 270, 28, 20936, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2056), "Asim Nezirovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20936", 2 },
                    { 271, 55, 20937, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2057), "Goce Spaseski", "Production", "Rolling Unit", "20937", 2 },
                    { 272, 41, 20942, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2058), "Dragi Ickovski", "Projects and IT", "Crane Maintenance & Internal Transport", "20942", 2 },
                    { 273, 27, 20944, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2060), "Ibrahim Mujovikj", "Production", "Rolling Unit", "20944", 2 },
                    { 274, 47, 20948, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2061), "Boban Grozdanovski", "Projects and IT", "Quality control", "20948", 2 },
                    { 275, 27, 20951, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2063), "Robert Stojanovikj", "Projects and IT", "Rolling Unit", "20951", 2 },
                    { 276, 33, 20953, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2064), "Mihajlo Zafirovikj", "Production", "Coating Unit", "20953", 2 },
                    { 277, 41, 20955, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2065), "Aleksandra Trgachevska", "Supply chain", "Central mechanical maintenance", "20955", 2 },
                    { 278, 55, 20958, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2066), "Marjanche Ristovski", "Production", "Customer service & Logistics", "20958", 2 },
                    { 279, 35, 20963, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2067), "Dalibor Cvetkovski", "Production", "Automation", "20963", 2 },
                    { 280, 40, 20964, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2069), "Ivica Stanoeski", "Projects and IT", "Rolling Unit", "20964", 2 },
                    { 281, 47, 20967, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2070), "Gjorgji Velichkovski", "Supply chain", "Customer service & Logistics", "20967", 2 },
                    { 282, 47, 20968, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2071), "Karanfilka Giceva", "Supply chain", "Rolling Unit", "20968", 2 },
                    { 283, 56, 20971, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2072), "Djevat Feratovski", "Production", "Planning & Strategy", "20971", 2 },
                    { 284, 26, 20973, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2074), "Ivan Mitodevski", "Production", "Coating Unit", "20973", 2 },
                    { 285, 53, 20975, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2075), "Robert Ristovski", "Projects and IT", "Maintenance Progress", "20975", 1 },
                    { 286, 34, 20977, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2076), "Vlatko Dimevski", "Supply chain", "Coating Unit", "20977", 2 },
                    { 287, 53, 20979, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2107), "Violeta Vidinska", "HR", "Coating Unit", "20979", 2 },
                    { 288, 52, 20981, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2108), "Aco Jovanovski", "Projects and IT", "Coating Unit", "20981", 2 },
                    { 289, 47, 20982, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2111), "Rade Panovski", "Production", "Coating Unit", "20982", 2 },
                    { 290, 45, 20983, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2112), "Slave Joshovikj", "Production", "Coating Unit", "20983", 2 },
                    { 291, 48, 20988, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2113), "Nenad Petkovikj", "Projects and IT", "Maintenance Progress", "20988", 2 },
                    { 292, 56, 20989, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2114), "Borche Livrinski", "Projects and IT", "Coating Unit", "20989", 2 },
                    { 293, 33, 20994, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2116), "Sanja Lambrinidis", "Supply chain", "Rolling Unit", "20994", 2 },
                    { 294, 52, 20998, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2117), "Ace Jovanovski", "Production", "Rolling Unit", "20998", 2 },
                    { 295, 44, 20999, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2118), "Sashe Smilkovski", "Projects and IT", "Rolling Unit", "20999", 2 },
                    { 296, 25, 21002, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2119), "Leon Danilovski", "Supply chain", "Automation", "21002", 2 },
                    { 297, 37, 21003, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2121), "Enis Zekjiri", "Projects and IT", "Coating Unit", "21003", 2 },
                    { 298, 57, 21006, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2122), "Metodija Malkov", "Production", "Rolling Unit", "21006", 2 },
                    { 299, 33, 21010, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2123), "Stefan Risteski", "Projects and IT", "Customer service & Logistics", "21010", 2 },
                    { 300, 50, 21012, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2124), "Igor Momchilovski", "Production", "Rolling Unit", "21012", 2 },
                    { 301, 47, 21016, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2126), "Oliver Govedarovski", "Projects and IT", "Planning & Strategy", "21016", 2 },
                    { 302, 52, 21017, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2127), "Bobi Nikolovski", "Projects and IT", "Coating Unit", "21017", 2 },
                    { 303, 30, 21020, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2128), "Kristijan Stojanovski", "Supply chain", "Maintenance Progress", "21020", 2 },
                    { 304, 58, 21082, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2129), "Rufat Rufati", "Production", "Coating Unit", "21082", 2 },
                    { 305, 53, 21090, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2132), "Vase Pecevski", "Projects and IT", "Coating Unit", "21090", 2 },
                    { 306, 32, 21094, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2133), "Kristina Karajanovska", "Sales", "Coating Unit", "21094", 2 },
                    { 307, 57, 21095, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2134), "Srechko Vidinski", "Production", "Coating Unit", "21095", 2 },
                    { 308, 42, 21096, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2135), "Nikola Spasevski", "Projects and IT", "Crane Maintenance & Internal Transport", "21096", 2 },
                    { 309, 52, 21097, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2137), "Zvonko Miloshoski", "Supply chain", "Quality control", "21097", 2 },
                    { 310, 45, 21100, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2138), "Ismail Redzepi", "Production", "Rolling Unit", "21100", 2 },
                    { 311, 37, 21104, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2139), "Aleksandar Kekenovski", "Production", "Health, Safety and Environment", "21104", 2 },
                    { 312, 24, 21112, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2140), "Nikola Nikolovski", "Production", "Financial Controlling and Reporting", "21112", 2 },
                    { 313, 33, 21117, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2142), "Jovica Stojanovikj", "Production", "Coating Unit", "21117", 2 },
                    { 314, 33, 21119, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2143), "Vasil Kocevski", "Production", "Rolling Unit", "21119", 2 },
                    { 315, 52, 21121, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2144), "Petre Kushinovski", "Production", "Coating Unit", "21121", 2 },
                    { 316, 56, 21125, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2145), "Mitko Lebamov", "Projects and IT", "Crane Maintenance & Internal Transport", "21125", 2 },
                    { 317, 27, 21126, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2146), "Aleksandar Boshkovski", "Production", "Coating Unit", "21126", 2 },
                    { 318, 32, 21128, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2148), "Gjuner Ismailovski", "Projects and IT", "Rolling Unit", "21128", 2 },
                    { 319, 28, 21131, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2149), "Jovan Markovski", "Production", "Coating Unit", "21131", 2 },
                    { 320, 47, 21133, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2152), "Kjamuran Muaremovski", "Production", "Coating Unit", "21133", 2 },
                    { 321, 62, 21134, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2153), "Nikola Panovski", "Projects and IT", "Accounting & Treasury", "21134", 2 },
                    { 322, 23, 21136, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2154), "Stefan Ristovski", "Production", "Coating Unit", "21136", 2 },
                    { 323, 24, 21139, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2155), "Jasin Ismailovski", "Production", "Facility", "21139", 2 },
                    { 324, 59, 21140, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2157), "Borko Sokolovikj", "Production", "Customer service & Logistics", "21140", 2 },
                    { 325, 41, 21142, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2158), "Stojan Despotoski", "Production", "Coating Unit", "21142", 2 },
                    { 326, 51, 21143, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2159), "Shezair Lazam", "Production", "High voltage", "21143", 2 },
                    { 327, 24, 21149, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2160), "Jovan Stojanovski", "Projects and IT", "Crane Maintenance & Internal Transport", "21149", 2 },
                    { 328, 34, 21151, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2161), "Kire Krusharski", "Production", "Stores", "21151", 2 },
                    { 329, 45, 21152, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2163), "Igorche Kuzmanov", "Production", "Customer service & Logistics", "21152", 2 },
                    { 330, 31, 21154, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2164), "Goce Zdravevski", "Supply chain", "Crane Maintenance & Internal Transport", "21154", 2 },
                    { 331, 46, 21156, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2165), "Goran Vasilevski", "Production", "Crane Maintenance & Internal Transport", "21156", 2 },
                    { 332, 28, 21160, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2166), "Deni Popovski", "Supply chain", "Coating Unit", "21160", 2 },
                    { 333, 32, 21171, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2167), "Jovan Chankulovski", "Production", "High voltage", "21171", 2 },
                    { 334, 53, 21174, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2169), "Dragi Risteski", "Projects and IT", "Quality control", "21174", 2 },
                    { 335, 50, 21175, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2170), "Zoran Urdarevikj", "Production", "Rolling Unit", "21175", 2 },
                    { 336, 41, 21178, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2172), "Miroslav Martinovski", "Production", "Quality control", "21178", 2 },
                    { 337, 34, 21183, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2173), "Emran Iseinov", "Production", "Human Resources", "21183", 2 },
                    { 338, 54, 21184, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2174), "Mirche Milkovski", "Production", "Crane Maintenance & Internal Transport", "21184", 2 },
                    { 339, 33, 21188, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2176), "Aleksandar Kitanovski", "Production", "Rolling Unit", "21188", 2 },
                    { 340, 31, 21189, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2177), "Dejan Stefanovski", "Production", "Coating Unit", "21189", 2 },
                    { 341, 29, 21190, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2205), "Viktor Stojchevski", "Production", "Rolling Unit", "21190", 2 },
                    { 342, 44, 21191, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2207), "Dragan Risteski", "Supply chain", "Crane Maintenance & Internal Transport", "21191", 2 },
                    { 343, 38, 21193, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2208), "Dzemail Ljimani", "Production", "Coating Unit", "21193", 2 },
                    { 344, 46, 21194, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2209), "Biljana Trajkovska", "Supply chain", "Accounting & Treasury", "21194", 2 },
                    { 345, 36, 21196, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2210), "Miroslav Krstikj", "Production", "Planning & Strategy", "21196", 2 },
                    { 346, 39, 21197, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2212), "Violeta Stojanovska", "CEO office", "Facility", "21197", 2 },
                    { 347, 36, 21198, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2213), "Kristina Kolaroska", "Finance Department", "Crane Maintenance & Internal Transport", "21198", 2 },
                    { 348, 23, 21200, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2214), "David Savevski", "Production", "Automation", "21200", 2 },
                    { 349, 35, 21201, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2215), "Emrah Sali", "Production", "Coating Unit", "21201", 2 },
                    { 350, 46, 21204, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2216), "Robert Ristovski", "Production", "Accounting & Treasury", "21204", 2 },
                    { 351, 52, 21206, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2218), "Marjanche Milkovski", "Projects and IT", "Coating Unit", "21206", 2 },
                    { 352, 62, 21209, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2220), "Ice Trajkoski", "Production", "Facility", "21209", 2 },
                    { 353, 22, 21212, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2221), "Viktor Ilievski", "Production", "Customer service & Logistics", "21212", 2 },
                    { 354, 21, 21218, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2222), "Daniel Slavkovski", "Production", "Coating Unit", "21218", 2 },
                    { 355, 47, 21219, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2224), "Goce Peshevski", "Production", "High voltage", "21219", 2 },
                    { 356, 51, 21224, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2225), "Natasha Mihova", "Finance Department", "Crane Maintenance & Internal Transport", "21224", 2 },
                    { 357, 32, 21225, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2226), "Bujar Zenuli", "Production", "Stores", "21225", 2 },
                    { 358, 28, 21227, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2227), "Tamara Stojchevska", "HR", "Coating Unit", "21227", 2 },
                    { 359, 47, 21229, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2230), "Dragana Velkovikj-Krsteva", "Supply chain", "Customer service & Logistics", "21229", 2 },
                    { 360, 48, 21231, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2231), "Jovica Stojanovski", "Production", "Crane Maintenance & Internal Transport", "21231", 2 },
                    { 361, 23, 21233, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2232), "Mario Trajkovski", "Projects and IT", "Crane Maintenance & Internal Transport", "21233", 2 },
                    { 362, 35, 21240, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2233), "Dancho Kostadinovski", "Projects and IT", "Coating Unit", "21240", 2 },
                    { 363, 30, 21241, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2235), "Konstantin Koneski", "Supply chain", "High voltage", "21241", 2 },
                    { 364, 42, 21243, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2236), "Nenad Mihajloski", "Production", "Quality control", "21243", 2 },
                    { 365, 41, 21247, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2237), "Ilija Andonoski", "Supply chain", "Rolling Unit", "21247", 2 },
                    { 366, 40, 21252, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2238), "Toni Karovchevikj", "Projects and IT", "Quality control", "21252", 2 },
                    { 367, 29, 21254, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2239), "Hristijan Todorovski", "Projects and IT", "Coating Unit", "21254", 2 },
                    { 368, 28, 21257, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2242), "Atanas Boshkov", "Production", "Coating Unit", "21257", 2 },
                    { 369, 23, 21259, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2243), "Damjan Petrovski", "Projects and IT", "Crane Maintenance & Internal Transport", "21259", 2 },
                    { 370, 47, 21260, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2244), "Viktorija Karafiloska", "Supply chain", "Crane Maintenance & Internal Transport", "21260", 2 },
                    { 371, 50, 21261, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2245), "Sashko Janevski", "Production", "Coating Unit", "21261", 2 },
                    { 372, 52, 21262, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2246), "Maja Miloshoska", "Supply chain", "Crane Maintenance & Internal Transport", "21262", 2 },
                    { 373, 44, 21263, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2248), "Elena Stoilkovska", "HR", "Coating Unit", "21263", 2 },
                    { 374, 21, 21268, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2249), "Dragan Najdovski", "Projects and IT", "Crane Maintenance & Internal Transport", "21268", 2 },
                    { 375, 23, 21269, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2250), "Luka Bostandzievski", "Production", "Coating Unit", "21269", 2 },
                    { 376, 39, 21270, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2251), "Sinisha Voinoski", "Production", "Crane Maintenance & Internal Transport", "21270", 2 },
                    { 377, 27, 21271, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2252), "Muhamed Mimin", "Production", "Coating Unit", "21271", 2 },
                    { 378, 26, 21274, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2253), "Nuija Nuijovski", "Projects and IT", "Facility", "21274", 2 },
                    { 379, 49, 21275, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2255), "Svetlana Davkovska", "Finance Department", "Facility", "21275", 2 },
                    { 380, 51, 21277, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2256), "Isa Zenelji", "Production", "Coating Unit", "21277", 2 },
                    { 381, 22, 21280, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2257), "Mario Nikolovski", "Projects and IT", "Quality control", "21280", 2 },
                    { 382, 22, 21281, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2258), "Angel Kostovski", "Production", "Crane Maintenance & Internal Transport", "21281", 2 },
                    { 383, 24, 21282, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2260), "Hristijan Stevkovski", "Supply chain", "Coating Unit", "21282", 2 },
                    { 384, 44, 21283, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2261), "Naim Ajvazi", "Production", "Crane Maintenance & Internal Transport", "21283", 2 },
                    { 385, 51, 21284, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2263), "Miodrag Achkovikj", "Production", "Coating Unit", "21284", 2 },
                    { 386, 26, 21285, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2264), "Andrej Velichkovski", "Projects and IT", "Crane Maintenance & Internal Transport", "21285", 2 },
                    { 387, 47, 21286, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2265), "Dejan Smilevski", "Projects and IT", "Coating Unit", "21286", 2 },
                    { 388, 60, 21288, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2266), "Trajche Trajkovski", "Production", "Crane Maintenance & Internal Transport", "21288", 2 },
                    { 389, 36, 21290, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2267), "Sashko Dimovski", "Projects and IT", "Coating Unit", "21290", 2 },
                    { 390, 57, 21292, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2268), "Dushan Manojlovikj", "Production", "Quality control", "21292", 2 },
                    { 391, 40, 21293, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2270), "Zoran Ilieski", "Projects and IT", "Coating Unit", "21293", 2 },
                    { 392, 20, 21294, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2271), "Antonio Panovski", "Production", "Crane Maintenance & Internal Transport", "21294", 2 },
                    { 393, 44, 21295, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2272), "Violeta Joshovikj", "HR", "Human Resources", "21295", 2 },
                    { 394, 36, 21297, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2291), "Sashka Stojanovska", "HR", "Crane Maintenance & Internal Transport", "21297", 2 },
                    { 395, 51, 21298, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2293), "Ljupcho Emsherijov", "Production", "Rolling Unit", "21298", 2 },
                    { 396, 30, 21299, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2294), "Nikola Risteski", "Supply chain", "Coating Unit", "21299", 2 },
                    { 397, 19, 21300, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2295), "Ljupcho Bogojev", "Projects and IT", "Crane Maintenance & Internal Transport", "21300", 2 },
                    { 398, 32, 21302, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2297), "Erol Idriz", "Projects and IT", "Coating Unit", "21302", 2 },
                    { 399, 59, 21303, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2299), "Blagoja Jovchevski", "Projects and IT", "Crane Maintenance & Internal Transport", "21303", 2 },
                    { 400, 22, 21304, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2300), "Stefan Trajkovikj", "Production", "Coating Unit", "21304", 2 },
                    { 401, 43, 21305, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2301), "Vesna Gjorgjevska", "HR", "Facility", "21305", 2 },
                    { 402, 24, 21306, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2302), "Mihaela Gecheva", "HR", "Facility", "21306", 2 },
                    { 403, 24, 21307, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2304), "Marija Malinova", "Supply chain", "Planning & Strategy", "21307", 2 },
                    { 404, 24, 21308, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2305), "Viktorija Siljanoska", "Projects and IT", "Automation", "21308", 2 },
                    { 405, 30, 21309, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2306), "Aleksandar Paunkovikj", "Projects and IT", "Crane Maintenance & Internal Transport", "21309", 2 },
                    { 406, 20, 21310, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2307), "Stefan Cvetanovski", "Production", "Coating Unit", "21310", 2 },
                    { 407, 51, 21311, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2309), "Valentina Cibreva", "Finance Department", "Accounting & Treasury", "21311", 2 },
                    { 408, 27, 21312, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2310), "Milancho Uroshevski", "Supply chain", "Planning & Strategy", "21312", 2 },
                    { 409, 61, 21313, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2311), "Jashar Ismaili", "HR", "Facility", "21313", 2 },
                    { 410, 34, 21314, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2312), "Daniel Neshkovikj", "Daniel", "Crane Maintenance & Internal Transport", "21314", 2 },
                    { 411, 26, 21315, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2314), "Hristina Jovanovska", "Projects and IT", "Automation", "21315", 2 },
                    { 412, 46, 21316, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2315), "Marjan Georgiev", "Production", "Coating Unit", "21316", 2 },
                    { 413, 56, 3320621, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2316), "Todorka Ristovska", "CEO office", "CEO office", "20621", 3 },
                    { 414, 44, 3320724, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2317), "Vesna Velichkovska", "HR", "Human Resources and Legal Affairs", "20724", 3 },
                    { 415, 48, 3320640, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2320), "Kiro Risteski", "Production", "Production", "20640", 3 },
                    { 416, 46, 3320650, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2321), "Dejana Jovanova Krsteva", "Supply chain", "Supply chain", "20650", 3 },
                    { 417, 45, 3320623, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2322), "Elena Blazeva", "Finance Department", "Finance Department", "20623", 3 },
                    { 418, 43, 3320889, new DateTime(2025, 11, 13, 13, 21, 55, 615, DateTimeKind.Utc).AddTicks(2323), "Dushan Jovanoski", "Sales", "Sales", "20889", 3 }
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
