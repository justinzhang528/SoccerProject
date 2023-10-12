
CREATE PROCEDURE [dbo].[Soccer_SBOMatchResult_UpdateSBOMatchResultDetailHistory_v1]
	@Results dbo.SBOMatchResultType READONLY,
	@Details dbo.SBOMatchDetailType READONLY
AS
BEGIN TRANSACTION;

	Declare @tempHistory Table (Id varchar(80),
								HomeFirstHalfScore varchar(3),
								AwayFirstHalfScore varchar(3),
								HomeFullTimeScore varchar(3),
								AwayFullTimeScore varchar(3),
								UpdateTime DATETIME);

	Declare @tempDetailHistory Table (Id varchar(80),
								MarketType varchar(100),
								HomeFirstHalfScore varchar(3),
								AwayFirstHalfScore varchar(3),
								HomeFullTimeScore varchar(3),
								AwayFullTimeScore varchar(3),
								Code int,
								UpdateTime DATETIME);
	
	-- update SBOMatchResult Table:
	SELECT * INTO #TempResult
	FROM @Results;

	MERGE dbo.SBOMatchResult AS tar
	USING #TempResult AS src
	ON tar.Id = src.Id

	-- For Insert
	WHEN NOT MATCHED BY TARGET THEN
		INSERT VALUES (src.ID, src.League, src.HomeTeam, src.AwayTeam, src.MatchTime, src.HomeFirstHalfScore,
						src.AwayFirstHalfScore, src.HomeFullTimeScore, src.AwayFullTimeScore, src.IsShowMoreData, GETDATE())
	-- For Update
	WHEN MATCHED AND (
		tar.HomeFirstHalfScore <> src.HomeFirstHalfScore or 
		tar.AwayFirstHalfScore <> src.AwayFirstHalfScore or
		tar.HomeFullTimeScore <> src.HomeFullTimeScore or
		tar.AwayFullTimeScore <> src.AwayFullTimeScore
	) THEN UPDATE SET
		tar.HomeFirstHalfScore = src.HomeFirstHalfScore,
		tar.AwayFirstHalfScore = src.AwayFirstHalfScore,
		tar.HomeFullTimeScore = src.HomeFullTimeScore,
		tar.AwayFullTimeScore = src.AwayFullTimeScore,
		tar.IsShowMoreData = src.IsShowMoreData,
		tar.UpdateTime = GETDATE()
	OUTPUT
		DELETED.Id, DELETED.HomeFirstHalfScore, DELETED.AwayFirstHalfScore, 
		DELETED.HomeFullTimeScore, DELETED.AwayFullTimeScore, DELETED.UpdateTime
	INTO @tempHistory;
		
	-- update SBOHistory Table:
	INSERT INTO dbo.SBOHistory
	SELECT * FROM @tempHistory
	WHERE Id IS NOT NULL;


	-- update SBOMatchDetail Table:
    SELECT * INTO #TempDetail
    FROM @Details;

	MERGE dbo.SBOMatchDetail AS tar
	USING #TempDetail AS src
	ON tar.Id = src.Id and tar.Code = src.Code

	-- For Insert
	WHEN NOT MATCHED BY TARGET THEN
		INSERT VALUES (src.ID, src.MarketType, src.HomeFirstHalfScore, src.AwayFirstHalfScore, 
						src.HomeFullTimeScore, src.AwayFullTimeScore, src.Code, GETDATE())

	-- For Update
	WHEN MATCHED AND (
		tar.HomeFirstHalfScore <> src.HomeFirstHalfScore or 
		tar.AwayFirstHalfScore <> src.AwayFirstHalfScore or
		tar.HomeFullTimeScore <> src.HomeFullTimeScore or
		tar.AwayFullTimeScore <> src.AwayFullTimeScore
	) THEN UPDATE SET
		tar.HomeFirstHalfScore = src.HomeFirstHalfScore,
		tar.AwayFirstHalfScore = src.AwayFirstHalfScore,
		tar.HomeFullTimeScore = src.HomeFullTimeScore,
		tar.AwayFullTimeScore = src.AwayFullTimeScore,
		tar.UpdateTime = GETDATE()
	OUTPUT
		DELETED.*
	INTO @tempDetailHistory;
		
	-- update SBOHistory Table:
	INSERT INTO dbo.SBOMatchDetailHistory
	SELECT * FROM @tempDetailHistory
	WHERE Id IS NOT NULL;
COMMIT;
GO