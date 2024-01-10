CREATE TABLE [dbo].[UserTable] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [FirstName] NVARCHAR (50)    NOT NULL,
    [LastName]  NVARCHAR (50)    NOT NULL,
    [Email]     NVARCHAR (50)    NOT NULL,
    [Password]  NVARCHAR (MAX)   NOT NULL,
    [Role]      INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

