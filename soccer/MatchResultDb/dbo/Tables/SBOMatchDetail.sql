﻿CREATE TABLE [dbo].[SBOMatchDetail]
(
	Id varchar(80) NOT NULL PRIMARY KEY,
	[MarketType] varchar(100) NOT NULL,
	FirstHalfScore int,
	FullTimeScore int,
	Code int,
	UpdateTime DATETIME
)
