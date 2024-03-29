﻿using Microsoft.Build.Framework;
using Proiect.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class Excursie : Entity<int>
    {
        [Required]
        public int id_obiectiv { get; set; }
        
        [Required]
        public int id_firma_transport { get; set; }

        [Required]
        public int ora { get; set; }

        [Required]
        public float pret { get; set; }
        
        [Required]
        public int nr_locuri_totale { get; set; }
    }
}
