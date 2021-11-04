-- Bestuurder
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Bestuurder](
   [bestuurder_id][int] IDENTITY(1, 1) PRIMARY KEY,
   [bestuurder_naam] [varchar](50) NOT NULL,
   [bestuurder_voornaam] [varchar](50) NOT NULL,
   [bestuurder_geboortedatum] [date] NOT NULL,
   [bestuurder_rijksregister] [varchar](15) NOT NULL,
   [bestuurder_adres_id] [int] FOREIGN KEY REFERENCES dbo.Adres(adres_id),
   [bestuurder_voertuig_id] [int] FOREIGN KEY REFERENCES dbo.Voertuig(voertuig_id),
   [bestuurder_tankkaart_id] [int] FOREIGN KEY REFERENCES dbo.Tankkaart(tankkaart_id),
   [bestuurder_geslacht] [bit] NOT NULL);