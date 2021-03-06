/****** Object:  Table [dbo].[Cases]    Script Date: 05-08-2019 14:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cases](
	[Id] [nvarchar](850) NOT NULL,
	[Etag] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[ModifiedAt] [datetimeoffset](7) NOT NULL,
	[Document] [varbinary](max) NULL,
	[Metadata] [varbinary](max) NULL,
	[Discriminator] [nvarchar](850) NULL,
	[AwaitsReprojection] [bit] NOT NULL,
	[Version] [int] NOT NULL,
	[Timestamp] [timestamp] NOT NULL,
	[LastOperation] [tinyint] NOT NULL,
	[CompanyId] [nvarchar](1024) NULL,
	[ConsultantId] [nvarchar](1024) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](1024) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Documents]    Script Date: 05-08-2019 14:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[Id] [nvarchar](850) NOT NULL,
	[Etag] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetimeoffset](7) NOT NULL,
	[ModifiedAt] [datetimeoffset](7) NOT NULL,
	[Document] [varbinary](max) NULL,
	[Metadata] [varbinary](max) NULL,
	[Discriminator] [nvarchar](850) NULL,
	[AwaitsReprojection] [bit] NOT NULL,
	[Version] [int] NOT NULL,
	[Timestamp] [timestamp] NOT NULL,
	[LastOperation] [tinyint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HybridDb]    Script Date: 05-08-2019 14:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HybridDb](
	[SchemaVersion] [int] NOT NULL
) ON [PRIMARY]

GO
INSERT [dbo].[Cases] ([Id], [Etag], [CreatedAt], [ModifiedAt], [Document], [Metadata], [Discriminator], [AwaitsReprojection], [Version], [LastOperation], [CompanyId], [ConsultantId], [IsDeleted], [Name]) VALUES (N'case/T636cQWAQO-kAicFUSyd_w', N'615eb875-81fc-59ea-b873-919bf76f5b6d', CAST(N'2019-08-05T14:03:08.2096332+02:00' AS DateTimeOffset), CAST(N'2019-08-05T14:03:38.8929179+02:00' AS DateTimeOffset), 0x7B7D, NULL, N'DV.Application.Cases.Case, DV.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 0, 0, 2, N'company/EO9G-ayHRjWwrPN-GGbb8g', N'profile/uUjWLClhok-5UIji-BevyA', 0, N'min æåøsag')
INSERT [dbo].[HybridDb] ([SchemaVersion]) VALUES (0)
ALTER TABLE [dbo].[Cases] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[Cases] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [Etag]
GO
ALTER TABLE [dbo].[Cases] ADD  DEFAULT ('01-01-0001 00:00:00 +00:00') FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Cases] ADD  DEFAULT ('01-01-0001 00:00:00 +00:00') FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Cases] ADD  DEFAULT ('False') FOR [AwaitsReprojection]
GO
ALTER TABLE [dbo].[Cases] ADD  DEFAULT ('0') FOR [Version]
GO
ALTER TABLE [dbo].[Cases] ADD  DEFAULT ('0') FOR [LastOperation]
GO
ALTER TABLE [dbo].[Cases] ADD  DEFAULT ('False') FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [Etag]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('01-01-0001 00:00:00 +00:00') FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('01-01-0001 00:00:00 +00:00') FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('False') FOR [AwaitsReprojection]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('0') FOR [Version]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT ('0') FOR [LastOperation]
GO
ALTER TABLE [dbo].[HybridDb] ADD  DEFAULT ('0') FOR [SchemaVersion]
GO
