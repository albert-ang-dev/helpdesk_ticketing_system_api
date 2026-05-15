using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace helpdesk_ticketing_system_api.Migrations
{
    /// <inheritdoc />
    public partial class finalFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Employees_employeeID",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Technicians_technicianID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_employeeID",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_technicianID",
                table: "Tickets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_employeeID",
                table: "Tickets",
                column: "employeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_technicianID",
                table: "Tickets",
                column: "technicianID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Employees_employeeID",
                table: "Tickets",
                column: "employeeID",
                principalTable: "Employees",
                principalColumn: "employeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Technicians_technicianID",
                table: "Tickets",
                column: "technicianID",
                principalTable: "Technicians",
                principalColumn: "technicianID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
