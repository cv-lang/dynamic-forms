using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Cvl.DynamicForms.Core.SharpSerializer.Model;

namespace Cvl.DynamicForms.Core.SharpSerializer
{
    public interface ISharpSerializerXmlToModelDeserializer
    {
        Complex DeserializeXml(string sharpSerializerXml);
    }

    public class SharpSerializerXmlToModelDeserializer : ISharpSerializerXmlToModelDeserializer
    {
        public Complex DeserializeXml(string sharpSerializerXml)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(sharpSerializerXml);
            MemoryStream stream = new MemoryStream(byteArray);
            Complex? complex;
            XmlSerializer serializer = new XmlSerializer(typeof(Complex));
            complex = (Complex?)serializer.Deserialize(stream);
            return complex ?? throw new Exception("Couldn't deserialize xml to model");
        }
    }
}
