CREATE PROCEDURE [dbo].[GetTransactions]
AS
BEGIN
	SELECT [Transactions] FROM dbo.[Transaction]
END