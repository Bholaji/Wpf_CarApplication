CREATE PROCEDURE [dbo].[GetProductProcedure]
AS
	SELECT ProductId, ProductName, ProductPrice, ProductDetail, ProductImage from ProductTable
RETURN 0
