using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace helpdesk_ticketing_system_api.Migrations
{
    /// <inheritdoc />
    public partial class addedHelpdeskTicketObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ticketID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ticketTitle = table.Column<string>(type: "text", nullable: false),
                    ticketDescription = table.Column<string>(type: "text", nullable: false),
                    ticketStatus = table.Column<string>(type: "text", nullable: false),
                    technicianID = table.Column<int>(type: "integer", nullable: false),
                    employeeID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ticketID);
                    table.ForeignKey(
                        name: "FK_Tickets_Employees_employeeID",
                        column: x => x.employeeID,
                        principalTable: "Employees",
                        principalColumn: "employeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Technicians_technicianID",
                        column: x => x.technicianID,
                        principalTable: "Technicians",
                        principalColumn: "technicianID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_employeeID",
                table: "Tickets",
                column: "employeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_technicianID",
                table: "Tickets",
                column: "technicianID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
