/****** Object:  Table [SE].[FeatureRequests]    Script Date: 19-Aug-16 03:06:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [SE].[FeatureRequests](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_FeatureRequests_Id]  DEFAULT (newsequentialid()),
	[ProposedOn] [datetime2](7) NOT NULL CONSTRAINT [DF_FeatureRequests_ProposedOn]  DEFAULT (sysutcdatetime()),
	[Display] [bit] NOT NULL CONSTRAINT [DF_FeatureRequests_Display]  DEFAULT ((0)),
	[Priority] [int] NOT NULL CONSTRAINT [DF_FeatureRequests_Priority]  DEFAULT ((0)),
	[Status] [varchar](32) NULL,
	[ProposedBy] [varchar](64) NULL,
	[Area] [varchar](64) NULL,
	[Description] [varchar](max) NOT NULL,
 CONSTRAINT [PK_FeatureRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


