/****** Object:  View [SE].[vwSitesWithRelationsShared]    Script Date: 19-Aug-16 03:12:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [SE].[vwSitesWithRelationsShared]
WITH SCHEMABINDING
AS
	SELECT
		[CurrentSite].[Id]
		,[CurrentSite].[Aliases]
		,[CurrentSite].[ApiSiteParameter]
		,[CurrentSite].[Audience]
		,CASE WHEN [CurrentSite].[SiteType] = 'meta_site' THEN (SELECT [MainSite].[ClosedBetaDate] FROM [SE].[Sites] AS [MainSite] WHERE [MainSite].[ApiSiteParameter] = REPLACE([CurrentSite].[ApiSiteParameter], 'meta.', '')) ELSE [CurrentSite].[ClosedBetaDate] END AS [ClosedBetaDate]
		,[CurrentSite].[FaviconUrl]
		,[CurrentSite].[HighResolutionIconUrl]
		,[CurrentSite].[IconUrl]
		,CASE WHEN [CurrentSite].[SiteType] = 'meta_site' THEN (SELECT [MainSite].[LaunchDate] FROM [SE].[Sites] AS [MainSite] WHERE [MainSite].[ApiSiteParameter] = REPLACE([CurrentSite].[ApiSiteParameter], 'meta.', '')) ELSE [CurrentSite].[LaunchDate] END AS [LaunchDate]
		,[CurrentSite].[LogoUrl]
		,[CurrentSite].[MarkdownExtensions]
		,[CurrentSite].[Name]
		,CASE WHEN [CurrentSite].[SiteType] = 'meta_site' THEN (SELECT [MainSite].[OpenBetaDate] FROM [SE].[Sites] AS [MainSite] WHERE [MainSite].[ApiSiteParameter] = REPLACE([CurrentSite].[ApiSiteParameter], 'meta.', '')) ELSE [CurrentSite].[OpenBetaDate] END AS [OpenBetaDate]
		,[CurrentSite].[SiteState]
		,[CurrentSite].[SiteType]
		,[CurrentSite].[SiteUrl]
		,[CurrentSite].[StylingId]
		,[CurrentSite].[TwitterAccount]
		,[CurrentSite].[LastUpdate]
		,[CurrentSite].[FirstUpdate]
	FROM
		[SE].[Sites] AS [CurrentSite]
GO
