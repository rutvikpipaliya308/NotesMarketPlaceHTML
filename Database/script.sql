USE [master]
GO
/****** Object:  Database [NotesMarketPlace]    Script Date: 11-04-2021 11:29:49 ******/
CREATE DATABASE [NotesMarketPlace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NotesMarketPlace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\NotesMarketPlace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NotesMarketPlace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\NotesMarketPlace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [NotesMarketPlace] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NotesMarketPlace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ARITHABORT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NotesMarketPlace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NotesMarketPlace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NotesMarketPlace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET RECOVERY FULL 
GO
ALTER DATABASE [NotesMarketPlace] SET  MULTI_USER 
GO
ALTER DATABASE [NotesMarketPlace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NotesMarketPlace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NotesMarketPlace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NotesMarketPlace] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'NotesMarketPlace', N'ON'
GO
ALTER DATABASE [NotesMarketPlace] SET QUERY_STORE = OFF
GO
USE [NotesMarketPlace]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[CountryCode] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Downloads]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Downloads](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[Seller] [int] NOT NULL,
	[Downloader] [int] NOT NULL,
	[IsSellerHasAllowedDownload] [bit] NOT NULL,
	[AttachmentPath] [varchar](max) NULL,
	[IsAttachmentDownloaded] [bit] NOT NULL,
	[AttachmentDownloadedDate] [datetime] NULL,
	[IsPaid] [bit] NOT NULL,
	[PurchasedPrice] [decimal](18, 0) NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[NoteCategory] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Downloads] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteCategories]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteCategories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_NoteCategories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteTypes]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_NoteTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReferenceData]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReferenceData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[Datavalue] [varchar](100) NOT NULL,
	[RefCategory] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ReferenceData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotes]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SellerID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ActionedBy] [int] NULL,
	[AdminRemarks] [varchar](max) NULL,
	[PublishedDate] [datetime] NULL,
	[Title] [varchar](100) NOT NULL,
	[Category] [int] NOT NULL,
	[DisplayPicture] [varchar](500) NULL,
	[NoteType] [int] NULL,
	[NumberofPages] [int] NULL,
	[Description] [varchar](max) NOT NULL,
	[UniversityName] [varchar](200) NULL,
	[Country] [int] NULL,
	[Course] [varchar](100) NULL,
	[CourseCode] [varchar](100) NULL,
	[Professor] [varchar](100) NULL,
	[IsPaid] [bit] NOT NULL,
	[SellingPrice] [decimal](18, 0) NULL,
	[NotesPreview] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SellerNotes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotesAttachements]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotesAttachements](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SellerNotesAttachements] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotesReportedIssues]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotesReportedIssues](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[ReportedByID] [int] NOT NULL,
	[AgainstDownloadID] [int] NOT NULL,
	[Remarks] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SellerNotesReportedIssues] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotesReviews]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotesReviews](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[ReviewedByID] [int] NOT NULL,
	[AgainstDownloadsID] [int] NOT NULL,
	[Ratings] [decimal](18, 0) NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SellerNotesReviews] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemConfigurations]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemConfigurations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Key] [varchar](100) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBY] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SystemConfigurations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[DOB] [datetime] NULL,
	[Gender] [int] NULL,
	[SecondaryEmailAddress] [varchar](100) NULL,
	[PhoneNumber-CountryCode] [varchar](5) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[ProfilePicture] [varchar](500) NULL,
	[AddressLine1] [varchar](100) NOT NULL,
	[AddressLine2] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[ZipCode] [varchar](50) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[University] [varchar](100) NULL,
	[College] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11-04-2021 11:29:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](24) NOT NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([ID], [Name], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'India', N'91', CAST(N'2021-04-05T18:32:53.620' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[Countries] ([ID], [Name], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Australiya', N'23', CAST(N'2021-04-05T18:33:00.240' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[Countries] ([ID], [Name], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'England', N'45', CAST(N'2021-04-05T18:33:03.873' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[Countries] ([ID], [Name], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1002, N'new zelend', N'2356', CAST(N'2021-04-05T18:45:54.537' AS DateTime), 5, CAST(N'2021-04-05T19:01:30.113' AS DateTime), 5, 1)
INSERT [dbo].[Countries] ([ID], [Name], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1003, N'Shri Lanka', N'800', CAST(N'2021-04-05T18:46:13.843' AS DateTime), 5, CAST(N'2021-04-09T00:08:39.860' AS DateTime), 1046, 0)
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Downloads] ON 

INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 11, 5, 5, 1, N'~/Members/5/11/Attachements/rutvik.txt', 1, CAST(N'2021-03-06T21:56:17.853' AS DateTime), 1, CAST(0 AS Decimal(18, 0)), N'cricket', N'History', CAST(N'2021-03-06T21:56:17.960' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 12, 5, 5, 1, N'~/Members/5/12/Attachements/rutvik.txt', 1, CAST(N'2021-03-06T23:13:30.490' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'Money', N'Science', CAST(N'2021-03-06T23:13:30.627' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 13, 5, 5, 1, N'~/Members/5/13/Attachements/rutvik.txt', 1, CAST(N'2021-03-06T23:13:53.277' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'power', N'History', CAST(N'2021-03-06T23:13:53.510' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 14, 5, 5, 1, N'~/Members/5/14/Attachements/rutvik.txt', 1, CAST(N'2021-03-06T23:14:04.617' AS DateTime), 1, CAST(12 AS Decimal(18, 0)), N'c', N'Science', CAST(N'2021-03-06T23:14:04.620' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 15, 5, 5, 1, N'~/Members/5/15/Attachements/rutvik.txt', 1, CAST(N'2021-03-06T23:14:16.763' AS DateTime), 1, CAST(78 AS Decimal(18, 0)), N'java', N'Science', CAST(N'2021-03-06T23:14:16.767' AS DateTime), NULL, CAST(N'2021-04-08T15:22:00.897' AS DateTime), 5, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 16, 5, 5, 0, N'~/Members/5/16/Attachements/rutvik.txt', 1, CAST(N'2021-03-06T23:14:24.763' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'ai', N'History', CAST(N'2021-03-06T23:14:24.763' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1002, 16, 5, 4, 0, N'~/Members/5/16/Attachements/rutvik.txt', 1, CAST(N'2021-03-16T10:40:51.027' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'ai', N'History', CAST(N'2021-03-16T10:40:51.027' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1003, 2025, 5, 5, 1, N'~/Members/5/2025/Attachements/', 1, CAST(N'2021-03-21T17:48:25.080' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'zipnote', N'Science', CAST(N'2021-03-21T17:48:25.080' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1004, 17, 5, 5, 1, N'~/Members/5/17/Attachements/rutvik.txt', 0, CAST(N'2021-03-21T17:51:41.073' AS DateTime), 1, CAST(12 AS Decimal(18, 0)), N'react', N'History', CAST(N'2021-03-21T17:51:41.073' AS DateTime), NULL, CAST(N'2021-03-27T18:44:25.030' AS DateTime), NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1005, 13, 5, 4, 1, N'~/Members/5/13/Attachements/', 1, CAST(N'2021-03-23T09:56:25.700' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'power', N'History', CAST(N'2021-03-23T09:56:25.700' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1006, 15, 5, 5, 0, N'~/Members/5/15/Attachements/', 0, CAST(N'2021-03-23T09:56:42.553' AS DateTime), 1, CAST(78 AS Decimal(18, 0)), N'java', N'Science', CAST(N'2021-03-23T09:56:42.553' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1007, 2025, 5, 4, 1, N'~/Members/5/2025/Attachements/', 1, CAST(N'2021-03-23T09:56:54.287' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'zipnote', N'Science', CAST(N'2021-03-23T09:56:54.287' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1008, 11, 5, 4, 1, N'~/Members/5/11/Attachements/', 1, CAST(N'2021-03-23T09:57:07.583' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'cricket', N'History', CAST(N'2021-03-23T09:57:07.583' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1009, 11, 5, 34, 1, N'~/Members/5/11/Attachements/', 1, CAST(N'2021-03-23T10:03:02.607' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'cricket', N'History', CAST(N'2021-03-23T10:03:02.607' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1010, 2025, 5, 34, 1, N'~/Members/5/2025/Attachements/', 1, CAST(N'2021-03-24T01:06:27.343' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'zipnote', N'Science', CAST(N'2021-03-24T01:06:27.343' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1011, 1020, 5, 5, 1, N'~/Members/5/1020/Attachements/', 1, CAST(N'2021-03-27T00:55:36.320' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'electical', N'Science', CAST(N'2021-03-27T00:55:36.320' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1012, 1021, 5, 5, 1, N'~/Members/5/1021/Attachements/', 1, CAST(N'2021-03-27T01:01:43.713' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'gate', N'History', CAST(N'2021-03-27T01:01:43.713' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1013, 2030, 5, 5, 1, N'~/Members/5/2030/Attachements/', 1, CAST(N'2021-03-27T17:38:18.873' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'note 6', N'Science', CAST(N'2021-03-27T17:38:18.873' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2003, 9, 5, 5, 1, N'~/Members/5/9/Attachements/', 1, CAST(N'2021-04-02T18:11:34.920' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'mechanical', N'History', CAST(N'2021-04-02T18:11:34.920' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2004, 14, 5, 34, 1, N'~/Members/5/14/Attachements/', 0, CAST(N'2021-04-04T01:44:30.890' AS DateTime), 1, CAST(12 AS Decimal(18, 0)), N'c', N'Science', CAST(N'2021-04-04T01:44:30.890' AS DateTime), NULL, CAST(N'2021-04-04T01:45:17.367' AS DateTime), NULL, 0)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3003, 11, 5, 1042, 1, N'~/Members/5/11/Attachements/', 1, CAST(N'2021-04-08T00:16:48.683' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'cricket', N'History', CAST(N'2021-04-08T00:16:48.683' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3004, 14, 5, 1042, 1, N'~/Members/5/14/Attachements/', 0, CAST(N'2021-04-08T00:17:49.310' AS DateTime), 1, CAST(12 AS Decimal(18, 0)), N'c', N'Science', CAST(N'2021-04-08T00:17:49.310' AS DateTime), NULL, CAST(N'2021-04-08T15:17:54.080' AS DateTime), 5, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3005, 15, 5, 1042, 1, N'~/Members/5/15/Attachements/', 0, CAST(N'2021-04-08T11:31:47.850' AS DateTime), 1, CAST(78 AS Decimal(18, 0)), N'java', N'Science', CAST(N'2021-04-08T11:31:47.850' AS DateTime), NULL, CAST(N'2021-04-08T15:17:57.647' AS DateTime), 5, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3006, 17, 5, 1042, 1, N'~/Members/5/17/Attachements/', 0, CAST(N'2021-04-08T11:42:01.943' AS DateTime), 1, CAST(12 AS Decimal(18, 0)), N'react', N'History', CAST(N'2021-04-08T11:42:01.943' AS DateTime), NULL, CAST(N'2021-04-08T15:17:47.133' AS DateTime), 5, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3007, 3029, 1043, 1043, 1, N'~/Members/1043/3029/Attachements/', 1, CAST(N'2021-04-08T16:41:10.770' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'kartik', N'body', CAST(N'2021-04-08T16:41:10.767' AS DateTime), 1043, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3008, 3028, 1043, 1043, 1, N'~/Members/1043/3028/Attachements/', 1, CAST(N'2021-04-08T16:51:49.317' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), N'morgan', N'Science', CAST(N'2021-04-08T16:51:49.317' AS DateTime), 1043, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3009, 14, 5, 1043, 0, N'~/Members/5/14/Attachements/', 0, CAST(N'2021-04-09T12:02:11.470' AS DateTime), 1, CAST(12 AS Decimal(18, 0)), N'c', N'Science', CAST(N'2021-04-09T12:02:11.470' AS DateTime), 1043, NULL, NULL, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3010, 4035, 3059, 3058, 1, N'~/Members/3059/4035/Attachements/', 0, CAST(N'2021-04-09T15:40:14.703' AS DateTime), 1, CAST(20 AS Decimal(18, 0)), N'Vbnote', N'History', CAST(N'2021-04-09T15:40:14.703' AS DateTime), 3058, CAST(N'2021-04-09T15:40:47.637' AS DateTime), 3059, 1)
INSERT [dbo].[Downloads] ([ID], [NoteID], [Seller], [Downloader], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [PurchasedPrice], [NoteTitle], [NoteCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3011, 14, 5, 3058, 0, N'~/Members/5/14/Attachements/', 0, CAST(N'2021-04-09T16:24:44.653' AS DateTime), 1, CAST(12 AS Decimal(18, 0)), N'c', N'Science', CAST(N'2021-04-09T16:24:44.653' AS DateTime), 3058, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Downloads] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteCategories] ON 

INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Science', N'All science books', CAST(N'2021-04-05T10:33:40.637' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'History', N'Gujarati history book', CAST(N'2021-04-05T10:33:44.653' AS DateTime), 5, CAST(N'2021-04-05T16:44:55.083' AS DateTime), 5, 1)
INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Maths', N'Maths books', CAST(N'2021-04-05T10:33:48.033' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'body', N'human body', CAST(N'2021-04-05T15:23:43.937' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[NoteCategories] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'naturalo', N'this is natural category', CAST(N'2021-04-05T15:37:57.000' AS DateTime), 5, CAST(N'2021-04-09T00:46:25.360' AS DateTime), 1055, 0)
SET IDENTITY_INSERT [dbo].[NoteCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteTypes] ON 

INSERT [dbo].[NoteTypes] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'HandWritten', N'This is handwritten book', CAST(N'2021-04-05T20:30:25.843' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[NoteTypes] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'University notes', N'This is university note', CAST(N'2021-04-05T20:30:30.020' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[NoteTypes] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Story book', N'This is story book', CAST(N'2021-04-05T20:30:34.250' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[NoteTypes] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'Science ', N'This is scirnce book', CAST(N'2021-04-05T21:36:32.617' AS DateTime), 5, CAST(N'2021-04-05T21:38:09.427' AS DateTime), 5, 1)
INSERT [dbo].[NoteTypes] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'type', N'this is typr', CAST(N'2021-04-09T00:07:56.967' AS DateTime), 1046, CAST(N'2021-04-09T00:09:51.550' AS DateTime), 1046, 0)
SET IDENTITY_INSERT [dbo].[NoteTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[ReferenceData] ON 

INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Male', N'M', N'Gender', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Female', N'Fe', N'Gender', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Unknown', N'U', N'Gender', NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'Paid', N'P', N'Selling Mode', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, N'Free', N'F', N'Selling Mode', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, N'Draft', N'Draft', N'Notes Status', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, N'Submitted For Review', N'Submitted For Review', N'Notes Status', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, N'In Review', N'In Review', N'Notes Status', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, N'Published', N'Approved', N'Notes Status', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, N'Rejected', N'Rejected', N'Notes Status', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[ReferenceData] ([ID], [Value], [Datavalue], [RefCategory], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, N'Removed', N'Removed', N'Notes Status', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[ReferenceData] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotes] ON 

INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 5, 10, 5, N'this is not good', NULL, N'tut', 2, N'~/Members/5/6/IMG_20200114_172441.jpg', 2, 66, N'qqqqqqqqqqqqqqqqq', N'qqqqqqqqqq', 2, N'qqqqqqqqqq', N'4785', N'qqqqqqqqqqq', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/6/computer.txt', CAST(N'2021-03-04T19:08:16.080' AS DateTime), 5, CAST(N'2021-04-03T01:28:25.570' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 21, 6, 21, NULL, NULL, N'science', 1, N'~/Members/21/7/IMG_20200113_120531.jpg', 2, 45, N'this is science book', N'government college', 1, N'vbvbvbvbvbvbvbv', N'4785', N'mkloyfrg', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/21/7/rutvik.txt', CAST(N'2021-03-05T15:24:46.527' AS DateTime), 21, NULL, NULL, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, 21, 6, 21, NULL, NULL, N'python', 1, N'~/Members/21/8/IMG_20200114_115926.jpg', 2, 96, N'this is python book ', N'atmiya', 2, N'python', N'4563', N'panda sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/21/8/rutvik.txt', CAST(N'2021-03-05T15:26:21.983' AS DateTime), 21, NULL, NULL, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 5, 9, 1046, NULL, CAST(N'2021-04-08T22:12:02.160' AS DateTime), N'mechanical', 2, N'~/Members/5/9/IMG_20200103_132537.jpg', 2, 450, N'this is mechanical', N'TMIYA', 3, N'mechanical', N'2356', N'pandya', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/9/mechanical.txt', CAST(N'2021-03-05T21:22:09.193' AS DateTime), 5, CAST(N'2021-04-08T22:12:02.160' AS DateTime), 1046, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, 5, 10, 1046, N'you are reejctes', NULL, N'travel', 2, N'~/Members/5/10/IMG_20200103_132537.jpg', 2, 96, N'travel travel', N'marwadi', 3, N'travel', N'1200', N'rohit', 1, CAST(4 AS Decimal(18, 0)), N'~/Members/5/10/computer.txt', CAST(N'2021-03-05T21:45:09.910' AS DateTime), 5, CAST(N'2021-04-08T22:11:44.547' AS DateTime), 1046, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, 5, 9, 5, NULL, CAST(N'2021-03-25T15:50:52.717' AS DateTime), N'cricket', 2, N'~/Members/5/11/IMG_20200103_132537.jpg', 1, 30, N'cricket is cricket', N'pathak', 1, N'cricket', N'10', N'rohit', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/11/rutvik.txt', CAST(N'2021-03-05T21:46:13.090' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, 5, 9, 5, NULL, CAST(N'2021-03-25T15:50:52.717' AS DateTime), N'Money', 1, N'~/Members/5/12/IMG_20200103_132537.jpg', 2, 45, N'science is everything', N'atmiya', 3, N'money', N'70', N'harshad', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/12/rutvik.txt', CAST(N'2021-03-05T21:47:23.510' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (13, 5, 9, 5, NULL, CAST(N'2021-03-25T15:50:52.717' AS DateTime), N'power', 2, N'~/Members/5/13/IMG_20200103_132535.jpg', 2, 90, N'power power power', N'atmiya', 2, N'power', N'4785', N'rohit', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/13/rutvik.txt', CAST(N'2021-03-05T21:48:17.860' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (14, 5, 9, 5, NULL, CAST(N'2021-03-25T15:50:52.717' AS DateTime), N'c', 1, N'~/Members/5/14/computer-science.png', 2, 100, N'c languague', N'atmiya', 2, N'c', N'1200', N'panda sir', 1, CAST(12 AS Decimal(18, 0)), N'~/Members/5/14/rutvik.txt', CAST(N'2021-03-05T23:27:35.970' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (15, 5, 9, 5, NULL, CAST(N'2021-03-25T15:50:52.717' AS DateTime), N'java', 1, N'~/Members/5/15/computer-science.png', 1, 66, N'java object', N'atmiya', 1, N'java', N'4785', N'panda sir', 1, CAST(78 AS Decimal(18, 0)), N'~/Members/5/15/rutvik.txt', CAST(N'2021-03-05T23:29:33.603' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (16, 5, 11, 5, N'this is harmfull', CAST(N'2021-03-25T15:50:52.717' AS DateTime), N'ai', 2, N'~/Members/5/16/computer-science.png', 2, 20, N'artificial inteliggence', N'atmiya', 2, N'ai', N'1200', N'panda sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/16/rutvik.txt', CAST(N'2021-03-05T23:30:20.483' AS DateTime), 5, CAST(N'2021-04-03T01:17:49.357' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (17, 5, 9, 5, NULL, CAST(N'2021-03-25T15:50:52.717' AS DateTime), N'react', 2, N'~/Members/5/17/computer-science.png', 2, 45, N'react js', N'atmiya', 3, N'react js', N'4563', N'panda sir', 1, CAST(12 AS Decimal(18, 0)), N'~/Members/5/17/rutvik.txt', CAST(N'2021-03-05T23:31:15.993' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (18, 5, 6, 5, NULL, NULL, N'Foot', 3, N'~/Members/5/18/1.jpg', 2, 1000, N'this is  football book', N'atmiya', 2, N'football', N'1212', N'panda sir', 1, CAST(4 AS Decimal(18, 0)), N'~/Members/5/18/football.txt', CAST(N'2021-03-06T11:39:52.927' AS DateTime), 5, CAST(N'2021-04-09T17:30:30.103' AS DateTime), 5, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (19, 5, 6, 5, NULL, NULL, N'angular', 2, N'~/Members/5/19/1.jpg', 2, 30, N'angular js book', N'atmiya', 2, N'angular', N'170200', N'panda sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/19/rutvik.txt', CAST(N'2021-03-06T14:29:58.080' AS DateTime), 5, CAST(N'2021-04-09T17:31:30.543' AS DateTime), 5, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (23, 5, 6, 5, NULL, NULL, N'Android', 3, N'~/Members/5/23/computer-science.png', 1, 123, N'this is android book', N'atmiya', 2, N'Android', N'1200', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/23/rutvik.txt', CAST(N'2021-03-12T16:02:03.690' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (29, 5, 6, 5, NULL, NULL, N'linux', 1, N'~/Members/5/29/computer-science.png', 1, 66, N'linux book', N'atmiya', 2, N'linux', N'23434', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/29/mechanical.txt', CAST(N'2021-03-12T17:58:45.757' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1020, 5, 10, 5, N'this is admin remark', NULL, N'electical', 1, N'~/Members/5/1020/computer-science.png', 2, 12, N'this is electrical', N'vvp', 2, N'electrical', N'23434', N'khan sir', 1, CAST(12 AS Decimal(18, 0)), N'~/Members/5/1020/mechanical.pdf', CAST(N'2021-03-13T10:48:19.847' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1021, 5, 10, 5, N'this is admin remark', NULL, N'gate', 2, N'~/Members/5/1021/computer-science.png', 1, 66, N'this is gate book', N'atmiya', 2, N'gate', N'80060', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/1021/mechanical.pdf', CAST(N'2021-03-13T11:05:30.363' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1022, 5, 9, 1055, N'this is admin remark', CAST(N'2021-04-09T00:45:45.373' AS DateTime), N'material', 2, N'~/Members/5/1022/computer-science.png', 2, 66, N'this is material book', N'atmiya', 1, N'material', N'123', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/1022/mechanical.pdf', CAST(N'2021-03-13T11:07:14.513' AS DateTime), 5, CAST(N'2021-04-09T00:45:45.373' AS DateTime), 1055, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2020, 5, 6, NULL, NULL, NULL, N'material', 2, N'~/Members/5/2020/computer-science.png', 2, 66, N'this is material book', N'atmiya', 1, N'material', N'123', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/2020/mechanical.pdf', CAST(N'2021-03-20T08:55:24.680' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2021, 5, 7, 5, NULL, NULL, N'note1', 1, N'~/Members/5/2021/computer-science.png', 2, 123, N'note 1 descripotion', N'atmiya', 2, N'note1', N'4563', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/2021/civil.pdf', CAST(N'2021-03-21T09:59:31.977' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2022, 5, 7, 5, NULL, NULL, N'note 2', 1, N'~/Members/5/2022/computer-science.png', 2, 123, N'note2', N'zxccvvbbn', 2, N'note2', N'123', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/2022/civil.pdf', CAST(N'2021-03-21T10:08:30.223' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2024, 5, 8, 5, NULL, NULL, N'note3', 2, N'~/Members/5/2024/computer-science.png', 1, 1000, N'this is note 3', N'atmiya', 2, N'note 3', N'23434', N'khan sir', 1, CAST(4 AS Decimal(18, 0)), N'~/Members/5/2024/civil.pdf', CAST(N'2021-03-21T15:33:23.730' AS DateTime), 5, CAST(N'2021-04-02T18:09:46.027' AS DateTime), 5, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2025, 5, 9, 5, N'this is note harmfull', CAST(N'2021-04-03T21:24:31.633' AS DateTime), N'zipnote', 1, N'~/Members/5/2025/computer-science.png', 3, 123, N'zip note book', N'zxccvvbbn', 1, N'zip book', N'9030', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/2025/civil.pdf', CAST(N'2021-03-21T17:46:33.743' AS DateTime), 5, CAST(N'2021-04-03T21:24:31.633' AS DateTime), 5, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2026, 5, 11, 5, N'this is harmfull', CAST(N'2021-04-02T21:33:05.497' AS DateTime), N'note 4', 2, N'~/Members/5/2026/computer-science.png', 1, 100, N'this is note 4', N'atmiya', 1, N'note 4', N'170200', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/2026/civil.pdf', CAST(N'2021-03-22T11:24:36.397' AS DateTime), 5, CAST(N'2021-04-03T01:11:59.887' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2027, 5, 6, 5, NULL, NULL, N'note', 2, N'~/Members/5/2027/1.jpg', 1, 45, N'note 5', N'atmiya', 1, N'note 5', N'14789', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/2027/computer.pdf', CAST(N'2021-03-22T11:30:42.797' AS DateTime), 5, CAST(N'2021-04-09T17:31:10.047' AS DateTime), 5, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2029, 5, 11, 5, N'this is not usefull note', CAST(N'2021-04-02T21:33:12.270' AS DateTime), N'note 5', 2, N'~/Members/5/2029/images (11).jpeg', 2, NULL, N'this is  note 5', NULL, 2, N'note 3', N'12963', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/2029/civil.pdf', CAST(N'2021-03-26T00:18:29.140' AS DateTime), 5, CAST(N'2021-04-03T00:55:12.130' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2030, 5, 11, 5, N'this is note usefull', CAST(N'2021-04-02T21:33:21.290' AS DateTime), N'note 6', 1, N'~/Members/5/2030/Screenshot (882).png', 2, NULL, N'this is note 6', NULL, 2, N'Android', N'23434', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/2030/civil.pdf', CAST(N'2021-03-26T00:23:58.930' AS DateTime), 5, CAST(N'2021-04-03T11:55:46.640' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2031, 5, 6, 5, NULL, NULL, N'note', 1, NULL, 2, 10, N'this is note 6', N'parth school', 2, N'ntoe 6', N'89', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/5/2031/mechanical.pdf', CAST(N'2021-03-26T18:12:41.463' AS DateTime), 5, CAST(N'2021-04-09T17:30:50.723' AS DateTime), 5, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3020, 1042, 11, 1042, NULL, NULL, N'virat', 3, N'~/Content/image/default-note-img/customer-2.jpg', 2, 123, N'description of virat book', N'marwadi', 2, N'Android', N'12345', N'khan sir', 1, CAST(34 AS Decimal(18, 0)), N'~/Members/1042/3020/civil.pdf', CAST(N'2021-04-08T00:06:14.470' AS DateTime), 1042, CAST(N'2021-04-08T01:36:06.903' AS DateTime), NULL, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3021, 1042, 11, 1042, NULL, NULL, N'abd', 2, N'~/Content/image/default-note-img/customer-2.jpg', 2, 100, N'abd book', N'marwadi', 3, N'linux', N'4563', N'khan sir', 1, CAST(0 AS Decimal(18, 0)), N'~/Members/1042/3021/civil.pdf', CAST(N'2021-04-08T00:22:06.613' AS DateTime), 1042, CAST(N'2021-04-08T01:04:45.720' AS DateTime), NULL, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3022, 1042, 11, 1042, NULL, NULL, N'chahal', 2, N'~/Members/1042/3022/cats.jpg', 3, 66, N'description', N'atmiya', 2, N'Android', N'4785', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/1042/3022/mechanical.pdf', CAST(N'2021-04-08T01:50:02.367' AS DateTime), 1042, CAST(N'2021-04-08T01:50:36.820' AS DateTime), NULL, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3023, 1042, 11, 1042, NULL, NULL, N'maxiiiiiiiiiiii', 3, N'~/Content/image/default-note-img/customer-2.jpg', 2, 66, N'description', N'atmiya', 3, N'Android', N'23434', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/1042/3023/computer.pdf', CAST(N'2021-04-08T02:19:35.433' AS DateTime), 1042, CAST(N'2021-04-08T02:21:36.127' AS DateTime), NULL, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3024, 1042, 6, 1042, NULL, NULL, N'zzzzzrcb', 3, N'~/Content/image/default-note-img/customer-2.jpg', 2, 123, N'descri', N'atmiya', 3, N'Android', N'23434', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/1042/3024/civil.pdf', CAST(N'2021-04-08T02:30:02.890' AS DateTime), 1042, CAST(N'2021-04-08T10:22:58.690' AS DateTime), NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3025, 1044, 11, 1046, N'i m unpublishing this', CAST(N'2021-04-08T17:33:30.533' AS DateTime), N'pant', 3, N'~/Content/image/default-note-img/customer-2.jpg', 2, 12, N'this is pant', N'delhi gnu', 2, N'pant', N'1717', N'ponting', 1, CAST(34 AS Decimal(18, 0)), N'~/Members/1044/3025/super kings.pdf', CAST(N'2021-04-08T15:52:33.680' AS DateTime), 1044, CAST(N'2021-04-08T22:17:01.103' AS DateTime), 1046, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3026, 1044, 6, 1044, NULL, NULL, N'dhawan', 2, N'~/Members/1044/3026/IMG_20191208_144138.jpg', 2, 23, N'this is dhawan', N'delhi university', 1, N'dhawan', N'2323', N'ponting sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/1044/3026/rcb.pdf', CAST(N'2021-04-08T15:56:56.570' AS DateTime), 1044, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3027, 1044, 6, 1044, NULL, NULL, N'axar', 2, N'~/Members/1044/3027/IMG_20191219_162116.jpg', 2, 33, N'this is axar patel', N'delhi university', 2, N'axar', N'33', N'ponting sir', 1, CAST(33 AS Decimal(18, 0)), N'~/Members/1044/3027/mumbai indians.pdf', CAST(N'2021-04-08T15:58:29.577' AS DateTime), 1044, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3028, 1043, 8, 1043, NULL, NULL, N'morgan', 1, N'~/Members/1043/3028/IMG_20191219_162244.jpg', 1, 52, N'this is morgan', N'kolkata', 2, N'morgan', N'5252', N'maculam sir', 1, CAST(52 AS Decimal(18, 0)), N'~/Members/1043/3028/super kings.pdf', CAST(N'2021-04-08T16:03:05.847' AS DateTime), 1043, CAST(N'2021-04-08T22:11:15.160' AS DateTime), 1046, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3029, 1043, 11, 15, N'this is unpublished note ', CAST(N'2021-04-08T17:32:13.600' AS DateTime), N'kartik', 4, N'~/Members/1043/3029/IMG_20191219_162116.jpg', 2, 86, N'this is dk', N'kolkata uni', 1, N'kartik', N'8686', N'maculam sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/1043/3029/super kings.pdf', CAST(N'2021-04-08T16:04:29.890' AS DateTime), 1043, CAST(N'2021-04-08T17:32:39.447' AS DateTime), 15, 0)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3030, 1043, 7, 1043, NULL, NULL, N'russel', 2, N'~/Content/image/default-note-img/customer-2.jpg', 2, 12, N'andre russel', N'atmiya', 2, N'Android', N'123', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/1043/3030/super kings.pdf', CAST(N'2021-04-09T02:11:13.277' AS DateTime), 1043, CAST(N'2021-04-09T02:11:52.120' AS DateTime), 1043, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4030, 3058, 10, 3060, N'this is rutvik ranote', NULL, N'Ranote', 2, N'~/Content/image/default-note-img/1.jpg', 1, 10, N'this is rutvik note1', N'nirma', 2, N'note1', N'4785', N'kotak sir', 0, CAST(0 AS Decimal(18, 0)), NULL, CAST(N'2021-04-09T15:14:01.803' AS DateTime), 3058, CAST(N'2021-04-09T15:42:38.083' AS DateTime), 3060, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4031, 3058, 6, 3058, NULL, NULL, N'Rbnote', 4, N'~/Content/image/default-note-img/1.jpg', 4, 100, N'this is rutvik note2', N'marwadi', 1, N'note2', N'123', N'pandya sir', 1, CAST(10 AS Decimal(18, 0)), NULL, CAST(N'2021-04-09T15:16:12.933' AS DateTime), 3058, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4032, 3058, 6, 3058, NULL, NULL, N'Rcnote', 2, N'~/Members/3058/4032/computer-science.png', 4, 90, N'this is rutvik note 3', N'pathak', 3, N'note 3', N'3333', N'russel', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/3058/4032/computer.pdf', CAST(N'2021-04-09T15:17:28.980' AS DateTime), 3058, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4033, 3058, 9, 3060, NULL, CAST(N'2021-04-09T15:36:56.143' AS DateTime), N'Rdnote', 1, N'~/Members/3058/4033/computer-science.png', 2, 40, N'this is rutvik note4', N'dholakiya', 2, N'Android', N'4444', N'khan sir', 1, CAST(50 AS Decimal(18, 0)), N'~/Members/3058/4033/mechanical.pdf', CAST(N'2021-04-09T15:18:40.230' AS DateTime), 3058, CAST(N'2021-04-09T15:36:56.143' AS DateTime), 3060, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4034, 3059, 8, 3059, NULL, NULL, N'Vanote', 3, N'~/Members/3059/4034/1.jpg', 4, 10, N'this is virat 1 book', N'banglore', 1002, N'note1', N'1111', N'panda sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/3059/4034/super kings.pdf', CAST(N'2021-04-09T15:25:03.557' AS DateTime), 3059, CAST(N'2021-04-09T15:36:19.433' AS DateTime), 3060, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4035, 3059, 9, 3060, NULL, CAST(N'2021-04-09T15:36:39.653' AS DateTime), N'Vbnote', 2, N'~/Members/3059/4035/1.jpg', 2, 22, N'this is  note 2 virat', N'modi college', 2, N'Android', N'2222', N'khan sir', 1, CAST(20 AS Decimal(18, 0)), N'~/Members/3059/4035/mumbai indians.pdf', CAST(N'2021-04-09T15:26:13.957' AS DateTime), 3059, CAST(N'2021-04-09T15:36:39.653' AS DateTime), 3060, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4036, 3059, 6, 3059, NULL, NULL, N'Vcnote', 2, N'~/Members/3059/4036/1.jpg', 2, 33, N'this is vira note 3', N'atmiya', 1, N'note 3', N'1200', N'khan sir', 0, CAST(0 AS Decimal(18, 0)), N'~/Members/3059/4036/mumbai indians.pdf', CAST(N'2021-04-09T15:27:13.743' AS DateTime), 3059, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4037, 3059, 6, 3059, NULL, NULL, N'Vdnote', 1, N'~/Content/image/default-note-img/1.jpg', 3, 44, N'this is note 4 virat', N'atmiya', 3, N'Android', N'4444', N'khan sir', 1, CAST(90 AS Decimal(18, 0)), N'~/Members/3059/4037/super kings.pdf', CAST(N'2021-04-09T15:28:18.593' AS DateTime), 3059, NULL, NULL, 1)
INSERT [dbo].[SellerNotes] ([ID], [SellerID], [Status], [ActionedBy], [AdminRemarks], [PublishedDate], [Title], [Category], [DisplayPicture], [NoteType], [NumberofPages], [Description], [UniversityName], [Country], [Course], [CourseCode], [Professor], [IsPaid], [SellingPrice], [NotesPreview], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4038, 3058, 11, NULL, NULL, NULL, N'Ranote', 2, N'~/Members/3058/4038/1.jpg', 1, 10, N'this is rutvik note1', N'nirma', 2, N'note1', N'4785', N'kotak sir', 0, CAST(0 AS Decimal(18, 0)), NULL, CAST(N'2021-04-09T15:43:05.093' AS DateTime), 3058, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[SellerNotes] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotesAttachements] ON 

INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 6, N'rutvik.txt', N'~/Members/5/6/Attachements/rutvik.txt', CAST(N'2021-03-04T19:08:16.730' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 7, N'rutvik.txt', N'~/Members/21/7/Attachements/rutvik.txt', CAST(N'2021-03-05T15:24:49.017' AS DateTime), 21, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 8, N'rutvik.txt', N'~/Members/21/8/Attachements/rutvik.txt', CAST(N'2021-03-05T15:26:22.250' AS DateTime), 21, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 9, N'rutvik.txt', N'~/Members/5/9/Attachements/rutvik.txt', CAST(N'2021-03-05T21:22:11.993' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 10, N'rutvik.txt', N'~/Members/5/10/Attachements/rutvik.txt', CAST(N'2021-03-05T21:45:10.983' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 11, N'rutvik.txt', N'~/Members/5/11/Attachements/rutvik.txt', CAST(N'2021-03-05T21:46:13.290' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 12, N'rutvik.txt', N'~/Members/5/12/Attachements/rutvik.txt', CAST(N'2021-03-05T21:47:23.713' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, 13, N'rutvik.txt', N'~/Members/5/13/Attachements/rutvik.txt', CAST(N'2021-03-05T21:48:18.053' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 14, N'rutvik.txt', N'~/Members/5/14/Attachements/rutvik.txt', CAST(N'2021-03-05T23:27:38.000' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, 15, N'rutvik.txt', N'~/Members/5/15/Attachements/rutvik.txt', CAST(N'2021-03-05T23:29:33.887' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, 16, N'rutvik.txt', N'~/Members/5/16/Attachements/rutvik.txt', CAST(N'2021-03-05T23:30:20.630' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, 17, N'rutvik.txt', N'~/Members/5/17/Attachements/rutvik.txt', CAST(N'2021-03-05T23:31:16.180' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (13, 18, N'football.txt', N'~/Members/5/18/Attachements/football.txt', CAST(N'2021-03-06T11:42:07.983' AS DateTime), 5, CAST(N'2021-03-07T15:00:44.737' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (14, 19, N'rutvik.txt', N'~/Members/5/19/Attachements/rutvik.txt', CAST(N'2021-03-06T14:31:54.743' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (15, 23, N'rutvik.txt', N'~/Members/5/23/Attachements/rutvik.txt', CAST(N'2021-03-12T16:02:04.447' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (16, 29, N'rutvik.txt', N'~/Members/5/29/Attachements/rutvik.txt', CAST(N'2021-03-12T17:58:47.133' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1015, 1020, N'mechanical.pdf', N'~/Members/5/1020/Attachements/mechanical.pdf', CAST(N'2021-03-13T10:48:22.517' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1016, 1021, N'mechanical.pdf', N'~/Members/5/1021/Attachements/mechanical.pdf', CAST(N'2021-03-13T11:05:30.447' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1017, 1022, N'mechanical.pdf', N'~/Members/5/1022/Attachements/mechanical.pdf', CAST(N'2021-03-13T11:07:14.663' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2015, 2020, N'mechanical.pdf', N'~/Members/5/2020/Attachements/', CAST(N'2021-03-20T08:55:27.863' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2016, 2022, N'civil.pdf', N'C:\Users\Rutvik\source\repos\NotesMarketPlace\NotesMarketPlace\Members\5\2022\Attachements\', CAST(N'2021-03-21T10:08:31.273' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2017, 2022, N'computer.pdf', N'C:\Users\Rutvik\source\repos\NotesMarketPlace\NotesMarketPlace\Members\5\2022\Attachements\', CAST(N'2021-03-21T10:08:31.357' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2018, 2022, N'mechanical.pdf', N'C:\Users\Rutvik\source\repos\NotesMarketPlace\NotesMarketPlace\Members\5\2022\Attachements\', CAST(N'2021-03-21T10:08:31.397' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2019, 2024, N'civil.pdf', N'~/Members/5/2024/Attachements/', CAST(N'2021-03-21T15:33:24.943' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2020, 2024, N'computer.pdf', N'~/Members/5/2024/Attachements/', CAST(N'2021-03-21T15:33:25.053' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2021, 2025, N'civil.pdf', N'~/Members/5/2025/Attachements/', CAST(N'2021-03-21T17:46:35.827' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2022, 2025, N'computer.pdf', N'~/Members/5/2025/Attachements/', CAST(N'2021-03-21T17:46:35.940' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2023, 2025, N'mechanical.pdf', N'~/Members/5/2025/Attachements/', CAST(N'2021-03-21T17:46:36.000' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2024, 2026, N'civil.pdf', N'~/Members/5/2026/Attachements/', CAST(N'2021-03-22T11:24:39.653' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2025, 2026, N'computer.pdf', N'~/Members/5/2026/Attachements/', CAST(N'2021-03-22T11:24:39.807' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2026, 2027, N'mechanical.pdf', N'~/Members/5/2027/Attachements/', CAST(N'2021-03-22T11:30:43.390' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2027, 2029, N'mechanical.pdf', N'~/Members/5/2029/Attachements/', CAST(N'2021-03-26T00:18:33.537' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2028, 2030, N'computer.pdf', N'~/Members/5/2030/Attachements/', CAST(N'2021-03-26T00:23:59.730' AS DateTime), 5, CAST(N'2021-04-03T11:55:46.787' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2029, 2031, N'civil.pdf', N'~/Members/5/2031/Attachements/', CAST(N'2021-03-26T18:12:44.740' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2030, 2031, N'computer.pdf', N'~/Members/5/2031/Attachements/', CAST(N'2021-03-26T18:12:44.983' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2031, 2031, N'mechanical.pdf', N'~/Members/5/2031/Attachements/', CAST(N'2021-03-26T18:12:45.210' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2032, 2031, N'civil.pdf', N'~/Members/5/2031/Attachements/', CAST(N'2021-03-26T18:37:43.127' AS DateTime), 5, CAST(N'2021-03-26T18:37:43.127' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2033, 2031, N'computer.pdf', N'~/Members/5/2031/Attachements/', CAST(N'2021-03-26T18:44:54.753' AS DateTime), 5, CAST(N'2021-03-26T18:44:54.753' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2034, 2031, N'mechanical.pdf', N'~/Members/5/2031/Attachements/', CAST(N'2021-03-26T18:50:38.403' AS DateTime), 5, CAST(N'2021-03-26T18:50:38.403' AS DateTime), 5, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3015, 3020, N'computer.pdf', N'~/Members/1042/3020/Attachements/', CAST(N'2021-04-08T00:20:08.850' AS DateTime), 1042, CAST(N'2021-04-08T00:20:08.850' AS DateTime), 1042, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3016, 3021, N'civil.pdf', N'~/Members/1042/3021/Attachements/', CAST(N'2021-04-08T00:22:07.003' AS DateTime), 1042, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3017, 3021, N'computer.pdf', N'~/Members/1042/3021/Attachements/', CAST(N'2021-04-08T00:22:07.173' AS DateTime), 1042, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3018, 3021, N'mechanical.pdf', N'~/Members/1042/3021/Attachements/', CAST(N'2021-04-08T00:22:07.317' AS DateTime), 1042, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3019, 3020, N'computer.pdf', N'~/Members/1042/3020/Attachements/', CAST(N'2021-04-08T01:34:31.937' AS DateTime), 1042, CAST(N'2021-04-08T01:34:31.937' AS DateTime), 1042, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3020, 3020, N'computer.pdf', N'~/Members/1042/3020/Attachements/', CAST(N'2021-04-08T01:35:25.880' AS DateTime), 1042, CAST(N'2021-04-08T01:35:25.880' AS DateTime), 1042, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3021, 3020, N'mechanical.pdf', N'~/Members/1042/3020/Attachements/', CAST(N'2021-04-08T01:36:06.943' AS DateTime), 1042, CAST(N'2021-04-08T01:36:06.943' AS DateTime), 1042, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3022, 3022, N'civil.pdf', N'~/Members/1042/3022/Attachements/', CAST(N'2021-04-08T01:50:04.697' AS DateTime), 1042, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3023, 3022, N'computer.pdf', N'~/Members/1042/3022/Attachements/', CAST(N'2021-04-08T01:50:04.897' AS DateTime), 1042, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3024, 3022, N'mechanical.pdf', N'~/Members/1042/3022/Attachements/', CAST(N'2021-04-08T01:50:04.973' AS DateTime), 1042, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3025, 3023, N'civil.pdf', N'~/Members/1042/3023/Attachements/', CAST(N'2021-04-08T02:19:36.500' AS DateTime), 1042, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3026, 3023, N'computer.pdf', N'~/Members/1042/3023/Attachements/', CAST(N'2021-04-08T02:19:36.710' AS DateTime), 1042, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3027, 3023, N'mechanical.pdf', N'~/Members/1042/3023/Attachements/', CAST(N'2021-04-08T02:19:36.730' AS DateTime), 1042, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3028, 3024, N'mechanical.pdf', N'~/Members/1042/3024/Attachements/', CAST(N'2021-04-08T02:30:05.680' AS DateTime), 1042, NULL, NULL, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3029, 3024, N'civil.pdf', N'~/Members/1042/3024/Attachements/', CAST(N'2021-04-08T02:31:54.777' AS DateTime), 1042, CAST(N'2021-04-08T02:31:54.777' AS DateTime), 1042, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3030, 3024, N'computer.pdf', N'~/Members/1042/3024/Attachements/', CAST(N'2021-04-08T02:31:54.847' AS DateTime), 1042, CAST(N'2021-04-08T02:31:54.847' AS DateTime), 1042, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3031, 3024, N'mechanical.pdf', N'~/Members/1042/3024/Attachements/', CAST(N'2021-04-08T02:31:54.937' AS DateTime), 1042, CAST(N'2021-04-08T02:31:54.937' AS DateTime), 1042, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3032, 3024, N'computer.pdf', N'~/Members/1042/3024/Attachements/', CAST(N'2021-04-08T10:10:03.147' AS DateTime), 1042, CAST(N'2021-04-08T10:10:03.147' AS DateTime), 1042, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3033, 3024, N'civil.pdf', N'~/Members/1042/3024/Attachements/', CAST(N'2021-04-08T10:22:59.167' AS DateTime), 1042, CAST(N'2021-04-08T10:22:59.167' AS DateTime), 1042, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3034, 3024, N'computer.pdf', N'~/Members/1042/3024/Attachements/', CAST(N'2021-04-08T10:22:59.283' AS DateTime), 1042, CAST(N'2021-04-08T10:22:59.283' AS DateTime), 1042, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3035, 3024, N'mechanical.pdf', N'~/Members/1042/3024/Attachements/', CAST(N'2021-04-08T10:22:59.323' AS DateTime), 1042, CAST(N'2021-04-08T10:22:59.323' AS DateTime), 1042, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3036, 3025, N'mumbai indians.pdf', N'~/Members/1044/3025/Attachements/', CAST(N'2021-04-08T15:52:34.233' AS DateTime), 1044, CAST(N'2021-04-08T22:17:01.110' AS DateTime), 1046, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3037, 3025, N'rcb.pdf', N'~/Members/1044/3025/Attachements/', CAST(N'2021-04-08T15:52:34.323' AS DateTime), 1044, CAST(N'2021-04-08T22:17:01.110' AS DateTime), 1046, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3038, 3026, N'super kings.pdf', N'~/Members/1044/3026/Attachements/', CAST(N'2021-04-08T15:56:56.680' AS DateTime), 1044, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3039, 3027, N'rcb.pdf', N'~/Members/1044/3027/Attachements/', CAST(N'2021-04-08T15:58:29.737' AS DateTime), 1044, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3040, 3028, N'mumbai indians.pdf', N'~/Members/1043/3028/Attachements/', CAST(N'2021-04-08T16:03:05.980' AS DateTime), 1043, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3041, 3029, N'super kings.pdf', N'~/Members/1043/3029/Attachements/', CAST(N'2021-04-08T16:04:30.067' AS DateTime), 1043, CAST(N'2021-04-08T17:32:39.457' AS DateTime), 15, 0)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3042, 3030, N'super kings.pdf', N'~/Members/1043/3030/Attachements/', CAST(N'2021-04-09T02:11:15.743' AS DateTime), 1043, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4042, 4030, N'civil.pdf', N'~/Members/3058/4030/Attachements/', CAST(N'2021-04-09T15:14:02.500' AS DateTime), 3058, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4043, 4031, N'civil.pdf', N'~/Members/3058/4031/Attachements/', CAST(N'2021-04-09T15:16:13.000' AS DateTime), 3058, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4044, 4031, N'computer.pdf', N'~/Members/3058/4031/Attachements/', CAST(N'2021-04-09T15:16:13.083' AS DateTime), 3058, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4045, 4032, N'computer.pdf', N'~/Members/3058/4032/Attachements/', CAST(N'2021-04-09T15:17:29.233' AS DateTime), 3058, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4046, 4033, N'civil.pdf', N'~/Members/3058/4033/Attachements/', CAST(N'2021-04-09T15:18:40.403' AS DateTime), 3058, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4047, 4033, N'computer.pdf', N'~/Members/3058/4033/Attachements/', CAST(N'2021-04-09T15:18:40.427' AS DateTime), 3058, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4048, 4033, N'mechanical.pdf', N'~/Members/3058/4033/Attachements/', CAST(N'2021-04-09T15:18:40.440' AS DateTime), 3058, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4049, 4034, N'super kings.pdf', N'~/Members/3059/4034/Attachements/', CAST(N'2021-04-09T15:25:03.637' AS DateTime), 3059, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4050, 4035, N'mumbai indians.pdf', N'~/Members/3059/4035/Attachements/', CAST(N'2021-04-09T15:26:14.020' AS DateTime), 3059, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4051, 4035, N'rcb.pdf', N'~/Members/3059/4035/Attachements/', CAST(N'2021-04-09T15:26:14.037' AS DateTime), 3059, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4052, 4035, N'super kings.pdf', N'~/Members/3059/4035/Attachements/', CAST(N'2021-04-09T15:26:14.053' AS DateTime), 3059, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4053, 4036, N'mumbai indians.pdf', N'~/Members/3059/4036/Attachements/', CAST(N'2021-04-09T15:27:13.867' AS DateTime), 3059, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4054, 4037, N'rcb.pdf', N'~/Members/3059/4037/Attachements/', CAST(N'2021-04-09T15:28:18.820' AS DateTime), 3059, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachements] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4055, 4038, N'civil.pdf', N'~/Members/3058/4038/Attachements/', CAST(N'2021-04-09T15:43:05.397' AS DateTime), 3058, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[SellerNotesAttachements] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotesReportedIssues] ON 

INSERT [dbo].[SellerNotesReportedIssues] ([ID], [NoteID], [ReportedByID], [AgainstDownloadID], [Remarks], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, 9, 5, 2003, N'this is mechanical book', CAST(N'2021-04-04T19:08:13.207' AS DateTime), 5, NULL, NULL)
INSERT [dbo].[SellerNotesReportedIssues] ([ID], [NoteID], [ReportedByID], [AgainstDownloadID], [Remarks], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, 2030, 5, 1013, N'this is note6', CAST(N'2021-04-04T19:08:31.433' AS DateTime), 5, NULL, NULL)
INSERT [dbo].[SellerNotesReportedIssues] ([ID], [NoteID], [ReportedByID], [AgainstDownloadID], [Remarks], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, 1021, 5, 1012, N'this is gate note', CAST(N'2021-04-04T19:08:44.773' AS DateTime), 5, NULL, NULL)
INSERT [dbo].[SellerNotesReportedIssues] ([ID], [NoteID], [ReportedByID], [AgainstDownloadID], [Remarks], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, 1020, 5, 1011, N'this is electrical note', CAST(N'2021-04-04T19:09:21.590' AS DateTime), 5, NULL, NULL)
INSERT [dbo].[SellerNotesReportedIssues] ([ID], [NoteID], [ReportedByID], [AgainstDownloadID], [Remarks], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, 17, 5, 1004, N'this is react note', CAST(N'2021-04-04T19:09:32.493' AS DateTime), 5, NULL, NULL)
INSERT [dbo].[SellerNotesReportedIssues] ([ID], [NoteID], [ReportedByID], [AgainstDownloadID], [Remarks], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, 2025, 5, 1003, N'this is zip note', CAST(N'2021-04-04T19:09:45.260' AS DateTime), 5, NULL, NULL)
INSERT [dbo].[SellerNotesReportedIssues] ([ID], [NoteID], [ReportedByID], [AgainstDownloadID], [Remarks], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (8, 15, 5, 5, N'this is java note', CAST(N'2021-04-04T19:09:55.307' AS DateTime), 5, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SellerNotesReportedIssues] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotesReviews] ON 

INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 14, 5, 4, CAST(3 AS Decimal(18, 0)), N'thid is c notebook', CAST(N'2021-03-15T11:25:41.627' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 14, 5, 4, CAST(1 AS Decimal(18, 0)), N'cricket book', CAST(N'2021-03-15T11:41:08.427' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 11, 5, 1, CAST(1 AS Decimal(18, 0)), N'Edit note', CAST(N'2021-03-15T14:38:38.013' AS DateTime), 5, CAST(N'2021-04-07T10:05:38.487' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 12, 5, 2, CAST(3 AS Decimal(18, 0)), N'This is money note', CAST(N'2021-03-23T09:51:34.100' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 13, 5, 3, CAST(5 AS Decimal(18, 0)), N'this is powe note', CAST(N'2021-03-23T09:51:50.503' AS DateTime), 5, NULL, NULL, 0)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 15, 5, 5, CAST(2 AS Decimal(18, 0)), N'this is java note', CAST(N'2021-03-23T09:52:02.890' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 2025, 5, 1003, CAST(4 AS Decimal(18, 0)), N'this is zip note', CAST(N'2021-03-23T09:52:16.780' AS DateTime), 5, NULL, NULL, 1)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, 11, 4, 1008, CAST(3 AS Decimal(18, 0)), N'cricket book', CAST(N'2021-03-23T10:00:14.387' AS DateTime), 4, CAST(N'2021-04-07T02:35:54.537' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 2025, 4, 1007, CAST(3 AS Decimal(18, 0)), N'zip note zip note', CAST(N'2021-03-23T10:00:23.777' AS DateTime), 4, CAST(N'2021-03-23T10:00:53.617' AS DateTime), NULL, 1)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, 13, 4, 1005, CAST(5 AS Decimal(18, 0)), N'power book', CAST(N'2021-03-23T10:00:41.083' AS DateTime), 4, NULL, NULL, 0)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, 11, 34, 1009, CAST(3 AS Decimal(18, 0)), N'this cricket note', CAST(N'2021-03-23T10:03:27.493' AS DateTime), 34, CAST(N'2021-04-07T02:55:09.377' AS DateTime), 5, 0)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, 2025, 34, 1010, CAST(5 AS Decimal(18, 0)), N'this is zip note by puji', CAST(N'2021-03-24T01:07:03.500' AS DateTime), 34, NULL, NULL, 1)
INSERT [dbo].[SellerNotesReviews] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Ratings], [Comments], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1004, 3029, 1043, 3007, CAST(4 AS Decimal(18, 0)), N'this is dinesh kartik book', CAST(N'2021-04-08T16:45:25.347' AS DateTime), 1043, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SellerNotesReviews] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemConfigurations] ON 

INSERT [dbo].[SystemConfigurations] ([ID], [Key], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBY], [IsActive]) VALUES (2, N'SupportEmailAddress', N'system@gmail.com', CAST(N'2021-04-06T17:06:06.953' AS DateTime), 5, CAST(N'2021-04-09T17:00:18.653' AS DateTime), NULL, 1)
INSERT [dbo].[SystemConfigurations] ([ID], [Key], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBY], [IsActive]) VALUES (3, N'SupportContactNumber', N'9876543219', CAST(N'2021-04-06T17:06:06.953' AS DateTime), 5, CAST(N'2021-04-09T17:00:18.653' AS DateTime), NULL, 1)
INSERT [dbo].[SystemConfigurations] ([ID], [Key], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBY], [IsActive]) VALUES (4, N'EmailAddressesForNotify', N'system@gmail.com', CAST(N'2021-04-06T17:06:06.953' AS DateTime), 5, CAST(N'2021-04-09T17:00:18.653' AS DateTime), NULL, 1)
INSERT [dbo].[SystemConfigurations] ([ID], [Key], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBY], [IsActive]) VALUES (5, N'DefaultNoteDisplayPicture', N'~/Content/image/default-note-img/1.jpg', CAST(N'2021-04-06T17:06:06.953' AS DateTime), 5, CAST(N'2021-04-09T17:00:18.653' AS DateTime), NULL, 1)
INSERT [dbo].[SystemConfigurations] ([ID], [Key], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBY], [IsActive]) VALUES (7, N'DefaultMemberDisplayPicture', N'~/Content/image/default-user-img/customer-2.jpg', CAST(N'2021-04-06T17:06:06.953' AS DateTime), 5, CAST(N'2021-04-09T17:00:18.653' AS DateTime), NULL, 1)
INSERT [dbo].[SystemConfigurations] ([ID], [Key], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBY], [IsActive]) VALUES (8, N'FBICON', N'www.facebookurl.com', CAST(N'2021-04-06T17:06:06.953' AS DateTime), 5, CAST(N'2021-04-09T17:00:18.653' AS DateTime), NULL, 1)
INSERT [dbo].[SystemConfigurations] ([ID], [Key], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBY], [IsActive]) VALUES (9, N'TWITTERICON', N'www.twittericon.com', CAST(N'2021-04-06T17:06:06.953' AS DateTime), 5, CAST(N'2021-04-09T17:00:18.653' AS DateTime), NULL, 1)
INSERT [dbo].[SystemConfigurations] ([ID], [Key], [Value], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBY], [IsActive]) VALUES (10, N'LNICON', N'www.linkedinicon.com', CAST(N'2021-04-06T17:47:25.973' AS DateTime), 5, CAST(N'2021-04-09T17:00:18.653' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[SystemConfigurations] OFF
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 5, NULL, 1, N'rmpipaliya308@gmail.com', N'91', N'7069779298', N'~/Members/5/customer-1.jpg', N'rajkot', N'rajkot', N'rajkot', N'gujarat', N'360004', N'india', N'gecr', N'gecr', NULL, NULL, CAST(N'2021-03-24T18:54:42.183' AS DateTime), NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 4, NULL, 1, NULL, N'91', N'9856452223', N'~/Members/4/customer-2.png', N'10-BALAJI PARK, NR.BAPA SITARAM CHOWK,', N'NR.REAL PRIME MAVDI, RAJKOT', N'Mavdi', N'Gujarat', N'360004', N'India', N'gujarat university', N'government college rajkot', CAST(N'2021-03-10T22:42:14.177' AS DateTime), 4, CAST(N'2021-03-23T14:48:01.540' AS DateTime), NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1002, 34, CAST(N'2021-03-13T00:00:00.000' AS DateTime), 1, NULL, N'91', N'263515', N'~/Admins/34/SUCCESS.png', N'10-BALAJI PARK, NR.BAPA SITARAM CHOWK,', N'NR.REAL PRIME MAVDI, RAJKOT', N'Mavdi', N'Gujarat', N'360004', N'India', N'vvp', N'vvp', CAST(N'2021-03-24T01:28:14.833' AS DateTime), 34, CAST(N'2021-04-06T02:58:16.567' AS DateTime), NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2002, 15, CAST(N'2020-06-18T00:00:00.000' AS DateTime), 1, NULL, N'91', N'222222222', N'~/Members/15/WIN_20200727_10_29_48_Pro.jpg', N'10-BALAJI PARK, NR.BAPA SITARAM CHOWK,', N'NR.REAL PRIME MAVDI, RAJKOT', N'Mavdi', N'Gujarat', N'360004', N'India', N'gujarat university', N'government college rajkot', CAST(N'2021-04-06T10:13:08.520' AS DateTime), 15, CAST(N'2021-04-07T11:04:07.393' AS DateTime), NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2003, 10, NULL, 1, N'klsec@gmail.com', N'91', N'1515515', N'~/Members/10/Screenshot (845).png', N'10-BALAJI PARK, NR.BAPA SITARAM CHOWK,', N'NR.REAL PRIME MAVDI, RAJKOT', N'Mavdi', N'Gujarat', N'360004', N'India', N'gujarat university', N'government college rajkot', CAST(N'2021-04-06T11:12:01.507' AS DateTime), 10, CAST(N'2021-04-06T11:21:20.643' AS DateTime), NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2004, 12, CAST(N'2021-04-01T00:00:00.000' AS DateTime), 1, NULL, N'12', N'1515515', N'~/Content/image/user-profile-img/user-img.png', N'10-BALAJI PARK, NR.BAPA SITARAM CHOWK,', N'NR.REAL PRIME MAVDI, RAJKOT', N'Mavdi', N'Gujarat', N'360004', N'India', N'gujarat university', N'vvp', CAST(N'2021-04-06T11:23:36.540' AS DateTime), 12, NULL, NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2005, 1039, CAST(N'2021-04-07T11:04:45.147' AS DateTime), 1, NULL, N'NA', N'1234567893', N'NA', N'NA', N'NA', N'NA', N'NA', N'NA', N'NA', N'NA', N'NA', CAST(N'2021-04-07T11:04:45.147' AS DateTime), 4, CAST(N'2021-04-07T11:05:05.537' AS DateTime), NULL, 0)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2006, 1042, CAST(N'2021-04-02T00:00:00.000' AS DateTime), 1, NULL, N'91', N'1234563657', N'~/Content/image/default-user-img/customer-2.jpg', N'10-BALAJI PARK, NR.BAPA SITARAM CHOWK,', N'NR.REAL PRIME MAVDI, RAJKOT', N'Mavdi', N'Gujarat', N'360004', N'India', N'gtu', N'government college rajkot', CAST(N'2021-04-08T00:03:49.750' AS DateTime), 1042, NULL, NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2007, 1044, CAST(N'2021-04-25T00:00:00.000' AS DateTime), 1, NULL, N'91', N'1234567896', N'~/Members/1044/customer-3.jpg', N'10-BALAJI PARK, NR.BAPA SITARAM CHOWK,', N'NR.REAL PRIME MAVDI, RAJKOT', N'Mavdi', N'Gujarat', N'360004', N'India', N'delhi university', N'noyda university', CAST(N'2021-04-08T15:47:05.473' AS DateTime), 1044, CAST(N'2021-04-08T15:48:11.297' AS DateTime), NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2008, 1043, NULL, 1, NULL, N'91', N'1515515', N'~/Members/1043/customer-3.jpg', N'10-BALAJI PARK, NR.BAPA SITARAM CHOWK,', N'NR.REAL PRIME MAVDI, RAJKOT', N'Mavdi', N'Gujarat', N'360004', N'India', N'kolkata university', N'kkr', CAST(N'2021-04-08T16:01:53.797' AS DateTime), 1043, CAST(N'2021-04-09T09:28:27.870' AS DateTime), NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2009, 1054, CAST(N'2021-04-08T21:42:15.303' AS DateTime), 1, NULL, N'NA', N'125633', NULL, N'NA', N'NA', N'NA', N'NA', N'NA', N'NA', NULL, NULL, CAST(N'2021-04-08T21:42:15.303' AS DateTime), 4, CAST(N'2021-04-08T22:09:27.357' AS DateTime), NULL, 0)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2010, 1055, CAST(N'2021-04-08T21:56:01.080' AS DateTime), 1, N'hindu@gmail.com', N'12', N'545454', N'~/Admins/1055/customer-2.jpg', N'NA', N'NA', N'NA', N'NA', N'NA', N'NA', NULL, NULL, CAST(N'2021-04-08T21:56:01.080' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2011, 1056, CAST(N'2021-04-08T22:09:56.550' AS DateTime), 1, NULL, N'12', N'1010101', NULL, N'NA', N'NA', N'NA', N'NA', N'NA', N'NA', NULL, NULL, CAST(N'2021-04-08T22:09:56.550' AS DateTime), 4, CAST(N'2021-04-09T00:10:23.773' AS DateTime), NULL, 0)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2012, 3058, CAST(N'2021-03-12T00:00:00.000' AS DateTime), 1, NULL, N'10', N'1020304050', N'~/Members/3058/customer-1.jpg', N'10-BALAJI PARK, NR.BAPA SITARAM CHOWK,', N'NR.REAL PRIME MAVDI, RAJKOT', N'Mavdi', N'Gujarat', N'360004', N'India', N'gtu', N'government college rajkot', CAST(N'2021-04-09T15:07:49.783' AS DateTime), 3058, NULL, NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2013, 3059, CAST(N'2021-04-21T00:00:00.000' AS DateTime), 1, NULL, N'18', N'1020304050', N'~/Members/3059/customer-3.jpg', N'10-BALAJI PARK, NR.BAPA SITARAM CHOWK,', N'NR.REAL PRIME MAVDI, RAJKOT', N'Mavdi', N'Gujarat', N'360004', N'India', N'banglore university', N'delhi', CAST(N'2021-04-09T15:22:19.477' AS DateTime), 3059, NULL, NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2014, 3060, CAST(N'2021-04-09T15:33:37.743' AS DateTime), 1, N'bccibcci@gmail.com', N'99', N'1234564512', N'~/Content/image/default-user-img/customer-2.jpg', N'NA', N'NA', N'NA', N'NA', N'NA', N'NA', NULL, NULL, CAST(N'2021-04-09T15:33:37.743' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[UserProfile] ([ID], [UserID], [DOB], [Gender], [SecondaryEmailAddress], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine1], [AddressLine2], [City], [State], [ZipCode], [Country], [University], [College], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2015, 3062, CAST(N'2021-04-09T17:01:15.423' AS DateTime), 1, NULL, N'NA', N'1223121212', NULL, N'NA', N'NA', N'NA', N'NA', N'NA', N'NA', NULL, NULL, CAST(N'2021-04-09T17:01:15.423' AS DateTime), 4, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Member', N'member', CAST(N'2021-04-07T11:25:39.820' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'Admin', N'admin', CAST(N'2021-04-07T11:25:45.453' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, N'SuperAdmin', N'super admin', CAST(N'2021-04-07T11:25:52.677' AS DateTime), 4, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 2, N'rutvik', N'pipaliya', N'rmpipaliya308@gmail.com', N'pant', 1, CAST(N'2021-02-25T21:46:40.413' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 2, N'virat', N'kohli', N'viratkohli@gmail.com', N'Vk90Bcfh@', 1, CAST(N'2021-02-25T22:19:50.590' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 2, N'rohit', N'sarma', N'ro45@gmail.com', N'Rohit45@', 1, CAST(N'2021-02-26T11:38:51.907' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 2, N'hardik', N'pandya', N'hp33@gmail.com', N'Hardik@333', 1, CAST(N'2021-02-26T15:25:40.997' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 2, N'krunal', N'pandya', N'kp23@gmail', N'S7wO7ozT', 1, CAST(N'2021-02-26T16:28:47.847' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (10, 2, N'rahulkxip', N'klkxip', N'kl1@gmail.com', N'Klrahul11@', 1, CAST(N'2021-03-02T22:23:35.913' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, 2, N'iyer', N'shrayesh', N'iyer@gmail.com', N'Iyer123@', 1, CAST(N'2021-03-02T22:39:39.310' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (13, 2, N'Iyer', N'Shrayesh', N'iyer123@gmail.com', N'Iyer123@', 1, CAST(N'2021-03-02T22:48:23.293' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (14, 2, N'dhoni', N'ms', N'msd7@123', N'Msdhoni7@', 1, CAST(N'2021-03-02T22:56:46.310' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (15, 3, N'SureshBhai', N'Rainacskyellow', N'raina3@gmail.com', N'Sraina3@', 1, CAST(N'2021-03-02T23:04:08.237' AS DateTime), NULL, CAST(N'2021-04-07T11:04:07.393' AS DateTime), NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (16, 2, N'hardik', N'pandya', N'hp333@gmail.com', N'Hardik33@', 1, CAST(N'2021-03-02T23:22:01.487' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (17, 2, N'hardik', N'pandya', N'hp3333@gmail.com', N'Hardik33@', 1, CAST(N'2021-03-02T23:25:35.043' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (18, 2, N'hardik', N'pandya', N'hp1234@gmail.com', N'Hardik33@', 1, CAST(N'2021-03-02T23:27:57.870' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (19, 2, N'hardik', N'pandya', N'hp2@gmail.com', N'Hardik33@', 0, CAST(N'2021-03-02T23:30:49.980' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (20, 2, N'hardik', N'pandya', N'hp3@gmail.com', N'Hardik33@', 0, CAST(N'2021-03-02T23:37:19.793' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (21, 2, N'hardik', N'pandya', N'hp4@gmail.com', N'%&wlKGJH', 1, CAST(N'2021-03-02T23:45:32.187' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (22, 2, N'hardik', N'pandya', N'hp5@gmail.com', N'Hardik33@', 0, CAST(N'2021-03-03T00:03:53.690' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (23, 2, N'hardik', N'pandya', N'hp6@gmail.com', N'Hardik33@', 0, CAST(N'2021-03-03T00:08:57.897' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (27, 2, N'cheteshwar', N'pujara', N'puji@gmail.com', N'Puji@123', 0, CAST(N'2021-03-17T21:07:16.267' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (28, 2, N'cheteshwar', N'pujara', N'puji1@gmail.com', N'Puji@123', 0, CAST(N'2021-03-17T21:10:40.847' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (29, 2, N'cheteshwar', N'pujara', N'puji2@gmail.com', N'BGi1sqtX', 0, CAST(N'2021-03-17T21:25:41.030' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (30, 2, N'cheteshwar', N'pujara', N'puji3@gmail.com', N'P6mQuRbz', 1, CAST(N'2021-03-17T21:36:29.007' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (31, 2, N'cheteshwar', N'pujara', N'puji4@gmail.com', N'MHICRsyc', 1, CAST(N'2021-03-17T22:13:50.727' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (32, 2, N'cheteshwar', N'pujara', N'puji5@gmail.com', N'6*)YC7aq', 1, CAST(N'2021-03-17T22:15:51.847' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (33, 2, N'cheteshwar', N'pujara', N'puji6@gmail.com', N'Puji@123', 1, CAST(N'2021-03-17T22:28:38.060' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (34, 3, N'cheteshwarbhai', N'pujaracsk', N'puji77@gmail.com', N'Puji@123', 1, CAST(N'2021-03-17T22:40:27.887' AS DateTime), NULL, CAST(N'2021-04-06T02:58:16.567' AS DateTime), NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (35, 2, N'cheteshwar', N'pipaliya', N'mail1@gmail.com', N'Puji@123', 0, CAST(N'2021-03-17T22:53:43.067' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (36, 2, N'cheteshwar', N'pujara', N'puji8@gmail.com', N'Puji@123', 0, CAST(N'2021-03-17T23:05:35.890' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (37, 2, N'cheteshwar', N'pujara', N'email2@gmail.com', N'Puji@123', 0, CAST(N'2021-03-17T23:26:40.423' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (38, 2, N'cheteshwar', N'pujara', N'email3@gmail.com', N'Puji@123', 0, CAST(N'2021-03-17T23:59:48.260' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (39, 2, N'cheteshwar', N'pujara', N'puji12@gmail.com', N'Puji@123', 0, CAST(N'2021-03-18T15:49:33.427' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (40, 2, N'cheteshwar', N'pujara', N'puji13@gmail.com', N'Puji@123', 0, CAST(N'2021-03-18T17:49:49.243' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (41, 2, N'cheteshwar', N'pujara', N'puji14@gmail.com', N'Puji@123', 0, CAST(N'2021-03-18T17:51:39.267' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (42, 2, N'rutvik1', N'pipaliya', N'rutvikpipaliya1@gmail.com', N'dX(!KrIQ', 0, CAST(N'2021-03-23T18:37:31.167' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1039, 3, N'virat', N'kohli', N'virat18@gmail.com', N'Admin@123', 1, CAST(N'2021-04-07T11:04:44.287' AS DateTime), 4, CAST(N'2021-04-07T11:05:05.537' AS DateTime), NULL, 0)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1040, 2, N'Mumbai', N'Indians', N'mumbai@gmail.com', N'Mumbai@123', 0, CAST(N'2021-04-07T22:44:55.247' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1041, 2, N'demo', N'demo', N'demo@gmail.com', N'Demo@123', 0, CAST(N'2021-04-07T23:07:28.040' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1042, 2, N'royal', N'banglore', N'rcb@gmail.com', N'Rcb@1234', 1, CAST(N'2021-04-07T23:18:59.027' AS DateTime), NULL, CAST(N'2021-04-07T23:20:54.253' AS DateTime), 1042, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1043, 2, N'kolkata', N'riders', N'kkr@gmail.com', N'Kkr@1234', 1, CAST(N'2021-04-08T15:42:11.170' AS DateTime), NULL, CAST(N'2021-04-08T15:43:18.360' AS DateTime), 1043, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1044, 2, N'delhi', N'capitals', N'delhi@gmail.com', N'Delhi@777', 1, CAST(N'2021-04-08T15:44:52.657' AS DateTime), NULL, CAST(N'2021-04-08T15:45:31.320' AS DateTime), 1044, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1046, 4, N'Rutvik', N'Pipaliya', N'rutvikpipaliya@gmail.com', N'Master@77', 1, CAST(N'2021-04-08T17:54:48.980' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1047, 2, N'royals', N'rajs', N'royal@gmail.com', N'Royal@123', 0, CAST(N'2021-04-08T18:37:20.857' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1048, 2, N'royals', N'rajs', N'royals@gmail.com', N'Royal@123', 0, CAST(N'2021-04-08T18:38:12.233' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1049, 2, N'royals', N'rajs', N'chavla@gmail.com', N'Chavla@123', 0, CAST(N'2021-04-08T18:39:11.480' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1050, 2, N'arcahr', N'archar', N'archar@gmail.com', N'Archar@123', 0, CAST(N'2021-04-08T18:46:46.207' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1051, 2, N'jofra', N'jofra', N'jofra@gmail.com', N'Jofra@1123', 0, CAST(N'2021-04-08T18:54:19.087' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1052, 2, N'jofra', N'jofra', N'jofra1@gmail.com', N'Jfra@123', 0, CAST(N'2021-04-08T19:01:46.870' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1053, 2, N'parag', N'parag', N'parag@gmail.com', N'Parag@123', 0, CAST(N'2021-04-08T21:30:42.600' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1054, 3, N'india', N'india', N'india@gmail.com', N'Admin@123', 1, CAST(N'2021-04-08T21:42:14.277' AS DateTime), 4, CAST(N'2021-04-08T22:09:27.357' AS DateTime), NULL, 0)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1055, 3, N'hindistan', N'hindustan', N'hindustan@gmail.com', N'Hindu@123', 1, CAST(N'2021-04-08T21:55:59.640' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1056, 3, N'arbiarvbiy', N'arbi', N'arbi@gmail.com', N'Admin@123', 1, CAST(N'2021-04-08T22:09:56.130' AS DateTime), 4, CAST(N'2021-04-09T00:10:23.773' AS DateTime), NULL, 0)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1057, 2, N'buttler', N'buttler', N'butt@gmail.com', N'Butt@123', 0, CAST(N'2021-04-09T01:02:50.007' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2057, 2, N'pollard', N'pollard', N'poly@gmail.com', N'Poly@1234', 0, CAST(N'2021-04-09T09:30:34.197' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3057, 2, N'ashwin', N'ravi', N'ashwin@gmail.com', N'Ash@1234', 0, CAST(N'2021-04-09T12:00:44.777' AS DateTime), NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3058, 2, N'Rutvik', N'Pipaliya', N'rutvikpipaliya33@gmail.com', N'Rutvik@123', 1, CAST(N'2021-04-09T15:03:14.233' AS DateTime), 3058, CAST(N'2021-04-09T15:04:19.693' AS DateTime), 3058, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3059, 2, N'Virat', N'kohli', N'viratkohli18@gmail.com', N'Virat@123', 1, CAST(N'2021-04-09T15:20:03.707' AS DateTime), 3059, CAST(N'2021-04-09T15:20:23.330' AS DateTime), 3059, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3060, 3, N'Bcci', N'icc', N'bcci@gmail.com', N'Bcci@123', 1, CAST(N'2021-04-09T15:33:37.673' AS DateTime), 4, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3061, 2, N'bhaji', N'bhaji', N'bhaji@gmail.com', N'Bhaji@123', 1, CAST(N'2021-04-09T16:52:16.253' AS DateTime), 3061, CAST(N'2021-04-09T16:52:55.840' AS DateTime), 3061, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [Email], [Password], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3062, 3, N'icc', N'icc', N'icc@gmail.com', N'Admin@123', 1, CAST(N'2021-04-09T17:01:15.233' AS DateTime), 4, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Countries_code]    Script Date: 11-04-2021 11:29:50 ******/
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [UQ_Countries_code] UNIQUE NONCLUSTERED 
(
	[CountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Countries_name]    Script Date: 11-04-2021 11:29:50 ******/
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [UQ_Countries_name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_NoteTypes_name]    Script Date: 11-04-2021 11:29:50 ******/
ALTER TABLE [dbo].[NoteTypes] ADD  CONSTRAINT [UQ_NoteTypes_name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534B1DB28C8]    Script Date: 11-04-2021 11:29:50 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [DF_Countries_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Downloads] ADD  CONSTRAINT [DF__Downloads__IsAct__18EBB532]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[NoteCategories] ADD  CONSTRAINT [DF_NoteCategories_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[NoteTypes] ADD  CONSTRAINT [DF_NoteTypes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ReferenceData] ADD  CONSTRAINT [DF_ReferenceData_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SellerNotes] ADD  CONSTRAINT [DF_SellerNotes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SellerNotesAttachements] ADD  CONSTRAINT [DF_SellerNotesAttachements_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SellerNotesReviews] ADD  CONSTRAINT [DF_SellerNotesReviews_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SystemConfigurations] ADD  CONSTRAINT [DF_SystemConfigurations_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [DF_UserRoles_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsEmailVerified]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK__Downloads__Downl__1332DBDC] FOREIGN KEY([Downloader])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK__Downloads__Downl__1332DBDC]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK__Downloads__NoteI__114A936A] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellerNotes] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK__Downloads__NoteI__114A936A]
GO
ALTER TABLE [dbo].[Downloads]  WITH CHECK ADD  CONSTRAINT [FK__Downloads__Selle__123EB7A3] FOREIGN KEY([Seller])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Downloads] CHECK CONSTRAINT [FK__Downloads__Selle__123EB7A3]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__Actio__778AC167] FOREIGN KEY([ActionedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK__SellerNot__Actio__778AC167]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__Categ__787EE5A0] FOREIGN KEY([Category])
REFERENCES [dbo].[NoteCategories] ([ID])
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK__SellerNot__Categ__787EE5A0]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__Count__7D439ABD] FOREIGN KEY([Country])
REFERENCES [dbo].[Countries] ([ID])
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK__SellerNot__Count__7D439ABD]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__NoteT__7C4F7684] FOREIGN KEY([NoteType])
REFERENCES [dbo].[NoteTypes] ([ID])
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK__SellerNot__NoteT__7C4F7684]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__Selle__75A278F5] FOREIGN KEY([SellerID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK__SellerNot__Selle__75A278F5]
GO
ALTER TABLE [dbo].[SellerNotes]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__Statu__76969D2E] FOREIGN KEY([Status])
REFERENCES [dbo].[ReferenceData] ([ID])
GO
ALTER TABLE [dbo].[SellerNotes] CHECK CONSTRAINT [FK__SellerNot__Statu__76969D2E]
GO
ALTER TABLE [dbo].[SellerNotesAttachements]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__NoteI__04E4BC85] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellerNotes] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesAttachements] CHECK CONSTRAINT [FK__SellerNot__NoteI__04E4BC85]
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__Again__0E6E26BF] FOREIGN KEY([AgainstDownloadID])
REFERENCES [dbo].[Downloads] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues] CHECK CONSTRAINT [FK__SellerNot__Again__0E6E26BF]
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__NoteI__0C85DE4D] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellerNotes] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues] CHECK CONSTRAINT [FK__SellerNot__NoteI__0C85DE4D]
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__Repor__0D7A0286] FOREIGN KEY([ReportedByID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues] CHECK CONSTRAINT [FK__SellerNot__Repor__0D7A0286]
GO
ALTER TABLE [dbo].[SellerNotesReviews]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__Again__09A971A2] FOREIGN KEY([AgainstDownloadsID])
REFERENCES [dbo].[Downloads] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReviews] CHECK CONSTRAINT [FK__SellerNot__Again__09A971A2]
GO
ALTER TABLE [dbo].[SellerNotesReviews]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__NoteI__07C12930] FOREIGN KEY([NoteID])
REFERENCES [dbo].[SellerNotes] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReviews] CHECK CONSTRAINT [FK__SellerNot__NoteI__07C12930]
GO
ALTER TABLE [dbo].[SellerNotesReviews]  WITH CHECK ADD  CONSTRAINT [FK__SellerNot__Revie__08B54D69] FOREIGN KEY([ReviewedByID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReviews] CHECK CONSTRAINT [FK__SellerNot__Revie__08B54D69]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK__UserProfi__Gende__6EF57B66] FOREIGN KEY([Gender])
REFERENCES [dbo].[ReferenceData] ([ID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK__UserProfi__Gende__6EF57B66]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK__UserProfi__UserI__6E01572D] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK__UserProfi__UserI__6E01572D]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__RoleID__656C112C] FOREIGN KEY([RoleID])
REFERENCES [dbo].[UserRoles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__RoleID__656C112C]
GO
USE [master]
GO
ALTER DATABASE [NotesMarketPlace] SET  READ_WRITE 
GO
