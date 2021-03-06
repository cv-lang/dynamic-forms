using Cvl.DynamicForms.Model;
using Cvl.DynamicForms.Test;
using Cvl.DynamicForms.Tools;
using Pather.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cvl.DynamicForms.Services
{
    /// <summary>
    /// Serwis do pobierania danych - obiektów, kolekcji
    /// </summary>
    public class DataServiceBase
    {
        public DataServiceBase()
        {
        }

        /// <summary>
        /// Zwraca pojedyńczy obiekt
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="typeFullname"></param>
        /// <returns></returns>
        public virtual object GetObject(string objectId, string typeFullname)
        {            
            return null;
        }

        public class ObjectCollection
        {
            public object[] Collection { get; set; }
        }


        /// <summary>
        /// Zwraca pojedyńczy obiekt (lub pole obiektu)
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="typeFullname"></param>
        /// <param name="bindingPath"></param>
        /// <returns></returns>
        public virtual object GetObject(string objectId, string typeFullname, string bindingPath)
        {
            var resolver = new Resolver();
            if (string.IsNullOrEmpty(objectId))
            {
                //zwracamy kolekcję obiektów dla typu
                var col = new ObjectCollection();
                col.Collection = GetCollection(typeFullname, null, null, new CollectionViewModelParameters()).ToArray();
                var valCol = resolver.Resolve(col, bindingPath);
                return valCol;
            }

            var obj = GetObject(objectId, typeFullname);
            if(obj == null)
            {
                return null;
            }
            if( string.IsNullOrEmpty(bindingPath))
            {
                return obj;
            }

            
            var val = resolver.Resolve(obj, bindingPath);

            return val;
        }

        /// <summary>
        /// Helper do pobierania obiektów z EF
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="typeFullname"></param>
        /// <param name="processesContext"></param>
        /// <returns></returns>
        protected object GetObjectDb(object objectId, string typeFullname, object processesContext)
        {
            dynamic set = GetDbSet(typeFullname, processesContext);
            object obj = set.Find(objectId);

            return obj;
        }

        /// <summary>
        /// Zwraca dzieci obiektu - dla obiektu hierarchicznego
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IQueryable<object> GetChildrenCollection(string objectId, string typeFullname, CollectionViewModelParameters parameters, string mainFilter)
        {
            

            return null;
        }

        /// <summary>
        /// Helper do pobierania obiektów z EF
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="collectionTypeName"></param>
        /// <param name="parameters"></param>
        /// <param name="processesContext"></param>
        /// <returns></returns>
        public virtual IQueryable<object> GetChildrenCollectionDb(object objectId, string collectionTypeName, CollectionViewModelParameters parameters, object processesContext)
        {
            var set = GetDbSet(collectionTypeName, processesContext);
            var query = (IQueryable<object>)System.Linq.Queryable.Cast<object>(set);

            var dane = query.ToList();
            return dane.AsQueryable<object>();
        }

        /// <summary>
        /// Zwraca kolekcję obiektów
        /// </summary>
        /// <param name="collectionTypeName"></param>
        /// <param name="objectId"></param>
        /// <param name="objectType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual IQueryable<object> GetCollection(string collectionTypeName, string objectId, string objectType, CollectionViewModelParameters parameters)
        {
            
            return new List<object>().AsQueryable();
        }
               
        /// <summary>
        /// Zwraca nazwę propercji Id'ka
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public virtual string GetIdPropertyName(Type valueType)
        {
            return "Id";
        }

        public static dynamic GetDbSet(string typeFullname, object processesContext)
        {
            var objectType = GetType(typeFullname);

            MethodInfo method = processesContext.GetType().GetMethods().FirstOrDefault(x => x.Name == "Set");
            MethodInfo generic = method.MakeGenericMethod(objectType);
            dynamic set = generic.Invoke(processesContext, null);
            return set;
        }

        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
    }
}
