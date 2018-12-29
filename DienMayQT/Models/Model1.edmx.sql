
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/28/2018 20:49:39
-- Generated from EDMX file: C:\Users\USER\Desktop\DienMayQT\DienMayQT\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DmQT06];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CashBillDetail_CashBill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashBillDetail] DROP CONSTRAINT [FK_CashBillDetail_CashBill];
GO
IF OBJECT_ID(N'[dbo].[FK_CashBillDetail_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CashBillDetail] DROP CONSTRAINT [FK_CashBillDetail_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_InstallmentBill_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstallmentBill] DROP CONSTRAINT [FK_InstallmentBill_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_InstallmentBillDetail_InstallmentBill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstallmentBillDetail] DROP CONSTRAINT [FK_InstallmentBillDetail_InstallmentBill];
GO
IF OBJECT_ID(N'[dbo].[FK_InstallmentBillDetail_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstallmentBillDetail] DROP CONSTRAINT [FK_InstallmentBillDetail_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_Product_ProductType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_ProductType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Account]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account];
GO
IF OBJECT_ID(N'[dbo].[CashBill]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashBill];
GO
IF OBJECT_ID(N'[dbo].[CashBillDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CashBillDetail];
GO
IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[InstallmentBill]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstallmentBill];
GO
IF OBJECT_ID(N'[dbo].[InstallmentBillDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstallmentBillDetail];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[ProductType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductType];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CashBill'
CREATE TABLE [dbo].[CashBill] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BillCode] varchar(10)  NULL,
    [CustomerName] nvarchar(100)  NOT NULL,
    [PhoneNumber] varchar(12)  NULL,
    [Address] nvarchar(100)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Shipper] nvarchar(100)  NULL,
    [Note] nvarchar(255)  NULL,
    [GrandTotal] int  NOT NULL
);
GO

-- Creating table 'CashBillDetail'
CREATE TABLE [dbo].[CashBillDetail] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BillID] int  NOT NULL,
    [ProductID] int  NOT NULL,
    [Quantity] int  NOT NULL,
    [SalePrice] int  NOT NULL
);
GO

-- Creating table 'Customer'
CREATE TABLE [dbo].[Customer] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CustomerCode] varchar(12)  NULL,
    [CustomerName] nvarchar(100)  NOT NULL,
    [PhoneNumber] varchar(12)  NULL,
    [Address] nvarchar(100)  NOT NULL,
    [YearOfBirth] int  NULL
);
GO

-- Creating table 'InstallmentBill'
CREATE TABLE [dbo].[InstallmentBill] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BillCode] varchar(10)  NULL,
    [CustomerID] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Shipper] nvarchar(100)  NULL,
    [Note] nvarchar(255)  NULL,
    [Method] nvarchar(10)  NOT NULL,
    [Period] int  NOT NULL,
    [GrandTotal] int  NOT NULL,
    [Taken] int  NOT NULL,
    [Remain] int  NOT NULL
);
GO

-- Creating table 'InstallmentBillDetail'
CREATE TABLE [dbo].[InstallmentBillDetail] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [BillID] int  NOT NULL,
    [ProductID] int  NOT NULL,
    [Quantity] int  NOT NULL,
    [InstallmentPrice] int  NOT NULL
);
GO

-- Creating table 'Product'
CREATE TABLE [dbo].[Product] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProductCode] varchar(10)  NULL,
    [ProductName] nvarchar(100)  NOT NULL,
    [ProductTypeID] int  NOT NULL,
    [SalePrice] int  NOT NULL,
    [OriginPrice] int  NOT NULL,
    [InstallmentPrice] int  NOT NULL,
    [Quantity] int  NOT NULL,
    [Avatar] varchar(50)  NULL,
    [Status] bit  NULL
);
GO

-- Creating table 'ProductType'
CREATE TABLE [dbo].[ProductType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProductTypeCode] varchar(3)  NOT NULL,
    [ProductTypeName] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Account'
CREATE TABLE [dbo].[Account] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(50)  NOT NULL,
    [PassWord] varchar(50)  NOT NULL,
    [FullName] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'CashBill'
ALTER TABLE [dbo].[CashBill]
ADD CONSTRAINT [PK_CashBill]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CashBillDetail'
ALTER TABLE [dbo].[CashBillDetail]
ADD CONSTRAINT [PK_CashBillDetail]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Customer'
ALTER TABLE [dbo].[Customer]
ADD CONSTRAINT [PK_Customer]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'InstallmentBill'
ALTER TABLE [dbo].[InstallmentBill]
ADD CONSTRAINT [PK_InstallmentBill]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'InstallmentBillDetail'
ALTER TABLE [dbo].[InstallmentBillDetail]
ADD CONSTRAINT [PK_InstallmentBillDetail]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ProductType'
ALTER TABLE [dbo].[ProductType]
ADD CONSTRAINT [PK_ProductType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID], [UserName], [PassWord], [FullName] in table 'Account'
ALTER TABLE [dbo].[Account]
ADD CONSTRAINT [PK_Account]
    PRIMARY KEY CLUSTERED ([ID], [UserName], [PassWord], [FullName] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BillID] in table 'CashBillDetail'
ALTER TABLE [dbo].[CashBillDetail]
ADD CONSTRAINT [FK_CashBillDetail_CashBill]
    FOREIGN KEY ([BillID])
    REFERENCES [dbo].[CashBill]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CashBillDetail_CashBill'
CREATE INDEX [IX_FK_CashBillDetail_CashBill]
ON [dbo].[CashBillDetail]
    ([BillID]);
GO

-- Creating foreign key on [ProductID] in table 'CashBillDetail'
ALTER TABLE [dbo].[CashBillDetail]
ADD CONSTRAINT [FK_CashBillDetail_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CashBillDetail_Product'
CREATE INDEX [IX_FK_CashBillDetail_Product]
ON [dbo].[CashBillDetail]
    ([ProductID]);
GO

-- Creating foreign key on [CustomerID] in table 'InstallmentBill'
ALTER TABLE [dbo].[InstallmentBill]
ADD CONSTRAINT [FK_InstallmentBill_Customer]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[Customer]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstallmentBill_Customer'
CREATE INDEX [IX_FK_InstallmentBill_Customer]
ON [dbo].[InstallmentBill]
    ([CustomerID]);
GO

-- Creating foreign key on [BillID] in table 'InstallmentBillDetail'
ALTER TABLE [dbo].[InstallmentBillDetail]
ADD CONSTRAINT [FK_InstallmentBillDetail_InstallmentBill]
    FOREIGN KEY ([BillID])
    REFERENCES [dbo].[InstallmentBill]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstallmentBillDetail_InstallmentBill'
CREATE INDEX [IX_FK_InstallmentBillDetail_InstallmentBill]
ON [dbo].[InstallmentBillDetail]
    ([BillID]);
GO

-- Creating foreign key on [ProductID] in table 'InstallmentBillDetail'
ALTER TABLE [dbo].[InstallmentBillDetail]
ADD CONSTRAINT [FK_InstallmentBillDetail_Product]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Product]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstallmentBillDetail_Product'
CREATE INDEX [IX_FK_InstallmentBillDetail_Product]
ON [dbo].[InstallmentBillDetail]
    ([ProductID]);
GO

-- Creating foreign key on [ProductTypeID] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [FK_Product_ProductType]
    FOREIGN KEY ([ProductTypeID])
    REFERENCES [dbo].[ProductType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Product_ProductType'
CREATE INDEX [IX_FK_Product_ProductType]
ON [dbo].[Product]
    ([ProductTypeID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------