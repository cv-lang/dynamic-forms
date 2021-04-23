using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Cvl.DynamicForms.Fluent
{
    public class ColumnsForType
    {
        public string TypeFullName { get; set; }
        public virtual List<PropertyInfo> GetProperties()
        { throw new NotImplementedException(); }
    }

    public class ColumnsForType<T> : ColumnsForType
    {
        List<string> colList = new List<string>();
        private ColumnBuilder conf;
        public ColumnsForType(ColumnBuilder conf)
        {
            this.conf = conf;
            TypeFullName = typeof(T).FullName;
        }
        public override List<PropertyInfo> GetProperties()
        {
            var type = typeof(T);
            List<PropertyInfo> propList = new List<PropertyInfo>();
            for (int i = 0; i < colList.Count; i++)
            {
                propList.Add(type.GetProperty(colList[i]));
            }

            return propList;
        }


        internal ColumnsForType<T> AddColumn<T2>(Expression<Func<T, T2>> p)
        {
            var testName = p.ToString().Replace("x => x.", "");
            colList.Add(testName);
            return this;
        }
    }

    public class ColumnBuilder
    {
        public List<ColumnsForType> typesColums = new List<ColumnsForType>();

        public ColumnsForType<T> ForType<T>()
        {
            var typeCols = new ColumnsForType<T>(this);
            typesColums.Add(typeCols);
            return typeCols;
        }

        internal PropertyInfo[] GetPropertiesForType(Type elementType)
        {
            return typesColums.SingleOrDefault(x => x.TypeFullName == elementType.FullName)?.GetProperties().ToArray();
        }
    }
}
