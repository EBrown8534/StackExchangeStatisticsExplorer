/****** Object:  Table [SE].[SiteStyling]    Script Date: 19-Aug-16 03:03:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [SE].[SiteStyling](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_SiteStyling_Id]  DEFAULT (newsequentialid()),
	[TagBackgroundColor] [char](7) NOT NULL,
	[TagForegroundColor] [char](7) NOT NULL,
	[LinkColor] [char](7) NOT NULL,
 CONSTRAINT [PK_SiteStyling] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


