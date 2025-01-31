USE [master]
GO
/****** Object:  Database [E_project]    Script Date: 1/3/2025 3:39:05 PM ******/
CREATE DATABASE [E_project]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'E_project', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\E_project.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'E_project_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\E_project_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [E_project] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [E_project].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [E_project] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [E_project] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [E_project] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [E_project] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [E_project] SET ARITHABORT OFF 
GO
ALTER DATABASE [E_project] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [E_project] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [E_project] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [E_project] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [E_project] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [E_project] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [E_project] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [E_project] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [E_project] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [E_project] SET  ENABLE_BROKER 
GO
ALTER DATABASE [E_project] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [E_project] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [E_project] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [E_project] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [E_project] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [E_project] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [E_project] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [E_project] SET RECOVERY FULL 
GO
ALTER DATABASE [E_project] SET  MULTI_USER 
GO
ALTER DATABASE [E_project] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [E_project] SET DB_CHAINING OFF 
GO
ALTER DATABASE [E_project] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [E_project] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [E_project] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [E_project] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'E_project', N'ON'
GO
ALTER DATABASE [E_project] SET QUERY_STORE = OFF
GO
USE [E_project]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[AdminId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[ProfileImage] [nvarchar](max) NULL,
	[Name] [nvarchar](255) NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmpRegisters]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmpRegisters](
	[EmpId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Salary] [decimal](18, 2) NULL,
	[Address] [nvarchar](max) NOT NULL,
	[ContactNo] [nvarchar](max) NOT NULL,
	[RoleId] [int] NOT NULL,
	[Status] [int] NULL,
	[PolicyId] [int] NULL,
	[Remaining_Amount] [decimal](18, 2) NULL,
	[PolicyDate] [datetime] NULL,
 CONSTRAINT [PK_EmpRegister] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinanceManagers]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinanceManagers](
	[FinanceManagerId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_FinanceManagers] PRIMARY KEY CLUSTERED 
(
	[FinanceManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hospitals]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hospitals](
	[HospitalId] [int] IDENTITY(1,1) NOT NULL,
	[HospitalName] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[ContactNumber] [nvarchar](20) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Hospitals] PRIMARY KEY CLUSTERED 
(
	[HospitalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medical_Invoice]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medical_Invoice](
	[MedicalId] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Desc] [nvarchar](255) NULL,
	[Emp_Name] [nvarchar](50) NULL,
	[Emp_Email] [nvarchar](255) NULL,
	[CreateDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Medical_Invoice] PRIMARY KEY CLUSTERED 
(
	[MedicalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentRequestId] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Per_Hospitals]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Per_Hospitals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TotalBeds] [int] NOT NULL,
	[ActiveBeds] [int] NOT NULL,
	[TotalDoctors] [int] NULL,
	[TotalNurses] [int] NULL,
 CONSTRAINT [PK_Per_Hospitals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Policies]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Policies](
	[PolicyId] [int] IDENTITY(1,1) NOT NULL,
	[PolicyName] [nvarchar](150) NULL,
	[PolicyDesc] [nvarchar](255) NULL,
	[PolicyAmount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED 
(
	[PolicyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TreatmentRequests]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentRequests](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[HospitalId] [int] NOT NULL,
	[FinanceManagerId] [int] NOT NULL,
	[TreatmentDetails] [nvarchar](500) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[RequestDate] [datetime2](7) NOT NULL,
	[ApprovalDate] [datetime2](7) NOT NULL,
	[EmpId] [int] NOT NULL,
	[Emp_Email] [varchar](255) NULL,
 CONSTRAINT [PK_TreatmentRequests] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VisitRequest]    Script Date: 1/3/2025 3:39:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitRequest](
	[VisitRequestId] [int] IDENTITY(1,1) NOT NULL,
	[HospitalId] [int] NOT NULL,
	[Reason] [nvarchar](255) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[RequestedAt] [datetime2](7) NOT NULL,
	[ApprovedAt] [datetime2](7) NULL,
	[FinanceManagerId] [int] NOT NULL,
	[EmpId] [int] NOT NULL,
	[Emp_CNIC] [int] NULL,
	[Emp_Email] [varchar](255) NULL
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'202408281234_AddEmpCnicColumn', N'7.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241207173904_mymigration', N'7.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241221095026_AddVisitRequestIdColumn', N'7.0.0')
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([AdminId], [Email], [Password], [ProfileImage], [Name]) VALUES (1, N'wassisheikh448@gmail.com', N'12345678910', N'dashboard-profile.jpg', N'Waqas')
INSERT [dbo].[Admin] ([AdminId], [Email], [Password], [ProfileImage], [Name]) VALUES (2, N'aqsajamil379@gmail.com', N'121212', N'dashboard-profile2.jpg', N'Aqsa')
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[EmpRegisters] ON 

INSERT [dbo].[EmpRegisters] ([EmpId], [Name], [Email], [Password], [Salary], [Address], [ContactNo], [RoleId], [Status], [PolicyId], [Remaining_Amount], [PolicyDate]) VALUES (1043, N'Lacy Carey', N'carey@gmail.com', N'AQAAAAIAAYagAAAAEE2BOdRXm4dFgR+MUJmxwfoKrgssmg5RW818pkk6sH0kXeRtdmaz/1/lIwBln5VYYA==', CAST(40000.00 AS Decimal(18, 2)), N'Nihil laboris est v', N'+1 (664) 522-2372', 1, 1, 2, CAST(20000.00 AS Decimal(18, 2)), CAST(N'2024-01-01T15:19:17.603' AS DateTime))
SET IDENTITY_INSERT [dbo].[EmpRegisters] OFF
GO
SET IDENTITY_INSERT [dbo].[FinanceManagers] ON 

INSERT [dbo].[FinanceManagers] ([FinanceManagerId], [FullName], [Email]) VALUES (1, N'Jane Smith', N'aqsajamil973@gmail.com')
SET IDENTITY_INSERT [dbo].[FinanceManagers] OFF
GO
SET IDENTITY_INSERT [dbo].[Hospitals] ON 

INSERT [dbo].[Hospitals] ([HospitalId], [HospitalName], [Address], [ContactNumber], [CreatedAt], [UpdatedAt], [Email]) VALUES (3, N'City Hospital', N'123 Main Street, City', N'123-456-7890', CAST(N'2024-12-21T04:42:15.1000000' AS DateTime2), CAST(N'2024-12-21T04:42:15.1000000' AS DateTime2), N'info@cityhospital.com')
SET IDENTITY_INSERT [dbo].[Hospitals] OFF
GO
SET IDENTITY_INSERT [dbo].[Medical_Invoice] ON 

INSERT [dbo].[Medical_Invoice] ([MedicalId], [EmpId], [RoleId], [TotalAmount], [Desc], [Emp_Name], [Emp_Email], [CreateDate]) VALUES (35, 1, 1, CAST(3000.00 AS Decimal(18, 2)), N'gvhcfn', N'Carey', N'carey@gmail.com', CAST(N'2025-03-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Medical_Invoice] ([MedicalId], [EmpId], [RoleId], [TotalAmount], [Desc], [Emp_Name], [Emp_Email], [CreateDate]) VALUES (36, 1043, 1, CAST(3000.00 AS Decimal(18, 2)), N'dfxgdtrfxgt', N'Carey', N'carey@gmail.com', CAST(N'2025-01-03T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Medical_Invoice] OFF
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON 

INSERT [dbo].[Notifications] ([NotificationId], [TreatmentRequestId], [Message], [CreatedAt]) VALUES (29, 1041, N'ghfygh', CAST(N'2025-03-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Notifications] ([NotificationId], [TreatmentRequestId], [Message], [CreatedAt]) VALUES (30, 1042, N'The treatment request for Employee 1043 has been Approved.', CAST(N'2025-01-03T15:30:28.2972746' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[Per_Hospitals] ON 

INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (2, 100, 80, 50, 120)
INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (3, 5, 3, 0, 0)
INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (4, 12, 10, 0, 0)
INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (5, 5, 6, 5, 6)
INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (6, 10, 8, 10, 10)
INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (7, 22, 222, 2222, 2222)
INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (8, 22, 22, 22, 22)
INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (9, 11, 11, 11, 11)
INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (10, 11, 11, 11, 11)
INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (11, 5, 5, 5, 5)
INSERT [dbo].[Per_Hospitals] ([Id], [TotalBeds], [ActiveBeds], [TotalDoctors], [TotalNurses]) VALUES (12, 10, 4, 15, 10)
SET IDENTITY_INSERT [dbo].[Per_Hospitals] OFF
GO
SET IDENTITY_INSERT [dbo].[Policies] ON 

INSERT [dbo].[Policies] ([PolicyId], [PolicyName], [PolicyDesc], [PolicyAmount]) VALUES (1, N'No Policy Assigned', N'Default entry for no policy', CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Policies] ([PolicyId], [PolicyName], [PolicyDesc], [PolicyAmount]) VALUES (2, N'Basic Health Policy', N'Covers basic health needs', CAST(50000.00 AS Decimal(18, 2)))
INSERT [dbo].[Policies] ([PolicyId], [PolicyName], [PolicyDesc], [PolicyAmount]) VALUES (3, N'Premium Health Policy', N'Comprehensive coverage for medical expenses, including doctor visits and surgeries.', CAST(100000.00 AS Decimal(18, 2)))
INSERT [dbo].[Policies] ([PolicyId], [PolicyName], [PolicyDesc], [PolicyAmount]) VALUES (4, N'Eduucation', N'free tuition fee for his 2 family members', CAST(100000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Policies] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Employee')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'Manager')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[TreatmentRequests] ON 

INSERT [dbo].[TreatmentRequests] ([RequestId], [HospitalId], [FinanceManagerId], [TreatmentDetails], [Status], [RequestDate], [ApprovalDate], [EmpId], [Emp_Email]) VALUES (1041, 1, 1, N'gfdchfg', N'1', CAST(N'2025-03-01T00:00:00.0000000' AS DateTime2), CAST(N'2025-04-01T00:00:00.0000000' AS DateTime2), 1, N'carey@gmail.com')
INSERT [dbo].[TreatmentRequests] ([RequestId], [HospitalId], [FinanceManagerId], [TreatmentDetails], [Status], [RequestDate], [ApprovalDate], [EmpId], [Emp_Email]) VALUES (1042, 3, 1, N'fdghvtghn', N'Approved', CAST(N'2025-01-03T00:00:00.0000000' AS DateTime2), CAST(N'2025-01-03T15:30:28.2963931' AS DateTime2), 1043, N'carey@gmail.com')
SET IDENTITY_INSERT [dbo].[TreatmentRequests] OFF
GO
SET IDENTITY_INSERT [dbo].[VisitRequest] ON 

INSERT [dbo].[VisitRequest] ([VisitRequestId], [HospitalId], [Reason], [Status], [RequestedAt], [ApprovedAt], [FinanceManagerId], [EmpId], [Emp_CNIC], [Emp_Email]) VALUES (3, 3, N'Routine Checkup', N'Pending', CAST(N'2024-12-30T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-30T00:00:00.0000000' AS DateTime2), 1, 1, 43012455, N'ws0856982@gmail.com')
INSERT [dbo].[VisitRequest] ([VisitRequestId], [HospitalId], [Reason], [Status], [RequestedAt], [ApprovedAt], [FinanceManagerId], [EmpId], [Emp_CNIC], [Emp_Email]) VALUES (4, 3, N'fefeewwefefdfsdffsfs', N'Pending', CAST(N'2024-12-30T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-31T00:00:00.0000000' AS DateTime2), 1, 1, 113344335, N'wassisheikh448@gmail.com')
INSERT [dbo].[VisitRequest] ([VisitRequestId], [HospitalId], [Reason], [Status], [RequestedAt], [ApprovedAt], [FinanceManagerId], [EmpId], [Emp_CNIC], [Emp_Email]) VALUES (5, 3, N'dsdvsdvsdvdsdbsbsdbb', N'Pending', CAST(N'2024-12-30T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-31T00:00:00.0000000' AS DateTime2), 1, 2, 998765334, N'ws0856982@gmail.com')
INSERT [dbo].[VisitRequest] ([VisitRequestId], [HospitalId], [Reason], [Status], [RequestedAt], [ApprovedAt], [FinanceManagerId], [EmpId], [Emp_CNIC], [Emp_Email]) VALUES (6, 3, N'bvcdfmhjngvmhnj', N'Pending', CAST(N'2024-12-31T00:00:00.0000000' AS DateTime2), NULL, 1, 5, 25602925, N'waqas@gmail.com')
SET IDENTITY_INSERT [dbo].[VisitRequest] OFF
GO
ALTER TABLE [dbo].[EmpRegisters] ADD  CONSTRAINT [DF_EmpRegisters_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[EmpRegisters] ADD  CONSTRAINT [DF_EmpRegisters_PolicyId]  DEFAULT ((1)) FOR [PolicyId]
GO
ALTER TABLE [dbo].[EmpRegisters]  WITH CHECK ADD FOREIGN KEY([PolicyId])
REFERENCES [dbo].[Policies] ([PolicyId])
GO
ALTER TABLE [dbo].[EmpRegisters]  WITH CHECK ADD FOREIGN KEY([PolicyId])
REFERENCES [dbo].[Policies] ([PolicyId])
GO
ALTER TABLE [dbo].[EmpRegisters]  WITH CHECK ADD FOREIGN KEY([PolicyId])
REFERENCES [dbo].[Policies] ([PolicyId])
GO
ALTER TABLE [dbo].[EmpRegisters]  WITH CHECK ADD FOREIGN KEY([PolicyId])
REFERENCES [dbo].[Policies] ([PolicyId])
GO
ALTER TABLE [dbo].[EmpRegisters]  WITH CHECK ADD  CONSTRAINT [FK_EmpRegister_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmpRegisters] CHECK CONSTRAINT [FK_EmpRegister_Roles_RoleId]
GO
ALTER TABLE [dbo].[Medical_Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Medical_Invoice_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Medical_Invoice] CHECK CONSTRAINT [FK_Medical_Invoice_Roles_RoleId]
GO
USE [master]
GO
ALTER DATABASE [E_project] SET  READ_WRITE 
GO
