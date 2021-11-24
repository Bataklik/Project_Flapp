-- Brandstof_Tankkaart
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Brandstof_Tankkaart](
   [brandstofId][int] FOREIGN KEY REFERENCES Brandstof(brandstofId),
   [tankkaartId][int] FOREIGN KEY REFERENCES Tankkaart(tankkaartId),
   PRIMARY KEY (brandstofId, tankkaartId));