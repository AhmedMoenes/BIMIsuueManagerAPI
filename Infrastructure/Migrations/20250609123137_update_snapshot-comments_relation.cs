using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_snapshotcomments_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SnapshotId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SnapshotId",
                table: "Comments",
                column: "SnapshotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Snapshots_SnapshotId",
                table: "Comments",
                column: "SnapshotId",
                principalTable: "Snapshots",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Snapshots_SnapshotId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SnapshotId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SnapshotId",
                table: "Comments");
        }
    }
}
