-- Voertuig
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Voertuig](
   [voertuig_id][int] IDENTITY(1, 1) PRIMARY KEY,
   [voertuig_merk] [varchar](50) NOT NULL,
   [voertuig_model] [varchar](50) NOT NULL,
   [voertuig_chassisnummer] [varchar](17) NOT NULL,
   [voertuig_nummerplaat] [varchar](9) NOT NULL,
   [voertuig_brandstof_id] [int] FOREIGN KEY REFERENCES dbo.Brandstof(brandstof_id),
   [voertuig_type] [varchar](50) NOT NULL,
   [voertuig_kleur] [varchar](50) NULL,
   [voertuig_deuren] [int] NULL);