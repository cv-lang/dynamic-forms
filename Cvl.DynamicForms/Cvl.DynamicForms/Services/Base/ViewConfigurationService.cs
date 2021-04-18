using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.DynamicForms.Services
{
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

        protected System.Reflection.PropertyInfo[] GetProperties(Type elementType, string[] propertyNames)
        {
            var properties = new System.Reflection.PropertyInfo[propertyNames.Length];

            for (int i = 0; i < propertyNames.Length; i++)
            {
                properties[i] = elementType.GetProperty(propertyNames[i]);
            }

            return properties;
        }

        
    }
}
