CREATE TABLE [dbo].[Joke] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [JokeQuestion] NVARCHAR (MAX) NULL,
    [JokeAnswer]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

