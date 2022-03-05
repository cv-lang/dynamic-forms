using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cvl.DynamicForms.Core.SharpSerializer.Model
{
    public class Dictionary : BaseObject
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlArray("Items")]
        [XmlArrayItem("Simple", typeof(Simple))]
        [XmlArrayItem("Complex", typeof(Complex))]
        [XmlArrayItem("Null", typeof(Null))]
        [XmlArrayItem("Reference", typeof(Reference))]
        [XmlArrayItem("Dictionary", typeof(Dictionary))]
        [XmlArrayItem("Collection", typeof(Collection))]
        public BaseObject[] Items { get; set; }

        public override void Render(IRenderer renderer)
        {
            renderer.BeginObject(Name, "Dictionary");

            renderer.BeginCollection();
            foreach (var baseObject in Items)
            {
                baseObject.Render(renderer);
            }
            renderer.EndCollection();

            renderer.EndObject(Name, "Dictionary");
        }
    }
}
