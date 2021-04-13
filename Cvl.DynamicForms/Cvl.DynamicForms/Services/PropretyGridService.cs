using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Model.ViewModel;
using Cvl.DynamicForms.Tools;
using Cvl.DynamicForms.Tools.Extension;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Cvl.DynamicForms.Services
{
    public class PropretyGridService
    {
        private BaseService helper = new BaseService();
        private GridService gridService;
        private readonly DataServiceBase dataService;
        private readonly ViewConfigurationService viewConfigurationService;

        public PropretyGridService(DataServiceBase dataService, ViewConfigurationService viewConfigurationService)
        {
            this.dataService = dataService;
            this.viewConfigurationService = viewConfigurationService;
            gridService = new GridService(dataService, viewConfigurationService);
        }

        public PropertyGridViewModel GetPropertyGrid(object obj, Parameters parameters)
        {
            var pg = new PropertyGridViewModel();

            if (obj is ObjectXmlWrapper wrapper)
            {
                var complex = Serializer.DeserializeXml(wrapper.Xml);
                pg.ObjectTypeName = complex.Name;
                createPropertyGridFromXml(complex, pg);
            }
            else
            {
                //mamy zwykły obiekt - przechodzimy refleksjami
                createPropertyGridFromObject(obj, pg, 2);
                pg.PropertyValue = obj?.ToString();
                pg.ObjectTypeName = obj?.GetType().Name;
                pg.ListUrl = helper.GetEditUrlForCollection(obj.GetType(), null, null);
            }

            

            return pg;
        }

        #region Xml object
        private void createPropertyGridFromXml(Complex complex, PropertyGridElementViewModel pg)
        {
            xmlPropertyGridControlId = 0;
            createPropertyGridFromXml_internal(complex, pg);
        }

        private int xmlPropertyGridControlId = 0;
        private void createPropertyGridFromXml_internal(Complex complex, PropertyGridElementViewModel pg)
        {
            var group = new PropertyGroupViewModel();
            pg.Groups.Add(group);

            foreach (var item in complex.Properties)
            {
                xmlPropertyGridControlId++;

                if (item is Simple simple)
                {
                    var pvm = new PropertyViewModel() { Type = PropertyTypes.String, Header = simple.Name, BindingPath = simple.Name, Value = simple.Value };
                    group.Properties.Add(pvm);
                }
                else if (item is Complex compx)
                {
                    var childPg = new PropertyGridElementViewModel();
                    childPg.PropertyName = compx.Name;
                    childPg.PropertyUniqueName = compx.Name + "__" + xmlPropertyGridControlId;
                    
                    group.Properties.Add(childPg);
                    createPropertyGridFromXml_internal(compx, childPg );
                } else if(item is Collection collection)
                {
                    ////properies
                    //var childPg = new PropertyGridElementViewModel();
                    //childPg.PropertyName = collection.Name;
                    //childPg.PropertyUniqueName = compx.Name + "__" + xmlPropertyGridControlId;
                    //group.Properties.Add(childPg);
                    //var compx2 = new Complex() { Properties = collection.Properties };
                    //createPropertyGridFromXml(compx2, childPg);

                    //items
                    var childPg2 = new PropertyGridElementViewModel();
                    childPg2.PropertyName = collection.Name;
                    childPg2.PropertyUniqueName = collection.Name + "__" + xmlPropertyGridControlId;
                    group.Properties.Add(childPg2);
                    var compx3 = new Complex() { Properties = collection.Items };
                    createPropertyGridFromXml_internal(compx3, childPg2);                    
                }
            }
        }


        #endregion

        #region C# object

        private void createPropertyGridFromObject(object obj, PropertyGridElementViewModel pg, int level)
        {
            uniquePropertyGridId = 0;
            createPropertyGridFromObject_internal(obj, pg, level);
        }

        private int uniquePropertyGridId;
        private void createPropertyGridFromObject_internal(object obj, PropertyGridElementViewModel pg, int level, string parentPath = "")
        {
            if(level <= 0 )
            {
                return;
            }

            var group = new PropertyGroupViewModel();
            pg?.Groups.Add(group);

            if(obj == null)
            {
                return;
            }

            var type = obj.GetType();
            var props = type.GetProperties();
            parentPath = type.Name;
            foreach (var item in props)
            {
                uniquePropertyGridId++;

                var value = item.GetValue(obj);
                var propertyType = item.PropertyType;
                var propType = helper.CheckPropType(propertyType);

                if (propType == PropertyTypes.Collection)
                {
                    if (value != null)
                    {
                        var collection = (IEnumerable)value;
                        var gridViewModel = gridService.GetGridViewModel(collection?.Cast<object>().AsQueryable(), new CollectionViewModelParameters());
                        gridViewModel.PropertyName = item.Name;
                        gridViewModel.PropertyValue = helper.GetValue(value);
                        gridViewModel.EditUrl = helper.GetEditUrlForCollection(propertyType, "", propertyType);//TODO

                        group.Properties.Add(gridViewModel);
                    } else
                    {
                        group.Properties.Add(new GridViewModel() { PropertyName = item.Name, PropertyValue = "NULL" });
                    }
                }
                else if (propType == PropertyTypes.Class)
                {
                    var propertyGridElementViewModel = new PropertyGridElementViewModel();
                    propertyGridElementViewModel.PropertyName = item.Name;
                    propertyGridElementViewModel.PropertyUniqueName = item.Name + uniquePropertyGridId;
                    propertyGridElementViewModel.PropertyValue = value?.ToString();
                    if (value != null)
                    {
                        var idPropName = dataService.GetIdPropertyName(propertyType);
                        var idProp = propertyType.GetProperty(idPropName);
                        if(idProp == null)
                        {
                            throw new Exception($"Brak zdefiniowanej nazwy properji z Id dla typu {propertyType.FullName}");
                        }
                        var id = idProp.GetValue(value).ToString();
                        propertyGridElementViewModel.EditUrl = helper.GetEditUrlForClass(id, propertyType);
                        group.Properties.Add(propertyGridElementViewModel);
                        parentPath = parentPath + "." + item.Name;
                        createPropertyGridFromObject_internal(value, propertyGridElementViewModel, level-1, parentPath);
                    } else
                    {
                        group.Properties.Add(new PropertyGridElementViewModel() { PropertyName = item.Name, PropertyValue = "NULL" });
                    }

                } else
                {
                    var pvm = new PropertyViewModel() { Type = propType, Header = item.Name, BindingPath = parentPath + "." + item.Name, Value = helper.GetValue(value) };

                    if(propType == PropertyTypes.String)
                    {
                        //jeśli duży string to wyświetlam jako opis
                        var stringSize = pvm.Value?.ToString().Length;
                        if (stringSize != null && stringSize.Value > 130)
                        {
                            pvm.Type = PropertyTypes.StringEditor;

                            //jeśli xml to wyświetlam jako xml
                            try
                            {
                                var xml = pvm.Value?.ToString();

                                //tu jest jakiś problem z kodowaniem, i bez tego hacka parser się wywala
                                xml = $"<Complex><Properties>{xml}</Properties></Complex>";

                                var complex = Serializer.DeserializeXml(xml);
                                pvm.Type = PropertyTypes.StringXml;
                               
                                //dodaje jeszcze propertygrida
                                var propertyGridElementViewModel = new PropertyGridElementViewModel();
                                propertyGridElementViewModel.PropertyName = item.Name;
                                createPropertyGridFromXml(complex, propertyGridElementViewModel);   
                                group.Properties.Add(propertyGridElementViewModel);                                
                            }
                            catch (Exception ex)
                            {
                                //nie jest to serizlizowany obiekt (<Complex> ... </Complex>)
                               
                            }

                            //sprawdzam czy zwykły xml i formatuje go
                            try
                            {
                                var xml = pvm.Value?.ToString();                                
                                xml = $@"<?xml version=""1.0""?>
<Root>  
    {xml} 
</Root>";
                                var sb = new StringBuilder();
                                var doc = XDocument.Parse(xml);
                                var tr = new StringWriter(sb);
                                doc.Save(tr);
                                pvm.Value = (sb.ToString());

                                pvm.Type = PropertyTypes.StringXml;

                            }
                            catch (Exception ex2)
                            {
                            }
                        }
                    }

                    pvm.Order = item.GetPropertyOrder();
                    pvm.Description = item.GetPropertyDescription();
                    pvm.Group = item.GetPropertyGroup();
                    
                    group.Properties.Add(pvm);
                }                
            }
        }        
       
        #endregion
    }
}
