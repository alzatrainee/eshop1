
/****** Object:  Table [dbo].[Colour]    Script Date: 26.7.2017 16:40:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Colour](
	[rgb] [varchar](6) NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Colour] PRIMARY KEY CLUSTERED 
(
	[rgb] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
