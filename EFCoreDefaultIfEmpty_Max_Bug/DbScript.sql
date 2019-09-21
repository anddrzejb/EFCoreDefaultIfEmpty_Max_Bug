USE [EFCoreDefaultIfEmpty_Max_Bug];
GO
CREATE SCHEMA [ext]
GO
CREATE TABLE [ext].[tmpExtCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](10) NULL)
CREATE TABLE [ext].[tmpExtProduct](
	[Version] [int] NOT NULL PRIMARY KEY,
	[ImporDate] [date] NULL,
	[CategoryId] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	CONSTRAINT [FK_tmpExtProduct_tmpExtCategory] FOREIGN KEY ([CategoryId]) REFERENCES [ext].[tmpExtCategory]([Id])
)
INSERT INTO [ext].[tmpExtCategory]([Name])
VALUES(N'Engines'),
      (N'Body'),
      (N'Electrical'),
      (N'Aircon');
INSERT INTO [ext].[tmpExtProduct]([Version],[ImporDate],[CategoryId],[Description])
VALUES(1,GETDATE(),1,N'Decr1'),
      (2,GETDATE(),1,N'Decr34f');

