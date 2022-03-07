using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Vaetech.Collections.Generic.Extensions
{
    public static class ParametersExtensions    
    {        
        public static Parameters ToParameters<TEntity>(this IEnumerable<TEntity> list,
            Expression<Func<TEntity, string>> name,
            Expression<Func<TEntity, object>> value)
            where TEntity : class
        {
            var keyExpression = name.Body as MemberExpression;
            var value1Expression = value.Body as MemberExpression;

            PropertyInfo keyInfo = typeof(TEntity).GetProperty(keyExpression.Member.Name);
            PropertyInfo value1Info = typeof(TEntity).GetProperty(value1Expression.Member.Name);

            Parameters parameters = new Parameters();

            foreach (TEntity entity in list)
            {
                string _name = (string)keyInfo.GetValue(entity);
                object _value = value1Info.GetValue(entity);
                parameters.Add(_name, _value, Parameters.TypeMap[_value.GetType()]);
            }

            return parameters;
        }
    }
}
