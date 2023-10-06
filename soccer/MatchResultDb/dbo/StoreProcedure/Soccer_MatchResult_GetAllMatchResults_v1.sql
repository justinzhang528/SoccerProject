CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetAllMatchResults_v1]
AS
	SELECT * FROM dbo.MatchResult ORDER BY GameTime
RETURN
