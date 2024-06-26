USE [master]
GO
/****** Object:  Database [MyShop]    Script Date: 4/4/2024 8:04:44 PM ******/
CREATE DATABASE [MyShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MyShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\MyShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MyShop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MyShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MyShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyShop] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MyShop] SET  MULTI_USER 
GO
ALTER DATABASE [MyShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [MyShop] SET QUERY_STORE = ON
GO
ALTER DATABASE [MyShop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MyShop]
GO
/****** Object:  User [admin]    Script Date: 4/4/2024 8:04:45 PM ******/
CREATE USER [admin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[category]    Script Date: 4/4/2024 8:04:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[CatID] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](50) NULL,
	[CatIcon] [nvarchar](50) NULL,
	[CatDescription] [nvarchar](max) NULL,
 CONSTRAINT [PK_category] PRIMARY KEY CLUSTERED 
(
	[CatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 4/4/2024 8:04:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[CusID] [int] IDENTITY(1,1) NOT NULL,
	[CusName] [nvarchar](50) NULL,
	[CusTel] [nvarchar](50) NULL,
	[CusAddress] [nvarchar](100) NULL,
	[CusEmail] [nvarchar](50) NULL,
 CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED 
(
	[CusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 4/4/2024 8:04:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[ProID] [int] IDENTITY(1,1) NOT NULL,
	[ProName] [nvarchar](150) NULL,
	[Des] [nvarchar](300) NULL,
	[Price] [money] NULL,
	[ImagePath] [text] NULL,
	[CatID] [int] NULL,
	[Quantity] [int] NULL,
	[Block] [int] NULL,
	[PromoID] [int] NULL,
	[PromotionPrice] [money] NULL,
 CONSTRAINT [PK_product] PRIMARY KEY CLUSTERED 
(
	[ProID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[promotion]    Script Date: 4/4/2024 8:04:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[promotion](
	[IdPromo] [int] IDENTITY(1,1) NOT NULL,
	[PromoCode] [nvarchar](50) NULL,
	[DiscountPercent] [int] NULL,
 CONSTRAINT [pk_promo] PRIMARY KEY CLUSTERED 
(
	[IdPromo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[purchase]    Script Date: 4/4/2024 8:04:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[purchase](
	[PurchaseID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[ProID] [int] NULL,
	[Quantity] [int] NULL,
	[TotalPrice] [money] NULL,
 CONSTRAINT [PK_purchase] PRIMARY KEY CLUSTERED 
(
	[PurchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shopOrder]    Script Date: 4/4/2024 8:04:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shopOrder](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CusID] [int] NULL,
	[CreateAt] [date] NULL,
	[FinalTotal] [money] NULL,
	[ProfitTotal] [money] NULL,
 CONSTRAINT [PK_shop_order] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 4/4/2024 8:04:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Fullname] [text] NULL,
 CONSTRAINT [pk_user] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (1, N'DevOps', N'Infinity', N'DevOps is the combination of cultural philosophies.')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (2, N'Python', N'Python', N'Python is a high-level programming language.')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (3, N'Javascript', N'Js', N'JavaScript allows you to implement complex features on web pages.')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (4, N'Java', N'Java', N'Java can be used as a platform in itself.')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (5, N'SQL', N'Database', N'SQL is a standard language for databases.')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (6, N'Cyber Security', N'UserSecret', N'Cyber security is the practice of defending.')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (7, N'Big Data', N'CloudArrowUp', N'Big data organizations collect')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (8, N'Coffee Mug', N'MugSaucer', N'Sth make coder better ;))')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (12, N'Angular', N'Js', N'Component based JS framework.')
INSERT [dbo].[category] ([CatID], [CatName], [CatIcon], [CatDescription]) VALUES (14, N'Spring boot ', N'Java', N'Strong Java backend framework')
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[customer] ON 

INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (1, N'Dương Phước Sang', N'0842969898', N'District 1', N'sang@gmail.com')
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (2, N'Malo Gusto', N'0842969898', N'District 2', N'malo@gmail.com')
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (3, N'Enzo Feznadez', N'0842969898', N'District 3', N'enzo@gmail.com')
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (4, N'Ben Chillwell', N'0842969898', N'District 7', N'ben@gmail.com')
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (5, N'Olivier Giroud', N'0842969898', N'District 10', N'olivier@gmail.com')
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (11, N'Pulisic', N'0987654321', N'holding', N'Pulisic@example.com')
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (12, N'Yamal Musiala', N'0912345678', N'holding', N'Yamal@example.com')
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (13, N'Jao pedro', N'0901234567', N'holding', N'pedro@example.com')
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (14, N'David Muasalito', N'0976543210', N'holding', N'Muasalito@example.com')
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (15, N'Kalifina', N'0965432109', N'holding', N'Kalifina@example.com')
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (16, N'Frankia Dejong', N'004233284422', N'Mars', NULL)
INSERT [dbo].[customer] ([CusID], [CusName], [CusTel], [CusAddress], [CusEmail]) VALUES (18, N'Dimitri Payet', N'09584412365', N'Francisco', NULL)
SET IDENTITY_INSERT [dbo].[customer] OFF
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (1, N'Crash Course in Python', N'Learn Python at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 14.9900, N'Assets/Images/books/book-luv2code-1000.png', 2, 17, 0, 4, 2.9980)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (2, N'Become a Guru in JavaScript', N'Learn JavaScript at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 20.9900, N'Assets/Images/books/book-luv2code-1002.png', 7, 1000, 0, 1, 19.9405)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (3, N'Exploring Vue', N'Learn Vue.js at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 14.9900, N'Assets/Images/books/book-luv2code-1019.png', 3, 100, 0, 1, 14.2405)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (4, N'Advanced Techniques in Big Data', N'Learn Big Data at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 13.9900, N'Assets/Images/books/book-luv2code-1010.png', 7, 100, 0, 4, 2.7980)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (5, N'Crash Course in Big Data Update', N'Learn Big Data at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today! Updated', 18.9900, N'Assets/Images/books/book-luv2code-1017.png', 5, 123, 0, 1, 18.0405)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (6, N'JavaScript Cookbook', N'Learn JavaScript at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 23.9900, N'Assets/Images/books/book-luv2code-1009.png', 1, 10, 0, 4, 4.7980)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (7, N'Beginners Guide to SQL', N'Learn SQL at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 14.9900, N'Assets/Images/books/book-luv2code-1006.png', 5, 98, 0, 4, 23.9940)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (8, N'Advanced Techniques in JavaScript', N'Learn JavaScript at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 16.9900, N'Assets/Images/books/book-luv2code-1007.png', 3, 88, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (9, N'Introduction to Spring Boot', N'Learn Spring Boot at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 25.9900, N'Assets/Images/books/book-luv2code-1008.png', 4, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (10, N'Become a Guru in React.js', N'Learn React.js at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 23.9900, N'Assets/Images/books/book-luv2code-1009.png', 3, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (11, N'Beginners Guide to Data Science', N'Learn Data Science at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 24.9900, N'Assets/Images/books/book-luv2code-1010.png', 7, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (12, N'Advanced Techniques in Java', N'Learn Java at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 19.9900, N'Assets/Images/books/book-luv2code-1011.png', 4, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (13, N'Exploring DevOps', N'Learn DevOps at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 24.9900, N'Assets/Images/books/book-luv2code-1012.png', 1, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (14, N'The Expert Guide to SQL', N'Learn SQL at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 19.9900, N'Assets/Images/books/book-luv2code-1013.png', 5, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (15, N'Introduction to SQL', N'Learn SQL at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 22.9900, N'Assets/Images/books/book-luv2code-1014.png', 5, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (16, N'The Expert Guide to JavaScript', N'Learn JavaScript at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 22.9900, N'Assets/Images/books/book-luv2code-1015.png', 3, 100, 1, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (17, N'Exploring React.js', N'Learn React.js at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 27.9900, N'Assets/Images/books/book-luv2code-1016.png', 3, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (18, N'Advanced Techniques in React.js', N'Learn React.js at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 13.9900, N'Assets/Images/books/book-luv2code-1017.png', 3, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (19, N'Introduction to C#', N'Learn C# at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 26.9900, N'Assets/Images/books/book-luv2code-1018.png', 6, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (20, N'Crash Course in JavaScript', N'Learn JavaScript at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 13.9900, N'Assets/Images/books/book-luv2code-1019.png', 3, 90, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (21, N'Introduction to Machine Learning', N'Learn Machine Learning at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 19.9900, N'Assets/Images/books/book-luv2code-1020.png', 1, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (22, N'Become a Guru in Java', N'Learn Java at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 18.9900, N'Assets/Images/books/book-luv2code-1021.png', 4, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (23, N'Introduction to Python', N'Learn Python at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 26.9900, N'Assets/Images/books/book-luv2code-1022.png', 2, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (24, N'Advanced Techniques in C#', N'Learn C# at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 22.9900, N'Assets/Images/books/book-luv2code-1023.png', 6, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (25, N'The Expert Guide to Machine Learning', N'Learn Machine Learning at your own pace. The author explains how the technology works in easy-to-understand language. This book includes working examples that you can apply to your own projects. Purchase the book and get started today!', 16.9900, N'Assets/Images/books/book-luv2code-1024.png', 1, 100, 0, 4, 23.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (29, N'Coffee Mug - Express', N'Do you love mathematics? If so, then you need this elegant coffee mug with an amazing fractal design. You don\''t have to worry about boring coffee mugs anymore. This coffee mug will be the topic of conversation in the office, guaranteed! Buy it now!', 18.9900, N'Assets/Images/books/coffeemug-luv2code-1000.png', 8, 100, 0, 4, 18.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (30, N'Coffee Mug - Cherokee', N'Do you love mathematics? If so, then you need this elegant coffee mug with an amazing fractal design. You don\''t have to worry about boring coffee mugs anymore. This coffee mug will be the topic of conversation in the office, guaranteed! Buy it now!', 18.9900, N'Assets/Images/books/coffeemug-luv2code-1001.png', 8, 100, 0, 4, 18.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (31, N'Coffee Mug - Sweeper', N'Do you love mathematics? If so, then you need this elegant coffee mug with an amazing fractal design. You don\''t have to worry about boring coffee mugs anymore. This coffee mug will be the topic of conversation in the office, guaranteed! Buy it now!', 18.9900, N'Assets/Images/books/coffeemug-luv2code-1002.png', 8, 100, 0, 4, 18.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (32, N'Coffee Mug - Aspire', N'Do you love mathematics? If so, then you need this elegant coffee mug with an amazing fractal design. You don\''t have to worry about boring coffee mugs anymore. This coffee mug will be the topic of conversation in the office, guaranteed! Buy it now!', 18.9900, N'Assets/Images/books/coffeemug-luv2code-1003.png', 8, 100, 0, 4, 18.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (33, N'Coffee Mug - Dorian', N'Do you love mathematics? If so, then you need this elegant coffee mug with an amazing fractal design. You don\''t have to worry about boring coffee mugs anymore. This coffee mug will be the topic of conversation in the office, guaranteed! Buy it now!', 18.9900, N'Assets/Images/books/coffeemug-luv2code-1004.png', 8, 100, 0, 4, 18.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (34, N'Coffee Mug - Columbia', N'Do you love mathematics? If so, then you need this elegant coffee mug with an amazing fractal design. You don\''t have to worry about boring coffee mugs anymore. This coffee mug will be the topic of conversation in the office, guaranteed! Buy it now!', 18.9900, N'Assets/Images/books/coffeemug-luv2code-1005.png', 8, 100, 0, 4, 18.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (35, N'Coffee Mug - Worthing', N'Do you love mathematics? If so, then you need this elegant coffee mug with an amazing fractal design. You don\''t have to worry about boring coffee mugs anymore. This coffee mug will be the topic of conversation in the office, guaranteed! Buy it now!', 18.9900, N'Assets/Images/books/coffeemug-luv2code-1006.png', 8, 100, 0, 4, 18.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (36, N'Coffee Mug - Oak Cliff', N'Do you love mathematics? If so, then you need this elegant coffee mug with an amazing fractal design. You don\''t have to worry about boring coffee mugs anymore. This coffee mug will be the topic of conversation in the office, guaranteed! Buy it now!', 18.9900, N'Assets/Images/books/coffeemug-luv2code-1007.png', 8, 100, 0, 4, 18.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (64, N'hello', N'sad', 12.0000, N'Assets/Images/books/book-luv2code-1000.png', 7, 12, 1, NULL, 12.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (65, N'Daklak coffee', N'Imprtant thing for coder', 5.9900, N'Assets/Images/books/coffeemug-luv2code-1020.png', 8, 12, 1, NULL, 5.9900)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (66, N'Intro to NodeJS', N'Master nodejs server ', 20.0000, N'Assets/Images/books/book-luv2code-1010.png', 3, 88, 0, 3, 10.0000)
INSERT [dbo].[product] ([ProID], [ProName], [Des], [Price], [ImagePath], [CatID], [Quantity], [Block], [PromoID], [PromotionPrice]) VALUES (77, N'New Items', N'Testing', 12.0000, N'Assets/Images/books/book-luv2code-1007.png', 1, 123, 0, NULL, 12.0000)
SET IDENTITY_INSERT [dbo].[product] OFF
GO
SET IDENTITY_INSERT [dbo].[promotion] ON 

INSERT [dbo].[promotion] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (1, N'5% discount', 5)
INSERT [dbo].[promotion] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (2, N'25% discount', 25)
INSERT [dbo].[promotion] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (3, N'50% discount', 50)
INSERT [dbo].[promotion] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (4, N'80% discount', 80)
INSERT [dbo].[promotion] ([IdPromo], [PromoCode], [DiscountPercent]) VALUES (5, N'No discount', 0)
SET IDENTITY_INSERT [dbo].[promotion] OFF
GO
SET IDENTITY_INSERT [dbo].[purchase] ON 

INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (1, 6, 1, 3, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (2, 6, 2, 6, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (3, 6, 1, 2, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (4, 6, 1, 2, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (5, 7, 1, 6, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (6, 7, 2, 5, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (7, 7, 4, 2, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (8, 7, 5, 2, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (9, 7, 6, 2, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (10, 7, 7, 3, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (11, 8, 8, 1, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (12, 8, 3, 2, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (13, 8, 5, 4, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (14, 9, 6, 1, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (15, 9, 6, 3, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (16, 9, 7, 2, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (17, 9, 8, 3, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (18, 9, 2, 2, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (19, 9, 4, 2, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (20, 6, 5, 2, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (21, 6, 9, 3, 20.0000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (26, 36, 8, 12, 295.3200)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (27, 36, 20, 10, 246.1000)
INSERT [dbo].[purchase] ([PurchaseID], [OrderID], [ProID], [Quantity], [TotalPrice]) VALUES (28, 36, 66, 10, 107.0000)
SET IDENTITY_INSERT [dbo].[purchase] OFF
GO
SET IDENTITY_INSERT [dbo].[shopOrder] ON 

INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (1, 1, CAST(N'2024-04-01' AS Date), 15.0000, 5.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (2, 3, CAST(N'2024-04-02' AS Date), 20.0000, 5.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (3, 1, CAST(N'2024-04-02' AS Date), 31.0000, 5.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (4, 1, CAST(N'2024-04-03' AS Date), 32.0000, 10.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (5, 3, CAST(N'2024-04-04' AS Date), 33.0000, 20.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (6, 1, CAST(N'2024-04-05' AS Date), 32.0000, 25.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (7, 1, CAST(N'2024-04-02' AS Date), 100.0000, 2.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (8, 3, CAST(N'2024-04-02' AS Date), 100.0000, 3.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (9, 1, CAST(N'2024-04-02' AS Date), 100.0000, 56.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (10, 1, CAST(N'2024-04-02' AS Date), 100.0000, 45.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (11, 3, CAST(N'2024-04-01' AS Date), 100.0000, 67.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (12, 1, CAST(N'2024-04-01' AS Date), 100.0000, 43.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (13, 1, CAST(N'2024-04-01' AS Date), 100.0000, 56.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (14, 3, CAST(N'2024-04-01' AS Date), 100.0000, 21.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (15, 1, CAST(N'2024-04-01' AS Date), 100.0000, 34.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (16, 1, CAST(N'2024-04-01' AS Date), 100.0000, 67.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (17, 3, CAST(N'2024-04-02' AS Date), 100.0000, 34.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (18, 1, CAST(N'2024-04-02' AS Date), 100.0000, 21.0000)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (19, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (20, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (21, 14, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (22, 14, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (23, 14, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (24, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (25, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (26, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (27, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (28, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (29, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (30, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (31, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (32, 1, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (33, 16, CAST(N'2024-04-04' AS Date), NULL, NULL)
INSERT [dbo].[shopOrder] ([OrderID], [CusID], [CreateAt], [FinalTotal], [ProfitTotal]) VALUES (36, 18, CAST(N'2024-04-04' AS Date), 648.4200, 42.4200)
SET IDENTITY_INSERT [dbo].[shopOrder] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([UserID], [Username], [Password], [Fullname]) VALUES (1, N'sang', N'123', N'Duong Phuoc Sang')
INSERT [dbo].[user] ([UserID], [Username], [Password], [Fullname]) VALUES (2, N'malo', N'123', N'Malo Gusto')
INSERT [dbo].[user] ([UserID], [Username], [Password], [Fullname]) VALUES (3, N'enzo', N'123', N'Enzo Feznandez')
INSERT [dbo].[user] ([UserID], [Username], [Password], [Fullname]) VALUES (4, N'oliver', N'123', N'Oliver Giroud')
SET IDENTITY_INSERT [dbo].[user] OFF
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_category] FOREIGN KEY([CatID])
REFERENCES [dbo].[category] ([CatID])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_category]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [FK_product_promotion] FOREIGN KEY([PromoID])
REFERENCES [dbo].[promotion] ([IdPromo])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [FK_product_promotion]
GO
ALTER TABLE [dbo].[purchase]  WITH CHECK ADD  CONSTRAINT [FK_purchase_product] FOREIGN KEY([ProID])
REFERENCES [dbo].[product] ([ProID])
GO
ALTER TABLE [dbo].[purchase] CHECK CONSTRAINT [FK_purchase_product]
GO
ALTER TABLE [dbo].[purchase]  WITH CHECK ADD  CONSTRAINT [FK_purchase_shop_order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[shopOrder] ([OrderID])
GO
ALTER TABLE [dbo].[purchase] CHECK CONSTRAINT [FK_purchase_shop_order]
GO
ALTER TABLE [dbo].[shopOrder]  WITH CHECK ADD  CONSTRAINT [FK_shop_order_customer] FOREIGN KEY([CusID])
REFERENCES [dbo].[customer] ([CusID])
GO
ALTER TABLE [dbo].[shopOrder] CHECK CONSTRAINT [FK_shop_order_customer]
GO
USE [master]
GO
ALTER DATABASE [MyShop] SET  READ_WRITE 
GO
