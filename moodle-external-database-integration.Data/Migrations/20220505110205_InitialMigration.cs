using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace moodle_external_database_integration.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "external_transfer_courses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_external_transfer_courses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "external_transfer_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    e_mail = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_external_transfer_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "moodle_courses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    short_name = table.Column<string>(type: "text", nullable: false),
                    id_number = table.Column<string>(type: "text", nullable: false, computedColumnSql: "CAST(id AS text)", stored: true),
                    moodle_id = table.Column<int>(type: "integer", nullable: true),
                    external_transfer_course_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moodle_courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_moodle_courses_external_transfer_courses_external_transfer_~",
                        column: x => x.external_transfer_course_id,
                        principalTable: "external_transfer_courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "external_transfer_courses_users",
                columns: table => new
                {
                    external_transfer_course_id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_transfer_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_external_transfer_courses_users", x => new { x.external_transfer_course_id, x.external_transfer_user_id });
                    table.ForeignKey(
                        name: "FK_external_transfer_courses_users_external_transfer_courses_e~",
                        column: x => x.external_transfer_course_id,
                        principalTable: "external_transfer_courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_external_transfer_courses_users_external_transfer_users_ext~",
                        column: x => x.external_transfer_user_id,
                        principalTable: "external_transfer_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "moodle_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    e_mail = table.Column<string>(type: "text", nullable: false),
                    id_number = table.Column<string>(type: "text", nullable: false, computedColumnSql: "CAST(id AS text)", stored: true),
                    moodle_id = table.Column<int>(type: "integer", nullable: true),
                    external_transfer_user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moodle_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_moodle_users_external_transfer_users_external_transfer_user~",
                        column: x => x.external_transfer_user_id,
                        principalTable: "external_transfer_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "moodle_enrolments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id_number = table.Column<string>(type: "text", nullable: false, computedColumnSql: "CAST(moodle_user_id AS text)", stored: true),
                    moodle_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    course_id_number = table.Column<string>(type: "text", nullable: false, computedColumnSql: "CAST(moodle_course_id AS text)", stored: true),
                    moodle_course_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: true),
                    external_transfer_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_transfer_course_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moodle_enrolments", x => x.id);
                    table.ForeignKey(
                        name: "FK_moodle_enrolments_external_transfer_courses_external_transf~",
                        column: x => x.external_transfer_course_id,
                        principalTable: "external_transfer_courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_moodle_enrolments_external_transfer_users_external_transfer~",
                        column: x => x.external_transfer_user_id,
                        principalTable: "external_transfer_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_moodle_enrolments_moodle_courses_moodle_course_id",
                        column: x => x.moodle_course_id,
                        principalTable: "moodle_courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_moodle_enrolments_moodle_users_moodle_user_id",
                        column: x => x.moodle_user_id,
                        principalTable: "moodle_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_external_transfer_courses_users_external_transfer_user_id",
                table: "external_transfer_courses_users",
                column: "external_transfer_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_moodle_courses_external_transfer_course_id",
                table: "moodle_courses",
                column: "external_transfer_course_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_moodle_enrolments_external_transfer_course_id",
                table: "moodle_enrolments",
                column: "external_transfer_course_id");

            migrationBuilder.CreateIndex(
                name: "IX_moodle_enrolments_external_transfer_user_id",
                table: "moodle_enrolments",
                column: "external_transfer_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_moodle_enrolments_moodle_course_id",
                table: "moodle_enrolments",
                column: "moodle_course_id");

            migrationBuilder.CreateIndex(
                name: "IX_moodle_enrolments_moodle_user_id",
                table: "moodle_enrolments",
                column: "moodle_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_moodle_users_external_transfer_user_id",
                table: "moodle_users",
                column: "external_transfer_user_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "external_transfer_courses_users");

            migrationBuilder.DropTable(
                name: "moodle_enrolments");

            migrationBuilder.DropTable(
                name: "moodle_courses");

            migrationBuilder.DropTable(
                name: "moodle_users");

            migrationBuilder.DropTable(
                name: "external_transfer_courses");

            migrationBuilder.DropTable(
                name: "external_transfer_users");
        }
    }
}
