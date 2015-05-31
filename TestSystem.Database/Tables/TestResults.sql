CREATE TABLE [dbo].[TestResults]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [AlgorithmId] INT NOT NULL,
	[TP] NVARCHAR(10) NULL, 
    [FN] NVARCHAR(10) NULL, 
    [TN] NVARCHAR(10) NULL, 
    [FP] NVARCHAR(10) NULL, 
    [RocCoordinatesSensiv] NVARCHAR(MAX) NULL, 
    [TestRunId] INT NOT NULL, 
    [CorrectRate] NVARCHAR(10) NULL, 
    [OtherInfo] NVARCHAR(50) NULL, 
    [RocCoordinatesSpecif] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_TestResults_ToTestRuns] FOREIGN KEY (TestRunId) REFERENCES TestRuns(Id),
	CONSTRAINT [FK_TestResults_ToAlgorithms] FOREIGN KEY (AlgorithmId) REFERENCES Algorithms(Id)
)
