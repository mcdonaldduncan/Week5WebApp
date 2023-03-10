USE [PROG455SP23]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/1/2023 11:05:34 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/1/2023 11:05:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Permissions] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [Username], [Password], [Permissions]) VALUES (1, N'dfmcd98', N'e957042e44dd5d723c5c9d87aebc936561515c841e6121e30b7c121e58fd9998', N'super admin')
INSERT [dbo].[User] ([ID], [Username], [Password], [Permissions]) VALUES (2, N'testuser', N'86ba47f64783d3ae4509128b8f365b6424361a7d558ac1dfc90aa8fa3ccba285', N'admin')
INSERT [dbo].[User] ([ID], [Username], [Password], [Permissions]) VALUES (4, N'jeff', N'937e8d5fbb48bd4949536cd65b8d35c426b80d2f830c5c308e2cdec422ae2244', N'user')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
