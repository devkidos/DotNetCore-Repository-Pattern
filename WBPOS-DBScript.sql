 
/****** Object:  Table [dbo].[Country]    Script Date: 19-01-2023 00:00:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountryId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
	[CountryCode] [nvarchar](5) NULL,
	[CountryPhoneCode] [nvarchar](7) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 19-01-2023 00:00:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
	[CountryId] [numeric](18, 0) NOT NULL,
	[StateName] [nvarchar](50) NOT NULL,
	[StateCode] [nvarchar](5) NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_QState] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 19-01-2023 00:00:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[OrganizationId] [uniqueidentifier] NULL,
	[usertype] [nvarchar](20) NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[Salutation] [nvarchar](10) NULL,
	[FirstName] [nvarchar](40) NOT NULL,
	[LastName] [nvarchar](30) NULL,
	[BirthDate] [date] NULL,
	[Sex] [nvarchar](6) NULL,
	[SocialSecurityNnumber] [nvarchar](100) NULL,
	[AddressLine1] [nvarchar](150) NULL,
	[AddressLine2] [nvarchar](150) NULL,
	[CityId] [uniqueidentifier] NULL,
	[EmailId] [nvarchar](50) NULL,
	[MobileNumber] [nvarchar](15) NULL,
	[myRefferalCode] [nvarchar](15) NULL,
	[RefferalCode] [nvarchar](15) NULL,
	[profilePicture] [nvarchar](50) NULL,
	[Status] [nvarchar](15) NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[UpdatedDate] [datetime] NULL,
	[UsrID] [numeric](18, 0) IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO 
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [OrganizationId], [usertype], [Username], [Password], [Salutation], [FirstName], [LastName], [BirthDate], [Sex], [SocialSecurityNnumber], [AddressLine1], [AddressLine2], [CityId], [EmailId], [MobileNumber], [myRefferalCode], [RefferalCode], [profilePicture], [Status], [CreatedBy], [CreatedDate], [UpdatedBy], [UpdatedDate], [UsrID]) VALUES (N'f9badfe6-096b-4dc1-baa3-4fdaffecda05', NULL, N'admin', N'admin', N'fgvaGOqJdGiwphgZj3SOKg==', NULL, N'Admin', NULL, CAST(N'2012-04-23' AS Date), N'M', NULL, NULL, NULL, NULL, N'anrorathod@gmail.com', N'1111111111', N'xsPdIu', N'vqraQ71', NULL, N'Active', N'f9badfe6-096b-4dc1-baa3-4fdaffecda05', CAST(N'2022-06-28T14:53:48.250' AS DateTime), NULL, NULL, CAST(2 AS Numeric(18, 0)))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Country] ADD  CONSTRAINT [DF_Country_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[State] ADD  CONSTRAINT [DF_State_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[State] ADD  CONSTRAINT [DF_State_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[State]  WITH NOCHECK ADD  CONSTRAINT [FK_State_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([CountryId])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_State_Country]
GO
