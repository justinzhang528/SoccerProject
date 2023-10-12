CREATE TYPE [dbo].[SBOMatchDetailType] AS TABLE
(
	Id varchar(80) NOT NULL PRIMARY KEY,
	MarketType varchar(100) NOT NULL,
	HomeFirstHalfScore varchar(3),
	AwayFirstHalfScore varchar(3),
	HomeFullTimeScore varchar(3),
	AwayFullTimeScore varchar(3),
	Code int NOT NULL,
	UpdateTime DATETIME
)
