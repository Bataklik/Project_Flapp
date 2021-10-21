USE[Project_Flapp_DB];
CREATE TABLE[dbo].[voertuig](
   [id][int] IDENTITY(1, 1) PRIMARY KEY,
   [merk] [varchar](50) NOT NULL,
   [model] [varchar](50) NOT NULL,
   [chassisnummer] [varchar](17) NOT NULL,
   [nummerplaat] [varchar](9) NOT NULL,
   [brandstof_type] [int] FOREIGN KEY REFERENCES dbo.Brandstof(id),
   [voertuig_type] [varchar](50) NOT NULL,
   [kleur] [varchar](50) NULL,
   [deuren] [int] NULL,
   [bestuurder_id] [int] FOREIGN KEY REFERENCES dbo.Bestuurder(id));