CREATE PROCEDURE [dbo].[GetHashProcedure]
	@Email nvarchar(50)
AS
	SELECT Password FROM UserTable WHERE Email = @Email
RETURN 0
