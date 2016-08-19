/****** Object:  Table [SE].[ApiBatchLog]    Script Date: 19-Aug-16 03:08:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [SE].[ApiBatchLog](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_ApiBatchLog_Id]  DEFAULT (newsequentialid()),
	[StartDateTime] [datetime2](7) NOT NULL,
	[EndDateTime] [datetime2](7) NOT NULL,
	[AddedDateTime] [datetime2](7) NOT NULL CONSTRAINT [DF_ApiBatchLog_AddedDateTime]  DEFAULT (sysutcdatetime()),
	[RequestedBy] [varchar](256) NOT NULL,
	[RequestCount] [int] NOT NULL,
	[BackoffCount] [int] NOT NULL,
	[TotalBackoff] [int] NOT NULL,
	[HasMoreCount] [int] NOT NULL,
	[QuotaMax] [int] NOT NULL,
	[StartQuotaRemaining] [int] NOT NULL,
	[EndQuotaRemaining] [int] NOT NULL,
 CONSTRAINT [PK_ApiBatchLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


