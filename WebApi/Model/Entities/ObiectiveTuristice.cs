using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class ObiectiveTuristice : Entity<int>
    {
        [Required]
        public string nume { get; set; }
    }
}
