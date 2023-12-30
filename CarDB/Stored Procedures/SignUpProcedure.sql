CREATE PROCEDURE [dbo].[SignUpProcedure]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Email nvarchar(50),
	@Password nvarchar(Max)
AS
	Insert into UserTable Values (NEWID(), @FirstName, @LastName,@Email,@Password)
RETURN 0
