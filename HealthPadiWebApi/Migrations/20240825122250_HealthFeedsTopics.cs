using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthPadiWebApi.Migrations
{
    /// <inheritdoc />
    public partial class HealthFeedsTopics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HealthyLivingTopics",
                columns: table => new
                {
                    HealthyLivingTopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthyLivingTopics", x => x.HealthyLivingTopicId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthyLivingTopics");
        }
    }
}
