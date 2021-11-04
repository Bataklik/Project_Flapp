-- Rijbewijs_Bestuurder
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Rijbewijs_Bestuurder](
   [rijbewijsbestuurder_rijbewijs_id][int] FOREIGN KEY REFERENCES Rijbewijs(rijbewijs_id),
   [rijbewijsbestuurder_bestuurder_id][int] FOREIGN KEY REFERENCES Bestuurder(bestuurder_id),
   PRIMARY KEY (rijbewijsbestuurder_rijbewijs_id, rijbewijsbestuurder_bestuurder_id));