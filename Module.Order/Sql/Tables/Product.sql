/****** Object:  Table [dbo].[Product]    Script Date: 26.7.2017 18:05:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[id_pr] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[price] [float] NOT NULL,
	[date] [date] NOT NULL,
	[description] [nvarchar](max) NULL,
	[id_fir] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id_pr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Firm] FOREIGN KEY([id_fir])
REFERENCES [dbo].[Firm] ([id_fir])
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Firm]
GO

