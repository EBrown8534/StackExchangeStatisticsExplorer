/****** Object:  Table [SE].[BadgeStats]    Script Date: 19-Aug-16 03:06:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [SE].[BadgeStats](
	[SiteId] [uniqueidentifier] NOT NULL,
	[BadgeId] [int] NOT NULL,
	[Gathered] [datetime2](7) NOT NULL,
	[BadgeType] [varchar](16) NOT NULL,
	[AwardCount] [int] NOT NULL,
	[Rank] [varchar](16) NOT NULL,
	[Link] [varchar](512) NOT NULL,
	[Name] [varchar](64) NOT NULL,
	[Manual] [bit] NOT NULL CONSTRAINT [DF_BadgeStats_Manual]  DEFAULT ((0)),
 CONSTRAINT [PK_BadgeStats] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC,
	[BadgeId] ASC,
	[Gathered] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [SE].[BadgeStats]  WITH CHECK ADD  CONSTRAINT [FK_BadgeStats_Sites] FOREIGN KEY([SiteId])
REFERENCES [SE].[Sites] ([Id])
GO

ALTER TABLE [SE].[BadgeStats] CHECK CONSTRAINT [FK_BadgeStats_Sites]
GO


