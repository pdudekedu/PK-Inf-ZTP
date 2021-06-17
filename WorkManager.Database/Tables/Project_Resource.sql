CREATE TABLE [dbo].[Project_Resource] (
    [ProjectId]  INT NOT NULL,
    [ResourceId] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ProjectId] ASC, [ResourceId] ASC),
    CONSTRAINT [FK_Project_Resource_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id]),
    CONSTRAINT [FK_Project_Resource_Resources] FOREIGN KEY ([ResourceId]) REFERENCES [dbo].[Resources] ([Id])
);


