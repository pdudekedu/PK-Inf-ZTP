CREATE TABLE [dbo].[Task_Resource]
(
	[TaskId] INT NOT NULL,
	[ResourceId] INT NOT NULL,
	PRIMARY KEY ([TaskId], [ResourceId])
)
