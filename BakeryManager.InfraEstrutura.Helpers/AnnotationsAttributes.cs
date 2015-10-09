using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BakeryManager.Infraestrutura.Helpers
{
    public static class AnnotationsAttributes
    {
        
        
         public static string GetPropertyDisplayName(PropertyInfo property)
         {
             var prop = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
             return prop != null ? ((DisplayNameAttribute)prop).DisplayName : string.Empty;
         }

         public static string GetPropertyTitle(Type type, string propertyName)
         {

             var display = type.GetProperty(propertyName).GetCustomAttribute(typeof(DisplayAttribute), false);

             if (display != null)
                 return (display as DisplayAttribute).GetName();

             var displayName = type.GetProperty(propertyName).GetCustomAttribute(typeof(DisplayNameAttribute), false);

             if (displayName != null)
                 return (displayName as DisplayNameAttribute).DisplayName;

             return propertyName;

         }

        public static string GetClassTitle(Type type)
        {
            var display = type.GetCustomAttribute(typeof (DisplayNameClass), false);
            if (display != null)
                return ((DisplayNameClass) display).GetName();
            else
                return type.Name;
        }

    }
}
