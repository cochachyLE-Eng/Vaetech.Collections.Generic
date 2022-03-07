using System;
using System.Globalization;
using System.Linq.Expressions;

namespace Vaetech.Collections.Generic.Extensions
{
    public static class PropertyExtensions
    {
        public static object Coalesce(object value, object defaultValue) => value == null ? defaultValue : value;
        public static TValue Coalesce<TValue>(this TValue value, TValue defaultValue) => value == null ?  defaultValue : value;
        public static TValue Coalesce<TEntity, TValue>(this TEntity entity, Expression<Func<TEntity, TValue>> value, TValue defaultValue)
        {
            TValue property = default(TValue);
            if (value.Body is MemberExpression)
            { 
                MemberExpression me = value.Body as MemberExpression;
                object _value = typeof(TEntity).GetProperty(me.Member.Name).GetValue(entity);
                property = _value == null ? defaultValue : (TValue)_value;
            }
            return property;
        }
        public static TValue Parse<TValue>(this object value)
        {
            if (value is null || value is DBNull) return default;
            if (value is TValue t) return t;
            var type = typeof(TValue);
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type.IsEnum)
            {
                if (value is float || value is double || value is decimal)
                {
                    value = Convert.ChangeType(value, Enum.GetUnderlyingType(type), CultureInfo.InvariantCulture);
                }
                return (TValue)Enum.ToObject(type, value);
            }
            return (TValue)Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }
    }
}
