USE [messager_db]
GO
/****** Object:  StoredProcedure [dbo].[AddAttachment]    Script Date: 17.10.2017 14:49:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddAttachment] 
	-- Add the parameters for the stored procedure here
	@MessageId uniqueidentifier,
	@FileId uniqueidentifier = NULL, 
	@File varbinary(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET @FileId = NEWID();
    -- Insert statements for procedure here
	INSERT INTO Files VALUES(@FileId, @File);
	INSERT INTO Attachments VALUES(@MessageId,@FileId);
END

GO
