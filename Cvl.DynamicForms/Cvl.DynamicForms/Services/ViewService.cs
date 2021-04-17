using Cvl.DynamicForms.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.DynamicForms.Services
{
    public class ViewService
    {
        private readonly DataServiceBase dataService;
        private readonly PropretyGridService propretyGridService;
        private readonly GridService gridService;

        public ViewService(DataServiceBase dataService, PropretyGridService propretyGridService, GridService gridService)
        {
            this.dataService = dataService;
            this.propretyGridService = propretyGridService;
            this.gridService = gridService;
        }

        public PropertyBaseVM GetViewModel(string objectIdStr, string typeFullname, string bindingPath)
        {
            var obj = dataService.GetObject(objectIdStr, typeFullname, bindingPath);

            if (obj is IEnumerable collection)
            {
                return gridService.GetGridViewModelForObject(collection.Cast<object>().AsQueryable(),objectIdStr, typeFullname, bindingPath,  new CollectionViewModelParameters());
            }
            else
            {
                return propretyGridService.GetPropertyGrid(objectIdStr, typeFullname, bindingPath);
            }
        }
    }
}
