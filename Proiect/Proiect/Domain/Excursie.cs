using Microsoft.Build.Framework;
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
        public int idObiectiv { get; set; }
        
        [Required]
        public int idFirmaTransport { get; set; }

        [Required]
        public TimeSpan ora { get; set; }

        [Required]
        public float pret { get; set; }
        
        [Required]
        public int nrLocuriTotale { get; set; }
    }
}
