-- Tankkaart
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Tankkaart](
   [tankkaart_id][int] IDENTITY(1, 1) PRIMARY KEY,
   [tankkaart_geldigheidsdatum] [date] NOT NULL,
   [tankkaart_pincode] [varchar](50) NULL,
   [geblokkeerd] [bit] NOT NULL);