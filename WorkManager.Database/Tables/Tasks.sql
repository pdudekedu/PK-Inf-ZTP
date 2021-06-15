﻿CREATE TABLE [dbo].[Tasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ProjectId] INT NOT NULL, 
	[UserId] INT NULL,
	[Name] NVARCHAR(200) NOT NULL,
	[State] INT NOT NULL DEFAULT((0)),
	[EstimateStart] DATETIME NULL,
	[EstimateEnd] DATETIME NULL,
	[Description] NVARCHAR(MAX) NULL,
	[InUse] BIT NOT NULL DEFAULT(1)
)