CREATE TABLE [dbo].[ProductTable]
(
	[ProductId]     UNIQUEIDENTIFIER NOT NULL,
    [ProductName]   NVARCHAR (100)   NOT NULL,
    [ProductPrice]  NVARCHAR (50)    NOT NULL,
    [ProductDetail] NVARCHAR (MAX)   NOT NULL,
    [ProductImage]  NVARCHAR (MAX)   NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductId] ASC)
)
