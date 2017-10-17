using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ServiceFabric.Utils.Shared.Helpers
{
    public static class QueryStringHelpers
    {
        public static string Parse<T>(T model)
        {
            StringBuilder builder = new StringBuilder();

            // Get the type and properties of the model passed in
            var modelType = model.GetType();
            var modelProperties = modelType.GetProperties();

            // Iterate through each property in the model checking if it's a simple, primitive, type or a collection.
            foreach(var prop in modelProperties)
            {
                var propType = prop.PropertyType;
                var propValue = prop.GetValue(model);

                if (IsSimple(propType))
                {
                    builder.Append(ConvertSimpleProperty(prop, propValue));
                }

                else if (isDateTime(propType))
                {
                    builder.Append(ConvertDateTimeProperty(prop, (DateTime)propValue));
                }

                else if (typeof(IList).IsAssignableFrom(propType))
                {
                    var listType = propType.GetGenericArguments().Single();

                    MethodInfo method = typeof(QueryStringHelpers).GetMethod("ConvertCollectionProperty", 
                        BindingFlags.Static | BindingFlags.NonPublic);

                    MethodInfo generic = method.MakeGenericMethod(typeof(T), listType);

                    builder.Append(generic.Invoke(null, new[] { model, prop, propValue }));
                }
            }

            // Used to remove the last & character as this is appended when converting.
            // todo: Find a better solution
            builder.Length -= 1;

            return builder.ToString();
        }

   
        private static string ConvertSimpleProperty(PropertyInfo prop, object value)
        {
            var camelCasePropertyName = Char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1);
            return $"{camelCasePropertyName}={value}&";
        }

        private static string ConvertDateTimeProperty(PropertyInfo prop, DateTime value)
        {
            var uriFormattedDateTime = value.ToUniversalTime().ToString("o");
            var camelCasePropertyName = Char.ToLowerInvariant(prop.Name[0]) + prop.Name.Substring(1);

            return $"{camelCasePropertyName}={uriFormattedDateTime}&";
        }

        private static string ConvertNonPrimitiveProperty<TOwner, T>(TOwner owner, T prop)
        {
            StringBuilder builder = new StringBuilder();

            var rootPropType = prop.GetType();
            var innerProps = rootPropType.GetProperties();

            foreach (var innerProp in innerProps)
            {
                var innerPropType = innerProp.PropertyType;
                var innerPropValue = innerProp.GetValue(prop);

                if (IsSimple(innerPropType))
                {
                    builder.Append(ConvertSimpleProperty(innerProp, innerPropValue));
                }

                else if(isDateTime(innerPropType))
                {
                    builder.Append(ConvertDateTimeProperty(innerProp, (DateTime) innerPropValue));
                }

                else if (typeof(IList).IsAssignableFrom(innerPropType))
                {
                    var listType = innerPropType.GetGenericArguments().Single();

                    MethodInfo method = typeof(QueryStringHelpers).GetMethod("ConvertCollectionProperty",
                        BindingFlags.Static | BindingFlags.NonPublic);

                    MethodInfo generic = method.MakeGenericMethod(typeof(TOwner), listType);

                    builder.Append(generic.Invoke(null, new[] { owner, innerPropType, innerPropValue }));
                }
            }

            return builder.ToString();
        }

        private static string ConvertCollectionProperty<TOwner, T>(TOwner owner, PropertyInfo prop, IList<T> collection)
        {
            StringBuilder builder = new StringBuilder();

            foreach(var item in collection)
            {
                var rootType = item.GetType();

                if (IsSimple(rootType))
                {
                    builder.Append(ConvertSimpleProperty(prop, item));
                }

                else
                {
                    builder.Append(ConvertNonPrimitiveProperty(owner, item));
                }
            }

            return builder.ToString();
        }

        private static bool IsSimple(Type type)
        {
            return type.IsPrimitive
              || type.IsEnum
              || type.Equals(typeof(string))
              || type.Equals(typeof(Guid))
              || type.Equals(typeof(decimal));
        }

        private static bool isDateTime(Type type)
        {
            return type.Equals(typeof(DateTime));
        }
    }
}
