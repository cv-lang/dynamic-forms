using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Cvl.DynamicForms.Services
{
    public class DataWriter
    {
        public DataWriter()
        {

        }
        public void SetCustomValue(object target, string property, object setTo)
        {
            bool isCollection = false;
            int collectionIndex = 0;
            var parts = property.Split('.');
            PropertyInfo prop;
            string part = parts[0];
            Match match = Regex.Match(part, @"\d+");
            if (match.Success)
            {
                isCollection = true;
                collectionIndex = int.Parse(match.Value);
            }
            if (isCollection == true)
            {
                part = part.Remove(part.Length - 3, 3);
            }
            prop = target.GetType().GetProperty(part);
            if (isCollection)
            {
                dynamic value = prop.GetValue(target);
                int i = 0;
                foreach(var item in value)
                {
                    if(i == collectionIndex)
                        if(parts.Length == 1)
                        {
                            Type type = item.GetValue(target).GetType();
                            prop.SetValue(target, ParseValue(setTo, type), null);
                        }
                        else
                        {
                            SetCustomValue(item, string.Join(".", parts.Skip(1)), setTo);
                        }
                            
                    i++;
                }
            }
            else
            {
                if (parts.Length == 1)
                {
                    Type type = prop.GetValue(target).GetType();
                    prop.SetValue(target, ParseValue(setTo, type), null);
                }
                else
                {
                    var value = prop.GetValue(target);
                    SetCustomValue(value, string.Join(".", parts.Skip(1)), setTo);
                }
            }
        }

        private object ParseValue(object value, Type type)
        {
            var localvalue = value.ToString();
            if (type == typeof(int))
                return Int32.Parse(localvalue);
            if (type == typeof(int))
                return Int32.Parse(localvalue);
            else if (type == typeof(float))
                return Single.Parse(localvalue);
            else if (type == typeof(double))
                return Double.Parse(localvalue);
            else if (type == typeof(decimal))
                return Decimal.Parse(localvalue);
            else if (type == typeof(bool))
                return Boolean.Parse(localvalue);
            if (type == typeof(long))
                return Int64.Parse(localvalue);
            if (type == typeof(DateTime))
                return DateTime.Parse(localvalue);

            return localvalue;
        }
    }
}
