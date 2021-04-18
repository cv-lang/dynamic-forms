using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Model.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.DynamicForms.Services
{
    public class CollectionViewModelParameters
    {       
        public int PageSize { get; set; } = 200;
        public int Page { get; set; } = 0;
    }


    public class GridService
    {
        private BaseService helper = new BaseService();
        private readonly DataServiceBase dataService;
        private readonly ViewConfigurationService viewConfigurationService;

        public GridService(DataServiceBase dataService, ViewConfigurationService viewConfigurationService)
        {
            this.dataService = dataService;
            this.viewConfigurationService = viewConfigurationService;
        }

        public Model.ViewModel.GridVM GetGridViewModelForType(string collectionTypeName, string parentObjectId, string parentType, CollectionViewModelParameters parameters)
        {
            var collection = dataService.GetCollection(collectionTypeName, parentObjectId, parentType, parameters);
            return GetGridViewModelForObject(collection, null, collectionTypeName, "Collection", parameters);
        }

        public Model.ViewModel.GridVM GetGridViewModelForObject(IQueryable<object> collection, string objectId, string objectType, string bindingPath, CollectionViewModelParameters parameters)
        {
            var gv = new Model.ViewModel.GridVM();
            
            gv.PropertyValue = $"{collection.Cast<object>().FirstOrDefault()?.GetType().Name}[{collection.Count()}]";

            var firstElement = collection.Cast<object>().FirstOrDefault();
            if(firstElement == null)
            {
                return gv;
            }

            var elementType = firstElement.GetType();
            var elementIdPropertyName = dataService.GetIdPropertyName(elementType);
            var propertyInfos = viewConfigurationService.GetGridCollumnInPropertyGrid(elementType, elementIdPropertyName);
            var idProperty = elementType.GetProperty(elementIdPropertyName);

            bool isFirst = true;
            var page = collection.Skip(parameters.Page * parameters.PageSize).Take(parameters.PageSize);
            int iRow = 0;

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

                    var cell = new CellViewModel() {
                        IsBigString = helper.IsBigString(cellValue, BaseService.EnumPreviewType.Grid),
                        Value = helper.GetPreview(cellValue, BaseService.EnumPreviewType.Grid)};
                    cell.MainObjectId = objectId;
                    cell.MainObjectType = objectType;
                    cell.BindingPath = $"{bindingPath}[{iRow}].{cellProperty.Name}";

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
                iRow++;
            }

            return gv;
        }                   
    }
}
