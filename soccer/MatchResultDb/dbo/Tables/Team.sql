﻿CREATE TABLE [dbo].[Team]
(
	Id int NOT NULL PRIMARY KEY IDENTITY(1,1),
    BTITeamName VARCHAR(255) NOT NULL UNIQUE,
    SBOTeamName VARCHAR(255) NOT NULL UNIQUE
)
