CREATE TABLE [dbo].[Task_Resource] (
    [TaskId]     INT NOT NULL,
    [ResourceId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([TaskId] ASC, [ResourceId] ASC),
    CONSTRAINT [FK_Task_Resource_Resources] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resources] ([Id]),
    CONSTRAINT [FK_Task_Resource_Tasks] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Tasks] ([Id])
);


