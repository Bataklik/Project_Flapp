-- Adres Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Adres] ([straat] ,[huisnummer] ,[stad] ,[postcode]) VALUES ('Keizerstraat' ,'1' ,'Gent' ,9000);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Adres] ([straat] ,[huisnummer] ,[stad] ,[postcode]) VALUES ('Stationstraat' ,'1' ,'Eke-Nazareth' ,9810);

-- Voertuig Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Voertuig] ([merk] ,[model] ,[chassisnummer] ,[nummerplaat] ,[type] ,[kleur] ,[deuren]) VALUES ('BMW' ,'Berline' ,'3GCUKREC7FG207394' ,'1-LIN-003' ,'Vier persoonswagen' ,'Blauw' ,4);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Voertuig] ([merk] ,[model] ,[chassisnummer] ,[nummerplaat] ,[type] ,[kleur] ,[deuren]) VALUES ('Tesla' ,'Model X' ,'JN1CA21D7ST071336' ,'1-ASN-010' ,'Vier persoonswagen' ,'Grijs' ,4);

-- Tankkaart Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Tankkaart] ([geldigheidsdatum] ,[pincode] ,[geblokkeerd]) VALUES ('01/02/2025' ,'1232' ,0);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Tankkaart] ([geldigheidsdatum] ,[pincode] ,[geblokkeerd]) VALUES ('01/02/2023' ,'1112' ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Tankkaart] ([geldigheidsdatum] ,[pincode] ,[geblokkeerd]) VALUES ('01/02/2020' ,'4442' ,0);

-- Bestuurder Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Bestuurder] ([naam] ,[voornaam] ,[geboortedatum] ,[rijksregister] ,[adresId] ,[voertuigId] ,[tankkaartId] ,[geslacht]) VALUES ('Declerck' ,'Tibo' ,'06/08/1999' ,'99.08.06-289.17' ,NULL ,NULL ,NULL ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Bestuurder] ([naam] ,[voornaam] ,[geboortedatum] ,[rijksregister] ,[adresId] ,[voertuigId] ,[tankkaartId] ,[geslacht]) VALUES ('Balci' ,'Burak' ,'12/05/1999' ,'99.05.12-289.17' ,2 ,1 ,1 ,1);

-- Brandstof Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof] ([naam]) VALUES ('Benzine'); 
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof] ([naam]) VALUES ('Elektrisch'); 

-- Rijbewijs Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs] ([naam]) VALUES ('B');
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs] ([naam]) VALUES ('A');

-- RijWijs_Bestuurder Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs_Bestuurder] ([rijbewijsId] ,[bestuurderId]) VALUES (1 ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs_Bestuurder] ([rijbewijsId] ,[bestuurderId]) VALUES (2 ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Rijbewijs_Bestuurder] ([rijbewijsId] ,[bestuurderId]) VALUES (1 ,2);

-- Brandstof_Tankkaart Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof_Tankkaart] ([brandstofId] ,[tankkaartId]) VALUES (1 ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof_Tankkaart] ([brandstofId] ,[tankkaartId]) VALUES (2 ,3);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof_Tankkaart] ([brandstofId] ,[tankkaartId]) VALUES (2 ,1);

-- Brandstof_Voertuig Insert
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof_Voertuig] ([brandstofId] ,[voertuigId]) VALUES (1 ,1);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof_Voertuig] ([brandstofId] ,[voertuigId]) VALUES (2 ,2);
USE [Project_Flapp_DB]; INSERT INTO [dbo].[Brandstof_Voertuig] ([brandstofId] ,[voertuigId]) VALUES (1 ,2);
