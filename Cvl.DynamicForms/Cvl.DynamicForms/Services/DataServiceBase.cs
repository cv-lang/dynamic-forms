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

        public virtual object GetObject(string objectId, string typeFullname, string bindingPath)
        {
            var obj = GetObject(objectId, typeFullname);
            if(obj == null)
            {
                return null;
            }
            if( string.IsNullOrEmpty(bindingPath))
            {
                return obj;
            }

            var resolver = new Resolver();
            var val = resolver.Resolve(obj, bindingPath);

            return val;
        }

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
        public virtual IQueryable<object> GetChildrenCollection(string objectId, string typeFullname, CollectionViewModelParameters parameters)
        {
            

            return null;
        }

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
