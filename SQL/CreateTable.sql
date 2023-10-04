IF OBJECT_ID(N'[dbo].[Result]', N'U') IS NULL
BEGIN
    CREATE TABLE Result
    (
        Id varchar(80) NOT NULL PRIMARY KEY,
		GameTime DateTime NOT NULL,
		Leagues varchar(100) NOT NULL,
		HomeTeam varchar(50) NOT NULL,
		AwayTeam varchar(50) NOT NULL,
		HomeScore int,
		AwayScore int,
		Condition int --1:normal, 0:cancelled, 2:notStart
    );
END

IF OBJECT_ID(N'[dbo].[Detail]', N'U') IS NULL
BEGIN
    CREATE TABLE Detail
    (
        Id varchar(80) NOT NULL PRIMARY KEY,
		FirstHalf_H int,
		FirstHalf_A int,
		SecondHalf_H int,
		SecondHalf_A int,
		RegularTime_H int,
		RegularTime_A int,
		Corners_H int,
		Corners_A int,
		Penalties_H int,
		Penalties_A int,
		YellowCards_H int,
		YellowCards_A int,
		RedCards_H int,
		RedCards_A int,
		FirstET_H int,
		FirstET_A int,
		SecondET_H int,
		SecondET_A int,
		PenaltiesShootout_H int,
		PenaltiesShootout_A int
    );
END

IF OBJECT_ID(N'[dbo].[History]', N'U') IS NULL
BEGIN
    CREATE TABLE History
    (
        Id int IDENTITY(1,1) PRIMARY KEY,
		ResultId varchar(80) NOT NULL,
		FirstHalf_H int,
		FirstHalf_A int,
		SecondHalf_H int,
		SecondHalf_A int,
		RegularTime_H int,
		RegularTime_A int,
		Corners_H int,
		Corners_A int,
		Penalties_H int,
		Penalties_A int,
		YellowCards_H int,
		YellowCards_A int,
		RedCards_H int,
		RedCards_A int,
		FirstET_H int,
		FirstET_A int,
		SecondET_H int,
		SecondET_A int,
		PenaltiesShootout_H int,
		PenaltiesShootout_A int
    );
END