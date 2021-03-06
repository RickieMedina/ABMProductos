USE [master]
GO
/****** Object:  Database [Informatica]    Script Date: 19/10/2021 10:50:20 ******/
CREATE DATABASE [Informatica]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Informatica', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Informatica.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Informatica_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Informatica_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Informatica] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Informatica].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Informatica] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Informatica] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Informatica] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Informatica] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Informatica] SET ARITHABORT OFF 
GO
ALTER DATABASE [Informatica] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Informatica] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Informatica] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Informatica] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Informatica] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Informatica] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Informatica] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Informatica] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Informatica] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Informatica] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Informatica] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Informatica] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Informatica] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Informatica] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Informatica] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Informatica] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Informatica] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Informatica] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Informatica] SET  MULTI_USER 
GO
ALTER DATABASE [Informatica] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Informatica] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Informatica] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Informatica] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Informatica] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Informatica]
GO
/****** Object:  Table [dbo].[Marcas]    Script Date: 19/10/2021 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Marcas](
	[idMarca] [int] NOT NULL,
	[nombreMarca] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Marcas] PRIMARY KEY CLUSTERED 
(
	[idMarca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 19/10/2021 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Productos](
	[codigo] [int] NOT NULL,
	[detalle] [varchar](50) NOT NULL,
	[tipo] [int] NOT NULL,
	[marca] [int] NOT NULL,
	[precio] [float] NOT NULL,
	[fecha] [date] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[ProductosMarcas]    Script Date: 19/10/2021 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view [dbo].[ProductosMarcas]
as

select detalle Detalle, precio Precio,fecha Fecha, nombreMarca Marca 
from Productos p join Marcas m on p.marca=m.idMarca
GO
INSERT [dbo].[Marcas] ([idMarca], [nombreMarca]) VALUES (1, N'HP')
INSERT [dbo].[Marcas] ([idMarca], [nombreMarca]) VALUES (2, N'EPSON')
INSERT [dbo].[Marcas] ([idMarca], [nombreMarca]) VALUES (3, N'COMPAQ')
INSERT [dbo].[Marcas] ([idMarca], [nombreMarca]) VALUES (4, N'DELL')
INSERT [dbo].[Marcas] ([idMarca], [nombreMarca]) VALUES (5, N'ASUS')
INSERT [dbo].[Marcas] ([idMarca], [nombreMarca]) VALUES (6, N'BANGHO')
INSERT [dbo].[Marcas] ([idMarca], [nombreMarca]) VALUES (7, N'SONY')
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (1, N' Pavilion', 1, 1, 50000, CAST(N'2021-05-01' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (2, N'Studio', 2, 4, 70000, CAST(N'2021-06-01' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (3, N' Pcbook2', 1, 6, 150000, CAST(N'2021-07-02' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (4, N' Estadia', 2, 6, 200000, CAST(N'2021-07-01' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (5, N' basic', 1, 2, 125000, CAST(N'2021-07-11' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (6, N' Dellbook', 1, 4, 2132132, CAST(N'2021-07-01' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (7, N' Hyper', 1, 6, 200000, CAST(N'2021-07-15' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (8, N'HyperX', 1, 4, 260000, CAST(N'2021-07-15' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (9, N'double2', 1, 7, 350000, CAST(N'2021-07-01' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (20, N' Mini Book', 1, 3, 52000, CAST(N'2021-05-01' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (21, N' Ultra', 2, 5, 90000, CAST(N'2021-05-01' AS Date))
INSERT [dbo].[Productos] ([codigo], [detalle], [tipo], [marca], [precio], [fecha]) VALUES (22, N'Data', 2, 2, 125000, CAST(N'2021-10-19' AS Date))
/****** Object:  StoredProcedure [dbo].[InsertarProducto]    Script Date: 19/10/2021 10:50:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[InsertarProducto]
@codigo int,
@detalle varchar(50),
@tipo int,
@marca int,
@precio float,
@fecha date
as
insert into Productos
VALUES (@codigo,@detalle,@tipo,@marca,@precio,@fecha)
GO
USE [master]
GO
ALTER DATABASE [Informatica] SET  READ_WRITE 
GO
