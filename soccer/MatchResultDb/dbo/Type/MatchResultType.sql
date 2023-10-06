CREATE TYPE [dbo].[MatchResultType] AS TABLE
(
	Id varchar(80) NOT NULL PRIMARY KEY,
	GameTime DateTime NOT NULL,
	Leagues varchar(100) NOT NULL,
	HomeTeam varchar(50) NOT NULL,
	AwayTeam varchar(50) NOT NULL,
	HomeScore int,
	AwayScore int,
	Condition int
)
