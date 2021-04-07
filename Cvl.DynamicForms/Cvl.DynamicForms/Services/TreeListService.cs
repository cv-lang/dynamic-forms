using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.DynamicForms.Services
{
    public class TreeListParameters : CollectionViewModelParameters
    {

    }

    public class TreeListService
    {
        private BaseService helper = new BaseService();        
        private readonly DataService dataService;
        private readonly ViewConfigurationService viewConfigurationService;

        public TreeListService(DataService dataService, ViewConfigurationService viewConfigurationService)
        {
            this.dataService = dataService;
            this.viewConfigurationService = viewConfigurationService;            
        }

        public TreeListViewModel GetTreeList(TreeListParameters parameters)
        {
            var collection = dataService.GetCollection(parameters);
            return GetGridViewModel(collection, parameters);
        }

        public TreeListViewModel GetGridViewModel(IQueryable<object> collection, TreeListParameters parameters)
        {
            var gv = new TreeListViewModel();

            gv.PropertyValue = $"{collection.Cast<object>().FirstOrDefault()?.GetType().Name}[{collection.Count()}]";

            var firstElement = collection.Cast<object>().FirstOrDefault();
            if (firstElement == null)
            {
                return gv;
            }

            var elementType = firstElement.GetType();
            var elementIdPropertyName = dataService.GetIdPropertyName(elementType);
            var propertyInfos = elementType.GetProperties().Where(x => x.Name != elementIdPropertyName).ToArray();
            var idProperty = elementType.GetProperty(elementIdPropertyName);

            bool isFirst = true;
            var page = collection.Skip(parameters.Page * parameters.PageSize).Take(parameters.PageSize);

            foreach (var element in page)
            {
                var row = new RowViewModel();
                row.Cells = new CellViewModel[propertyInfos.Length];
                var rowId = idProperty.GetValue(element);
                row.Id = rowId?.ToString();
                row.ElementTypeFullName = helper.GetTypeName(elementType);
                row.EditUrl = helper.GetEditUrlForClass(row.Id, elementType);

                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    var cellProperty = propertyInfos[i];
                    var cellValue = cellProperty.GetValue(element);
                    var cellPropertyType = cellProperty.PropertyType;
                    var cellType = helper.CheckPropType(cellPropertyType);

                    if (isFirst)
                    {
                        var cvm = new ColumnViewModel() { BindingPath = cellProperty.Name, Header = cellProperty.Name };
                        gv.Columns.Add(cvm);
                    }

                    var cell = new CellViewModel() { Value = helper.GetValue(cellValue) };
                    row.Cells[i] = cell;

                    if (cellType == PropertyTypes.Class)
                    {
                        if (cellValue != null)
                        {
                            var valueType = cellValue.GetType();
                            var idPropName = dataService.GetIdPropertyName(valueType);
                            var idProp = valueType.GetProperty(idPropName);
                            var id = idProp.GetValue(cellValue)?.ToString();

                            cell.EditUrl = helper.GetEditUrlForClass(id, valueType);
                        }
                    }
                    else if (cellType == PropertyTypes.Collection)
                    {
                        cell.EditUrl = helper.GetEditUrlForCollection(helper.GetCollectionElementType(cellPropertyType), rowId, elementType);
                    }

                }
                gv.Rows.Add(row);
                isFirst = false;
            }

            return gv;
        }
    }
}
