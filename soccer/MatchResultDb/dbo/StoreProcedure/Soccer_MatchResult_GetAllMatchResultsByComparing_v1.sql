CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetAllMatchResultsByComparing_v1]
AS
select bti.Id, bti.MatchTime, bti.League as BTILeague, bti.HomeTeam as BTIHomeTeam, bti.AwayTeam as BTIAwayTeam,
			sbo.League as SBOLeague, sbo.HomeTeam as SBOHomeTeam, sbo.AwayTeam as SBOAwayTeam,
			bti.HomeScore as BTIHomeScore, bti.AwayScore as BTIAwayScore, 
			sbo.HomeFullTimeScore as SBOHomeScore, sbo.AwayFullTimeScore as SBOAwayScore, bti.Condition from
	(select a.*, lg.Id as LeagueId, htm.Id as HomeTeamId, atm.Id as AwayTeamId 
		from MatchResult as a 
		left join League as lg on a.League = lg.BTILeagueName
		left join Team as htm on a.HomeTeam = htm.BTITeamName
		left join Team as atm on a.AwayTeam = atm.BTITeamName) as bti
left join
	(select a.*, lg.Id as LeagueId, htm.Id as HomeTeamId, atm.Id as AwayTeamId 
		from SBOMatchResult as a 
		left join League as lg on a.League = lg.SBOLeagueName
		left join Team as htm on a.HomeTeam = htm.SBOTeamName
		left join Team as atm on a.AwayTeam = atm.SBOTeamName) as sbo
on (bti.MatchTime = sbo.MatchTime or bti.MatchTime = DATEADD(MINUTE, 1, sbo.MatchTime))
	and (bti.League = sbo.League or bti.LeagueId = sbo.LeagueId)
	and (bti.HomeTeam = sbo.HomeTeam or bti.HomeTeamId = sbo.HomeTeamId)
order by bti.League
RETURN 0

