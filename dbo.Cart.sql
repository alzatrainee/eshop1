CREATE TABLE [dbo].[Cart] (
    [id_car] INT IDENTITY (1, 1) NOT NULL,
    [id_st]  INT NULL,
    [id_us]  INT NOT NULL,
    [isEmpty] BIT NOT NULL, 
    CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([id_car] ASC),
    CONSTRAINT [FK_Cart_Cart_st] FOREIGN KEY ([id_st]) REFERENCES [dbo].[Cart_st] ([id_st]),
    CONSTRAINT [FK_Cart_User] FOREIGN KEY ([id_us]) REFERENCES [dbo].[User] ([id_user])
);

