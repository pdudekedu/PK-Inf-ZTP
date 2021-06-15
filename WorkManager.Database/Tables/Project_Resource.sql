CREATE TABLE [dbo].[Project_Resource]
(
	[ProjectId] INT NOT NULL,
	[ResourceId] INT NOT NULL,
	PRIMARY KEY ([ProjectId], [ResourceId])
)
