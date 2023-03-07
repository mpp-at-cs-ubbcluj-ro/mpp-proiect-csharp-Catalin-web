using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.Domain
{
    public class Entity<Type>
    {
        [Required]
        public Type id { get; set; }
    }
}
