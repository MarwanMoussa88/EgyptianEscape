/****** Object:  Table [dbo].[Booking]    Committed by VersionSQL https://www.versionsql.com ******/

CREATE TABLE [dbo].[Booking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[VillaId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[TotalCost] [float] NOT NULL,
	[NumOfNights] [int] NOT NULL,
	[Status] [nvarchar](max) NULL,
	[BookingDate] [datetime2](7) NOT NULL,
	[CheckInDate] [datetime2](7) NOT NULL,
	[CheckOutDate] [datetime2](7) NOT NULL,
	[IsPaymentSuccessful] [bit] NOT NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
	[StripeSessionId] [nvarchar](max) NULL,
	[StripePaymentId] [nvarchar](max) NULL,
	[ActualCheckIn] [datetime2](7) NOT NULL,
	[ActualCheckOut] [datetime2](7) NOT NULL,
	[VillaNumber] [int] NOT NULL,
 CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

SET ANSI_PADDING ON

CREATE NONCLUSTERED INDEX [IX_Booking_UserId] ON [dbo].[Booking]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
CREATE NONCLUSTERED INDEX [IX_Booking_VillaId] ON [dbo].[Booking]
(
	[VillaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_AspNetUsers_UserId]
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK_Booking_Villa_VillaId] FOREIGN KEY([VillaId])
REFERENCES [dbo].[Villa] ([Id])
ON DELETE CASCADE
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK_Booking_Villa_VillaId]
