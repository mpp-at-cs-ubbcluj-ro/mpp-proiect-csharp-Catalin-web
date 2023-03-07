using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain
{
    public class Excursie : Entity<string>
    {
        [Required]
        public string idObiectiv { get; set; }
        
        [Required]
        public string idObiectiv2 { get; set; }

        [Required]
        public TimeSpan ora { get; set; }

        [Required]
        public float pret { get; set; }
        
        [Required]
        public int nrLocuriTotale { get; set; }
    }
}
