CREATE PROCEDURE [dbo].[Soccer_MatchResult_GetCookiesByCondition_v1]
	@Website varchar(100),
	@Name varchar(100)
AS
	SELECT * FROM dbo.Cookies WHERE Website = @Website AND Name = @Name
RETURN
