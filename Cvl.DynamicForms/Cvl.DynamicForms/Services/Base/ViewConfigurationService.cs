using Cvl.DynamicForms.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Cvl.DynamicForms.Services
{
    public class TypeDescription
    {
        public bool IsFavourite { get; set; }
        public string FullTypeName { get; set; }
    }

    public class BuilderForType<T>
    {
        List<string> colList = new List<string>();
        private GridBuilder conf;
        public BuilderForType(GridBuilder conf)
        {
            this.conf = conf;
        }
        internal List<PropertyInfo> Build()
        {
            var type = typeof(T);
            List<PropertyInfo> propList = new List<PropertyInfo>();
            for (int i = 0; i < colList.Count; i++)
            {
                propList.Add(type.GetProperty(colList[i]));
            }

            return propList;
        }


        internal BuilderForType<T> AddColumn(Expression<Func<TestPerson, string>> p)
        {
            var testName = p.ToString().Replace("x => x.", "");
            colList.Add(testName);
            return this;
        }
    }

    public class GridBuilder
    {
        internal BuilderForType<T> ForType<T>()
        {
            return new BuilderForType<T>(this);
        }
    }

    public class ViewConfigurationService
    {
        public virtual System.Reflection.PropertyInfo[] GetGridCollumn(Type elementType, string elementIdPropertyName)
        {
            var gridBuilder = new GridBuilder();
            PropertyInfo[] props = gridBuilder.ForType<TestPerson>()
                .AddColumn(x => x.Firstname)
                .AddColumn(x => x.Surname)
                .Build().ToArray();

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
