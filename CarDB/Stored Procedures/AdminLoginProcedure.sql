CREATE PROCEDURE [dbo].[AdminLoginProcedure]
	@Email nvarchar(50)
AS
	SELECT AdminPassword FROM AdminUserTable WHERE AdminEmail = @Email
RETURN 0
