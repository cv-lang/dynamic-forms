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
        public int PageSize { get; set; } = 200;
        public int Page { get; set; } = 0;
    }


    public class GridViewModelFactory
    {

        public GridViewModel GetGridViewModel(IQueryable<object> collection, GridViewModelParameters parameters)
        {
            var gv = new GridViewModel();

            gv.PropertyValue = $"{collection.Cast<object>().FirstOrDefault()?.GetType().Name}[{collection.Count()}]";

            var firstElement = collection.Cast<object>().FirstOrDefault();
            if(firstElement == null)
            {
                return gv;
            }

            var elementType = firstElement.GetType();
            var elementIdPropertyName = getIdPropertyName(elementType);
            var propertyInfos = elementType.GetProperties().Where(x=> x.Name != elementIdPropertyName).ToArray();
            var idProperty = elementType.GetProperty(elementIdPropertyName);

            bool isFirst = true;
            var page = collection.Skip(parameters.Page * parameters.PageSize).Take(parameters.PageSize);

            foreach (var element in page)
            {                
                var row = new RowViewModel();
                row.Cells = new CellViewModel[propertyInfos.Length];
                var rowId = idProperty.GetValue(element);
                row.Id = rowId?.ToString();
                row.ElementTypeFullName = getTypeName(elementType);
                row.EditUrl = getEditUrlForClass(row.Id, elementType);

                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    var cellProperty = propertyInfos[i];
                    var cellValue = cellProperty.GetValue(element);
                    var cellPropertyType = cellProperty.PropertyType;
                    var cellType = CheckPropType(cellPropertyType);

                    if (isFirst)
                    {
                        var cvm = new ColumnViewModel() { BindingPath = cellProperty.Name, Header = cellProperty.Name };
                        gv.Columns.Add(cvm);
                    }

                    var cell = new CellViewModel() { Value = getValue(cellValue) };
                    row.Cells[i] = cell;

                    if (cellType == PropertyTypes.Class)
                    {
                        if (cellValue != null)
                        {
                            var valueType = cellValue.GetType();
                            var idPropName = getIdPropertyName(valueType);
                            var idProp = valueType.GetProperty(idPropName);
                            var id = idProp.GetValue(cellValue)?.ToString();

                            cell.EditUrl = getEditUrlForClass(id, valueType);
                        }                        
                    }
                    else if (cellType == PropertyTypes.Collection)
                    {
                        cell.EditUrl = getEditUrlForCollection(getCollectionElementType(cellPropertyType), rowId, elementType);
                    }


                    
                }
                gv.Rows.Add(row);
                isFirst = false;
            }

            return gv;
        }

        private string getValue(object obj)
        {
            if (obj is ICollection collection1)
            {
                return $"{collection1.Cast<object>().FirstOrDefault()?.GetType().Name}[{collection1.Count}]";
            } else
            {
                return obj?.ToString() ?? "NULL";
            }
        }

        private Type getCollectionElementType(Type type)
        {
            return type.GetGenericArguments()[0];
        }

        private string getTypeName(Type type)
        {
            return type.Name;
        }

        private string getIdPropertyName(Type valueType)
        {
            return "Id";
        }

        private string getEditUrlForClass(string id, Type objectType)
        {
            var type = getTypeName(objectType);
            return $"PropertyGrid?id={id}&type={type}";
        }

        private string getEditUrlForCollection(Type collectionType, object parentId, Type parentType)
        {
            return $"Grid?type={getTypeName(collectionType)}&parentId={parentId}&parentType={getTypeName(parentType)}";
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
