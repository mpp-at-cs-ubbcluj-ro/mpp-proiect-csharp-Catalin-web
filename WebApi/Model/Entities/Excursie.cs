using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Excursie : Entity<int>
    {
        [Required]
        public int idObiectiv { get; set; }

        [Required]
        public int idFirmaTransport { get; set; }

        [Required]
        public string ora { get; set; }

        [Required]
        public float pret { get; set; }

        [Required]
        public int nrLocuriTotale { get; set; }
    }
}
