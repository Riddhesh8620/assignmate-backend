using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmateFunctional.DAL.Migrations;

/// <inheritdoc />
public partial class CreationAllTables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        _ = migrationBuilder.EnsureSchema(
            name: "assignmate");

        _ = migrationBuilder.CreateTable(
            name: "assignmate_users",
            schema: "assignmate",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                first_name = table.Column<string>(type: "varchar(200)", nullable: false),
                last_name = table.Column<string>(type: "text", nullable: false),
                email = table.Column<string>(type: "varchar(100)", nullable: false),
                user_name = table.Column<string>(type: "varchar(100)", nullable: false),
                password = table.Column<string>(type: "text", nullable: false),
                role = table.Column<int>(type: "integer", nullable: false),
                added_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                added_by = table.Column<Guid>(type: "uuid", nullable: false),
                updated_by = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_assignmate_users", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_assignmate_users_role_email",
            schema: "assignmate",
            table: "assignmate_users",
            columns: ["role", "email"],
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "assignmate_users",
            schema: "assignmate");
    }
}
