using System;
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
        public int id_excursie { get; set; }

        [Required]
        public int id_persoana { get; set; }

        [Required]
        public int nr_bilete { get; set; }
    }
}
