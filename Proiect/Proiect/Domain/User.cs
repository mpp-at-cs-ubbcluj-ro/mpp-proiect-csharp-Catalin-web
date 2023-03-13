using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain
{
    public class User : Entity<string>
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string parola { get; set; }
    }
}
