﻿CREATE TABLE [dbo].[QTable]
(
	[Qid] INT NOT NULL PRIMARY KEY,
	[Math] FLOAT NOT NULL,
	[Science] FLOAT NOT NULL,
	[English] FLOAT NOT NULL,
	[Social] FLOAT NOT NULL,
	[Id] INT NOT NULL FOREIGN KEY REFERENCES STable(Id)
)