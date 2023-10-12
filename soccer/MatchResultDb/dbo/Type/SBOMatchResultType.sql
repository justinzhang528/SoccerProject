CREATE TYPE [dbo].[SBOMatchResultType] AS TABLE
(
	Id varchar(80) NOT NULL PRIMARY KEY,
	League varchar(100) NOT NULL,
	HomeTeam varchar(50) NOT NULL,
	AwayTeam varchar(50) NOT NULL,
	MatchTime DateTime NOT NULL,
	HomeFirstHalfScore varchar(3),
	AwayFirstHalfScore varchar(3),
	HomeFullTimeScore varchar(3),
	AwayFullTimeScore varchar(3),
	IsShowMoreData int,
	UpdateTime DATETIME
)
