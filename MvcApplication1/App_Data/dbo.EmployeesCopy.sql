CREATE TABLE [dbo].[EmployeesCopy]
(
	[COPYID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ID] INT NOT NULL, 
    [Location] NVARCHAR(3) NOT NULL, 
    [Department] NVARCHAR(MAX) NULL, 
    [Phone] NVARCHAR(MAX) NULL, 
    [Cell] NCHAR(10) NULL, 
    [Fax] NTEXT NULL, 
    [Room] NCHAR(10) NULL, 
    [DateAdded] DATETIME NOT NULL, 
    [FirstName] NVARCHAR(MAX) NOT NULL, 
    [LastName] NVARCHAR(MAX) NOT NULL, 
    [Extension] NVARCHAR(MAX) NULL
)
