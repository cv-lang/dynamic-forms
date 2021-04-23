using Cvl.DynamicForms.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Cvl.DynamicForms.Services
{
    public class BaseService
    {
        public string GetTypeName(Type type)
        {
            if (type == null)
            {
                return null;
            }
            return type.FullName;
        }

        public PropertyTypes CheckPropType(Type propertyType)
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

        public enum EnumPreviewType
        {
            PropertyGrid = 200,
            Grid = 100
        }

        public bool IsBigString(object obj, EnumPreviewType previewType)
        {
            if (obj == null)
            {
                return false;
            }

            var val = GetValue(obj);
            return val.Length >= (int)previewType;
        }
        public string GetPreview(object obj, EnumPreviewType enumPreviewType)
        {
            var val = GetValue(obj);

            return val.Substring(0, Math.Min(val.Length, (int)enumPreviewType));
        }

        public string GetValue(object obj)
        {
            if (obj is ICollection collection1)
            {
                return $"{collection1.Cast<object>().FirstOrDefault()?.GetType().Name}[{collection1.Count}]";
            }
            else
            {
                var str = obj?.ToString() ?? "NULL";
                return str?.ToString().Replace(",", ", ").Replace("+", "+ ");//HttpUtility.HtmlEncode(str);
            }
        }

        public Type GetCollectionElementType(Type type)
        {
            return type.GetGenericArguments()[0];
        }

        public string GetEditUrlForClass(string id, Type objectType)
        {
            var type = GetTypeName(objectType);
            return $"PropertyGrid?id={id}&type={type}";
        }

        public string GetEditUrlForCollection(Type collectionType, object parentId, Type parentType)
        {
            return $"Grid?type={GetTypeName(collectionType)}&parentId={parentId}&parentType={GetTypeName(parentType)}";
        }


    }
}
