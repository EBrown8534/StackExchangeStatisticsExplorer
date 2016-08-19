/****** Object:  Table [SE].[SiteStats]    Script Date: 19-Aug-16 03:07:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [SE].[SiteStats](
	[SiteId] [uniqueidentifier] NOT NULL,
	[Gathered] [datetime2](7) NOT NULL,
	[AnswersPerMinute] [decimal](9, 4) NOT NULL,
	[ApiRevision] [varchar](32) NOT NULL,
	[BadgesPerMinute] [decimal](9, 4) NOT NULL,
	[NewActiveUsers] [int] NOT NULL,
	[QuestionsPerMinute] [decimal](9, 4) NOT NULL,
	[TotalAccepted] [int] NOT NULL,
	[TotalAnswers] [int] NOT NULL,
	[TotalBadges] [int] NOT NULL,
	[TotalComments] [int] NOT NULL,
	[TotalQuestions] [int] NOT NULL,
	[TotalUnanswered] [int] NOT NULL,
	[TotalUsers] [int] NOT NULL,
	[TotalVotes] [int] NOT NULL,
	[Manual] [bit] NOT NULL CONSTRAINT [DF_SiteStats_Manual]  DEFAULT ((0)),
	[UsersAbove150Rep] [int] NULL,
	[UsersAbove200Rep] [int] NULL,
 CONSTRAINT [PK_SiteStats] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC,
	[Gathered] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [SE].[SiteStats]  WITH CHECK ADD  CONSTRAINT [FK_SiteStats_Sites] FOREIGN KEY([SiteId])
REFERENCES [SE].[Sites] ([Id])
GO

ALTER TABLE [SE].[SiteStats] CHECK CONSTRAINT [FK_SiteStats_Sites]
GO


