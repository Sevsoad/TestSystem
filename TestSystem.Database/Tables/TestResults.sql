CREATE TABLE [dbo].[TestResults]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TestRun] INT NOT NULL, 
    [AlgorithmId] INT NOT NULL,
	[TP] INT NULL, 
    [FN] INT NULL, 
    [TN] INT NULL, 
    [FP] INT NULL, 
    [Others] NVARCHAR(MAX) NULL, 
    [TestRuns] INT NULL, 
    CONSTRAINT [FK_TestResults_ToTestRuns] FOREIGN KEY (TestRun) REFERENCES TestRuns(Id),
	CONSTRAINT [FK_TestResults_ToAlgorithms] FOREIGN KEY (AlgorithmId) REFERENCES Algorithms(Id)
)
