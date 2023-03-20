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
        public int idExcursie { get; set; }

        [Required]
        public int idPersoana { get; set; }

        [Required]
        public int nrBilete { get; set; }
    }
}
