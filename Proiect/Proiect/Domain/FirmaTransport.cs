﻿using Microsoft.Build.Framework;
using Proiect.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class Int32 : Entity<int>
    {
        [Required]
        public string nume { get; set; }
    }
}
