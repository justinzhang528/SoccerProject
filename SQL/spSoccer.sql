USE master;
GO

-- GetAllResults
IF OBJECT_ID('dbo.spGetAllResults', 'P') IS NOT NULL  
   DROP PROCEDURE dbo.spGetAllResults;  
GO

CREATE PROCEDURE dbo.spGetAllResults
AS    
   SET NOCOUNT ON;

   SELECT * FROM dbo.Result ORDER BY GameTime
   
   RETURN;
GO


-- GetResultById
IF OBJECT_ID('dbo.spGetResultById', 'P') IS NOT NULL  
   DROP PROCEDURE dbo.spGetResultById;  
GO  

CREATE PROCEDURE dbo.spGetResultById
	@Id nvarchar(80)
AS    
   SET NOCOUNT ON;

   SELECT * FROM dbo.Result WHERE Id = @Id
   
   RETURN;
GO


-- GetDetailById
IF OBJECT_ID('dbo.spGetDetailById', 'P') IS NOT NULL  
   DROP PROCEDURE dbo.spGetDetailById;  
GO  

CREATE PROCEDURE dbo.spGetDetailById
	@Id nvarchar(80)
AS    
   SET NOCOUNT ON;

   SELECT * FROM dbo.Detail WHERE Id = @Id
   
   RETURN;
GO


-- AddResult
IF OBJECT_ID('dbo.spAddResult', 'P') IS NOT NULL  
   DROP PROCEDURE dbo.spAddResult;  
GO  

CREATE PROCEDURE dbo.spAddResult
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

   INSERT INTO dbo.Result
   VALUES ( @Id,
            @GameTime,
            @Leagues,
            @HomeTeam,
			@AwayTeam,
			@HomeScore,
			@AwayScore,
			@Condition)
GO


-- AddDetail
IF OBJECT_ID('dbo.spAddDetail', 'P') IS NOT NULL  
   DROP PROCEDURE dbo.spAddDetail;  
GO  

CREATE PROCEDURE dbo.spAddDetail
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

   INSERT INTO dbo.Detail
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


-- AddHistory
IF OBJECT_ID('dbo.spAddHistory', 'P') IS NOT NULL  
   DROP PROCEDURE dbo.spAddHistory;  
GO  

CREATE PROCEDURE dbo.spAddHistory
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

   INSERT INTO dbo.History(
			ResultId,
			FirstHalf_H,
			FirstHalf_A,
			SecondHalf_H,
			SecondHalf_A,
			RegularTime_H,
			RegularTime_A,
			Corners_H,
			Corners_A,
			Penalties_H,
			Penalties_A,
			YellowCards_H,
			YellowCards_A,
			RedCards_H,
			RedCards_A,
			FirstET_H,
			FirstET_A,
			SecondET_H,
			SecondET_A,
			PenaltiesShootout_H,
			PenaltiesShootout_A)
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


-- UpdateResultById
IF OBJECT_ID('dbo.spUpdateResultById', 'P') IS NOT NULL  
   DROP PROCEDURE dbo.spUpdateResultById;  
GO  

CREATE PROCEDURE dbo.spUpdateResultById
	@Id varchar(80),
	@HomeScore int,
	@AwayScore int,
	@Condition int
AS
   SET NOCOUNT ON;

   UPDATE dbo.Result
   SET HomeScore = @HomeScore,
		AwayScore = @AwayScore,
		Condition = @Condition
	WHERE Id = @Id
GO


-- UpdateDetialById
IF OBJECT_ID('dbo.spUpdateDetialById', 'P') IS NOT NULL  
   DROP PROCEDURE dbo.spUpdateDetialById;  
GO  

CREATE PROCEDURE dbo.spUpdateDetialById
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

   UPDATE dbo.Detail
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
