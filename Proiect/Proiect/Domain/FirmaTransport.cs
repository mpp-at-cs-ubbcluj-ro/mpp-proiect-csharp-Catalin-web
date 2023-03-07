using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain
{
    public class FirmaTransport : Entity<string>
    {
        [Required]
        public string nume { get; set; }
    }
}
