using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Global
{
    public static class Extension
    {
        public static T ConvertTo<T>(this Object source) where T : new()
        {
            var result = new T();
            var resultTypeInfo = typeof(T).GetTypeInfo();
            var resultFields = resultTypeInfo.GetAllDeclaredFields();

            var sourceTypeInfo = source.GetType().GetTypeInfo();
            var sourceFields = sourceTypeInfo.DeclaredFields;

            foreach (var field in sourceFields)
            {
                if (resultFields.Contains(field))
                    field.SetValue(result, field.GetValue(source));
            }

            return result;
        }

        private static IEnumerable<FieldInfo> GetAllDeclaredFields(this TypeInfo typeInfo)
        {
            //TODO may be optimized
            foreach (var field in typeInfo.DeclaredFields)
            {
                yield return field;
            }

            if (typeInfo.BaseType != null)
            {
                typeInfo = typeInfo.BaseType.GetTypeInfo();
                foreach (var field in typeInfo.DeclaredFields)
                {
                    yield return field;
                }
            }
        }
    }
}