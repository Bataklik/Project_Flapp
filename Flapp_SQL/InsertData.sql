-- Adres Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Adres] ([straat] ,[huisnummer] ,[stad] ,[postcode]) VALUES ('Keizerstraat' ,'1' ,'Gent' ,9000);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Adres] ([straat] ,[huisnummer] ,[stad] ,[postcode]) VALUES ('Stationstraat' ,'1' ,'Eke-Nazareth' ,9810);

-- Bestuurder Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Bestuurder] ([naam] ,[voornaam] ,[geboortedatum] ,[rijksregister] ,[adresId] ,[voertuig_Id] ,[tankkaartId] ,[geslacht]) VALUES ('Declerck' ,'Tibo' ,'06/08/1999' ,'99.08.06-289.17' ,NULL ,NULL ,NULL ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Bestuurder] ([naam] ,[voornaam] ,[geboortedatum] ,[rijksregister] ,[adresId] ,[voertuigId] ,[tankkaartId] ,[geslacht]) VALUES ('Balci' ,'Burak' ,'12/05/1999' ,'99.05.12-289.17' ,2 ,NULL ,NULL ,1);

-- Brandstof Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof] ([naam]) VALUES ('Benzine'); 
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof] ([naam]) VALUES ('LPG'); 

-- Rijbewijs Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs] ([naam]) VALUES ('B');
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs] ([naam]) VALUES ('A');

-- RijWijs_Bestuurder Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs_Bestuurder] ([rijbewijsId] ,[bestuurderId]) VALUES (1 ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs_Bestuurder] ([rijbewijsId] ,[bestuurderId]) VALUES (2 ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs_Bestuurder] ([rijbewijsId] ,[bestuurderId]) VALUES (1 ,2);


