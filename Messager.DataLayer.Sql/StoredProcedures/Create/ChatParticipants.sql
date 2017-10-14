USE [messager_db]
GO

/****** Object:  Table [dbo].[ChatParticipants]    Script Date: 12.10.2017 14:14:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ChatParticipants](
	[ChatId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_Chats] PRIMARY KEY CLUSTERED 
(
	[ChatId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ChatParticipants]  WITH CHECK ADD  CONSTRAINT [FK_ParticipantsOfChat_Chats] FOREIGN KEY([ChatId])
REFERENCES [dbo].[Chats] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ChatParticipants] CHECK CONSTRAINT [FK_ParticipantsOfChat_Chats]
GO

ALTER TABLE [dbo].[ChatParticipants]  WITH CHECK ADD  CONSTRAINT [FK_ParticipantsOfChat_Persons] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ChatParticipants] CHECK CONSTRAINT [FK_ParticipantsOfChat_Persons]
GO


