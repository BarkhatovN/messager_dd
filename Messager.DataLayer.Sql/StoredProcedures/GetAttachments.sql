USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[GetAttachments]    Script Date: 12.10.2017 1:04:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[GetAttachments]
	@MessageId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 'File' FROM Files JOIN Attachments ON Files.Id = Attachments.FileId 
	WHERE Attachments.MessageId = @MessageId 
END
