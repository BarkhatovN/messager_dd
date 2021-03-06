USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[GetMessagesForUser]    Script Date: 17.10.2017 14:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMessagesForUser]
	-- Add the parameters for the stored procedure here
	@ChatId uniqueidentifier,
	@UserId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Messages.Id AS Id, ChatId, Text, Date, IsSelfDestructing, UserId, [File]
                    FROM Messages LEFT OUTER JOIN Attachments ON Messages.Id = Attachments.MessageId
                    LEFT OUTER JOIN Files ON Attachments.FileId = Files.Id WHERE Messages.Date >= (
                    SELECT ChatParticipants.Date FROM ChatParticipants
                    WHERE ChatParticipants.ChatId = @ChatId AND ChatParticipants.UserId = @UserId) AND ChatId = @ChatId
END

GO
