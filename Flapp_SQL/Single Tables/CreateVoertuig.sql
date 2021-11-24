-- Voertuig
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Voertuig](
   [voertuigId][int] IDENTITY(1, 1) PRIMARY KEY,
   [merk] [varchar](50) NOT NULL,
   [model] [varchar](50) NOT NULL,
   [chassisnummer] [varchar](17) NOT NULL,
   [nummerplaat] [varchar](9) NOT NULL,
   [type] [varchar](50) NOT NULL,
   [kleur] [varchar](50) NULL,
   [deuren] [int] NULL);