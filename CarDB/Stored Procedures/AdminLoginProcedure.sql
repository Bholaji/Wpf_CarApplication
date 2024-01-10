CREATE PROCEDURE [dbo].[AdminLoginProcedure]
	@Email nvarchar(50),
	@Password nvarchar(50),
	@Role int
	
AS
BEGIN
	DECLARE @UserID uniqueidentifier

	SELECT @UserID = Id
	From UserTable
	Where Email = @Email AND Password = @Password AND @Role = 2

	IF @UserID IS NOT NULL AND @Role = 2
    BEGIN
        SELECT 'SUCCESS' AS Result 
    END
	END
RETURN 0