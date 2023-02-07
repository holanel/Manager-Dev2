USE [vyvojovy_pomocnik]
GO

/****** Object:  Table [dbo].[Udalosti]    Script Date: 01.11.2022 15:27:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Udalosti](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[datum_zac] [nvarchar](max) NOT NULL,
	[datum_kon] [nvarchar](max) NOT NULL,
	[popis] [nvarchar](max) NOT NULL,
	[kos] [bit] NOT NULL,
	[datum_mazani] [nvarchar](max) NULL,
	[datum_uplneho_mazani] [nvarchar](max) NULL,
 CONSTRAINT [PK_Udalosti] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


