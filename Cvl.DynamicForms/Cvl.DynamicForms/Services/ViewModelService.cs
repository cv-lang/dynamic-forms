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
            var pg = new PropertyGridViewModel();

            var properties = propertyValueService.GetProperties(obj);
            pg.Properties = properties;

            return pg;
        }
    }
}
