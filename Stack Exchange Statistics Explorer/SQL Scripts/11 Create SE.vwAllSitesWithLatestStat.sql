/****** Object:  View [SE].[vwAllSitesWithLatestStat]    Script Date: 19-Aug-16 03:13:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [SE].[vwAllSitesWithLatestStat]
WITH SCHEMABINDING
AS
WITH MaxGatheredPerSite AS (
	SELECT
		s.Id AS Id
		,MAX(ss.Gathered) AS Gathered
	FROM
		SE.vwSitesWithRelationsShared AS s
	INNER JOIN
		SE.SiteStats AS ss
	ON
		ss.SiteId = s.Id
	GROUP BY
		s.Id
)
SELECT
	  s.[Id]
      ,[Aliases]
      ,[ApiSiteParameter]
      ,[Audience]
      ,[ClosedBetaDate]
      ,[FaviconUrl]
      ,[HighResolutionIconUrl]
      ,[IconUrl]
      ,[LaunchDate]
      ,[LogoUrl]
      ,[MarkdownExtensions]
      ,[Name]
      ,[OpenBetaDate]
      ,[SiteState]
      ,[SiteType]
      ,[SiteUrl]
      ,[StylingId]
      ,[TwitterAccount]
      ,[LastUpdate]
	  ,[FirstUpdate]
	  ,[SiteId]
      ,ss.[Gathered]
      ,[AnswersPerMinute]
      ,[ApiRevision]
      ,[BadgesPerMinute]
      ,[NewActiveUsers]
      ,[QuestionsPerMinute]
      ,[TotalAccepted]
      ,[TotalAnswers]
      ,[TotalBadges]
      ,[TotalComments]
      ,[TotalQuestions]
      ,[TotalUnanswered]
      ,[TotalUsers]
      ,[TotalVotes]
      ,[Manual]
      ,[UsersAbove150Rep]
      ,[UsersAbove200Rep]
FROM
	MaxGatheredPerSite AS mgps
INNER JOIN
	SE.vwSitesWithRelationsShared AS s
ON
	mgps.Id = s.Id
INNER JOIN
	SE.SiteStats AS ss
ON
	mgps.Id = ss.SiteId
	AND
	mgps.Gathered = ss.Gathered
GO
