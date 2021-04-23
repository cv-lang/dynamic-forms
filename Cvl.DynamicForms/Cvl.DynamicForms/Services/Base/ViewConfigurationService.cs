using Cvl.DynamicForms.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using Cvl.ApplicationServer.Logs.Model;
using Cvl.DynamicForms.Fluent;

namespace Cvl.DynamicForms.Services
{
    public class TypeDescription
    {
        public bool IsFavourite { get; set; }
        public string FullTypeName { get; set; }
    }



    public class ViewConfigurationService
    {

        /// <summary>
        /// Lista kolumn wyświetlanych w głównym gridzie
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="elementIdPropertyName"></param>
        /// <returns></returns>
        public virtual System.Reflection.PropertyInfo[] GetGridCollumn(Type elementType, string elementIdPropertyName)
        {
            var gridBuilder = new ColumnBuilder();
            gridBuilder.ForType<ApplicationServer.Logs.Model.LogElement>()
                    .AddColumn(x => x.MemberName)
                    .AddColumn(x => x.ExternalId1)
                    .AddColumn(x => x.Message)
                    .AddColumn(x => x.CreatedDate);
            gridBuilder.ForType<Logger>()
                .AddColumn(x => x.Date)
                .AddColumn(x => x.Type)
                .AddColumn(x => x.Member)
                .AddColumn(x => x.Message)
                .AddColumn(x => x.Subloggers)
                .AddColumn(x => x.ParentId);


            return gridBuilder.GetPropertiesForType(elementType) ?? elementType.GetProperties().Where(x => x.Name != elementIdPropertyName).ToArray();
        }


        /// <summary>
        /// Lista kolumn wyświetlana w gridzie w property gdziedzie (edycji obiektu, który zawiera w sobie kolekcje)
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="elementIdPropertyName"></param>
        /// <returns></returns>
        public virtual System.Reflection.PropertyInfo[] GetGridCollumnInPropertyGrid(Type elementType, string elementIdPropertyName)
        {
            return GetGridCollumn(elementType, elementIdPropertyName);
        }


        /// <summary>
        /// Lista kolumn w głównej liście treelist
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="elementIdPropertyName"></param>
        /// <returns></returns>
        public virtual System.Reflection.PropertyInfo[] GetTreeListCollumns(Type elementType, string elementIdPropertyName)
        {
            var gridBuilder = new ColumnBuilder();
            gridBuilder.ForType<ApplicationServer.Logs.Model.LogElement>()
                    .AddColumn(x => x.Message)
                    .AddColumn(x => x.MemberName)
                    .AddColumn(x => x.ExternalId1)
                    .AddColumn(x => x.CreatedDate);
            gridBuilder.ForType<Logger>()
                .AddColumn(x => x.Date)
                .AddColumn(x => x.Type)
                .AddColumn(x => x.Member)
                .AddColumn(x => x.Message)
                .AddColumn(x => x.Subloggers)
                .AddColumn(x => x.ParentId);


            return gridBuilder.GetPropertiesForType(elementType) ?? elementType.GetProperties().Where(x => x.Name != elementIdPropertyName).ToArray();
        }


        /// <summary>
        /// Lista propercji dla propertyGrida
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual System.Reflection.PropertyInfo[] GetPropertyGridProperties(Type elementType, string[] propertyNames)
        {
            var properties = new System.Reflection.PropertyInfo[propertyNames.Length];

            for (int i = 0; i < propertyNames.Length; i++)
            {
                properties[i] = elementType.GetProperty(propertyNames[i]);
            }

            return properties;
        }


        /// <summary>
        /// Lista typów - dostępna w widoku
        /// </summary>
        /// <returns></returns>
        public virtual List<TypeDescription> GetTypes()
        {
            var l = new List<TypeDescription>();
            l.Add(new TypeDescription() { FullTypeName = typeof(TestPerson).FullName, IsFavourite = true });
            l.Add(new TypeDescription() { FullTypeName = typeof(Logger).FullName, IsFavourite = true });
            l.Add(new TypeDescription() { FullTypeName = typeof(Invoice).FullName });
            l.Add(new TypeDescription() { FullTypeName = typeof(Address).FullName });
            l.Add(new TypeDescription() { FullTypeName = typeof(LogElement).FullName });


            return l;
        }
    }
}
