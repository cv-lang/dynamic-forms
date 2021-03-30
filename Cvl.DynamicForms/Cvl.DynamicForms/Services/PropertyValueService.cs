using Cvl.DynamicForms.Base;
using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Tools.Extension;
using Cvl.DynamicForms.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Cvl.DynamicForms.Test;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cvl.DynamicForms.Services
{
    public class PropertyValueService
    {
        public List<PropertyViewModel> GetProperties(object obj)
        {
            var list = new List<PropertyViewModel>();

            if (obj is ObjectXmlWrapper wrapper)
            {
                object person = DeserializeXml(wrapper.Xml);
                var type = person.GetType();
                var props = type.GetProperties();

                foreach (var item in props)
                {
                    var value = GetPropertyValue(person, item);

                    var pvm = new Base.PropertyViewModel() { Type = CheckPropType(value), Header = item.Name, BindingPath = item.Name, Value = value };
                    pvm.Order = item.GetPropertyOrder();
                    pvm.Description = item.GetPropertyDescription();
                    pvm.Group = item.GetPropertyGroup();

                    list.Add(pvm);
                }

            }
            else
            {
                var type = obj.GetType();
                var props = type.GetProperties();

                foreach (var item in props)
                {
                    var value = GetPropertyValue(obj, item);
                    var pvm = new Base.PropertyViewModel() { Type = CheckPropType(value), Header = item.Name, BindingPath = item.Name, Value = value };
                    pvm.Order = item.GetPropertyOrder();
                    pvm.Description = item.GetPropertyDescription();
                    pvm.Group = item.GetPropertyGroup();

                    list.Add(pvm);
                }
            }

            return list;
        }

        private PropertyTypes CheckPropType(object item)
        {
            var type = item.GetType();

            if (item is bool)
                return PropertyTypes.Bool;
            if (item is Enum)
                return PropertyTypes.Enum;
            if (item is float)
                return PropertyTypes.Float;
            if (item is int)
                return PropertyTypes.Int;
            if (item is string)

                return PropertyTypes.String;
            return PropertyTypes.Other;
        }

        private object DeserializeXml(string xml)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(xml);
            MemoryStream stream = new MemoryStream(byteArray);
            var xmlReader = XElement.Load(stream).CreateReader();
            xmlReader.MoveToContent();
            string innerXml = xmlReader.ReadInnerXml();

            byte[] byteArray1 = Encoding.UTF8.GetBytes(innerXml);
            MemoryStream stream1 = new MemoryStream(byteArray1);
            TestPerson person;
            XmlSerializer serializer = new XmlSerializer(typeof(TestPerson));
            person = (TestPerson)serializer.Deserialize(stream1);
            return person;
        }

        private object GetPropertyValue(object obj, System.Reflection.PropertyInfo item)
        {
            return item.GetValue(obj);
        }
    }
}
