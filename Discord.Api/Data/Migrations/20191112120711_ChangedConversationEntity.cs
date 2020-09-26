using Microsoft.EntityFrameworkCore.Migrations;

namespace Discord.Api.Migrations
{
    public partial class ChangedConversationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Userids",
                table: "Conversations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Userids",
                table: "Conversations");
        }
    }
}
