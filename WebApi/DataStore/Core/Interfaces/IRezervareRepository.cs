﻿using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStore.Core
{
    public interface IRezervareRepository : Repository<int, Rezervare>
    {
    }
}