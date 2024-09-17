using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models
{
    public abstract class Control
    {
        public required string Id { get; set; }
        public required string Name { get; set; }


    }
}
