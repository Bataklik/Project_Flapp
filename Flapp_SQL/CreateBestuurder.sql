USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Bestuurder](
   [id][int] IDENTITY(1, 1) PRIMARY KEY,
   [naam] [varchar](50) NOT NULL,
   [voornaam] [varchar](50) NOT NULL,
   [geboortedatum] [date] NOT NULL,
   [rijksregister] [varchar](15) NOT NULL,
   [adres_id] [int] FOREIGN KEY REFERENCES dbo.Adres(id),
   [voertuig_id] [int] FOREIGN KEY REFERENCES dbo.Voertuig(id),
   [tankkaart_id] [int] FOREIGN KEY REFERENCES dbo.Tankkaart(id),
   [geslacht] [bit] NOT NULL)