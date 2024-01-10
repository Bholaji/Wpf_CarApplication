CREATE PROCEDURE [dbo].[LogInProcedure]
	@Email nvarchar(50),
	@Password nvarchar(MAX),
	@Role int
	
AS
BEGIN
	DECLARE @UserID uniqueidentifier

	SELECT @UserID = Id
	From UserTable
	Where Email = @Email AND Password = @Password AND @Role = 1

	IF @UserID IS NOT NULL AND @Role = 1
    BEGIN
        SELECT 'SUCCESS' AS Result 
    END
	END
RETURN 0