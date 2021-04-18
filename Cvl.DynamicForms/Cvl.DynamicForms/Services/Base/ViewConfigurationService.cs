using Cvl.DynamicForms.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.DynamicForms.Services
{
    public class TypeDescription
    {
        public bool IsFavourite { get; set; }
        public string FullTypeName { get; set; }
    }

    public class ViewConfigurationService
    {
        public virtual System.Reflection.PropertyInfo[] GetGridCollumn(Type elementType, string elementIdPropertyName)
        {
            System.Reflection.PropertyInfo[] props = elementType.GetProperties().Where(x => x.Name != elementIdPropertyName).ToArray();

            return props;
        }

        public virtual System.Reflection.PropertyInfo[] GetGridCollumnInPropertyGrid(Type elementType, string elementIdPropertyName)
        {
            return GetGridCollumn(elementType, elementIdPropertyName);
        }


        public virtual System.Reflection.PropertyInfo[] GetTreeListCollumns(Type elementType, string elementIdPropertyName)
        {
            System.Reflection.PropertyInfo[] props = elementType.GetProperties().Where(x => x.Name != elementIdPropertyName).ToArray();

            return props;
        }


        /// <summary>
        /// Lista propercji dla propertyGrida
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        protected System.Reflection.PropertyInfo[] GetProperties(Type elementType, string[] propertyNames)
        {
            var properties = new System.Reflection.PropertyInfo[propertyNames.Length];

            for (int i = 0; i < propertyNames.Length; i++)
            {
                properties[i] = elementType.GetProperty(propertyNames[i]);
            }

            return properties;
        }



        public virtual List<TypeDescription> GetTypes()
        {
            var l= new List<TypeDescription>();
            l.Add(new TypeDescription() { FullTypeName = typeof(TestPerson).FullName, IsFavourite = true });
            l.Add(new TypeDescription() { FullTypeName = typeof(Logger).FullName, IsFavourite = true });
            l.Add(new TypeDescription() { FullTypeName = typeof(Invoice).FullName });
            l.Add(new TypeDescription() { FullTypeName = typeof(Address).FullName });

            return l;
        }
    }
}
