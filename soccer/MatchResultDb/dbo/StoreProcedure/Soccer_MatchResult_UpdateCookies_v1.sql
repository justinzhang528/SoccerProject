CREATE PROCEDURE [dbo].[Soccer_MatchResult_UpdateCookies_v1]
	@Cookies dbo.CookiesType READONLY

AS
	MERGE dbo.Cookies AS tar
	USING @Cookies AS src
	ON tar.Website = src.Website AND tar.Name = src.Name

	WHEN NOT MATCHED BY TARGET THEN
		INSERT VALUES (src.Website, src.Name, src.Value, src.Path, src.Domain)

	WHEN MATCHED THEN UPDATE SET
		tar.Value = src.Value,
		tar.Path = src.Path,
		tar.Domain = src.Domain;
RETURN
