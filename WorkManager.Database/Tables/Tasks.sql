CREATE TABLE [dbo].[Tasks] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [ProjectId]     INT            NOT NULL,
    [UserId]        INT            NULL,
    [Name]          NVARCHAR (200) NOT NULL,
    [State]         INT            DEFAULT ((0)) NOT NULL,
    [EstimateStart] DATETIME       NULL,
    [EstimateEnd]   DATETIME       NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [InUse]         BIT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tasks_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
);


