CREATE TABLE [dbo].[SBOMatchResult]
(
	Id varchar(80) NOT NULL PRIMARY KEY,
	[League] varchar(100) NOT NULL,
	[HomeTeam] varchar(50) NOT NULL,
	[AwayTeam] varchar(50) NOT NULL,
	[MatchTime] DateTime NOT NULL,
	FirstHalfScore int,
	FullTimeScore int,
	IsShowMoreData int,
	UpdateTime DATETIME
)
