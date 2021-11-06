-- Adres Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Adres] ([adres_straat] ,[adres_huisnummer] ,[adres_stad] ,[adres_postcode]) VALUES ('Keizerstraat' ,'1' ,'Gent' ,9000);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Adres] ([adres_straat] ,[adres_huisnummer] ,[adres_stad] ,[adres_postcode]) VALUES ('Stationstraat' ,'1' ,'Eke-Nazareth' ,9810);

-- Bestuurder Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Bestuurder] ([bestuurder_naam] ,[bestuurder_voornaam] ,[bestuurder_geboortedatum] ,[bestuurder_rijksregister] ,[bestuurder_adres_id] ,[bestuurder_voertuig_id] ,[bestuurder_tankkaart_id] ,[bestuurder_geslacht]) VALUES ('Declerck' ,'Tibo' ,'06/08/1999' ,'99.08.06-289.17' ,NULL ,NULL ,NULL ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Bestuurder] ([bestuurder_naam] ,[bestuurder_voornaam] ,[bestuurder_geboortedatum] ,[bestuurder_rijksregister] ,[bestuurder_adres_id] ,[bestuurder_voertuig_id] ,[bestuurder_tankkaart_id] ,[bestuurder_geslacht]) VALUES ('Balci' ,'Burak' ,'12/05/1999' ,'99.05.12-289.17' ,2 ,NULL ,NULL ,1);

-- Brandstof Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof] ([brandstof_naam]) VALUES ('Benzine'); 
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof] ([brandstof_naam]) VALUES ('LPG'); 

-- Rijbewijs Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs] ([rijbewijs_naam]) VALUES ('B');
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs] ([rijbewijs_naam]) VALUES ('A');

-- RijWijs_Bestuurder Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs_Bestuurder] ([rijbewijsbestuurder_rijbewijs_id] ,[rijbewijsbestuurder_bestuurder_id]) VALUES (1 ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs_Bestuurder] ([rijbewijsbestuurder_rijbewijs_id] ,[rijbewijsbestuurder_bestuurder_id]) VALUES (2 ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs_Bestuurder] ([rijbewijsbestuurder_rijbewijs_id] ,[rijbewijsbestuurder_bestuurder_id]) VALUES (1 ,2);


