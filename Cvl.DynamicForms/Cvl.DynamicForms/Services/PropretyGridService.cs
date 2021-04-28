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
    public class PropertyGridService
    {
        private BaseService helper = new BaseService();
        private GridService gridService;
        private readonly DataServiceBase dataService;
        private readonly ViewConfigurationService viewConfigurationService;

        public PropertyGridService(DataServiceBase dataService, ViewConfigurationService viewConfigurationService, GridService gridService)
        {
            this.dataService = dataService;
            this.viewConfigurationService = viewConfigurationService;
            this.gridService = gridService;
        }

        public PropertyGridVM GetPropertyGrid(string objectId, string typeFullname, string bindingPath)
        {
            var obj = dataService.GetObject(objectId, typeFullname, bindingPath);
            var pg = new PropertyGridVM();



            if (obj is ObjectXmlWrapper wrapper)
            {
                var complex = Serializer.DeserializeXml(wrapper.Xml);                
                createPropertyGridFromXml(complex, pg);
            } else if(obj is IEnumerable collection)
            {
                //mamy kolekcję                
                var gridVM = gridService.GetGridViewModelForObject(collection?.Cast<object>().AsQueryable(), objectId, typeFullname, bindingPath,  new CollectionViewModelParameters());
                gridVM.BindingPath = bindingPath;
                gridVM.PropertyValue = helper.GetValue(collection);
                gridVM.IsStatic = false;
                gridVM.MainObjectId = objectId;
                gridVM.MainObjectTypeFullname = typeFullname;
                //gridVM.EditUrl = helper.GetEditUrlForCollection(propertyType, "", propertyType);//TODO

                pg.Properties.Add(gridVM);
            }
            else
            {
                pg.PropertyValue = obj?.ToString();                
                pg.MainObjectId = objectId;
                pg.MainObjectTypeFullname = typeFullname;
                pg.BindingPath = bindingPath;
                pg.IsStatic = false;
                //mamy zwykły obiekt - przechodzimy refleksjami
                createPropertyGridFromObject_internal(obj, pg, 1, bindingPath);
                
                pg.ListUrl = helper.GetEditUrlForCollection(obj.GetType(), null, null);
            }

            

            return pg;
        }

        #region Xml object
        private void createPropertyGridFromXml(Complex complex, PropertyGridVM pg)
        {
            xmlPropertyGridControlId = 0;
            createPropertyGridFromXml_internal(complex, pg);
        }

        private int xmlPropertyGridControlId = 0;
        private void createPropertyGridFromXml_internal(Complex complex, PropertyGridVM pg)
        {
            //var group = new GroupVM();
            pg.IsStatic = true;
            pg.BindingPath = xmlPropertyGridControlId.ToString();//dla unikalny id
            xmlPropertyGridControlId++;            

            foreach (var item in complex.Properties)
            {
                xmlPropertyGridControlId++;

                if (item is Simple simple)
                {
                    var pvm = new SimplePropertyVM() { Type = PropertyTypes.String, Header = simple.Name, BindingPath = simple.Name, Value = simple.Value };

                    pvm.IsBigString = helper.IsBigString(simple.Value, BaseService.EnumPreviewType.PropertyGrid);

                    pg.Properties.Add(pvm);
                }
                else if (item is Complex compx)
                {
                    var childPg = new PropertyGridVM();
                    childPg.IsStatic = true;
                    childPg.PropertyName = compx.Name;
                    childPg.BindingPath = xmlPropertyGridControlId.ToString();//dla unikalny id


                    pg.Properties.Add(childPg);
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
                    var childPg2 = new PropertyGridVM();
                    childPg2.PropertyName = collection.Name;
                    childPg2.BindingPath = xmlPropertyGridControlId.ToString();//dla unikalny id
                    pg.Properties.Add(childPg2);
                    var compx3 = new Complex() { Properties = collection.Items };
                    createPropertyGridFromXml_internal(compx3, childPg2);                    
                }
            }
        }


        #endregion

        #region C# object       
        
        private void createPropertyGridFromObject_internal(object obj, PropertyGridVM parentPropertyGridVM, int level, string parentPath = "")
        {
            if(level <= 0 )
            {
                return;
            }

            if(obj == null)
            {
                return;
            }

            var type = obj.GetType();
            var objectProperties = type.GetProperties();
            
            foreach (var item in objectProperties)
            {              
                var value = item.GetValue(obj);
                var propertyType = item.PropertyType;
                var propType = helper.CheckPropType(propertyType);
                var bindingPath =  $"{parentPath}.{item.Name}".TrimStart('.');

                if (propType == PropertyTypes.Collection)
                {
                    if (value != null)
                    {
                        var collection = (IEnumerable)value;
                        var gridVM = gridService.GetGridViewModelForObject(collection?.Cast<object>().AsQueryable(),
                            parentPropertyGridVM.MainObjectId, parentPropertyGridVM.MainObjectTypeFullname, bindingPath,
                            new CollectionViewModelParameters());
                        gridVM.PropertyName = item.Name;
                        gridVM.BindingPath = bindingPath;
                        gridVM.PropertyValue = helper.GetValue(value);
                        gridVM.IsStatic = parentPropertyGridVM.IsStatic;
                        gridVM.MainObjectId = parentPropertyGridVM.MainObjectId;
                        gridVM.MainObjectTypeFullname = parentPropertyGridVM.MainObjectTypeFullname;
                        gridVM.EditUrl = helper.GetEditUrlForCollection(propertyType, "", propertyType);//TODO

                        parentPropertyGridVM.Properties.Add(gridVM);
                    } else
                    {
                        parentPropertyGridVM.Properties.Add(new Model.ViewModel.GridVM() { PropertyName = item.Name, PropertyValue = "NULL", BindingPath = bindingPath });
                    }
                }
                else if (propType == PropertyTypes.Class)
                {
                    var propertyGridVM = new PropertyGridVM();
                    propertyGridVM.PropertyName = item.Name;
                    propertyGridVM.BindingPath = bindingPath;
                    propertyGridVM.IsStatic = parentPropertyGridVM.IsStatic;
                    propertyGridVM.MainObjectId = parentPropertyGridVM.MainObjectId;
                    propertyGridVM.MainObjectTypeFullname = parentPropertyGridVM.MainObjectTypeFullname;
                    propertyGridVM.PropertyValue = value?.ToString();
                    if (value != null)
                    {
                        var idPropName = dataService.GetIdPropertyName(propertyType);
                        var idProp = propertyType.GetProperty(idPropName);
                        if(idProp == null)
                        {
                            throw new Exception($"Brak zdefiniowanej nazwy properji z Id dla typu {propertyType.FullName}");
                        }
                        var id = idProp.GetValue(value).ToString();
                        propertyGridVM.EditUrl = helper.GetEditUrlForClass(id, propertyType);
                        parentPropertyGridVM.Properties.Add(propertyGridVM);
                        
                        createPropertyGridFromObject_internal(value, propertyGridVM, level-1, bindingPath);
                    } else
                    {
                        parentPropertyGridVM.Properties.Add(new PropertyGridVM() { PropertyName = item.Name, PropertyValue = "NULL", BindingPath = bindingPath });
                    }

                } else
                {
                    var pvm = new SimplePropertyVM() { Type = propType, Header = item.Name, BindingPath = bindingPath, Value = helper.GetValue(value) };

                    if(propType == PropertyTypes.String)
                    {
                        //jeśli duży string to wyświetlam jako opis                        
                        pvm.IsBigString = helper.IsBigString(value, BaseService.EnumPreviewType.PropertyGrid);
                        if (pvm.IsBigString)
                        {
                            pvm.Value = helper.GetPreview(value, BaseService.EnumPreviewType.PropertyGrid);
                            //jeśli xml to wyświetlam jako xml
                            try
                            {
                                var xml = value?.ToString() ?? "NULL";

                                //tu jest jakiś problem z kodowaniem, i bez tego hacka parser się wywala
                                xml = $"<Complex><Properties>{xml}</Properties></Complex>";

                                var complex = Serializer.DeserializeXml(xml);
                               
                                //dodaje jeszcze propertygrida
                                var propertyGridElementViewModel = new PropertyGridVM();
                                propertyGridElementViewModel.IsStatic = true;
                                propertyGridElementViewModel.PropertyName = $"{item.Name}";
                                propertyGridElementViewModel.BindingPath = $"{bindingPath}.autogenerated";
                                propertyGridElementViewModel.PropertyValue = " - autogenerated from xml";
                                createPropertyGridFromXml(complex, propertyGridElementViewModel);   
                                parentPropertyGridVM.Properties.Add(propertyGridElementViewModel);                                
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
                            }
                            catch (Exception ex2)
                            {
                            }
                        }
                    }

                    pvm.Order = item.GetPropertyOrder();
                    pvm.Description = item.GetPropertyDescription();
                    pvm.Group = item.GetPropertyGroup();
                    
                    parentPropertyGridVM.Properties.Add(pvm);
                }                
            }
        }        
       
        #endregion
    }
}
