CREATE TABLE [dbo].[TEST_RUN_RESULTS]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [AlgorithmId] INT NOT NULL,
	[TruePositiveNumber] INT NULL, 
    [FalseNegativeNumber] INT NULL, 
    [TrueNegativeNumber] INT NULL, 
    [FalsePositiveNumber] INT NULL, 
    [NumberOfRuns] INT NOT NULL, 
    [OtherResults] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_TestResults_ToTestRuns] FOREIGN KEY (Id) REFERENCES Test_Runs(Id),
	CONSTRAINT [FK_TestResults_ToAlgorithms] FOREIGN KEY (AlgorithmId) REFERENCES Algorithms(Id)
)
