using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class VisitRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpRegisters_FinanceManagers_FinanceManagerId",
                table: "EmpRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_Medical_Invoice_EmpRegisters_EmpId",
                table: "Medical_Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Medical_Invoice_Polices_PolicyId",
                table: "Medical_Invoice");

            migrationBuilder.DropTable(
                name: "Polices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitRequest",
                table: "VisitRequest");

            migrationBuilder.DropIndex(
                name: "IX_Medical_Invoice_EmpId",
                table: "Medical_Invoice");

            migrationBuilder.DropIndex(
                name: "IX_EmpRegisters_FinanceManagerId",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "EmpRegisters");

            migrationBuilder.RenameColumn(
                name: "PolicyId",
                table: "Medical_Invoice",
                newName: "EmployeeEmpId");

            migrationBuilder.RenameColumn(
                name: "ManagerEmail",
                table: "Medical_Invoice",
                newName: "Emp_Name");

            migrationBuilder.RenameColumn(
                name: "EmployeeEmail",
                table: "Medical_Invoice",
                newName: "Emp_Email");

            migrationBuilder.RenameColumn(
                name: "AdminEmail",
                table: "Medical_Invoice",
                newName: "Desc");

            migrationBuilder.RenameIndex(
                name: "IX_Medical_Invoice_PolicyId",
                table: "Medical_Invoice",
                newName: "IX_Medical_Invoice_EmployeeEmpId");

            migrationBuilder.RenameColumn(
                name: "FinanceManagerId",
                table: "EmpRegisters",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "EmpId",
                table: "VisitRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "VisitRequestId",
                table: "VisitRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Emp_CNIC",
                table: "VisitRequest",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalDoctors",
                table: "Per_Hospitals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalNurses",
                table: "Per_Hospitals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Medical_Invoice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "EmpRegisters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "EmpRegisters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactNo",
                table: "EmpRegisters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EmpRegisters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "EmpRegisters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PolicyDate",
                table: "EmpRegisters",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Remaining_Amount",
                table: "EmpRegisters",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "EmpRegisters",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitRequest",
                table: "VisitRequest",
                column: "EmpId");

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    PolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitRequest_VisitRequestId",
                table: "VisitRequest",
                column: "VisitRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_VisitRequestId",
                table: "Hospitals",
                column: "VisitRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceManagers_VisitRequestId",
                table: "FinanceManagers",
                column: "VisitRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpRegisters_PolicyId",
                table: "EmpRegisters",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpRegisters_RoleId",
                table: "EmpRegisters",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmpRegisters_Policies_PolicyId",
                table: "EmpRegisters",
                column: "PolicyId",
                principalTable: "Policies",
                principalColumn: "PolicyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmpRegisters_Roles_RoleId",
                table: "EmpRegisters",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinanceManagers_VisitRequest_VisitRequestId",
                table: "FinanceManagers",
                column: "VisitRequestId",
                principalTable: "VisitRequest",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_VisitRequest_VisitRequestId",
                table: "Hospitals",
                column: "VisitRequestId",
                principalTable: "VisitRequest",
                principalColumn: "EmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medical_Invoice_EmpRegisters_EmployeeEmpId",
                table: "Medical_Invoice",
                column: "EmployeeEmpId",
                principalTable: "EmpRegisters",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmpRegisters_Policies_PolicyId",
                table: "EmpRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_EmpRegisters_Roles_RoleId",
                table: "EmpRegisters");

            migrationBuilder.DropForeignKey(
                name: "FK_FinanceManagers_VisitRequest_VisitRequestId",
                table: "FinanceManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_VisitRequest_VisitRequestId",
                table: "Hospitals");

            migrationBuilder.DropForeignKey(
                name: "FK_Medical_Invoice_EmpRegisters_EmployeeEmpId",
                table: "Medical_Invoice");

            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VisitRequest",
                table: "VisitRequest");

            migrationBuilder.DropIndex(
                name: "IX_VisitRequest_VisitRequestId",
                table: "VisitRequest");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_VisitRequestId",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_FinanceManagers_VisitRequestId",
                table: "FinanceManagers");

            migrationBuilder.DropIndex(
                name: "IX_EmpRegisters_PolicyId",
                table: "EmpRegisters");

            migrationBuilder.DropIndex(
                name: "IX_EmpRegisters_RoleId",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "Emp_CNIC",
                table: "VisitRequest");

            migrationBuilder.DropColumn(
                name: "TotalDoctors",
                table: "Per_Hospitals");

            migrationBuilder.DropColumn(
                name: "TotalNurses",
                table: "Per_Hospitals");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Medical_Invoice");

            migrationBuilder.DropColumn(
                name: "VisitRequestId",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "VisitRequestId",
                table: "FinanceManagers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "ContactNo",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "PolicyDate",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "Remaining_Amount",
                table: "EmpRegisters");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "EmpRegisters");

            migrationBuilder.RenameColumn(
                name: "EmployeeEmpId",
                table: "Medical_Invoice",
                newName: "PolicyId");

            migrationBuilder.RenameColumn(
                name: "Emp_Name",
                table: "Medical_Invoice",
                newName: "ManagerEmail");

            migrationBuilder.RenameColumn(
                name: "Emp_Email",
                table: "Medical_Invoice",
                newName: "EmployeeEmail");

            migrationBuilder.RenameColumn(
                name: "Desc",
                table: "Medical_Invoice",
                newName: "AdminEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Medical_Invoice_EmployeeEmpId",
                table: "Medical_Invoice",
                newName: "IX_Medical_Invoice_PolicyId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "EmpRegisters",
                newName: "FinanceManagerId");

            migrationBuilder.AlterColumn<int>(
                name: "VisitRequestId",
                table: "VisitRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "EmpId",
                table: "VisitRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "EmpRegisters",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "EmpRegisters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "EmpRegisters",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "EmpRegisters",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "EmpRegisters",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VisitRequest",
                table: "VisitRequest",
                column: "VisitRequestId");

            migrationBuilder.CreateTable(
                name: "Polices",
                columns: table => new
                {
                    PolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyAmount = table.Column<int>(type: "int", nullable: false),
                    PolicyDesc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RemaningAmount = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polices", x => x.PolicyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Invoice_EmpId",
                table: "Medical_Invoice",
                column: "EmpId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Medical_Invoice_EmpRegisters_EmpId",
                table: "Medical_Invoice",
                column: "EmpId",
                principalTable: "EmpRegisters",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medical_Invoice_Polices_PolicyId",
                table: "Medical_Invoice",
                column: "PolicyId",
                principalTable: "Polices",
                principalColumn: "PolicyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
