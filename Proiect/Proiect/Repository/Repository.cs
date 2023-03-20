﻿using Proiect.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public interface Repository<ID, entity> where entity : Entity<int>
    {
        void adauga(entity entity);
        void sterge(entity entity);
        entity cautaId(int id);
        List<entity> getAll();
        void update(entity entitate, entity nouaEntitate);
    }
}
