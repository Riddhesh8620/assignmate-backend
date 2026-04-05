using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmateFunctional.DAL.Migrations;

/// <inheritdoc />
public partial class AlterTable_User : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.AddColumn<string>(
            name: "phone_number",
            schema: "assignmate",
            table: "assignmate_users",
            type: "varchar(50)",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.DropColumn(
            name: "phone_number",
            schema: "assignmate",
            table: "assignmate_users");
    }
}