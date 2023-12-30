CREATE PROCEDURE [dbo].[GetProductByIdProcedure]
	@ProductId uniqueidentifier
AS
	SELECT ProductId, ProductName, ProductPrice, ProductDetail, ProductImage from ProductTable
	Where @ProductId = ProductId
RETURN 0
