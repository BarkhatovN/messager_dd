USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[SearchMessagesByPhrase]    Script Date: 17.10.2017 14:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SearchMessagesByPhrase] 
	-- Add the parameters for the stored procedure here
	@UserId uniqueidentifier,
	@Text nvarchar
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Result.Id AS Id, ChatId, Text, Date, IsSelfDestructing, UserId, [File]
                        FROM (SELECT * FROM Messages WHERE UserId = @UserId AND Text LIKE '%' + @Text + '%') AS Result
                        LEFT OUTER JOIN Attachments ON Result.Id = Attachments.MessageId
                        LEFT OUTER JOIN Files ON Attachments.FileId = Files.Id
                        WHERE Result.Date >= (SELECT ChatParticipants.Date FROM ChatParticipants
                        WHERE Result.UserId = ChatParticipants.UserId AND Result.ChatId = ChatParticipants.ChatId)
END

GO
