-- Brandstof_Voertuig
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Brandstof_Voertuig](
   [brandstofId][int] FOREIGN KEY REFERENCES Brandstof(brandstofId),
   [voertuigId][int] FOREIGN KEY REFERENCES Voertuig(voertuigId),
   PRIMARY KEY (brandstofId, voertuigId));