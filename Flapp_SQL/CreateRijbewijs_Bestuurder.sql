USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Rijbewijs_Bestuurder](
   [rijbewijs_id][int] FOREIGN KEY REFERENCES Rijbewijs(id),
   [bestuurder_id][int] FOREIGN KEY REFERENCES Bestuurder(id),
   PRIMARY KEY (rijbewijs_id, bestuurder_id));