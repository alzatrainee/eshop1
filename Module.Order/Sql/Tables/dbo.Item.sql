/****** Object:  Table [dbo].[Item]    Script Date: 26.7.2017 18:03:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Item](
	[id_it] [int] NOT NULL,
	[rgb] [varchar](6) NOT NULL,
	[id_si] [int] NOT NULL,
	[id_pr] [int] NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[id_it] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Colour] FOREIGN KEY([rgb])
REFERENCES [dbo].[Colour] ([rgb])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Colour]
GO

ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_Product] FOREIGN KEY([id_pr])
REFERENCES [dbo].[Product] ([id_pr])
GO

ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_Product]
GO

ALTER TABLE [dbo].[Item]  WITH CHECK ADD  CONSTRAINT [FK_Item_sizes] FOREIGN KEY([id_si])
REFERENCES [dbo].[sizes] ([id_si])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Item] CHECK CONSTRAINT [FK_Item_sizes]
GO