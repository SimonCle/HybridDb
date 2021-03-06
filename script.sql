USE [DV-dev-card5116]
GO
/****** Object:  Table [dbo].[commands]    Script Date: 05-08-2019 12:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[commands](
	[id] [uniqueidentifier] NOT NULL,
	[status] [int] NOT NULL,
	[data] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_commands] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Companies]    Script Date: 05-08-2019 12:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
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
	[IsEnabled] [bit] NOT NULL,
	[Name] [nvarchar](1024) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Documents]    Script Date: 05-08-2019 12:03:18 ******/
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
/****** Object:  Table [dbo].[events]    Script Date: 05-08-2019 12:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[events](
	[Position] [bigint] IDENTITY(0,1) NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[EventId] [uniqueidentifier] NOT NULL,
	[CommitId] [uniqueidentifier] NOT NULL,
	[StreamId] [nvarchar](850) NOT NULL,
	[SequenceNumber] [bigint] NOT NULL,
	[Name] [nvarchar](850) NOT NULL,
	[Generation] [nvarchar](10) NOT NULL,
	[Metadata] [nvarchar](max) NULL,
	[Data] [varbinary](max) NULL,
 CONSTRAINT [PK_events] PRIMARY KEY CLUSTERED 
(
	[Position] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HybridDb]    Script Date: 05-08-2019 12:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HybridDb](
	[SchemaVersion] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profiles]    Script Date: 05-08-2019 12:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profiles](
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
	[Username] [nvarchar](1024) NULL,
	[IsDisabled] [bit] NOT NULL,
	[CompanyId] [nvarchar](1024) NULL,
	[Fullname] [nvarchar](1024) NULL,
	[Initials] [nvarchar](1024) NULL,
	[AlternativeEmail] [nvarchar](1024) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeMappings]    Script Date: 05-08-2019 12:03:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeMappings](
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
	[Name] [nvarchar](1024) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Companies] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[Companies] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [Etag]
GO
ALTER TABLE [dbo].[Companies] ADD  DEFAULT ('01-01-0001 00:00:00 +00:00') FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Companies] ADD  DEFAULT ('01-01-0001 00:00:00 +00:00') FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Companies] ADD  DEFAULT ('False') FOR [AwaitsReprojection]
GO
ALTER TABLE [dbo].[Companies] ADD  DEFAULT ('0') FOR [Version]
GO
ALTER TABLE [dbo].[Companies] ADD  DEFAULT ('0') FOR [LastOperation]
GO
ALTER TABLE [dbo].[Companies] ADD  DEFAULT ('False') FOR [IsEnabled]
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
ALTER TABLE [dbo].[Profiles] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[Profiles] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [Etag]
GO
ALTER TABLE [dbo].[Profiles] ADD  DEFAULT ('01-01-0001 00:00:00 +00:00') FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Profiles] ADD  DEFAULT ('01-01-0001 00:00:00 +00:00') FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[Profiles] ADD  DEFAULT ('False') FOR [AwaitsReprojection]
GO
ALTER TABLE [dbo].[Profiles] ADD  DEFAULT ('0') FOR [Version]
GO
ALTER TABLE [dbo].[Profiles] ADD  DEFAULT ('0') FOR [LastOperation]
GO
ALTER TABLE [dbo].[Profiles] ADD  DEFAULT ('False') FOR [IsDisabled]
GO
ALTER TABLE [dbo].[TypeMappings] ADD  DEFAULT ('') FOR [Id]
GO
ALTER TABLE [dbo].[TypeMappings] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [Etag]
GO
ALTER TABLE [dbo].[TypeMappings] ADD  DEFAULT ('01-01-0001 00:00:00 +00:00') FOR [CreatedAt]
GO
ALTER TABLE [dbo].[TypeMappings] ADD  DEFAULT ('01-01-0001 00:00:00 +00:00') FOR [ModifiedAt]
GO
ALTER TABLE [dbo].[TypeMappings] ADD  DEFAULT ('False') FOR [AwaitsReprojection]
GO
ALTER TABLE [dbo].[TypeMappings] ADD  DEFAULT ('0') FOR [Version]
GO
ALTER TABLE [dbo].[TypeMappings] ADD  DEFAULT ('0') FOR [LastOperation]
GO
