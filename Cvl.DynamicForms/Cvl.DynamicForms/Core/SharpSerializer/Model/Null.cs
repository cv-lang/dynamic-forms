using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cvl.DynamicForms.Core.SharpSerializer.Model
{
    public class Null : BaseObject
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        public override void Render(IRenderer renderer)
        {
            renderer.RenderSimpleProperty(Name, "null", "null");
        }
    }
}
