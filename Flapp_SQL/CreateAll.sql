-- Rijbewijs
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Rijbewijs](
   [rijbewijs_id][int] IDENTITY(1, 1) PRIMARY KEY,
   [rijbewijs_naam] [varchar](5));

-- Adres
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Adres](
   [adres_id][int] IDENTITY(1, 1) PRIMARY KEY,
   [adres_straat] [varchar](50) NOT NULL,
   [adres_huisnummer] [varchar](50) NOT NULL,
   [adres_stad] [varchar](50) NOT NULL,
   [adres_postcode] [int] NOT NULL);

-- Brandstof
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Brandstof](
   [brandstof_id][int] IDENTITY(1, 1) PRIMARY KEY,
   [brandstof_naam] [varchar](20));

-- Tankkaart
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Tankkaart](
   [tankkaart_id][int] IDENTITY(1, 1) PRIMARY KEY,
   [tankkaart_geldigheidsdatum] [date] NOT NULL,
   [tankkaart_pincode] [varchar](50) NULL,
   [geblokkeerd] [bit] NOT NULL);

-- Voertuig
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Voertuig](
   [voertuig_id][int] IDENTITY(1, 1) PRIMARY KEY,
   [voertuig_merk] [varchar](50) NOT NULL,
   [voertuig_model] [varchar](50) NOT NULL,
   [voertuig_chassisnummer] [varchar](17) NOT NULL,
   [voertuig_nummerplaat] [varchar](9) NOT NULL,
   [voertuig_type] [varchar](50) NOT NULL,
   [voertuig_kleur] [varchar](50) NULL,
   [voertuig_deuren] [int] NULL);

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

-- Rijbewijs_Bestuurder
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Rijbewijs_Bestuurder](
   [rijbewijsbestuurder_rijbewijs_id][int] FOREIGN KEY REFERENCES Rijbewijs(rijbewijs_id),
   [rijbewijsbestuurder_bestuurder_id][int] FOREIGN KEY REFERENCES Bestuurder(bestuurder_id),
   PRIMARY KEY (rijbewijsbestuurder_rijbewijs_id, rijbewijsbestuurder_bestuurder_id));
   
   -- Brandstof_Voertuig
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Brandstof_Voertuig](
   [brandstofvoertuig_brandstof_id][int] FOREIGN KEY REFERENCES Brandstof(brandstof_id),
   [brandstofvoertuig_voertuig_id][int] FOREIGN KEY REFERENCES Voertuig(voertuig_id),
   PRIMARY KEY (brandstofvoertuig_brandstof_id, brandstofvoertuig_voertuig_id));

-- Brandstof_Tankkaart
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Brandstof_Tankkaart](
   [brandstoftankkaart_brandstof_id][int] FOREIGN KEY REFERENCES Brandstof(brandstof_id),
   [brandstoftankkaart_Tankkaart_id][int] FOREIGN KEY REFERENCES Tankkaart(tankkaart_id),
   PRIMARY KEY (brandstoftankkaart_brandstof_id, brandstoftankkaart_Tankkaart_id));