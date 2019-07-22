
CREATE PROCEDURE [dbo].[Logging_AddCategory]
	@CategoryName nvarchar(64),
	@LogID int
AS
BEGIN
	SET NOCOUNT ON;
    DECLARE @CatID INT
	SELECT @CatID = CategoryID FROM Category WHERE CategoryName = @CategoryName
	IF @CatID IS NULL
	BEGIN
		INSERT INTO Category (CategoryName) VALUES(@CategoryName)
		SELECT @CatID = SCOPE_IDENTITY()
	END

	EXEC dbo.Logging_InsertCategoryLog @CatID, @LogID 

	RETURN @CatID
END

