using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpRegisters_VisitRequest_VisitRequestId",
                table: "EmpRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceManagers_VisitRequest_VisitRequestId",
                table: "FinanceManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_VisitRequest_VisitRequestId",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_VisitRequestId",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_FinanceManagers_VisitRequestId",
                table: "FinanceManagers");

            migrationBuilder.DropIndex(
                name: "IX_EmpRegisters_VisitRequestId",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "VisitRequestId",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "VisitRequestId",
                table: "FinanceManagers");

            migrationBuilder.DropColumn(
                name: "VisitRequestId",
                table: "Employee");

            migrationBuilder.AddColumn<string>(
                name: "AdminEmail",
                table: "Medical_Invoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeEmail",
                table: "Medical_Invoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerEmail",
                table: "Medical_Invoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FinanceManagerId",
                table: "EmpRegisters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmpRegisters_FinanceManagerId",
                table: "EmpRegisters",
                column: "FinanceManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpRegisters_FinanceManagers_FinanceManagerId",
                table: "EmpRegisters",
                column: "FinanceManagerId",
                principalTable: "FinanceManagers",
                principalColumn: "FinanceManagerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_FinanceManagers_FinanceManagerId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_FinanceManagerId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "AdminEmail",
                table: "Medical_Invoice");

            migrationBuilder.DropColumn(
                name: "EmployeeEmail",
                table: "Medical_Invoice");

            migrationBuilder.DropColumn(
                name: "ManagerEmail",
                table: "Medical_Invoice");

            migrationBuilder.DropColumn(
                name: "FinanceManagerId",
                table: "Employee");

            migrationBuilder.AddColumn<int>(
                name: "VisitRequestId",
                table: "Hospitals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitRequestId",
                table: "FinanceManagers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VisitRequestId",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_VisitRequestId",
                table: "Hospitals",
                column: "VisitRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceManagers_VisitRequestId",
                table: "FinanceManagers",
                column: "VisitRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpRegisters_VisitRequestId",
                table: "EmpRegisters",
                column: "VisitRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpRegisters_VisitRequest_VisitRequestId",
                table: "EmpRegisters",
                column: "VisitRequestId",
                principalTable: "VisitRequest",
                principalColumn: "VisitRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceManagers_VisitRequest_VisitRequestId",
                table: "FinanceManagers",
                column: "VisitRequestId",
                principalTable: "VisitRequest",
                principalColumn: "VisitRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_VisitRequest_VisitRequestId",
                table: "Hospitals",
                column: "VisitRequestId",
                principalTable: "VisitRequest",
                principalColumn: "VisitRequestId");
        }
    }
}
