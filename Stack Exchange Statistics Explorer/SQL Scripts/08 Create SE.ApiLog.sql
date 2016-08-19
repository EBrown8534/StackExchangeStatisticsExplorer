/****** Object:  Table [SE].[ApiLog]    Script Date: 19-Aug-16 03:08:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [SE].[ApiLog](
	[BatchId] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ApiLog_BatchId]  DEFAULT ('00000000-0000-0000-0000-000000000001'),
	[AddedDateTime] [datetime2](7) NOT NULL CONSTRAINT [DF_ApiLog_AddedDateTime]  DEFAULT (sysutcdatetime()),
	[RequestDateTime] [datetime2](7) NOT NULL,
	[RequestedBy] [varchar](256) NULL,
	[EndpointRequested] [varchar](256) NULL,
	[Items] [varchar](max) NULL,
	[ItemCount] [int] NULL,
	[HasMore] [bit] NULL,
	[QuotaMax] [int] NULL,
	[QuotaRemaining] [int] NULL,
	[Backoff] [int] NULL,
	[Total] [int] NULL,
	[Page] [int] NULL,
	[PageSize] [int] NULL,
	[Type] [varchar](64) NULL,
 CONSTRAINT [PK_ApiLog] PRIMARY KEY CLUSTERED 
(
	[BatchId] ASC,
	[AddedDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [SE].[ApiLog]  WITH CHECK ADD  CONSTRAINT [FK_ApiLog_ApiBatchLog] FOREIGN KEY([BatchId])
REFERENCES [SE].[ApiBatchLog] ([Id])
GO

ALTER TABLE [SE].[ApiLog] CHECK CONSTRAINT [FK_ApiLog_ApiBatchLog]
GO


