USE [master]
GO
/****** Object:  Database [Hospital_Copy]    Script Date: 2021-08-20 5:08:05 PM ******/
CREATE DATABASE [Hospital_Copy]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hospital_Database', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Hospital_Database.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Hospital_Database_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Hospital_Database_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Hospital_Copy] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Hospital_Copy].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Hospital_Copy] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Hospital_Copy] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Hospital_Copy] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Hospital_Copy] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Hospital_Copy] SET ARITHABORT OFF 
GO
ALTER DATABASE [Hospital_Copy] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Hospital_Copy] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Hospital_Copy] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Hospital_Copy] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Hospital_Copy] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Hospital_Copy] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Hospital_Copy] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Hospital_Copy] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Hospital_Copy] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Hospital_Copy] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Hospital_Copy] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Hospital_Copy] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Hospital_Copy] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Hospital_Copy] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Hospital_Copy] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Hospital_Copy] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Hospital_Copy] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Hospital_Copy] SET RECOVERY FULL 
GO
ALTER DATABASE [Hospital_Copy] SET  MULTI_USER 
GO
ALTER DATABASE [Hospital_Copy] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Hospital_Copy] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Hospital_Copy] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Hospital_Copy] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Hospital_Copy] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Hospital_Copy] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Hospital_Database', N'ON'
GO
ALTER DATABASE [Hospital_Copy] SET QUERY_STORE = OFF
GO
USE [Hospital_Copy]
GO
/****** Object:  Table [dbo].[Admission]    Script Date: 2021-08-20 5:08:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admission](
	[idAdmission] [varchar](20) NOT NULL,
	[chirurgie] [bit] NULL,
	[dateAdmission] [date] NULL,
	[dateChirurgie] [date] NULL,
	[dateConge] [date] NULL,
	[televiseur] [bit] NULL,
	[telephone] [bit] NULL,
	[NSS] [varchar](20) NOT NULL,
	[numeroLit] [int] NOT NULL,
	[idMedecin] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Admission] PRIMARY KEY CLUSTERED 
(
	[idAdmission] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assurance]    Script Date: 2021-08-20 5:08:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assurance](
	[idAssurance] [varchar](20) NOT NULL,
	[nomCompagnie] [varchar](50) NULL,
	[prive] [bit] NULL,
 CONSTRAINT [PK_Assurance] PRIMARY KEY CLUSTERED 
(
	[idAssurance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departement]    Script Date: 2021-08-20 5:08:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departement](
	[idDepartement] [varchar](20) NOT NULL,
	[nomDepartement] [varchar](50) NULL,
 CONSTRAINT [PK_Departement] PRIMARY KEY CLUSTERED 
(
	[idDepartement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lit]    Script Date: 2021-08-20 5:08:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lit](
	[numeroLit] [int] NOT NULL,
	[occupe] [bit] NULL,
	[idType] [varchar](20) NOT NULL,
	[idDepartement] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Lit] PRIMARY KEY CLUSTERED 
(
	[numeroLit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medecin]    Script Date: 2021-08-20 5:08:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medecin](
	[idMedecin] [varchar](20) NOT NULL,
	[nom] [varchar](20) NULL,
	[prenom] [varchar](20) NULL,
 CONSTRAINT [PK_Medecin] PRIMARY KEY CLUSTERED 
(
	[idMedecin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 2021-08-20 5:08:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[NSS] [varchar](20) NOT NULL,
	[dateNaissance] [date] NULL,
	[nom] [varchar](20) NULL,
	[prenom] [varchar](20) NULL,
	[adresse] [varchar](50) NULL,
	[ville] [varchar](20) NULL,
	[province] [varchar](20) NULL,
	[codePostal] [varchar](10) NULL,
	[telephone] [varchar](20) NULL,
	[idAssurance] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[NSS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeLit]    Script Date: 2021-08-20 5:08:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeLit](
	[idType] [varchar](20) NOT NULL,
	[description] [varchar](50) NULL,
 CONSTRAINT [PK_TypeLit] PRIMARY KEY CLUSTERED 
(
	[idType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Admission] ([idAdmission], [chirurgie], [dateAdmission], [dateChirurgie], [dateConge], [televiseur], [telephone], [NSS], [numeroLit], [idMedecin]) VALUES (N'BouGil2021-08-10', 1, CAST(N'2021-08-10' AS Date), CAST(N'2021-08-18' AS Date), CAST(N'0001-01-01' AS Date), 1, 1, N'BouGil', 4, N'LeMSte')
INSERT [dbo].[Admission] ([idAdmission], [chirurgie], [dateAdmission], [dateChirurgie], [dateConge], [televiseur], [telephone], [NSS], [numeroLit], [idMedecin]) VALUES (N'TiJFra2021-08-13', 0, CAST(N'2021-08-13' AS Date), CAST(N'0001-01-01' AS Date), CAST(N'0001-01-01' AS Date), 0, 0, N'TiJFra', 11, N'LePBob')
GO
INSERT [dbo].[Assurance] ([idAssurance], [nomCompagnie], [prive]) VALUES (N'assPrivee', N'Sunlife', 1)
INSERT [dbo].[Assurance] ([idAssurance], [nomCompagnie], [prive]) VALUES (N'assPublique', N'Carte Soleil', 0)
GO
INSERT [dbo].[Departement] ([idDepartement], [nomDepartement]) VALUES (N'cardio', N'Cardiologie')
INSERT [dbo].[Departement] ([idDepartement], [nomDepartement]) VALUES (N'chirur', N'Chirurgie')
INSERT [dbo].[Departement] ([idDepartement], [nomDepartement]) VALUES (N'pedi', N'Pediatrie')
INSERT [dbo].[Departement] ([idDepartement], [nomDepartement]) VALUES (N'radio', N'Radiologie')
GO
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (1, 0, N'standard', N'cardio')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (2, 0, N'semi-privee', N'cardio')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (3, 0, N'privee', N'cardio')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (4, 1, N'standard', N'chirur')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (5, 0, N'semi-privee', N'chirur')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (6, 0, N'privee', N'chirur')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (7, 0, N'standard', N'radio')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (8, 0, N'semi-privee', N'radio')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (9, 0, N'privee', N'radio')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (10, 0, N'standard', N'pedi')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (11, 1, N'semi-privee', N'pedi')
INSERT [dbo].[Lit] ([numeroLit], [occupe], [idType], [idDepartement]) VALUES (12, 0, N'privee', N'pedi')
GO
INSERT [dbo].[Medecin] ([idMedecin], [nom], [prenom]) VALUES (N'LeMSte', N'LeMeilleur', N'Steeve')
INSERT [dbo].[Medecin] ([idMedecin], [nom], [prenom]) VALUES (N'LePBob', N'LePosh', N'Bobby')
GO
INSERT [dbo].[Patient] ([NSS], [dateNaissance], [nom], [prenom], [adresse], [ville], [province], [codePostal], [telephone], [idAssurance]) VALUES (N'BouGil', CAST(N'2021-08-04' AS Date), N'Bouchard', N'Gilles', N'123 allo', N'montreal', N'Quebec', N'55454', N'545454', N'assPrivee')
INSERT [dbo].[Patient] ([NSS], [dateNaissance], [nom], [prenom], [adresse], [ville], [province], [codePostal], [telephone], [idAssurance]) VALUES (N'JaiSyl', CAST(N'1990-08-08' AS Date), N'JaiMal', N'Sylvain', N'123 rue Cassé', N'Laval', N'Quebec', N'h3h3h3', N'1234567890', N'assPrivee')
INSERT [dbo].[Patient] ([NSS], [dateNaissance], [nom], [prenom], [adresse], [ville], [province], [codePostal], [telephone], [idAssurance]) VALUES (N'TiJFra', CAST(N'2010-06-02' AS Date), N'TiJeune', N'Francis', N'123 Rue Cool', N'Longueil', N'Quebec', N'4r4r4r', N'545454', N'assPublique')
GO
INSERT [dbo].[TypeLit] ([idType], [description]) VALUES (N'privee', N'Privee')
INSERT [dbo].[TypeLit] ([idType], [description]) VALUES (N'semi-privee', N'Semi-Privee')
INSERT [dbo].[TypeLit] ([idType], [description]) VALUES (N'standard', N'Standard')
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Lit] FOREIGN KEY([numeroLit])
REFERENCES [dbo].[Lit] ([numeroLit])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Lit]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Medecin] FOREIGN KEY([idMedecin])
REFERENCES [dbo].[Medecin] ([idMedecin])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Medecin]
GO
ALTER TABLE [dbo].[Admission]  WITH CHECK ADD  CONSTRAINT [FK_Admission_Patient] FOREIGN KEY([NSS])
REFERENCES [dbo].[Patient] ([NSS])
GO
ALTER TABLE [dbo].[Admission] CHECK CONSTRAINT [FK_Admission_Patient]
GO
ALTER TABLE [dbo].[Lit]  WITH CHECK ADD  CONSTRAINT [FK_Lit_Departement] FOREIGN KEY([idDepartement])
REFERENCES [dbo].[Departement] ([idDepartement])
GO
ALTER TABLE [dbo].[Lit] CHECK CONSTRAINT [FK_Lit_Departement]
GO
ALTER TABLE [dbo].[Lit]  WITH CHECK ADD  CONSTRAINT [FK_Lit_TypeLit] FOREIGN KEY([idType])
REFERENCES [dbo].[TypeLit] ([idType])
GO
ALTER TABLE [dbo].[Lit] CHECK CONSTRAINT [FK_Lit_TypeLit]
GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [FK_Patient_Assurance] FOREIGN KEY([idAssurance])
REFERENCES [dbo].[Assurance] ([idAssurance])
GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [FK_Patient_Assurance]
GO
USE [master]
GO
ALTER DATABASE [Hospital_Copy] SET  READ_WRITE 
GO
