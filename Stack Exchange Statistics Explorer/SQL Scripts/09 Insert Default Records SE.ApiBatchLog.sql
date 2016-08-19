INSERT INTO [SE].[ApiBatchLog]
           ([Id]
           ,[StartDateTime]
           ,[EndDateTime]
           ,[AddedDateTime]
           ,[RequestedBy]
           ,[RequestCount]
           ,[BackoffCount]
           ,[TotalBackoff]
           ,[HasMoreCount]
           ,[QuotaMax]
           ,[StartQuotaRemaining]
           ,[EndQuotaRemaining])
     VALUES
           ('00000000-0000-0000-0000-000000000000'
           ,'0001-01-01 00:00:00.0000000'
           ,'0001-01-01 00:00:00.0000000'
           ,'0001-01-01 00:00:00.0000000'
           ,'BADBATCH'
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0)
		   ,
           ('00000000-0000-0000-0000-000000000001'
           ,'0001-01-01 00:00:00.0000000'
           ,'0001-01-01 00:00:00.0000000'
           ,'0001-01-01 00:00:00.0000000'
           ,'INCOMPLETEBATCH'
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0
           ,0)
GO


