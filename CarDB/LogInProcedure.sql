CREATE PROCEDURE [dbo].[LogInProcedure2]
	@Email nvarchar(50),
	@Password nvarchar(50)
	
AS
BEGIN
	DECLARE @UserID uniqueidentifier

	SELECT *
	From SignUpTable
	Where Email = @Email AND Password = @Password

	END
Go
