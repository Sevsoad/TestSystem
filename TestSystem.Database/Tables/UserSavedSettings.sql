CREATE TABLE [dbo].[UserSavedSettings]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserID] INT NOT NULL, 
    [ObjectName] NVARCHAR(50) NOT NULL, 
    [ObjectValue] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_UserSavedSettings_ToUsers] FOREIGN KEY (UserId) REFERENCES Users(Id)	
)


