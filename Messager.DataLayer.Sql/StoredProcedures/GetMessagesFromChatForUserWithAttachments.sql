USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[GetMessagesFromChatForUser]    Script Date: 12.10.2017 2:28:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetMessagesFromChatForUserWithAttachment
	-- Add the parameters for the stored procedure here
	@UserId bigint,
	@ChatId bigint
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Messages.Id AS Id, ChatId, Text, Date, IsSelfDestructing, UserId, 'File'
	FROM Messages JOIN Attachments ON Messages.Id = Attachments.MessageId
	JOIN Files ON Attachments.FileId = Files.Id
    WHERE Messages.Date > (
		SELECT ChatParticipants.Date FROM ChatParticipants 
		WHERE @ChatId = ChatParticipants.ChatId AND @UserId = ChatParticipants.UserId
	) AND ChatId = @ChatId
END
