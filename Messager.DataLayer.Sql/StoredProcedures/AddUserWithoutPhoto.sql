USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[AddUserWithoutPhoto]    Script Date: 13.10.2017 18:24:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AddUserWithoutPhoto] 
	-- Add the parameters for the stored procedure here
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Login varchar(50),
	@PasswordHash nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Users(Id, FirstName, LastName, Login, PasswordHash)
		OUTPUT inserted.Id AS Id 
		VALUES (NEWID(), @FirstName, @LastName, @Login, @PasswordHash)
	RETURN
END
