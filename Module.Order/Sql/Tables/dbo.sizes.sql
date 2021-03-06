/****** Object:  Table [dbo].[sizes]    Script Date: 26.7.2017 17:23:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[sizes](
	[id_si] [int] IDENTITY(1,1) NOT NULL,
	[euro] [varchar](5) NOT NULL,
	[uk] [int] NOT NULL,
	[us_wo] [float] NOT NULL,
 CONSTRAINT [PK_sizes] PRIMARY KEY CLUSTERED 
(
	[id_si] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO