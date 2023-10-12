CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetAllMatchResults_v1]
AS
	SELECT bti.Id, bti.MatchTime, bti.League, bti.HomeTeam, bti.AwayTeam, 
			bti.HomeScore as BTIHomeScore, bti.AwayScore as BTIAwayScore, 
			sbo.HomeFullTimeScore as SBOHomeScore, sbo.AwayFullTimeScore as SBOAwayScore, bti.Condition 
	FROM dbo.MatchResult as bti
	LEFT JOIN dbo.SBOMatchResult as sbo
	ON bti.MatchTime = sbo.MatchTime AND bti.HomeScore = sbo.HomeFullTimeScore AND bti.AwayScore = sbo.AwayFullTimeScore
	ORDER BY bti.MatchTime
RETURN
