CREATE TABLE [dbo].[TEST_RUNS]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AlgorithmId] INT NOT NULL, 
    [TestSetId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [DateOfRun] DATETIME NOT NULL, 
	[RunComments] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_TestRuns_ToUsers] FOREIGN KEY (UserId) REFERENCES Users(Id),
	CONSTRAINT [FK_TestRuns_ToTestSets] FOREIGN KEY (TestSetId) REFERENCES Test_Data_Sets(Id),
	CONSTRAINT [FK_TestRuns_ToAlgorithms] FOREIGN KEY (AlgorithmId) REFERENCES Algorithms(Id)
)
