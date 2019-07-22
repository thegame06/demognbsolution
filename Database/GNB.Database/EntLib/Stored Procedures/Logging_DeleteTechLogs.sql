
CREATE PROCEDURE [dbo].[Logging_DeleteTechLogs]		
	@LogsLifeInDays INT,
	@BatchPageSize INT = 500,
	@MarginRows INT = 0														
AS

BEGIN
	
declare @RowsToDelete integer, @RowCount integer, @RowsToIgnore integer;
declare @Stop bit;
declare @DeletedTable table (LogId int not null primary key);


select @RowsToDelete = @BatchPageSize;

Select @RowsToIgnore = @MarginRows;

select @Stop = 0;

while (@stop = 0)
begin
    begin transaction
    insert into @DeletedTable select top (@RowsToDelete) LogId from dbo.TechLog tl where tl.Timestamp < getdate()  - @LogsLifeInDays;
    select @RowCount=count(1) from @DeletedTable;
    select @stop = case when @Rowcount<=@RowsToIgnore then 1 else 0 end;
    if (@stop = 0)
    begin
		delete dbo.CategoryLog from dbo.CategoryLog T1 inner join @DeletedTable dt on T1.LogID = dt.LogId;
        delete dbo.TechLog from dbo.TechLog T1 inner join @DeletedTable dt on T1.LogID=dt.LogId;
    end
    delete from @DeletedTable;
    commit transaction
end

END