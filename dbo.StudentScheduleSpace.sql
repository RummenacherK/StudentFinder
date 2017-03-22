CREATE TABLE [dbo].[StudentScheduleSpace] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [StudentId]  INT NOT NULL,
    [ScheduleId] INT NOT NULL,
    [SpaceId]    INT NOT NULL, 
    CONSTRAINT [PK_StudentScheduleSpace] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_StudentScheduleSpace_Student] FOREIGN KEY ([Id]) REFERENCES [dbo].[Student]([Id]), 
	CONSTRAINT [FK_StudentScheduleSpace_Schedule] FOREIGN KEY ([Id]) REFERENCES [dbo].[Schedule]([Id]), 
	CONSTRAINT [FK_StudentScheduleSpace_Space] FOREIGN KEY ([Id]) REFERENCES [dbo].[Space]([Id])    
);

