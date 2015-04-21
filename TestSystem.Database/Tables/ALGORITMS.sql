CREATE TABLE [dbo].[ALGORITHMS]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CreatorId] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [SourceCode] NVARCHAR(MAX) NULL,
	[RunsCount] NCHAR(10) NOT NULL, 
    [AlgoritmType] INT NOT NULL, 
    CONSTRAINT [FK_Algorithms_ToUsers] FOREIGN KEY (CreatorId) REFERENCES Users(Id),
	CONSTRAINT [FK_Algorithms_ToAlgTypes] FOREIGN KEY (AlgoritmType) REFERENCES ALGORITHM_TYPES(Id)
)
