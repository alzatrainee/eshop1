/****** Object:  Table [dbo].[Firm]    Script Date: 26.7.2017 17:59:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Firm](
	[id_fir] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[logo] [varchar](max) NULL,
 CONSTRAINT [PK_Firm] PRIMARY KEY CLUSTERED 
(
	[id_fir] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO