-- Brandstof_Tankkaart
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Brandstof_Tankkaart](
   [brandstoftankkaart_brandstof_id][int] FOREIGN KEY REFERENCES Brandstof(brandstof_id),
   [brandstoftankkaart_Tankkaart_id][int] FOREIGN KEY REFERENCES Tankkaart(tankkaart_id),
   PRIMARY KEY (brandstoftankkaart_brandstof_id, brandstoftankkaart_Tankkaart_id));