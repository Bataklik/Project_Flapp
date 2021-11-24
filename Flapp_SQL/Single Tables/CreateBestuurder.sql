-- Bestuurder
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Bestuurder](
   [bestuurderId][int] IDENTITY(1, 1) PRIMARY KEY,
   [naam] [varchar](50) NOT NULL,
   [voornaam] [varchar](50) NOT NULL,
   [geboortedatum] [date] NOT NULL,
   [rijksregister] [varchar](15) NOT NULL,
   [adresId] [int] FOREIGN KEY REFERENCES dbo.Adres(adresId),
   [voertuigId] [int] FOREIGN KEY REFERENCES dbo.Voertuig(voertuigId),
   [tankkaartId] [int] FOREIGN KEY REFERENCES dbo.Tankkaart(tankkaartId),
   [geslacht] [bit] NOT NULL);