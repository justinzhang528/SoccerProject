﻿CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetAllMatchResults_2023.10.04]
AS
	SELECT * FROM dbo.MatchResult ORDER BY GameTime
	
	RETURN
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetMatchResultById_2023.10.04]
	@Id nvarchar(80)
AS
   SELECT * FROM dbo.MatchResult WHERE Id = @Id
   
   RETURN;
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetDetailById_2023.10.04]
	@Id nvarchar(80)
AS    
   SET NOCOUNT ON;

   SELECT * FROM dbo.MatchDetail WHERE Id = @Id
   
   RETURN;
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_AddMatchResult_2023.10.04]
	@Id varchar(80),
	@GameTime DateTime,
	@Leagues varchar(100),
	@HomeTeam varchar(50),
	@AwayTeam varchar(50),
	@HomeScore int,
	@AwayScore int,
	@Condition int
AS
   SET NOCOUNT ON;

   INSERT INTO dbo.MatchResult
   VALUES ( @Id,
            @GameTime,
            @Leagues,
            @HomeTeam,
			@AwayTeam,
			@HomeScore,
			@AwayScore,
			@Condition)
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_AddMatchDetail_2023.10.04]
	@Id varchar(80),
	@FirstHalf_H int,
	@FirstHalf_A int,
	@SecondHalf_H int,
	@SecondHalf_A int,
	@RegularTime_H int,
	@RegularTime_A int,
	@Corners_H int,
	@Corners_A int,
	@Penalties_H int,
	@Penalties_A int,
	@YellowCards_H int,
	@YellowCards_A int,
	@RedCards_H int,
	@RedCards_A int,
	@FirstET_H int,
	@FirstET_A int,
	@SecondET_H int,
	@SecondET_A int,
	@PenaltiesShootout_H int,
	@PenaltiesShootout_A int
AS
   SET NOCOUNT ON;

   INSERT INTO dbo.MatchDetail
   VALUES ( @Id,
			@FirstHalf_H,
			@FirstHalf_A,
			@SecondHalf_H,
			@SecondHalf_A,
			@RegularTime_H,
			@RegularTime_A,
			@Corners_H,
			@Corners_A,
			@Penalties_H,
			@Penalties_A,
			@YellowCards_H,
			@YellowCards_A,
			@RedCards_H,
			@RedCards_A,
			@FirstET_H,
			@FirstET_A,
			@SecondET_H,
			@SecondET_A,
			@PenaltiesShootout_H,
			@PenaltiesShootout_A)
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_AddHistory_2023.10.04]
	@ResultId varchar(80),
	@FirstHalf_H int,
	@FirstHalf_A int,
	@SecondHalf_H int,
	@SecondHalf_A int,
	@RegularTime_H int,
	@RegularTime_A int,
	@Corners_H int,
	@Corners_A int,
	@Penalties_H int,
	@Penalties_A int,
	@YellowCards_H int,
	@YellowCards_A int,
	@RedCards_H int,
	@RedCards_A int,
	@FirstET_H int,
	@FirstET_A int,
	@SecondET_H int,
	@SecondET_A int,
	@PenaltiesShootout_H int,
	@PenaltiesShootout_A int
AS
   SET NOCOUNT ON;

   INSERT INTO dbo.History
   VALUES ( @ResultId,
			@FirstHalf_H,
			@FirstHalf_A,
			@SecondHalf_H,
			@SecondHalf_A,
			@RegularTime_H,
			@RegularTime_A,
			@Corners_H,
			@Corners_A,
			@Penalties_H,
			@Penalties_A,
			@YellowCards_H,
			@YellowCards_A,
			@RedCards_H,
			@RedCards_A,
			@FirstET_H,
			@FirstET_A,
			@SecondET_H,
			@SecondET_A,
			@PenaltiesShootout_H,
			@PenaltiesShootout_A)
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_UpdateMatchResultById_2023.10.04]
	@Id varchar(80),
	@HomeScore int,
	@AwayScore int,
	@Condition int
AS
   SET NOCOUNT ON;

   UPDATE dbo.MatchResult
   SET HomeScore = @HomeScore,
		AwayScore = @AwayScore,
		Condition = @Condition
	WHERE Id = @Id
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_UpdateMatchDetailById_2023.10.04]
	@Id varchar(80),
	@FirstHalf_H int,
	@FirstHalf_A int,
	@SecondHalf_H int,
	@SecondHalf_A int,
	@RegularTime_H int,
	@RegularTime_A int,
	@Corners_H int,
	@Corners_A int,
	@Penalties_H int,
	@Penalties_A int,
	@YellowCards_H int,
	@YellowCards_A int,
	@RedCards_H int,
	@RedCards_A int,
	@FirstET_H int,
	@FirstET_A int,
	@SecondET_H int,
	@SecondET_A int,
	@PenaltiesShootout_H int,
	@PenaltiesShootout_A int
AS
   SET NOCOUNT ON;

   UPDATE dbo.MatchDetail
   SET FirstHalf_H = @FirstHalf_H,
		FirstHalf_A = @FirstHalf_A,
		SecondHalf_H = @SecondHalf_H,
		SecondHalf_A = @SecondHalf_A,
		RegularTime_H = @RegularTime_H,
		RegularTime_A = @RegularTime_A,
		Corners_H = @Corners_H,
		Corners_A = @Corners_A,
		Penalties_H = @Penalties_H,
		Penalties_A = Penalties_A,
		YellowCards_H = @YellowCards_H,
		YellowCards_A = @YellowCards_A,
		RedCards_H = @RedCards_H,
		RedCards_A = @RedCards_A,
		FirstET_H = @FirstET_H,
		FirstET_A = @FirstET_A,
		SecondET_H = @SecondET_H,
		SecondET_A = @SecondET_A,
		PenaltiesShootout_H = @PenaltiesShootout_H,
		PenaltiesShootout_A = @PenaltiesShootout_A
	WHERE Id = @Id
GO