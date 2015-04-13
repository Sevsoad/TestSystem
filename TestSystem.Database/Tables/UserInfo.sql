CREATE TABLE [dbo].[UserInfo]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [Email] NVARCHAR(50) NULL, 
    [FullName] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(50) NULL, 
    [Other] NVARCHAR(MAX) NULL,
	CONSTRAINT [FK_UserInfo_ToUsers] FOREIGN KEY (UserID) REFERENCES Users(Id)
)
