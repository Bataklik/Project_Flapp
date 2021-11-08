-- Tankkaart
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Tankkaart](
   [tankkaartId][int] IDENTITY(1, 1) PRIMARY KEY,
   [geldigheidsdatum] [date] NOT NULL,
   [pincode] [varchar](50) NULL,
   [geblokkeerd] [bit] NOT NULL);