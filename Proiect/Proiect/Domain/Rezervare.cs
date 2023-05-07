using Microsoft.Build.Framework;
using Proiect.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class Rezervare : Entity<int>
    {
        [Required]
        public int id_excursie { get; set; }

        [Required]
        public int id_persoana { get; set; }

        [Required]
        public int nr_bilete { get; set; }
    }
}
