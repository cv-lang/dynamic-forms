using Cvl.DynamicForms.Fluent;
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
            var gridBuilder = new ColumnBuilder();            

            return gridBuilder.GetPropertiesForType(elementType) ?? base.GetGridCollumn(elementType, elementIdPropertyName);            
        }

        public override PropertyInfo[] GetTreeListCollumns(Type elementType, string elementIdPropertyName)
        {
            var columnBuilder = new ColumnBuilder();
            columnBuilder.ForType<Logger>()
                .AddColumn(x => x.Date)
                .AddColumn(x => x.Type)
                .AddColumn(x => x.Member)
                .AddColumn(x => x.Message)
                .AddColumn(x => x.Subloggers)
                .AddColumn(x => x.ParentId);

            return columnBuilder.GetPropertiesForType(elementType) ?? base.GetTreeListCollumns(elementType, elementIdPropertyName);            
        }
    }
}
