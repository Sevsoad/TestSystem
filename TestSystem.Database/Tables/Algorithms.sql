CREATE TABLE [dbo].[Algorithms]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CreatorId] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [SourceCode] NVARCHAR(MAX) NULL,
	CONSTRAINT [FK_Algorithms_ToUsers] FOREIGN KEY (CreatorId) REFERENCES Users(Id)
)
