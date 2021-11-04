-- Brandstof_Voertuig
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Brandstof_Voertuig](
   [brandstofvoertuig_brandstof_id][int] FOREIGN KEY REFERENCES Brandstof(brandstof_id),
   [brandstofvoertuig_voertuig_id][int] FOREIGN KEY REFERENCES Voertuig(voertuig_id),
   PRIMARY KEY (brandstofvoertuig_brandstof_id, brandstofvoertuig_voertuig_id));