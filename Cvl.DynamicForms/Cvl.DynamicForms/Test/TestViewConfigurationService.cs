using Cvl.DynamicForms.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cvl.DynamicForms.Test
{
    public class TestViewConfigurationService : ViewConfigurationService
    {
        public override PropertyInfo[] GetGridCollumn(Type elementType, string elementIdPropertyName)
        {
            if (elementType == typeof(Logger))
            {
                return base.GetProperties(elementType, new string[] { "Date", "Type", "Member", "Message", "Subloggers", "ParentId" });
            } else
            {
                return base.GetGridCollumn(elementType, elementIdPropertyName);
            }
        }

        public override PropertyInfo[] GetTreeListCollumns(Type elementType, string elementIdPropertyName)
        {
            if (elementType == typeof(Logger))
            {
                return base.GetProperties(elementType, new string[] { "Date", "Type", "Member", "Message", "Subloggers", "ParentId" });
            }
            else
            {
                return base.GetTreeListCollumns(elementType, elementIdPropertyName);
            }
        }
    }
}
