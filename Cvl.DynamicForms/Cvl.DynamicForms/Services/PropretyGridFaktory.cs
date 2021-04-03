using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Model.ViewModel;
using Cvl.DynamicForms.Tools;
using Cvl.DynamicForms.Tools.Extension;
using System;
using System.Collections;
using System.Linq;

namespace Cvl.DynamicForms.Services
{
    public class PropretyGridFaktory
    {
        private GridViewModelFactory gridViewModelFactory;

        public PropretyGridFaktory(GridViewModelFactory gridViewModelFactory)
        {
            this.gridViewModelFactory = gridViewModelFactory;
        }

        


        #region Property grid        

        public PropertyGridViewModel GetPropertyGrid(object obj, Parameters parameters)
        {
            var pg = new PropertyGridViewModel();

            if (obj is ObjectXmlWrapper wrapper)
            {
                var complex = Serializer.DeserializeXml(wrapper.Xml);
                createPropertyGridFromXml(complex, pg);
            }
            else
            {
                //mamy zwykły obiekt - przechodzimy refleksjami
                createPropertyGridFromObject(obj, pg);
            }

            return pg;
        }

        #region Xml object
        private void createPropertyGridFromXml(Complex complex, PropertyGridElementViewModel pg)
        {
            var group = new PropertyGroupViewModel();
            pg.Groups.Add(group);

            foreach (var item in complex.Properties)
            {
                if (item is Simple simple)
                {
                    var pvm = new PropertyViewModel() { Type = PropertyTypes.String, Header = simple.Name, BindingPath = simple.Name, Value = simple.Value };
                    group.Properties.Add(pvm);
                }
                else if (item is Complex compx)
                {
                    var childPg = new PropertyGridElementViewModel();
                    childPg.PropertyName = compx.Name;
                    group.Properties.Add(childPg);
                    createPropertyGridFromXml(compx, childPg);
                }
            }
        }

        
        #endregion

        #region C# object

        private void createPropertyGridFromObject(object obj, PropertyGridElementViewModel pg)
        {
            var group = new PropertyGroupViewModel();
            pg.Groups.Add(group);

            if(obj == null)
            {
                return;
            }

            var type = obj.GetType();
            var props = type.GetProperties();

            foreach (var item in props)
            {
                var value = item.GetValue(obj);
                var propertyType = item.PropertyType;
                var propType = gridViewModelFactory.CheckPropType(propertyType);

                if (propType == PropertyTypes.Collection)
                {
                    if (value != null)
                    {
                        var collection = (ICollection)value;
                        var gridViewModel = gridViewModelFactory.GetGridViewModel(collection.Cast<object>().AsQueryable(), new GridViewModelParameters());
                        gridViewModel.PropertyName = item.Name;
                        gridViewModel.PropertyValue = gridViewModelFactory.GetValue(value);
                        gridViewModel.EditUrl = gridViewModelFactory.GetEditUrlForCollection(propertyType, "", propertyType);//TODO

                        group.Properties.Add(gridViewModel);
                    }
                }
                else if (propType == PropertyTypes.Class)
                {
                    var propertyGridElementViewModel = new PropertyGridElementViewModel();
                    propertyGridElementViewModel.PropertyName = item.Name;
                    propertyGridElementViewModel.PropertyValue = value?.ToString();
                    var idPropName = gridViewModelFactory.GetIdPropertyName(propertyType);
                    var idProp = propertyType.GetProperty(idPropName);
                    var id = idProp.GetValue(value).ToString();
                    propertyGridElementViewModel.EditUrl = gridViewModelFactory.GetEditUrlForClass(id, propertyType);
                    group.Properties.Add(propertyGridElementViewModel);
                    createPropertyGridFromObject(value, propertyGridElementViewModel);

                } else
                {
                    var pvm = new PropertyViewModel() { Type = propType, Header = item.Name, BindingPath = item.Name, Value = gridViewModelFactory.GetValue(value) };
                    pvm.Order = item.GetPropertyOrder();
                    pvm.Description = item.GetPropertyDescription();
                    pvm.Group = item.GetPropertyGroup();
                    
                    group.Properties.Add(pvm);
                }                
            }
        }

        private void createGridFromCollection(IQueryable<object> collection, GridViewModel gv)
        {
            
        }

       
        #endregion

        #endregion


    }
}
