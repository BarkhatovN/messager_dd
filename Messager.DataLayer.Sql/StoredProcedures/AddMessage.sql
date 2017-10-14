USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[AddMessage]    Script Date: 12.10.2017 15:20:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[AddMessage] 
	-- Add the parameters for the stored procedure here
	@ChatId uniqueidentifier,
	@UserId uniqueidentifier,
	@Date datetime,
	@Text nvarchar(max),
	@IsSelfDestructing bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Messages(Id, ChatId, Text, Date, IsSelfDestructing, UserId)
           VALUES(NEWID(),@ChatId, @Text, @Date, @IsSelfDestructing, @UserId)
END
