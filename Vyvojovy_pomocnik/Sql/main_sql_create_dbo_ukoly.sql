USE [vyvojovy_pomocnik]
GO

/****** Object:  Table [dbo].[Ukoly]    Script Date: 27.10.2022 21:43:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ukoly](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nazev] [nvarchar](max) NOT NULL,
	[datum_zadani] [nvarchar](max) NOT NULL,
	[zadani] [nvarchar](max) NOT NULL,
	[cas_vypracovani] [nvarchar](max) NOT NULL,
	[datum_odevzdani] [nvarchar](max) NOT NULL,
	[komentare] [nvarchar](max) NOT NULL,
	[id_projektu] [int] NOT NULL,
	[hotovo] [bit] NOT NULL,
	[sazba] [int] NULL,
	[pocet_hodin] [int] NULL,
	[mena] [nvarchar](max) NOT NULL,
	[kos] [bit] NOT NULL,
	[datum_mazani] [nvarchar](max) NULL,
	[datum_uplneho_mazani] [nvarchar](max) NULL,
 CONSTRAINT [PK_Ukoly] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


