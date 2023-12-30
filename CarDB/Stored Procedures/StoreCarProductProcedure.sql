CREATE PROCEDURE [dbo].[StoreCarProductProcedure]
	@CarName nvarchar(50),
	@CarDetails nvarchar(MAX),
	@CarPrice nvarchar(50),
	@CarImage nvarchar(MAX)
AS
	INSERT into ProductTable values (NEWID(),@CarName,@CarPrice,@CarDetails,@CarImage)
RETURN 0
