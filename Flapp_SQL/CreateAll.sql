USE[Project_Flapp_DB];
-- ALLE TABLES MOETEN WEG ZIJN VOOR EEN TABLE TE MAKEN!
-- Rijbewijs
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rijbewijs]') AND type in (N'U'))
   BEGIN
	   CREATE TABLE[dbo].[Rijbewijs](
	   [rijbewijsId][int] IDENTITY(1, 1) PRIMARY KEY,
	   [naam] [varchar](5));
   END
   
-- Adres
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Adres]') AND type in (N'U'))
BEGIN
CREATE TABLE[dbo].[Adres](
   [adresId][int] IDENTITY(1, 1) PRIMARY KEY,
   [straat] [varchar](50) NOT NULL,
   [huisnummer] [varchar](50) NOT NULL,
   [stad] [varchar](50) NOT NULL,
   [postcode] [int] NOT NULL);
END

-- Brandstof
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Brandstof]') AND type in (N'U'))
BEGIN
CREATE TABLE[dbo].[Brandstof](
   [brandstofId][int] IDENTITY(1, 1) PRIMARY KEY,
   [naam] [varchar](20));
END

-- Tankkaart
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tankkaart]') AND type in (N'U'))
BEGIN
CREATE TABLE[dbo].[Tankkaart](
   [tankkaartId][int] IDENTITY(1, 1) PRIMARY KEY,
   [geldigheidsdatum] [date] NOT NULL,
   [pincode] [varchar](50) NULL,
   [geblokkeerd] [bit] NOT NULL);
   SET DATEFORMAT DMY
END
-- voertuigType
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VoertuigType]') AND type in (N'U'))
BEGIN
CREATE TABLE[dbo].[VoertuigType](
   [voertuigTypeId][int] IDENTITY(1, 1) PRIMARY KEY,
   [typeNaam] [varchar](50) NOT NULL); 
END
-- Voertuig
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Voertuig]') AND type in (N'U'))
BEGIN
CREATE TABLE[dbo].[Voertuig](
   [voertuigId][int] IDENTITY(1, 1) PRIMARY KEY,
   [merk] [varchar](50) NOT NULL,
   [model] [varchar](50) NOT NULL,
   [chassisnummer] [varchar](17) NOT NULL,
   [nummerplaat] [varchar](9) NOT NULL,
   [type] [int] FOREIGN KEY REFERENCES dbo.VoertuigType(voertuigTypeId),
   [kleur] [varchar](50) NULL,
   [deuren] [int] NULL);
END

-- Bestuurder
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bestuurder]') AND type in (N'U'))
BEGIN
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
   SET DATEFORMAT DMY
END

-- Rijbewijs_Bestuurder
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rijbewijs_Bestuurder]') AND type in (N'U'))
BEGIN
CREATE TABLE[dbo].[Rijbewijs_Bestuurder](
   [rijbewijsId][int] FOREIGN KEY REFERENCES Rijbewijs(rijbewijsId),
   [bestuurderId][int] FOREIGN KEY REFERENCES Bestuurder(bestuurderId),
   PRIMARY KEY (rijbewijsId, bestuurderId));
END

-- Brandstof_Voertuig
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Brandstof_Voertuig]') AND type in (N'U'))
BEGIN
CREATE TABLE[dbo].[Brandstof_Voertuig](
   [brandstofId][int] FOREIGN KEY REFERENCES Brandstof(brandstofId),
   [voertuigId][int] FOREIGN KEY REFERENCES Voertuig(voertuigId),
   PRIMARY KEY (brandstofId, voertuigId));
END

-- Brandstof_Tankkaart
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Brandstof_Tankkaart]') AND type in (N'U'))
BEGIN
CREATE TABLE[dbo].[Brandstof_Tankkaart](
   [brandstofId][int] FOREIGN KEY REFERENCES Brandstof(brandstofId),
   [tankkaartId][int] FOREIGN KEY REFERENCES Tankkaart(tankkaartId),
   PRIMARY KEY (brandstofId, tankkaartId));
END
