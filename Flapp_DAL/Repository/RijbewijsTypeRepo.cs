﻿using Flapp_BLL.Interfaces;
using Flapp_BLL.Models;
using System;

namespace Flapp_DAL.Repository
{
    public class RijbewijsTypeRepo : IRijbewijsTypeRepo
    {
        private string _connString;

        public RijbewijsTypeRepo(string connString)
        {
            _connString = connString;
        }

        public RijbewijsType GeefRijbewijs(int rId)
        {
            throw new NotImplementedException();
        }
    }
}
