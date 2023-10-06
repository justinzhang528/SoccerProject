CREATE PROCEDURE [dbo].[Soccer_MatchResult_UpdateMatchResultDetailHistory_v1]
	@Results dbo.MatchResultType READONLY,
	@Details dbo.MatchDetailType READONLY
AS
BEGIN TRANSACTION;  

	Declare @tempHistory Table (ResultId varchar(80),FirstHalf_H int,
			FirstHalf_A int,SecondHalf_H int,SecondHalf_A int,RegularTime_H int,
			RegularTime_A int,Corners_H int,Corners_A int,Penalties_H int,Penalties_A int,
			YellowCards_H int,YellowCards_A int,RedCards_H int,RedCards_A int,FirstET_H int,
			FirstET_A int,SecondET_H int,SecondET_A int,PenaltiesShootout_H int,
			PenaltiesShootout_A int,UpdateTime datetime);		
	
	-- update MatchResult Table:
	SELECT * INTO #TempResult
	FROM @Results;

	MERGE dbo.MatchResult AS tar
	USING #TempResult AS src
	ON tar.Id = src.Id

	-- For Insert
	WHEN NOT MATCHED BY TARGET THEN
		INSERT VALUES (src.ID, src.GameTime, src.Leagues, src.HomeTeam, 
						src.AwayTeam, src.HomeScore, src.AwayScore, src.Condition, GETDATE())
	-- For Update
	WHEN MATCHED AND (tar.HomeScore <> src.HomeScore or tar.AwayScore <> src.AwayScore) THEN UPDATE SET
		tar.HomeScore = src.HomeScore,
		tar.AwayScore = src.AwayScore,
		tar.Condition = src.Condition,
		tar.UpdateTime = GETDATE();


	-- update MatchDetail Table:
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
						src.SecondET_A, src.PenaltiesShootout_H, src.PenaltiesShootout_A, GETDATE())
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
		tar.PenaltiesShootout_A = src.PenaltiesShootout_A,
		tar.UpdateTime = GETDATE()
	OUTPUT
		DELETED.*
	INTO @tempHistory;
	
	-- update History Table:
	INSERT INTO dbo.History
	SELECT * FROM @tempHistory
	WHERE ResultId IS NOT NULL;
COMMIT;
GO