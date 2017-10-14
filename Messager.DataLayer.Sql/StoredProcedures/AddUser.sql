USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 14.10.2017 1:29:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AddUser] 
	-- Add the parameters for the stored procedure here
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Login nvarchar(50),
	@PasswordHash nvarchar(50),
	@Photo nvarchar(max) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	-- Insert statements for procedure here
	INSERT INTO Users(Id, FirstName, LastName, Photo, Login, PasswordHash)
		OUTPUT inserted.Id 
	VALUES(NEWID(), @FirstName, @LastName, CONVERT(varbinary, @Photo), @Login, @PasswordHash);
	RETURN
END
