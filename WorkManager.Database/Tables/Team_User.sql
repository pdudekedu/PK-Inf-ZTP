CREATE TABLE [dbo].[Team_User]
(
	[TeamId] INT NOT NULL,
	[UserId] INT NOT NULL,
	PRIMARY KEY (TeamId, UserId)
)
