using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class SamuraiBattleStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE FUNCTION dbo.EarliesBattleFoughtBySamurai ( @samuraiId INT )
                    RETURNS CHAR(30)
                    AS
                        BEGIN 
                            DECLARE @ret CHAR(30);
                            SELECT TOP 1
                                    @ret = Name
                            FROM    dbo.Battles
                            WHERE   Id IN ( SELECT  BattleId
                                                    FROM    dbo.SamuraiBattle
                                                    WHERE   SamuraiId = @samuraiId )
                            ORDER BY StartDate;
                            RETURN @ret; 
                        END;");
            migrationBuilder.Sql(
                @"CREATE VIEW dbo.SamuraiBattleStats
                    AS
                        SELECT  Name ,
                                COUNT(BattleId) AS NumberOfBattles ,
                                dbo.EarliesBattleFoughtBySamurai(MIN(Id)) AS EarliestBattle
                        FROM    dbo.SamuraiBattle
                                INNER JOIN dbo.Samurais ON SamuraiId = Id
                        GROUP BY Name ,
                                SamuraiId;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.SamuraiBattleStats");
            migrationBuilder.Sql("DROP FUNCTION dbo.EarliestBattleFoughtBySamurai");
        }
    }
}
