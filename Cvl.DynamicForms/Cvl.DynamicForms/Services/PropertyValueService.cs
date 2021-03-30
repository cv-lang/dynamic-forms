using Cvl.DynamicForms.Base;
using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.DynamicForms.Services
{
    public class PropertyValueService
    {
        public List<PropertyViewModel> GetProperties(object obj)
        {
            var list = new List<PropertyViewModel>();

            if (obj is ObjectXmlWrapper wrapper)
            {
                //tu parsowanie xmla
                //wrapper.Xml
            }
            else
            {
                var type = obj.GetType();
                var props = type.GetProperties();

                foreach (var item in props)
                {
                    var value = GetPropertyValue(obj, item);

                    var pvm = new Base.PropertyViewModel() { Header = item.Name, BindingPath = item.Name, Value = value };
                    pvm.Order = item.GetPropertyOrder();
                    pvm.Description = item.GetPropertyDescription();
                    pvm.Group = item.GetPropertyGroup();

                    list.Add(pvm);
                }
            }

            return list;
        }

        private object GetPropertyValue(object obj, System.Reflection.PropertyInfo item)
        {
            return item.GetValue(obj);
        }
    }
}
