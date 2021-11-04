-- Tankkaart
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Tankkaart](
   [tankkaart_id][int] IDENTITY(1, 1) PRIMARY KEY,
   [tankkaart_geldigheidsdatum] [date] NOT NULL,
   [tankkaart_pincode] [varchar](50) NULL,
   [tankkaart_brandstof_id] [int] NULL FOREIGN KEY REFERENCES dbo.Brandstof(brandstof_id),
   [geblokkeerd] [bit] NOT NULL);