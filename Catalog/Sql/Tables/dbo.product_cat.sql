/****** Object:  Table [dbo].[product_cat]    Script Date: 26.7.2017 17:58:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[product_cat](
	[id_it] [int] NOT NULL,
	[id_cat] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[product_cat]  WITH CHECK ADD  CONSTRAINT [FK_item_cat_category] FOREIGN KEY([id_cat])
REFERENCES [dbo].[category] ([id_cat])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[product_cat] CHECK CONSTRAINT [FK_item_cat_category]
GO

ALTER TABLE [dbo].[product_cat]  WITH CHECK ADD  CONSTRAINT [FK_item_cat_Product] FOREIGN KEY([id_it])
REFERENCES [dbo].[Product] ([id_pr])
GO

ALTER TABLE [dbo].[product_cat] CHECK CONSTRAINT [FK_item_cat_Product]
GO
