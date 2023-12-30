CREATE TABLE [dbo].[AdminUserTable]
(
	[AdminId]       UNIQUEIDENTIFIER NOT NULL,
    [AdminName]     NVARCHAR (50)    NOT NULL,
    [AdminEmail]    NVARCHAR (50)    NOT NULL,
    [AdminPassword] NVARCHAR (MAX)   NOT NULL,
    PRIMARY KEY CLUSTERED ([AdminId] ASC)
)
