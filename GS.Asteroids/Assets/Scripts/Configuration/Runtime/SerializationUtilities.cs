using System;
using System.Reflection;

namespace GS.Asteroids.Configuration
{
    internal static class SerializationUtilities
    {
        private const BindingFlags InstanceBindings = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        internal static FieldInfo GetField(Type declaringType, string fieldName)
        {
            while (declaringType != null)
            {
                FieldInfo declaringTypeField = declaringType.GetField(fieldName, InstanceBindings);

                if (declaringTypeField != null)
                    return declaringTypeField;

                declaringType = declaringType.BaseType;
            }

            return default;
        }

        internal static FieldInfo GetField(Type declaringType, Type fieldType, string fieldName = null)
        {
            while (declaringType != null)
            {
                FieldInfo[] declaringTypeFields = declaringType.GetFields(InstanceBindings);

                foreach (FieldInfo fieldInfo in declaringTypeFields)
                    if (IsMatch(fieldInfo, fieldType, fieldName))
                        return fieldInfo;

                declaringType = declaringType.BaseType;
            }

            return default;
        }

        internal static PropertyInfo GetProperty(Type declaringType, string propertyName)
        {
            while (declaringType != null)
            {
                PropertyInfo declaringTypeProperty = declaringType.GetProperty(propertyName, InstanceBindings);

                if (declaringTypeProperty != null)
                    return declaringTypeProperty;

                declaringType = declaringType.BaseType;
            }

            return default;
        }

        internal static PropertyInfo GetProperty(Type declaringType, Type propertyType, string propertyName = null)
        {
            while (declaringType != null)
            {
                PropertyInfo[] declaringTypeProperties = declaringType.GetProperties(InstanceBindings);

                foreach (PropertyInfo propertyInfo in declaringTypeProperties)
                    if (IsMatch(propertyInfo, propertyType, propertyName))
                        return propertyInfo;

                declaringType = declaringType.BaseType;
            }

            return default;
        }

        internal static T GetValue<T>(this object @object, string fieldName = null)
        {
            Type declaringType = @object.GetType();
            Type fieldType = typeof(T);
            FieldInfo fieldInfo = GetField(declaringType, fieldType, fieldName);

            T result = fieldInfo == null ? (T)GetProperty(declaringType, fieldType, fieldName)?.GetValue(@object) : (T)fieldInfo.GetValue(@object);

            if (result == null)
                throw new Exception($"Can't find '{fieldType.Name}' field or property for type '{declaringType.Name}'");

            return result;
        }

        internal static void SetValue<T>(this object @object, T value, string fieldName = null)
        {
            Type declaringType = @object.GetType();
            Type fieldType = typeof(T);
            FieldInfo fieldInfo = GetField(declaringType, fieldType, fieldName);

            if (fieldInfo == null)
            {
                PropertyInfo propertyInfo = GetProperty(declaringType, fieldType, fieldName);

                if (propertyInfo == null)
                    throw new Exception($"Can't find '{fieldType.Name}' field or property for type '{declaringType.Name}'");
                else
                    propertyInfo.SetValue(@object, value);
            }
            else
            {
                fieldInfo.SetValue(@object, value);
            }
        }

        private static bool IsMatch(FieldInfo fieldInfo, Type fieldType, string fieldName)
        {
            if (fieldInfo.FieldType != fieldType)
                return false;

            if (!string.IsNullOrEmpty(fieldName) && fieldName != fieldInfo.Name)
                return false;

            return true;
        }

        private static bool IsMatch(PropertyInfo propertyInfo, Type propertyType, string propertyName)
        {
            if (propertyInfo.PropertyType != propertyType)
                return false;

            if (!string.IsNullOrEmpty(propertyName) && propertyName != propertyInfo.Name)
                return false;

            return true;
        }
    }
}
