CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Password] NVARCHAR(50) NOT NULL, 
    [Role] INT NOT NULL, 
    [UserName] NVARCHAR(50) NOT NULL, 
	CONSTRAINT [Fk_Users_ToRoles] FOREIGN KEY (Role) references Roles(Id) 
)
