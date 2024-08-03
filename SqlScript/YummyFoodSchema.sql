USE [master]
GO
/****** Object:  Database [YummyFood]    Script Date: 6/18/2024 6:39:32 AM ******/
CREATE DATABASE [YummyFood]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'YummyFood', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\YummyFood.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'YummyFood_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\YummyFood_log.ldf' , SIZE = 3153920KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [YummyFood] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [YummyFood].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [YummyFood] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [YummyFood] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [YummyFood] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [YummyFood] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [YummyFood] SET ARITHABORT OFF 
GO
ALTER DATABASE [YummyFood] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [YummyFood] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [YummyFood] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [YummyFood] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [YummyFood] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [YummyFood] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [YummyFood] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [YummyFood] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [YummyFood] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [YummyFood] SET  ENABLE_BROKER 
GO
ALTER DATABASE [YummyFood] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [YummyFood] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [YummyFood] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [YummyFood] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [YummyFood] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [YummyFood] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [YummyFood] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [YummyFood] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [YummyFood] SET  MULTI_USER 
GO
ALTER DATABASE [YummyFood] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [YummyFood] SET DB_CHAINING OFF 
GO
ALTER DATABASE [YummyFood] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [YummyFood] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [YummyFood] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [YummyFood] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [YummyFood] SET QUERY_STORE = ON
GO
ALTER DATABASE [YummyFood] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [YummyFood]
GO
/****** Object:  Table [dbo].[CartItems]    Script Date: 6/18/2024 6:39:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CartId] [uniqueidentifier] NOT NULL,
	[ItemId] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 6/18/2024 6:39:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedDate] [datetime2](7) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImageDetail]    Script Date: 6/18/2024 6:39:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageName] [varchar](50) NULL,
	[ImageUrl] [varchar](150) NULL,
	[ImagePath] [varchar](100) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Guid] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 6/18/2024 6:39:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[Total] [decimal](18, 2) NULL,
	[OrderId] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 6/18/2024 6:39:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [nvarchar](100) NOT NULL,
	[PaymentId] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](300) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](20) NULL,
	[PhoneNumber] [nvarchar](100) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentDetails]    Script Date: 6/18/2024 6:39:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentDetails](
	[Id] [nvarchar](100) NOT NULL,
	[TransactionId] [nvarchar](100) NULL,
	[Tax] [decimal](18, 2) NULL,
	[Currency] [nvarchar](20) NULL,
	[Total] [decimal](18, 2) NULL,
	[Email] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[CartId] [uniqueidentifier] NULL,
	[GrandTotal] [decimal](18, 2) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 6/18/2024 6:39:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageUrl] [nvarchar](150) NULL,
	[Enabled] [bit] NULL,
	[Deleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[ProductName] [varchar](50) NULL,
	[ProductCode] [int] NULL,
	[UnitPrice] [decimal](18, 0) NULL,
	[ImageId] [int] NULL,
	[ImagePath] [nvarchar](150) NULL,
	[Description] [nvarchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SignInLogs]    Script Date: 6/18/2024 6:39:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SignInLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[IPAddress] [varchar](150) NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[BrowserInfo] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/18/2024 6:39:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Email] [nvarchar](150) NULL,
	[Enabled] [bit] NULL,
	[Deleted] [bit] NULL,
	[PhoneNumber] [bigint] NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[Password] [nvarchar](100) NULL,
	[SessionId] [int] NULL,
	[LoginDate] [datetime] NULL,
	[DateOfBirth] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_Carts_CartId] FOREIGN KEY([CartId])
REFERENCES [dbo].[Carts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_Carts_CartId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PaymentDetails]  WITH CHECK ADD  CONSTRAINT [FK_PaymentDetails_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[PaymentDetails] CHECK CONSTRAINT [FK_PaymentDetails_Users_UserId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ImageDetail] FOREIGN KEY([ImageId])
REFERENCES [dbo].[ImageDetail] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ImageDetail]
GO
ALTER TABLE [dbo].[SignInLogs]  WITH CHECK ADD  CONSTRAINT [FK_User_SignInLog] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[SignInLogs] CHECK CONSTRAINT [FK_User_SignInLog]
GO
USE [master]
GO
ALTER DATABASE [YummyFood] SET  READ_WRITE 
GO
