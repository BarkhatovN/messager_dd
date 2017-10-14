USE [messager_db]
GO

/****** Object:  Table [dbo].[Chats]    Script Date: 12.10.2017 14:13:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Chats](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreaterId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Chats_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Chats]  WITH CHECK ADD  CONSTRAINT [FK_Chats_Persons] FOREIGN KEY([CreaterId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Chats] CHECK CONSTRAINT [FK_Chats_Persons]
GO


