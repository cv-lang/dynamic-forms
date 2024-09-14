using Cvl.DynamicForms.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Core.Importer
{
    public interface IFormDefinitionImporter
    {
        FormDefinition LoadFromFile(string filename, int index);
    }
}
