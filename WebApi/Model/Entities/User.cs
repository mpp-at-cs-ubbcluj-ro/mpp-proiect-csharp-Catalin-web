using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class User : Entity<int>
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string parola { get; set; }
    }
}
