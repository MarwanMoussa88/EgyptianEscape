/****** Object:  Table [dbo].[VillaNumber]    Committed by VersionSQL https://www.versionsql.com ******/

CREATE TABLE [dbo].[VillaNumber](
	[Villa_Number] [int] NOT NULL,
	[VillaId] [int] NOT NULL,
	[SpecialDetails] [nvarchar](max) NULL,
 CONSTRAINT [PK_VillaNumber] PRIMARY KEY CLUSTERED 
(
	[Villa_Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

CREATE NONCLUSTERED INDEX [IX_VillaNumber_VillaId] ON [dbo].[VillaNumber]
(
	[VillaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
ALTER TABLE [dbo].[VillaNumber]  WITH CHECK ADD  CONSTRAINT [FK_VillaNumber_Villa_VillaId] FOREIGN KEY([VillaId])
REFERENCES [dbo].[Villa] ([Id])
ON DELETE CASCADE
ALTER TABLE [dbo].[VillaNumber] CHECK CONSTRAINT [FK_VillaNumber_Villa_VillaId]
