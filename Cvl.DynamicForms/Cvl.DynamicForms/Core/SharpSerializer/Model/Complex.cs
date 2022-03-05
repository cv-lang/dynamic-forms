using System.Xml.Serialization;

namespace Cvl.DynamicForms.Core.SharpSerializer.Model
{
    public class Complex : BaseObject
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlArray("Properties")]
        [XmlArrayItem("Simple", typeof(Simple))]
        [XmlArrayItem("Complex", typeof(Complex))]
        [XmlArrayItem("Null", typeof(Null))]
        [XmlArrayItem("Collection", typeof(Collection))]
        public BaseObject[] Properties { get; set; }


        public override void Render(IRenderer renderer)
        {
            renderer.BeginObject(Name, Type);

            renderer.BeginCollection();
            foreach (var baseObject in Properties)
            {
                baseObject.Render(renderer);
            }
            renderer.EndCollection();

            renderer.EndObject(Name, Type);
        }
    }
}
