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

namespace Cvl.DynamicForms.Services
{
    public class PropertyValueService
    {
        public List<PropertyViewModel> GetProperties(object obj)
        {
            var list = new List<PropertyViewModel>();

            if (obj is ObjectXmlWrapper wrapper)
            {
                var personObject = Serializer.DeserializeObject<TestPerson>(wrapper.Xml);
                var type = personObject.GetType();
                var props = type.GetProperties();
                foreach (var item in props)
                {
                    var value = GetPropertyValue(personObject, item);

                    var pvm = new Base.PropertyViewModel() { Header = item.Name, BindingPath = item.Name, Value = value };
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

        private object GetPropertyValue(object obj, System.Reflection.PropertyInfo item)
        {
            return item.GetValue(obj);
        }
    }
}
