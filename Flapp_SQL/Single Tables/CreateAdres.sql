-- Adres
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Adres](
   [adresId][int] IDENTITY(1, 1) PRIMARY KEY,
   [straat] [varchar](50) NOT NULL,
   [huisnummer] [varchar](50) NOT NULL,
   [stad] [varchar](50) NOT NULL,
   [postcode] [int] NOT NULL);