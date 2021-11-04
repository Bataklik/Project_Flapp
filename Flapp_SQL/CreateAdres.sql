-- Adres
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Adres](
   [adres_id][int] IDENTITY(1, 1) PRIMARY KEY,
   [adres_straat] [varchar](50) NOT NULL,
   [adres_huisnummer] [varchar](50) NOT NULL,
   [adres_stad] [varchar](50) NOT NULL,
   [adres_postcode] [int] NOT NULL);