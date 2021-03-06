USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 17.10.2017 14:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUser]
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Photo varbinary(max) = null,
	@Login varchar(50),
	@PasswordHash varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Users SET
			FirstName = @FirstName, LastName = @LastName, Photo = @Photo, Login = @Login, PasswordHash = @PasswordHash
            WHERE Id = @Id
END

GO
