using Cvl.DynamicForms.Model;
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
                pg.Properties.Add(new Base.PropertyViewModel() { Header = item.Name, BindingPath = item.Name, Value = value });
            }

            return pg;
        }
    }
}
