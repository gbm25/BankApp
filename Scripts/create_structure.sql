USE [master]
GO
CREATE DATABASE [bankappdb]
GO
USE [bankappdb];
GO
CREATE TABLE [dbo].[Customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [nvarchar](50) NULL,
	[last_name] [nvarchar](50) NULL,
	[username] [varchar](50) NULL,
	[password] [nvarchar](100) NULL,
	[country] [nvarchar](100) NULL,
	[region] [nvarchar](100) NULL,
	[city] [nvarchar](100) NULL,
	[address] [nvarchar](250) NULL,
	[last_update] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED (id)
);
GO
CREATE TABLE [dbo].[Account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer_id] [int] NOT NULL,
	[account_number] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED (id)
 );
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_id_customer_id] FOREIGN KEY([customer_id])
REFERENCES [dbo].[Customer] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_id_customer_id]
GO