CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetMatchDetailById_v1]
	@Id nvarchar(80)
AS
	SELECT * FROM dbo.MatchDetail WHERE Id = @Id
RETURN 0
