using Microsoft.Build.Framework;
using Proiect.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect
{
    public class User : Entity<int>
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string parola { get; set; }
    }
}
