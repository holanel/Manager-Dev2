USE [vyvojovy_pomocnik]
GO

/****** Object:  Table [dbo].[Projekty]    Script Date: 27.10.2022 21:42:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Projekty](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nazev] [nvarchar](max) NOT NULL,
	[datum_vytvoreni] [nvarchar](max) NOT NULL,
	[jmeno_klienta] [nvarchar](max) NOT NULL,
	[zadani] [nvarchar](max) NOT NULL,
	[technologie] [nvarchar](max) NOT NULL,
	[datum_odevzdani] [nvarchar](max) NOT NULL,
	[cas_vypracovani] [nvarchar](max) NOT NULL,
	[pocet_komentare] [nvarchar](max) NULL,
	[pocet_bugy] [nvarchar](max) NULL,
	[pocet_problemy] [nvarchar](max) NULL,
	[id_problemy] [nvarchar](max) NULL,
	[id_bugy] [nvarchar](max) NULL,
	[hotovo] [bit] NULL,
	[sazba] [int] NULL,
	[pocet_hodin] [int] NULL,
	[kos] [bit] NULL,
	[datum_mazani] [nvarchar](max) NULL,
	[datum_uplneho_mazani] [nvarchar](max) NULL,
 CONSTRAINT [PK_Projekty] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


