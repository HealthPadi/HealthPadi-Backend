using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthPadiWebApi.Migrations
{
    /// <inheritdoc />
    public partial class TaskExecutionLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskExecutionLogs",
                columns: table => new
                {
                    TaskExecutionLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastExecutionTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskExecutionLogs", x => x.TaskExecutionLogId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskExecutionLogs");
        }
    }
}
