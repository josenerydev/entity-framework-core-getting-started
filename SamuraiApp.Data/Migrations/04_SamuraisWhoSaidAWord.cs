using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class SamuraisWhoSaidAWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.SamuraisWhoSaidAWord @text VARCHAR(20)
                    AS
                        SELECT  Samurais.Id ,
                                Name ,
                                ClanId
                        FROM    dbo.Samurais
                                INNER JOIN dbo.Quotes ON Samurais.Id = SamuraiId
                        WHERE   ( Text LIKE '%' + @text + '%' );");

            migrationBuilder.Sql(
                @"CREATE PROCEDURE dbo.DeleteQuotesForSamurai @samuraiId INT
                    AS
                        DELETE  FROM dbo.Quotes
                        WHERE   SamuraiId = @samuraiId;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.SamuraisWhoSaidAWord");
            migrationBuilder.Sql("DROP PROCEDURE dbo.DeleteQuotesForSamurai");
        }
    }
}
