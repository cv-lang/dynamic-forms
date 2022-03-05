using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cvl.DynamicForms.Core.SharpSerializer.Model
{
    public interface IRenderer
    {
        void BeginCollection();
        void EndCollection();
        void BeginObject(string name, string type);
        void EndObject(string name, string type);
        void RenderSimpleProperty(string name, string type, string value);
    }

    public abstract class BaseObject
    {
        [XmlAttribute("id")]
        public long Id { get; set; }

        public abstract void Render(IRenderer renderer);
    }
}
