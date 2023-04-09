﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Rezervare : Entity<int>
    {
        [Required]
        public int idExcursie { get; set; }

        [Required]
        public int idPersoana { get; set; }

        [Required]
        public int nrBilete { get; set; }
    }
}
