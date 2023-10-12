CREATE TABLE MatchResult
(
    Id varchar(80) NOT NULL PRIMARY KEY,
	MatchTime DateTime NOT NULL,
	League varchar(100) NOT NULL,
	HomeTeam varchar(50) NOT NULL,
	AwayTeam varchar(50) NOT NULL,
	HomeScore int,
	AwayScore int,
	Condition int,
	UpdateTime DATETIME
);