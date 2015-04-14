CREATE TABLE [dbo].[UserInfo]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Email] NVARCHAR(50) NULL, 
    [FullName] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NULL, 
    [Other] NVARCHAR(MAX) NULL,
	CONSTRAINT [FK_UserInfo_ToUsers] FOREIGN KEY (Id) REFERENCES Users(Id)
)
