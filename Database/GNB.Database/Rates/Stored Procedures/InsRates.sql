﻿CREATE PROCEDURE [dbo].[InsRates]
	@Rates NVARCHAR(MAX)
AS
BEGIN
	DELETE dbo.[Rate]
	INSERT INTO  dbo.Rate VALUES (@Rates)
END