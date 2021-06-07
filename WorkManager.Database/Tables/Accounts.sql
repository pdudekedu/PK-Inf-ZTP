﻿CREATE TABLE [dbo].[Accounts]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	UserName NVARCHAR(200) NOT NULL,
	[Password] NVARCHAR(MAX) NOT NULL,
	[FirstName] NVARCHAR(100) NOT NULL,
	[LastName] NVARCHAR(100) NOT NULL, 
    [Role] INT NOT NULL DEFAULT 0
)
