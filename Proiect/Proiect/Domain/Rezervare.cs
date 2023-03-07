using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain
{
    public class Rezervare : Entity<string>
    {
        [Required]
        public string idExcursie { get; set; }

        [Required]
        public string idPersoana { get; set; }

        [Required]
        public int nrBilete { get; set; }
    }
}
