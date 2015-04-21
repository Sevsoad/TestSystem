CREATE TABLE [dbo].[TestSets]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CreatorId] INT NOT NULL, 
    [DateOfCreation] DATETIME NOT NULL, 
    [TotalRuns] INT NOT NULL, 
    [Type] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(MAX) NULL,
	CONSTRAINT [FK_TestSets_ToUsers] FOREIGN KEY (CreatorId) REFERENCES Users(Id)
)
