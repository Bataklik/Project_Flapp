using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flapp_BLL.Models
{
    public enum RijbewijsType
    {
        A, AM, B, C, D, G
    }
}

//USE[Project_Flapp_DB];
//CREATE TABLE[dbo].[Rijbewijs](
//   [id][int] IDENTITY(1, 1) PRIMARY KEY,
//   [rijbewijs_naam] [varchar](5))