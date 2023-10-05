CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetAllMatchResults_v1]
AS
	SELECT * FROM dbo.MatchResult ORDER BY GameTime
	
	RETURN
GO

CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetAllMatchDetails_v1]
AS
	SELECT * FROM dbo.MatchDetail
	
	RETURN
GO

CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetMatchResultById_v1]
	@Id nvarchar(80)
AS
   SELECT * FROM dbo.MatchResult WHERE Id = @Id
   
   RETURN;
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetDetailById_v1]
	@Id nvarchar(80)
AS    
   SET NOCOUNT ON;

   SELECT * FROM dbo.MatchDetail WHERE Id = @Id
   
   RETURN;
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_AddMatchResult_v1]
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


CREATE PROCEDURE [dbo].[Soccer_MatchResult_AddMatchDetail_v1]
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


CREATE PROCEDURE [dbo].[Soccer_MatchResult_AddHistory_v1]
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
			@PenaltiesShootout_A,
			GETDATE())
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_UpdateMatchResultById_v1]
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


CREATE PROCEDURE [dbo].[Soccer_MatchResult_UpdateMatchDetailById_v1]
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


CREATE TYPE MatchResultType 
   AS TABLE
      ( Id varchar(80) NOT NULL PRIMARY KEY,
		GameTime DateTime NOT NULL,
		Leagues varchar(100) NOT NULL,
		HomeTeam varchar(50) NOT NULL,
		AwayTeam varchar(50) NOT NULL,
		HomeScore int,
		AwayScore int,
		Condition int );
GO

CREATE TYPE MatchDetailType 
   AS TABLE
      ( Id varchar(80) NOT NULL PRIMARY KEY,
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
		PenaltiesShootout_A int );
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_UpdateAllMatchResults_v1]
	@Results dbo.MatchResultType READONLY
AS
BEGIN
    SELECT * INTO #TempResult
    FROM @Results;

	MERGE dbo.MatchResult AS tar
	USING #TempResult AS src
	ON tar.Id = src.Id

	-- For Insert
	WHEN NOT MATCHED BY TARGET THEN
		INSERT VALUES (src.ID, src.GameTime, src.Leagues, src.HomeTeam, 
						src.AwayTeam, src.HomeScore, src.AwayScore, src.Condition)
	-- For Update
	WHEN MATCHED THEN UPDATE SET
		tar.HomeScore = src.HomeScore,
		tar.AwayScore = src.AwayScore,
		tar.Condition = src.Condition;
END
GO


CREATE PROCEDURE [dbo].[Soccer_MatchResult_UpdateAllMatchDetails_v1]
	@Details dbo.MatchDetailType READONLY
AS
BEGIN
    SELECT * INTO #TempDetail
    FROM @Details;

	MERGE dbo.MatchDetail AS tar
	USING #TempDetail AS src
	ON tar.Id = src.Id

	-- For Insert
	WHEN NOT MATCHED BY TARGET THEN
		INSERT VALUES (src.ID, src.FirstHalf_H, src.FirstHalf_A,
						src.SecondHalf_H, src.SecondHalf_A, src.RegularTime_H,
						src.RegularTime_A, src.Corners_H, src.Corners_A,
						src.Penalties_H, src.Penalties_A, src.YellowCards_H,
						src.YellowCards_A, src.RedCards_H, src.RedCards_A,
						src.FirstET_H, src.FirstET_A, src.SecondET_H,
						src.SecondET_A, src.PenaltiesShootout_H, src.PenaltiesShootout_A)
	-- For Update
	WHEN MATCHED AND (
		tar.FirstHalf_H <> src.FirstHalf_H OR
		tar.FirstHalf_A <> src.FirstHalf_A OR
		tar.SecondHalf_H <> src.SecondHalf_H OR
		tar.SecondHalf_A <> src.SecondHalf_A OR
		tar.RegularTime_H <> src.RegularTime_H OR
		tar.RegularTime_A <> src.RegularTime_A OR
		tar.Corners_H <> src.Corners_H OR
		tar.Corners_A <> src.Corners_A OR
		tar.Penalties_H <> src.Penalties_H OR
		tar.Penalties_A <> src.Penalties_A OR
		tar.YellowCards_H <> src.YellowCards_H OR
		tar.YellowCards_A <> src.YellowCards_A OR
		tar.RedCards_H <> src.RedCards_H OR
		tar.RedCards_A <> src.RedCards_A OR
		tar.FirstET_H <> src.FirstET_H OR
		tar.FirstET_A <> src.FirstET_A OR
		tar.SecondET_H <> src.SecondET_H OR
		tar.SecondET_A <> src.SecondET_A OR
		tar.PenaltiesShootout_H <> src.PenaltiesShootout_H OR
		tar.PenaltiesShootout_A <> src.PenaltiesShootout_A
	) THEN UPDATE SET
		tar.FirstHalf_H = src.FirstHalf_H,
		tar.FirstHalf_A = src.FirstHalf_A,
		tar.SecondHalf_H = src.SecondHalf_H,
		tar.SecondHalf_A = src.SecondHalf_A,
		tar.RegularTime_H = src.RegularTime_H,
		tar.RegularTime_A = src.RegularTime_A,
		tar.Corners_H = src.Corners_H,
		tar.Corners_A = src.Corners_A,
		tar.Penalties_H = src.Penalties_H,
		tar.Penalties_A = src.Penalties_A,
		tar.YellowCards_H = src.YellowCards_H,
		tar.YellowCards_A = src.YellowCards_A,
		tar.RedCards_H = src.RedCards_H,
		tar.RedCards_A = src.RedCards_A,
		tar.FirstET_H = src.FirstET_H,
		tar.FirstET_A = src.FirstET_A,
		tar.SecondET_H = src.SecondET_H,
		tar.SecondET_A = src.SecondET_A,
		tar.PenaltiesShootout_H = src.PenaltiesShootout_H,
		tar.PenaltiesShootout_A = src.PenaltiesShootout_A
	OUTPUT
		DELETED.*, GETDATE() AS UpdateTime
	INTO dbo.History;
END
GO