CREATE USER demognb WITH PASSWORD = 'd3m0 gnb 2019 @'; 

GO

ALTER ROLE db_owner ADD MEMBER demognb;

GO

Server=bladimirperez06db.database.windows.net;Initial Catalog=DemoDb;Persist Security Info=False;User ID=demognb;Password=d3m0 gnb 2019 @;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;