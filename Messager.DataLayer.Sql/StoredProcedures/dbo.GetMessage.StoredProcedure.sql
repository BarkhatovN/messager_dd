USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[GetMessage]    Script Date: 17.10.2017 14:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetMessage] 
	-- Add the parameters for the stored procedure here
	@MessageId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Messages LEFT OUTER JOIN Attachments ON Messages.Id = Attachments.MessageId 
		LEFT OUTER JOIN Files ON Attachments.FileId = Files.Id 
	WHERE Messages.Id = @MessageId
END

GO
