USE [EFCoreDefaultIfEmpty_Max_Bug];
GO
CREATE SCHEMA [ext]
GO
CREATE TABLE [ext].[tmpExtProduct](
	[Version] [int] NOT NULL PRIMARY KEY,
	[CategoryId] [int] NOT NULL
)
INSERT INTO [ext].[tmpExtProduct]([Version],[CategoryId])
VALUES(1,1),
      (2,1);

