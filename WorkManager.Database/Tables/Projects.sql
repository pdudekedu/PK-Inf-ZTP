CREATE TABLE [dbo].[Projects] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [TeamId]      INT            NOT NULL,
    [UserId]      INT            NULL,
    [InUse]       BIT            DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Projects_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id]),
    CONSTRAINT [FK_Projects_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


