USE[Project_Flapp_DB];
CREATE TABLE[dbo].[BrandstoftType_Voertuig](
   [brandstof_id][int] FOREIGN KEY REFERENCES Brandstof(id),
   [voertuig_id][int] FOREIGN KEY REFERENCES Voertuig(id),
   PRIMARY KEY (brandstof_id, voertuig_id));