using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Tools;
using Cvl.DynamicForms.Tools.Extension;
using System;
using System.Collections;
using System.Linq;

namespace Cvl.DynamicForms.Services
{
    public class PropretyGridFaktory
    {        
        public PropretyGridFaktory()
        {            
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
                var propType = CheckPropType(item.PropertyType);

                if (propType == PropertyTypes.Collection)
                {
                    var gv = new GridElementViewModel();
                    gv.PropertyName = item.Name;
                    gv.PropertyValue = value?.ToString();
                    group.Properties.Add(gv);
                    createGridFromCollection((IEnumerable)value, gv);

                } else if (propType == PropertyTypes.Class)
                {
                    var pgv = new PropertyGridElementViewModel();
                    pgv.PropertyName = item.Name;
                    pgv.PropertyValue = value?.ToString();
                    group.Properties.Add(pgv);
                    createPropertyGridFromObject(value, pgv);

                } else
                {
                    var pvm = new PropertyViewModel() { Type = propType, Header = item.Name, BindingPath = item.Name, Value = value };
                    pvm.Order = item.GetPropertyOrder();
                    pvm.Description = item.GetPropertyDescription();
                    pvm.Group = item.GetPropertyGroup();

                    group.Properties.Add(pvm);
                }                
            }
        }

        private void createGridFromCollection(IEnumerable collection, GridElementViewModel gv)
        {
            if(collection is ICollection collection1)
            {
                gv.PropertyValue = $"{collection1.Cast<object>().FirstOrDefault()?.GetType().Name}[{collection1.Count}]";
            }
            

            //int propsLenght = 0;
            bool isFirst = true;
            foreach (var position in collection)
            {
                var type = position.GetType();
                var props = type.GetProperties();
                var rvm = new RowViewModel();
                rvm.Cells = new CellViewModel[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    var item = props[i];
                    var value = item.GetValue(position);
                    var propType = CheckPropType(item.PropertyType);

                    if (isFirst)
                    {
                        var cvm = new ColumnViewModel() { BindingPath = item.Name, Header = item.Name };
                        gv.Columns.Add(cvm);
                    }

                    var ccvm = new CellViewModel() { Value = value };
                    rvm.Cells[i] = ccvm;
                }
                gv.Rows.Add(rvm);
                isFirst = false;
            }            
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

        #endregion

        #endregion


    }
}
