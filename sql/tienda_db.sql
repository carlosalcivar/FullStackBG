USE [master]
GO
/****** Object:  Database [tienda_db]    Script Date: 30/3/2025 15:57:53 ******/
CREATE DATABASE [tienda_db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tienda_db', FILENAME = N'/var/opt/mssql/data/tienda_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'tienda_db_log', FILENAME = N'/var/opt/mssql/data/tienda_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [tienda_db] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tienda_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tienda_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tienda_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tienda_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tienda_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tienda_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [tienda_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [tienda_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tienda_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tienda_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tienda_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tienda_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tienda_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tienda_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tienda_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tienda_db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [tienda_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tienda_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tienda_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tienda_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tienda_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tienda_db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tienda_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tienda_db] SET RECOVERY FULL 
GO
ALTER DATABASE [tienda_db] SET  MULTI_USER 
GO
ALTER DATABASE [tienda_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tienda_db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tienda_db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tienda_db] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [tienda_db] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [tienda_db] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'tienda_db', N'ON'
GO
ALTER DATABASE [tienda_db] SET QUERY_STORE = ON
GO
ALTER DATABASE [tienda_db] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [tienda_db]
GO
/****** Object:  Table [dbo].[tbl_producto]    Script Date: 30/3/2025 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_producto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[sku] [varchar](12) NOT NULL,
	[nombre] [varchar](200) NOT NULL,
	[descripcion] [varchar](255) NOT NULL,
	[estado] [varchar](1) NOT NULL,
	[usuario_creacion_id] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[usuario_modificacion_id] [int] NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_usuario]    Script Date: 30/3/2025 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_usuario](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuario] [varchar](50) NOT NULL,
	[contrasena] [varchar](255) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[apellido] [varchar](100) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[estado] [varchar](1) NOT NULL,
	[usuario_creacion_id] [int] NOT NULL,
	[fecha_creacion] [datetime] NOT NULL,
	[usuario_modificacion_id] [int] NULL,
	[fecha_modificacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tbl_producto] ON 

INSERT [dbo].[tbl_producto] ([id], [sku], [nombre], [descripcion], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (1, N'SKU-000001', N'CELULAR SAMSUNG MODELO A10', N'CELULAR ANDROID, CAMARA 15MP, MEMORIA INTERNA 32GB', N'E', 0, CAST(N'2025-03-30T15:44:44.373' AS DateTime), NULL, NULL)
INSERT [dbo].[tbl_producto] ([id], [sku], [nombre], [descripcion], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (3, N'SKU-000002', N'CELULAR SAMSUNG MODELO A11', N'CELULAR ANDROID, CAMARA 15MP, MEMORIA INTERNA 128GB', N'A', 1, CAST(N'2025-03-30T20:55:48.920' AS DateTime), NULL, NULL)
INSERT [dbo].[tbl_producto] ([id], [sku], [nombre], [descripcion], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (4, N'SKU-000003', N'CELULAR NOKIA MODELO NK-500', N'CELULAR ANDROID, CAMARA 15MP, MEMORIA INTERNA 128GB', N'A', 1, CAST(N'2025-03-30T20:55:48.923' AS DateTime), NULL, NULL)
INSERT [dbo].[tbl_producto] ([id], [sku], [nombre], [descripcion], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (5, N'SKU-000004', N'TV SAMSUNG MODELO SM-50', N'TV 50'',ANDROID, HDMI', N'A', 1, CAST(N'2025-03-30T20:55:48.927' AS DateTime), NULL, NULL)
INSERT [dbo].[tbl_producto] ([id], [sku], [nombre], [descripcion], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (6, N'SKU-000005', N'TV SAMSUNG MODELO SM-75', N'TV 75'',ANDROID, HDMI', N'A', 1, CAST(N'2025-03-30T20:55:48.930' AS DateTime), NULL, NULL)
INSERT [dbo].[tbl_producto] ([id], [sku], [nombre], [descripcion], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (7, N'SKU-000006', N'TV LG MODELO LG-50', N'TV 50'',OS, HDMI', N'A', 1, CAST(N'2025-03-30T20:55:48.930' AS DateTime), NULL, NULL)
INSERT [dbo].[tbl_producto] ([id], [sku], [nombre], [descripcion], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (8, N'SKU-000007', N'TV LG MODELO LG-75', N'TV 75'',OS, HDMI', N'A', 1, CAST(N'2025-03-30T20:55:48.933' AS DateTime), NULL, NULL)
INSERT [dbo].[tbl_producto] ([id], [sku], [nombre], [descripcion], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (9, N'SKU-000008', N'TV SMC MODELO SMC-50', N'TV 50'',ANDROID, HDMI', N'A', 1, CAST(N'2025-03-30T20:55:48.937' AS DateTime), NULL, NULL)
INSERT [dbo].[tbl_producto] ([id], [sku], [nombre], [descripcion], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (10, N'SKU-000009', N'TV SMC MODELO SMC-75', N'TV 75'',ANDROID, HDMI', N'A', 1, CAST(N'2025-03-30T20:55:48.937' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_producto] OFF
GO
SET IDENTITY_INSERT [dbo].[tbl_usuario] ON 

INSERT [dbo].[tbl_usuario] ([id], [usuario], [contrasena], [nombre], [apellido], [email], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (1, N'calcivar', N'b4WOBfexd5yFXZaxPKCId2Ri6Jg0eKV4oecqh1nT+Uc=', N'Carlos', N'Alcivar', N'calcivar@email.com', N'A', 0, CAST(N'2025-03-30T20:00:46.037' AS DateTime), NULL, NULL)
INSERT [dbo].[tbl_usuario] ([id], [usuario], [contrasena], [nombre], [apellido], [email], [estado], [usuario_creacion_id], [fecha_creacion], [usuario_modificacion_id], [fecha_modificacion]) VALUES (2, N'admin', N'K7mWxOoxfddg7upNUUwToQcwGVqFEGLA26n87SYcuJM=', N'Administrador', N'Sistema', N'admin@email.com', N'A', 0, CAST(N'2025-03-30T20:00:46.037' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[tbl_usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [tbl_producto_uq_sku]    Script Date: 30/3/2025 15:57:53 ******/
ALTER TABLE [dbo].[tbl_producto] ADD  CONSTRAINT [tbl_producto_uq_sku] UNIQUE NONCLUSTERED 
(
	[sku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [tbl_producto_estado_IDX]    Script Date: 30/3/2025 15:57:53 ******/
CREATE NONCLUSTERED INDEX [tbl_producto_estado_IDX] ON [dbo].[tbl_producto]
(
	[estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [tbl_producto_sku_IDX]    Script Date: 30/3/2025 15:57:53 ******/
CREATE NONCLUSTERED INDEX [tbl_producto_sku_IDX] ON [dbo].[tbl_producto]
(
	[sku] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [tbl_usuario_uq_email]    Script Date: 30/3/2025 15:57:53 ******/
ALTER TABLE [dbo].[tbl_usuario] ADD  CONSTRAINT [tbl_usuario_uq_email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [tbl_usuario_uq_usuario]    Script Date: 30/3/2025 15:57:53 ******/
ALTER TABLE [dbo].[tbl_usuario] ADD  CONSTRAINT [tbl_usuario_uq_usuario] UNIQUE NONCLUSTERED 
(
	[usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [tbl_usuario_email_IDX]    Script Date: 30/3/2025 15:57:53 ******/
CREATE NONCLUSTERED INDEX [tbl_usuario_email_IDX] ON [dbo].[tbl_usuario]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [tbl_usuario_estado_IDX]    Script Date: 30/3/2025 15:57:53 ******/
CREATE NONCLUSTERED INDEX [tbl_usuario_estado_IDX] ON [dbo].[tbl_usuario]
(
	[estado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [tbl_usuario_usuario_IDX]    Script Date: 30/3/2025 15:57:53 ******/
CREATE NONCLUSTERED INDEX [tbl_usuario_usuario_IDX] ON [dbo].[tbl_usuario]
(
	[usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [tienda_db] SET  READ_WRITE 
GO
