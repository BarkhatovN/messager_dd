USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[DeleteMember]    Script Date: 17.10.2017 14:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteMember]
	-- Add the parameters for the stored procedure here
	@ChatId uniqueidentifier,
	@UserId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM ChatParticipants WHERE ChatId = @ChatId AND UserId = @UserId
END

GO
