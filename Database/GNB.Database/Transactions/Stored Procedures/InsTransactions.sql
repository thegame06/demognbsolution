CREATE PROCEDURE	[dbo].[InsTransactions]
	@Transactions NVARCHAR(MAX)
AS
BEGIN
	DELETE dbo.[Transaction]
	INSERT INTO  dbo.[Transaction] VALUES (@Transactions)
END