CREATE TABLE [dbo].[SBOMatchResult]
(
	Id varchar(80) NOT NULL PRIMARY KEY,
	Leagues varchar(100) NOT NULL,
	TeamA varchar(50) NOT NULL,
	TeamB varchar(50) NOT NULL,
	GameTime DateTime NOT NULL,
	FirstHalfScore int,
	FullTimeScore int,
	IsShowMoreData int,
	UpdateTime DATETIME
)
