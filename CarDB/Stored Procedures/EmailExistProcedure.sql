CREATE PROCEDURE [dbo].[EmailExistProcedure]
	@Email NVARCHAR(100),
    @Exists BIT OUTPUT
AS
	SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM UserTable WHERE Email = @Email)
        SET @Exists = 1; -- Email exists in the Users table
    ELSE
        SET @Exists = 0; -- Email doesn't exist in the Users table
RETURN 0
