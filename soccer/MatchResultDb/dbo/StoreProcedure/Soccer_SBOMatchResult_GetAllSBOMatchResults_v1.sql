CREATE PROCEDURE [dbo].[Soccer_SBOMatchResult_GetAllSBOMatchResults_v1]
AS
	SELECT * FROM dbo.SBOMatchResult ORDER BY MatchTime
RETURN
