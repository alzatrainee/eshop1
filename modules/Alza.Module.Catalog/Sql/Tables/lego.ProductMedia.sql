
/* Drop Foreign Key Constraints */

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProductMedia_Media]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [lego].[ProductMedia] DROP CONSTRAINT [FK_ProductMedia_Media]
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_ProductMedia_Products]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1) 
ALTER TABLE [lego].[ProductMedia] DROP CONSTRAINT [FK_ProductMedia_Products]
GO

/* Drop Tables */

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[lego].[ProductMedia]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [lego].[ProductMedia]
GO

/* Create Tables */

CREATE TABLE [lego].[ProductMedia]
(
	[ProductId] int NOT NULL,
	[MediaId] int NOT NULL
)
GO

/* Create Primary Keys, Indexes, Uniques, Checks */

ALTER TABLE [lego].[ProductMedia] 
 ADD CONSTRAINT [PK_ProductMedia]
	PRIMARY KEY CLUSTERED ([ProductId],[MediaId])
GO

CREATE INDEX [IXFK_ProductMedia_Media] 
 ON [lego].[ProductMedia] ([MediaId] ASC)
GO

CREATE INDEX [IXFK_ProductMedia_Products] 
 ON [lego].[ProductMedia] ([ProductId] ASC)
GO

/* Create Foreign Key Constraints */

ALTER TABLE [lego].[ProductMedia] ADD CONSTRAINT [FK_ProductMedia_Media]
	FOREIGN KEY ([MediaId]) REFERENCES [lego].[Media] ([Id]) ON DELETE No Action ON UPDATE No Action
GO

ALTER TABLE [lego].[ProductMedia] ADD CONSTRAINT [FK_ProductMedia_Products]
	FOREIGN KEY ([ProductId]) REFERENCES [lego].[Products] ([Id]) ON DELETE No Action ON UPDATE No Action
GO