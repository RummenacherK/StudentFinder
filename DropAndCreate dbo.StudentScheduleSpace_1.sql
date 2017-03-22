USE [studentfinder]
GO

/****** Object: Table [dbo].[StudentScheduleSpace] Script Date: 3/21/2017 6:25:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP TABLE [dbo].[StudentScheduleSpace];


--GO
CREATE TABLE [dbo].[StudentScheduleSpace] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
	[StudentId]  INT NOT NULL,
    [ScheduleId] INT NOT NULL,
    [SpaceId]    INT NOT NULL
    
);
--SET IDENTITY_INSERT [dbo].[StudentScheduleSpace] ON


