USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[AddChat]    Script Date: 17.10.2017 14:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddChat]
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier,
	@Name nvarchar(50),
	@CreaterId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Chats(Id, Name, CreaterId)
                            VALUES(@Id, @Name, @CreaterId)
END

GO
