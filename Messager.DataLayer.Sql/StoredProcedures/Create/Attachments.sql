USE [messager_db]
GO

/****** Object:  Table [dbo].[Attachments]    Script Date: 12.10.2017 14:14:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Attachments](
	[FileId] [uniqueidentifier] NOT NULL,
	[MessageId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC,
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Attachments]  WITH CHECK ADD  CONSTRAINT [FK_Attachments_Files] FOREIGN KEY([FileId])
REFERENCES [dbo].[Files] ([Id])
GO

ALTER TABLE [dbo].[Attachments] CHECK CONSTRAINT [FK_Attachments_Files]
GO

ALTER TABLE [dbo].[Attachments]  WITH CHECK ADD  CONSTRAINT [FK_Attachments_Messages] FOREIGN KEY([MessageId])
REFERENCES [dbo].[Messages] ([Id])
GO

ALTER TABLE [dbo].[Attachments] CHECK CONSTRAINT [FK_Attachments_Messages]
GO


