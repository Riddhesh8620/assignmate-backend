using Microsoft.EntityFrameworkCore.Migrations;
using System.Text.Json;

#nullable disable

namespace AssignmateFunctional.DAL.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "assignmate");

        migrationBuilder.CreateTable(
            name: "assignmate_users",
            schema: "assignmate",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                first_name = table.Column<string>(type: "varchar(200)", nullable: false),
                last_name = table.Column<string>(type: "varchar(200)", nullable: true),
                email = table.Column<string>(type: "varchar(100)", nullable: false),
                user_name = table.Column<string>(type: "varchar(100)", nullable: false),
                password = table.Column<string>(type: "text", nullable: false),
                phone_isd = table.Column<int>(type: "INT4", nullable: false),
                phone_number = table.Column<string>(type: "varchar(50)", nullable: false),
                role = table.Column<int>(type: "integer", nullable: false),
                writer_profile = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                added_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                added_by = table.Column<Guid>(type: "uuid", nullable: false),
                updated_by = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_assignmate_users", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "assignments",
            schema: "assignmate",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_id = table.Column<Guid>(type: "uuid", nullable: false),
                title = table.Column<string>(type: "TEXT", nullable: false),
                subject = table.Column<string>(type: "TEXT", nullable: false),
                topic = table.Column<string>(type: "TEXT", nullable: false),
                description = table.Column<string>(type: "text", nullable: true),
                num_pages = table.Column<long>(type: "bigint", nullable: false),
                deadline = table.Column<DateTime>(type: "TIMESTAMPTZ", nullable: false),
                budget = table.Column<double>(type: "double precision", nullable: false),
                special_instructions = table.Column<string>(type: "text", nullable: true),
                status = table.Column<string>(type: "text", nullable: false),
                college_id = table.Column<Guid>(type: "uuid", nullable: true),
                file_name = table.Column<string>(type: "text", nullable: true),
                file_content = table.Column<byte[]>(type: "bytea", nullable: true),
                added_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_on = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                added_by = table.Column<Guid>(type: "uuid", nullable: false),
                updated_by = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_assignments", x => x.id);
                table.ForeignKey(
                    name: "fk_assignments_assignmate_users_user_id",
                    column: x => x.user_id,
                    principalSchema: "assignmate",
                    principalTable: "assignmate_users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_assignmate_users_role_email",
            schema: "assignmate",
            table: "assignmate_users",
            columns: new[] { "role", "email" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "ix_assignments_status",
            schema: "assignmate",
            table: "assignments",
            column: "status");

        migrationBuilder.CreateIndex(
            name: "ix_assignments_subject",
            schema: "assignmate",
            table: "assignments",
            column: "subject");

        migrationBuilder.CreateIndex(
            name: "ix_assignments_user_id",
            schema: "assignmate",
            table: "assignments",
            column: "user_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "assignments",
            schema: "assignmate");

        migrationBuilder.DropTable(
            name: "assignmate_users",
            schema: "assignmate");
    }
}
