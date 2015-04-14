﻿CREATE TABLE [dbo].[TestRuns]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [AlgorithmId] INT NOT NULL, 
    [TestSetId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [DateOfRun] DATETIME NOT NULL, 
	CONSTRAINT [FK_TestRuns_ToUsers] FOREIGN KEY (UserId) REFERENCES Users(Id),
	CONSTRAINT [FK_TestRuns_ToTestSets] FOREIGN KEY (TestSetId) REFERENCES TestSets(Id),
	CONSTRAINT [FK_TestRuns_ToAlgorithms] FOREIGN KEY (AlgorithmId) REFERENCES Algorithms(Id)
)
