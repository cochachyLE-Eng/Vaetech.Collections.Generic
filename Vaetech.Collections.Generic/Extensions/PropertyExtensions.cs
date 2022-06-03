using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Vaetech.Collections.Generic.Expressions;

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

		public static Property ProcessUnaryExpression(UnaryExpression expression)
		{
			return null;
		}
		
		public static Property ConvertExpression<TElement>(Expression exp)
		{
			if (exp == null) return null;

			switch (exp.NodeType)
			{
				case ExpressionType.Negate:
				case ExpressionType.NegateChecked:
				case ExpressionType.Not:
				case ExpressionType.Convert:
				case ExpressionType.ConvertChecked:
				case ExpressionType.ArrayLength:
				case ExpressionType.Quote:
				case ExpressionType.TypeAs:
				case ExpressionType.UnaryPlus:
					return ConvertUnary<TElement>(exp as UnaryExpression);
				case ExpressionType.Add:
				case ExpressionType.AddChecked:
				case ExpressionType.Subtract:
				case ExpressionType.SubtractChecked:
				case ExpressionType.Multiply:
				case ExpressionType.MultiplyChecked:
				case ExpressionType.Divide:
				case ExpressionType.Power:
				case ExpressionType.Modulo:
				case ExpressionType.And:
				case ExpressionType.AndAlso:
				case ExpressionType.Or:
				case ExpressionType.OrElse:
				case ExpressionType.LessThan:
				case ExpressionType.LessThanOrEqual:
				case ExpressionType.GreaterThan:
				case ExpressionType.GreaterThanOrEqual:
				case ExpressionType.Equal:
				case ExpressionType.NotEqual:
				case ExpressionType.Coalesce:
				case ExpressionType.ArrayIndex:
				case ExpressionType.RightShift:
				case ExpressionType.LeftShift:
				case ExpressionType.ExclusiveOr:
					return ConvertBinary<TElement>(exp as BinaryExpression);
				case ExpressionType.TypeIs:
					return ConvertTypeBinary<TElement>(exp as TypeBinaryExpression);
				case ExpressionType.Conditional:
					return ConvertConditional<TElement>(exp as ConditionalExpression);
				case ExpressionType.Constant:
					return ConvertConstant<TElement>(exp as ConstantExpression);
				case ExpressionType.Parameter:
					return ConvertParameter<TElement>(exp as ParameterExpression);
				case ExpressionType.MemberAccess:
					return ConvertMember<TElement>(exp as MemberExpression);
				case ExpressionType.Call:
					return ConvertMethodCall<TElement>(exp as MethodCallExpression);
				case ExpressionType.Lambda:
					return ConvertLambda<TElement>(exp as LambdaExpression);
				case ExpressionType.New:
					return ConvertNew<TElement>(exp as NewExpression);
				case ExpressionType.NewArrayInit:
				case ExpressionType.NewArrayBounds:
					return ConvertNewArray<TElement>(exp as NewArrayExpression);
				case ExpressionType.Invoke:
					return ConvertInvocation<TElement>(exp as InvocationExpression);
				case ExpressionType.MemberInit:
					return ConvertMemberInit<TElement>(exp as MemberInitExpression);
				case ExpressionType.ListInit:
					return ConvertListInit<TElement>(exp as ListInitExpression);
				default:
					throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
			}
		}
		public static Property ConvertUnary<TElement>(UnaryExpression expression)
		{
			return null;
		}
		public static Property ConvertBinary<TElement>(BinaryExpression expression)
		{
			return null;
		}
		public static Property ConvertTypeBinary<TElement>(TypeBinaryExpression expression)
		{
			return null;
		}
		public static Property ConvertConditional<TElement>(ConditionalExpression expression)
		{
			return null;
		}
		public static Property ConvertConstant<TElement>(ConstantExpression expression)
		{
			return null;
		}
		public static Property ConvertParameter<TElement>(ParameterExpression expression)
		{
			return null;
		}
		public static Property ConvertMember<TElement>(MemberExpression expression)
		{		
			return new Property()
			{
				Name = expression.Member.Name,
				Info = typeof(TElement).GetProperty(expression.Member.Name)
			};
		}
		public static Property ConvertMethodCall<TElement>(MethodCallExpression expression)
		{
			Property property = new Property();
			
			property.Methods.Insert(0, new Method(expression.Method, expression.Arguments?.ToArray()));

			if (expression.Object is MemberExpression)
				property.SetProperty(typeof(TElement).GetProperty((expression.Object as MemberExpression).Member.Name));
			else if (expression.Object is MethodCallExpression)
			{
				do
				{
					expression = expression.Object as MethodCallExpression;
					property.Methods.Insert(0, new Method(expression.Method, expression.Arguments?.ToArray()));

					if (expression.Object is MemberExpression)
					{
						property.SetProperty(typeof(TElement).GetProperty((expression.Object as MemberExpression).Member.Name));
						break;
					}
					else if (expression.Object is BinaryExpression)
					{
						BinaryExpression bi = expression.Object as BinaryExpression;
						if (bi.Left is UnaryExpression)
						{
							UnaryExpression ue = bi.Left as UnaryExpression;
							if (ue.Operand is MemberExpression)
							{
								property.SetProperty(typeof(TElement).GetProperty((ue.Operand as MemberExpression).Member.Name));

								if (bi.Left.NodeType == ExpressionType.Convert)
								{
									MethodInfo mconvert = (MethodInfo)typeof(Convert).GetMember(nameof(Convert.ChangeType))[0];
									TypeCode typeCode = Type.GetTypeCode(bi.Left.Type);

									property.Methods.Insert(0, new Method(mconvert, new object[] { default(object), typeCode }));
								}
							}
						}

						if (bi.NodeType == ExpressionType.Coalesce)
						{
							if (bi.Right is ConstantExpression)
							{
								object defaultValue = (bi.Right as ConstantExpression).Value;
								MethodInfo mcoalesce = (MethodInfo)typeof(PropertyExtensions).GetMember(nameof(PropertyExtensions.Coalesce))[0];
								property.Methods.Insert(0, new Method(mcoalesce, new object[] { default(object), defaultValue }));								
							}
						}
					}
				}
				while (expression.Object is MethodCallExpression);
			}
			else if (expression.Object is BinaryExpression)
			{
				BinaryExpression bi = expression.Object as BinaryExpression;

				if (bi.Left is MemberExpression)
					property.SetProperty(typeof(TElement).GetProperty((bi.Left as MemberExpression).Member.Name));
				else if (bi.Left is MethodCallExpression)
				{
					do
					{
						expression = bi.Left as MethodCallExpression;
						property.Methods.Insert(0, new Method(expression.Method, expression.Arguments?.ToArray()));

						if (expression.Object is MemberExpression)
						{
							property.SetProperty(typeof(TElement).GetProperty((expression.Object as MemberExpression).Member.Name));
							break;
						}
					}
					while (expression.Object is MethodCallExpression);
				}
				if (bi.NodeType == ExpressionType.Coalesce)
				{
					if (bi.Right is ConstantExpression)
					{
						object defaultValue = (bi.Right as ConstantExpression).Value;
						MethodInfo mcoalesce = (MethodInfo)typeof(PropertyExtensions).GetMember(nameof(PropertyExtensions.Coalesce))[0];
						property.Methods.Insert(0, new Method(mcoalesce, new object[] { default(object), defaultValue }));
					}
				}
			}
			else if (expression.Arguments.Any())
			{
				List<Expression> arguments = expression.Arguments.ToList();
				do
				{
					Expression expr = arguments.Single();
					if (expr is MemberExpression)
					{
						property.SetProperty(typeof(TElement).GetProperty((expr as MemberExpression).Member.Name));
						break;
					}
					arguments.Remove(expr);
				}
				while (arguments.Any());
			}
			return null;
		}
		public static Property ConvertLambda<TElement>(this Property property, LambdaExpression expression)
			=> property.SetMethods(ConvertLambda<TElement>(expression).Methods);
		public static Property ConvertLambda<TElement>(LambdaExpression expression)
		{
			return null;
		}
		public static Property ConvertNew<TElement>(NewExpression expression)
		{
			return null;
		}
		public static Property ConvertNewArray<TElement>(NewArrayExpression expression)
		{
			return null;
		}
		public static Property ConvertInvocation<TElement>(InvocationExpression expression)
        {
			return null;
        }
		public static Property ConvertMemberInit<TElement>(MemberInitExpression expression)
		{
			return null;
		}
		public static Property ConvertListInit<TElement>(ListInitExpression expression)
		{
			return null;
		}
	}
}
