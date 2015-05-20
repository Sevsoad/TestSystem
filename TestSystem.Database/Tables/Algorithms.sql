CREATE TABLE [dbo].[Algorithms]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CreatorId] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
	[Name] NVARCHAR(20) NOT NULL, 
    [DateOfCreation] DATETIME NOT NULL, 
    CONSTRAINT [FK_Algorithms_ToUsers] FOREIGN KEY (CreatorId) REFERENCES Users(Id)
)
