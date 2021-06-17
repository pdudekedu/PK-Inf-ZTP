CREATE TABLE [dbo].[TaskTimes] (
    [Id]       INT      IDENTITY (1, 1) NOT NULL,
    [TaskId]   INT      NOT NULL,
    [DateTime] DATETIME NOT NULL,
    [Type]     INT      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaskTimes_Tasks] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Tasks] ([Id])
);


