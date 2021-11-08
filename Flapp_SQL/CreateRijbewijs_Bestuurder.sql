-- Rijbewijs_Bestuurder
USE[Project_Flapp_DB];
CREATE TABLE[dbo].[Rijbewijs_Bestuurder](
   [rijbewijsId][int] FOREIGN KEY REFERENCES Rijbewijs(rijbewijsId),
   [bestuurderId][int] FOREIGN KEY REFERENCES Bestuurder(bestuurderId),
   PRIMARY KEY (rijbewijsId, bestuurderId));