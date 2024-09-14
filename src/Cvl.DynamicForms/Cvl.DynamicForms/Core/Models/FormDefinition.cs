using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Models
{
    public class FormDefinition
    {
        public required string Name { get; set; }
        public required FormSection RootSection { get; set; }
    }
}
