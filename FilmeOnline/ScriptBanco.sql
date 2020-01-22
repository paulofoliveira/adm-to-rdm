CREATE DATABASE [FilmeOnline]
GO
USE [FilmeOnline]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 11/15/2017 4:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteID] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Status] [int] NOT NULL,
	[DataExpiracaoStatus] [datetime] NULL,
	[ValorGasto] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[ClienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Filme]    Script Date: 11/15/2017 4:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Filme](
	[FilmeID] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Licenca] [int] NOT NULL,
 CONSTRAINT [PK_Filme] PRIMARY KEY CLUSTERED 
(
	[FilmeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Aluguel]    Script Date: 11/15/2017 4:11:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aluguel](
	[AluguelID] [bigint] IDENTITY(1,1) NOT NULL,
	[FilmeID] [bigint] NOT NULL,
	[ClienteID] [bigint] NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[DataAluguel] [datetime] NOT NULL,
	[DataExpiracao] [datetime] NULL,
 CONSTRAINT [PK_Aluguel] PRIMARY KEY CLUSTERED 
(
	[AluguelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

GO
INSERT [dbo].[Cliente] ([ClienteID], [Nome], [Email], [Status], [DataExpiracaoStatus], [ValorGasto]) VALUES (1, N'James Peterson', N'james.peterson@gmail.com', 1, NULL, CAST(16.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cliente] ([ClienteID], [Nome], [Email], [Status], [DataExpiracaoStatus], [ValorGasto]) VALUES (2, N'Michal Samson', N'm.samson@yahoo.com', 2, CAST(N'2018-10-14 01:37:27.000' AS DateTime), CAST(9.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cliente] ([ClienteID], [Nome], [Email], [Status], [DataExpiracaoStatus], [ValorGasto]) VALUES (4, N'Alan Turing', N'the.alan@gmail.com', 1, NULL, CAST(0.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cliente] ([ClienteID], [Nome], [Email], [Status], [DataExpiracaoStatus], [ValorGasto]) VALUES (5, N'Alan Turing 2', N'the.alan2@gmail.com', 1, NULL, CAST(1004.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Cliente] ([ClienteID], [Nome], [Email], [Status], [DataExpiracaoStatus], [ValorGasto]) VALUES (6, N'Alan Turing 3', N'the.alan3@gmail.com', 1, NULL, CAST(0.00 AS Decimal(18, 2)))
GO
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[Filme] ON 

GO
INSERT [dbo].[Filme] ([FilmeID], [Nome], [Licenca]) VALUES (1, N'The Great Gatsby', 1)
GO
INSERT [dbo].[Filme] ([FilmeID], [Nome], [Licenca]) VALUES (2, N'The Secret Life of Pets', 2)
GO
SET IDENTITY_INSERT [dbo].[Filme] OFF
GO
SET IDENTITY_INSERT [dbo].[Aluguel] ON 

GO
INSERT [dbo].[Aluguel] ([AluguelID], [FilmeID], [ClienteID], [Valor], [DataAluguel], [DataExpiracao]) VALUES (1, 1, 2, CAST(5.00 AS Decimal(18, 2)), CAST(N'2017-09-16 16:30:05.773' AS DateTime), CAST(N'2017-09-18 00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Aluguel] ([AluguelID], [FilmeID], [ClienteID], [Valor], [DataAluguel], [DataExpiracao]) VALUES (2, 2, 2, CAST(4.00 AS Decimal(18, 2)), CAST(N'2017-09-15 15:30:05.773' AS DateTime), NULL)
GO
INSERT [dbo].[Aluguel] ([AluguelID], [FilmeID], [ClienteID], [Valor], [DataAluguel], [DataExpiracao]) VALUES (3, 1, 5, CAST(4.00 AS Decimal(18, 2)), CAST(N'2017-10-07 23:54:22.000' AS DateTime), CAST(N'2017-10-09 23:54:22.000' AS DateTime))
GO
INSERT [dbo].[Aluguel] ([AluguelID], [FilmeID], [ClienteID], [Valor], [DataAluguel], [DataExpiracao]) VALUES (6, 1, 1, CAST(4.00 AS Decimal(18, 2)), CAST(N'2017-10-15 13:26:19.000' AS DateTime), CAST(N'2017-10-17 13:26:19.000' AS DateTime))
GO
INSERT [dbo].[Aluguel] ([AluguelID], [FilmeID], [ClienteID], [Valor], [DataAluguel], [DataExpiracao]) VALUES (7, 1, 1, CAST(4.00 AS Decimal(18, 2)), CAST(N'2017-10-22 16:06:51.000' AS DateTime), CAST(N'2017-10-24 16:06:51.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Aluguel] OFF
GO
ALTER TABLE [dbo].[Aluguel]  WITH CHECK ADD  CONSTRAINT [FK_Aluguel_Cliente] FOREIGN KEY([ClienteID])
REFERENCES [dbo].[Cliente] ([ClienteID])
GO
ALTER TABLE [dbo].[Aluguel] CHECK CONSTRAINT [FK_Aluguel_Cliente]
GO
ALTER TABLE [dbo].[Aluguel]  WITH CHECK ADD  CONSTRAINT [FK_Aluguel_Filme] FOREIGN KEY([FilmeID])
REFERENCES [dbo].[Filme] ([FilmeID])
GO
ALTER TABLE [dbo].[Aluguel] CHECK CONSTRAINT [FK_Aluguel_Filme]
GO
