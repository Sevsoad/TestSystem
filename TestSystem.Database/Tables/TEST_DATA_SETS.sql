CREATE TABLE [dbo].[TEST_DATA_SETS]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [TestCreatorId] INT NOT NULL, 
    [DateOfCreation] DATETIME NOT NULL, 
    [NumberOfRuns] INT NOT NULL, 
    [TestDescription] NVARCHAR(MAX) NULL, 
    [TestFieldOfUse] NVARCHAR(MAX) NULL,
	CONSTRAINT [FK_TestSets_ToUsers] FOREIGN KEY ([TestCreatorId]) REFERENCES Users(Id)
)
