CREATE TABLE [dbo].[Appointment](
	[AppointmentId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Notes] [varchar](max) NULL,
	[Location] [varchar](max) NULL,
	[StartDate] [datetimeoffset](7) NOT NULL,
	[EndDate] [datetimeoffset](7) NULL,
	[CreatedOn] [datetime] NOT NULL,
	[UpdatedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY])
GO

ALTER TABLE [dbo].[Appointment] ADD  CONSTRAINT [DF_Appointment_AppointmentId]  DEFAULT (newid()) FOR [AppointmentId]
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Created_UserProfile] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Created_UserProfile]
GO

ALTER TABLE [dbo].[Appointment]  WITH CHECK ADD  CONSTRAINT [FK_Appointment_Updated_UserProfile] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Updated_UserProfile]
GO