using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.DynamicForms.Services
{
    public class ViewModelService
    {
        private readonly PropertyValueService propertyValueService;

        public ViewModelService(PropertyValueService propertyValueService)
        {
            this.propertyValueService = propertyValueService;
        }

        public PropertyGridViewModel GetPropertyGrid(object obj, Parameters parameters)
        {
            var type = obj.GetType();
            var properties = type.GetProperties();
            var pg = new PropertyGridViewModel();

            foreach (var item in properties)
            {
                var value = item.GetValue(obj);
                var pvm = new Base.PropertyViewModel() { Header = item.Name, BindingPath = item.Name, Value = value };
                pvm.Order = item.GetPropertyOrder();
                pvm.Description = item.GetPropertyDescription();
                pvm.Group = item.GetPropertyGroup();
                pg.Properties.Add(pvm);
            }

            return pg;
        }
    }
}
