/****** Object:  Table [SE].[Sites]    Script Date: 19-Aug-16 03:04:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [SE].[Sites](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Sites_Id]  DEFAULT (newsequentialid()),
	[Aliases] [varchar](1024) NULL,
	[ApiSiteParameter] [varchar](256) NOT NULL,
	[Audience] [varchar](1024) NOT NULL,
	[ClosedBetaDate] [datetime2](7) NULL,
	[FaviconUrl] [varchar](1024) NOT NULL,
	[HighResolutionIconUrl] [varchar](1024) NULL,
	[IconUrl] [varchar](1024) NOT NULL,
	[LaunchDate] [datetime2](7) NULL,
	[LogoUrl] [varchar](1024) NOT NULL,
	[MarkdownExtensions] [varchar](1024) NULL,
	[Name] [nvarchar](256) NOT NULL,
	[OpenBetaDate] [datetime2](7) NULL,
	[SiteState] [varchar](64) NOT NULL,
	[SiteType] [varchar](64) NOT NULL,
	[SiteUrl] [varchar](256) NOT NULL,
	[StylingId] [uniqueidentifier] NOT NULL,
	[TwitterAccount] [varchar](64) NULL,
	[LastUpdate] [datetime2](7) NOT NULL CONSTRAINT [DF_Sites_LastUpdate]  DEFAULT (sysutcdatetime()),
	[FirstUpdate] [datetime2](7) NOT NULL CONSTRAINT [DF_Sites_FirstUpdate]  DEFAULT (sysutcdatetime()),
 CONSTRAINT [PK_Sites] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [SE].[Sites]  WITH CHECK ADD  CONSTRAINT [FK_Sites_SiteStyling] FOREIGN KEY([StylingId])
REFERENCES [SE].[SiteStyling] ([Id])
GO

ALTER TABLE [SE].[Sites] CHECK CONSTRAINT [FK_Sites_SiteStyling]
GO


