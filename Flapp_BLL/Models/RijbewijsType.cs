﻿using Flapp_BLL.Exceptions.ModelExpections;

namespace Flapp_BLL.Models
{
    public class RijbewijsType
    {
        #region Props
        public int Id { get; private set; }
        public string Naam { get; private set; }
        #endregion

        #region Constructor
        public RijbewijsType(int id, string naam)
        {
            ZetId(id);
            ZetNaam(naam);
        }
        public RijbewijsType(string naam)
        {
            ZetNaam(naam);
        }
        #endregion

        #region Zetmethods
        public void ZetId(int id)
        {
            if (id <= 0) { throw new RijbewijsTypeException("RijbewijsType: ZetId: Id moet positief zijn!"); }
            Id = id;
        }
        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) { throw new RijbewijsTypeException("RijbewijsType: ZetNaam: Naam mag niet leeg zijn!"); }
            Naam = naam;
        }
        #endregion
    }
}

//USE[Project_Flapp_DB];
//CREATE TABLE[dbo].[Rijbewijs](
//   [id][int] IDENTITY(1, 1) PRIMARY KEY,
//   [rijbewijs_naam] [varchar](5))