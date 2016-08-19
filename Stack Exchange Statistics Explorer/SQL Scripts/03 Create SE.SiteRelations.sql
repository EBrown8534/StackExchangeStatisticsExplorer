/****** Object:  Table [SE].[SiteRelations]    Script Date: 19-Aug-16 03:05:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [SE].[SiteRelations](
	[SiteId] [uniqueidentifier] NOT NULL,
	[Relation] [varchar](16) NOT NULL,
	[ApiSiteParameter] [varchar](256) NOT NULL,
	[SiteUrl] [varchar](1024) NOT NULL,
 CONSTRAINT [PK_SiteRelations] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC,
	[Relation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [SE].[SiteRelations]  WITH CHECK ADD  CONSTRAINT [FK_SiteRelations_Sites] FOREIGN KEY([SiteId])
REFERENCES [SE].[Sites] ([Id])
GO

ALTER TABLE [SE].[SiteRelations] CHECK CONSTRAINT [FK_SiteRelations_Sites]
GO


