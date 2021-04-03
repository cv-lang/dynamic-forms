using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Model.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.DynamicForms.Services
{
    public class GridViewModelParameters
    {
        public string IdPropertyName { get; set; } = "Id";
    }


    public class GridViewModelFactory
    {

        public GridViewModel GetGridViewModel(ICollection collection, GridViewModelParameters parameters)
        {
            var gv = new GridViewModel();

            gv.PropertyValue = $"{collection.Cast<object>().FirstOrDefault()?.GetType().Name}[{collection.Count}]";

            var first = collection.Cast<object>().FirstOrDefault();
            if(first == null)
            {
                return gv;
            }

            var type = first.GetType();
            var propertyInfos = type.GetProperties().Where(x=> x.Name != parameters.IdPropertyName).ToArray();
            var idProperty = type.GetProperty(parameters.IdPropertyName);

            bool isFirst = true;
            foreach (var element in collection)
            {                
                var row = new RowViewModel();
                row.Cells = new CellViewModel[propertyInfos.Length];
                row.Id = idProperty.GetValue(element)?.ToString();
                row.ElementTypeFullName = type.Name;

                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    var item = propertyInfos[i];
                    var value = item.GetValue(element);
                    var propType = CheckPropType(item.PropertyType);

                    if (isFirst)
                    {
                        var cvm = new ColumnViewModel() { BindingPath = item.Name, Header = item.Name };
                        gv.Columns.Add(cvm);
                    }

                    var ccvm = new CellViewModel() { Value = value };
                    row.Cells[i] = ccvm;
                }
                gv.Rows.Add(row);
                isFirst = false;
            }

            return gv;
        }

        private PropertyTypes CheckPropType(Type propertyType)
        {


            if (propertyType == typeof(bool))
                return PropertyTypes.Bool;
            if (propertyType.IsEnum)
                return PropertyTypes.Enum;
            if (propertyType == typeof(float))
                return PropertyTypes.Float;
            if (propertyType == typeof(int))
                return PropertyTypes.Int;
            if (propertyType == typeof(string))
                return PropertyTypes.String;

            if (typeof(IEnumerable).IsAssignableFrom(propertyType))
            {
                return PropertyTypes.Collection;
            }

            if (propertyType.IsClass)
            {
                return PropertyTypes.Class;
            }

            return PropertyTypes.Other;
        }

    }
}
