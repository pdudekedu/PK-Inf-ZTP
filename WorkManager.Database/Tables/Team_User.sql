CREATE TABLE [dbo].[Team_User] (
    [TeamId] INT NOT NULL,
    [UserId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([TeamId] ASC, [UserId] ASC),
    CONSTRAINT [FK_Team_User_Teams] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Teams] ([Id]),
    CONSTRAINT [FK_Team_User_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);


