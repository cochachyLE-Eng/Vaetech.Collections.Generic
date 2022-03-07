using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Vaetech.Collections.Generic.Extensions
{
    public static class ArrayListExtensions
    {                
        public static ArrayList<TKey, TValue1> ToArrayList<TKey, TValue1, TEntity>(this IEnumerable<TEntity> list, 
            Expression<Func<TEntity, TKey>> key, 
            Expression<Func<TEntity, TValue1>> value1)
            where TEntity : class
        {
            PropertyInfo piKey = null, piValue1 = null;
            ArrayList<string, MethodInfo, object[]> miKey = new ArrayList<string, MethodInfo, object[]>(), miValue1 = new ArrayList<string, MethodInfo, object[]>();

            if (key.Body is MemberExpression)
            { 
                MemberExpression mekey = key.Body as MemberExpression;
                piKey = typeof(TEntity).GetProperty(mekey.Member.Name);
            }

            if (value1.Body is MemberExpression)
            {
                MemberExpression me = value1.Body as MemberExpression;
                piValue1 = typeof(TEntity).GetProperty(me.Member.Name);
            }
            else if (value1.Body is MethodCallExpression)
            {
                MethodCallExpression mce = value1.Body as MethodCallExpression;
                miValue1.Insert(0, new Array<string, MethodInfo, object[]>(mce.Method.Name, mce.Method, mce.Arguments?.ToArray()));

                if (mce.Object is MemberExpression)
                    piValue1 = typeof(TEntity).GetProperty((mce.Object as MemberExpression).Member.Name);
                else if (mce.Object is MethodCallExpression)
                {
                    do
                    {
                        mce = mce.Object as MethodCallExpression;
                        miValue1.Insert(0, new Array<string, MethodInfo, object[]>(mce.Method.Name, mce.Method, mce.Arguments?.ToArray()));

                        if (mce.Object is MemberExpression)
                        {
                            piValue1 = typeof(TEntity).GetProperty((mce.Object as MemberExpression).Member.Name);
                            break;
                        }
                        else if (mce.Object is BinaryExpression)
                        {
                            BinaryExpression bi = mce.Object as BinaryExpression;
                            if (bi.Left is UnaryExpression)
                            {
                                UnaryExpression ue = bi.Left as UnaryExpression;
                                if (ue.Operand is MemberExpression)
                                {
                                    piValue1 = typeof(TEntity).GetProperty((ue.Operand as MemberExpression).Member.Name);

                                    if (bi.Left.NodeType == ExpressionType.Convert)
                                    {
                                        MethodInfo mconvert = (MethodInfo)typeof(Convert).GetMember(nameof(Convert.ChangeType))[0];
                                        TypeCode typeCode = Type.GetTypeCode(bi.Left.Type);
                                        miValue1.Insert(0, new Array<string, MethodInfo, object[]>(mconvert.Name, mconvert, new object[] { default(object), typeCode }));
                                    }
                                }
                            }

                            if (bi.NodeType == ExpressionType.Coalesce)
                            {
                                if (bi.Right is ConstantExpression)
                                {
                                    object defaultValue = (bi.Right as ConstantExpression).Value;
                                    MethodInfo mcoalesce = (MethodInfo)typeof(PropertyExtensions).GetMember(nameof(PropertyExtensions.Coalesce))[0];
                                    miValue1.Insert(1, new Array<string, MethodInfo, object[]>(mcoalesce.Name, mcoalesce, new object[] { default(object), defaultValue }));
                                }
                            }
                        }
                    }
                    while (mce.Object is MethodCallExpression);
                }
                else if (mce.Object is BinaryExpression)
                {                    
                    BinaryExpression bi = mce.Object as BinaryExpression;

                    if (bi.Left is MemberExpression)                    
                        piValue1 = typeof(TEntity).GetProperty((bi.Left as MemberExpression).Member.Name);                    
                    else if (bi.Left is MethodCallExpression)
                    {
                        do
                        {                            
                            mce = bi.Left as MethodCallExpression;
                            miValue1.Insert(0, new Array<string, MethodInfo, object[]>(mce.Method.Name, mce.Method, mce.Arguments?.ToArray()));

                            if (mce.Object is MemberExpression)
                            {
                                piValue1 = typeof(TEntity).GetProperty((mce.Object as MemberExpression).Member.Name);
                                break;
                            }
                        }
                        while (mce.Object is MethodCallExpression);
                    }
                    if (bi.NodeType == ExpressionType.Coalesce)
                    {
                        if (bi.Right is ConstantExpression)
                        {
                            object defaultValue = (bi.Right as ConstantExpression).Value;
                            MethodInfo mcoalesce = (MethodInfo)typeof(PropertyExtensions).GetMember(nameof(PropertyExtensions.Coalesce))[0];
                            miValue1.Insert(0, new Array<string, MethodInfo, object[]>(mcoalesce.Name, mcoalesce, new object[] { default(object), defaultValue }));
                        }
                    }
                }
                else if (mce.Arguments.Any())
                {
                    List<Expression> arguments = mce.Arguments.ToList();
                    do
                    {
                        Expression expression = arguments.Single();
                        if (expression is MemberExpression)
                        {
                            piValue1 = typeof(TEntity).GetProperty((expression as MemberExpression).Member.Name);
                            break;
                        }
                        arguments.Remove(expression);
                    }
                    while (arguments.Any());
                }
            }
            else if (value1.Body is UnaryExpression)
            {
                UnaryExpression ue = value1.Body as UnaryExpression;
                if (ue.Operand is MemberExpression)
                {
                    piValue1 = typeof(TEntity).GetProperty((ue.Operand as MemberExpression).Member.Name);
                }
            }
            else if (value1.Body is LambdaExpression)
            {
                LambdaExpression le = value1.Body as LambdaExpression;
                if (le.Body is MemberExpression)
                {
                    piValue1 = typeof(TEntity).GetProperty((le.Body as MemberExpression).Member.Name);
                }
            }

            ArrayList<TKey,TValue1> arrays = new ArrayList<TKey, TValue1>();
            
            if (piKey == null && value1 == null)
                throw new NotImplementedException();

            foreach (TEntity entity in list)
            {
                Array<TKey, TValue1> array = new Array<TKey, TValue1>();
                object valueAux_ = piValue1.GetValue(entity);

                if (miValue1.Any())
                {
                    for (int i = 0; i < miValue1.Count; i++)
                    {
                        List<object> parameters = new List<object>();

                        if (miValue1[i].Value2 is Expression[])
                        {
                            foreach (Expression param in miValue1[i].Value2)
                            {
                                if (param is MemberExpression)
                                {
                                    object p = param.GetType().GetProperty((param as MemberExpression).Member.Name);
                                    parameters.Add(p);
                                }
                                else if (param is NewArrayExpression)
                                {
                                    List<Expression> arrayExpression = (param as NewArrayExpression).Expressions.ToList();

                                    if (arrayExpression.Any())
                                    {
                                        #region Code not rated! {...}
                                        List<object> @params = new List<object>();

                                        foreach (var exp in arrayExpression)
                                        {
                                            if (exp is MemberExpression)
                                            {
                                                object pex = exp.GetType().GetProperty((exp as MemberExpression).Member.Name);
                                                @params.Add(pex);
                                            }
                                        }
                                        parameters.Add(@params);
                                        #endregion
                                    }
                                    else
                                    {
                                        if (param.Type == typeof(char[]))
                                            parameters.Add(new char[] { });
                                        else if (param.Type == typeof(byte[]))
                                            parameters.Add(new byte[] { });
                                        else if (param.Type == typeof(sbyte[]))
                                            parameters.Add(new sbyte[] { });
                                        else if (param.Type == typeof(short[]))
                                            parameters.Add(new short[] { });
                                        else if (param.Type == typeof(int[]))
                                            parameters.Add(new int[] { });
                                        else if (param.Type == typeof(long[]))
                                            parameters.Add(new long[] { });
                                        else if (param.Type == typeof(ushort[]))
                                            parameters.Add(new ushort[] { });
                                        else if (param.Type == typeof(uint[]))
                                            parameters.Add(new uint[] { });
                                        else if (param.Type == typeof(ulong[]))
                                            parameters.Add(new ulong[] { });
                                        else if (param.Type == typeof(bool[]))
                                            parameters.Add(new bool[] { });
                                        else if (param.Type == typeof(float[]))
                                            parameters.Add(new float[] { });
                                        else if (param.Type == typeof(double[]))
                                            parameters.Add(new double[] { });
                                        else if (param.Type == typeof(decimal[]))
                                            parameters.Add(new decimal[] { });
                                    }
                                }
                            }
                        }
                        else if (miValue1[i].Value2 is object[])
                        {
                            switch (miValue1[i].Key)
                            {
                                case nameof(Convert.ChangeType):
                                    {
                                        miValue1[i].Value2[0] = valueAux_;
                                        parameters.AddRange(miValue1[i].Value2);
                                    }
                                    break;
                                case nameof(PropertyExtensions.Coalesce):
                                    {
                                        TypeCode typeCode = Type.GetTypeCode(miValue1[i].Value2[1].GetType());
                                        miValue1[i].Value2[0] = Convert.ChangeType(valueAux_, typeCode);
                                        parameters.AddRange(miValue1[i].Value2);
                                    }
                                    break;
                            }
                        }
                        
                        valueAux_ = miValue1[i].Value1.Invoke(valueAux_, parameters.ToArray());
                    }
                }                

                array.Key = piKey.GetValue(entity).Parse<TKey>();
                array.Value1 = valueAux_.Parse<TValue1>();
                
                arrays.Add(array);
            }

            return arrays;
        }
        public static ArrayList<TKey, TValue1, TValue2> ToArrayList<TEntity, TKey, TValue1, TValue2>(this IEnumerable<TEntity> list, 
            Expression<Func<TEntity, TKey>> key, 
            Expression<Func<TEntity, TValue1>> value1, 
            Expression<Func<TEntity, TValue2>> value2)
            where TEntity : class
        {
            var keyExpression = key.Body as MemberExpression;
            var value1Expression = value1.Body as MemberExpression;
            var value2Expression = value2.Body as MemberExpression;

            PropertyInfo keyInfo = typeof(TEntity).GetProperty(keyExpression.Member.Name);
            PropertyInfo value1Info = typeof(TEntity).GetProperty(value1Expression.Member.Name);
            PropertyInfo value2Info = typeof(TEntity).GetProperty(value2Expression.Member.Name);

            ArrayList<TKey, TValue1, TValue2> arrays = new ArrayList<TKey, TValue1, TValue2>();

            foreach (TEntity entity in list)
            {
                TKey key_ = (TKey)keyInfo.GetValue(entity);
                TValue1 value_1 = (TValue1)value1Info.GetValue(entity);
                TValue2 value_2 = (TValue2)value2Info.GetValue(entity);
                arrays.Add(new Array<TKey, TValue1, TValue2>(key_, value_1, value_2));
            }

            return arrays;
        }
        public static ArrayList<TKey, TValue1, TValue2, TValue3> ToArrayList<TEntity, TKey, TValue1, TValue2, TValue3>(this IEnumerable<TEntity> list, 
            Expression<Func<TEntity, TKey>> key, Expression<Func<TEntity, TValue1>> value1, 
            Expression<Func<TEntity, TValue2>> value2, 
            Expression<Func<TEntity, TValue3>> value3)
            where TEntity : class
        {
            var keyExpression = key.Body as MemberExpression;
            var value1Expression = value1.Body as MemberExpression;
            var value2Expression = value2.Body as MemberExpression;
            var value3Expression = value3.Body as MemberExpression;

            PropertyInfo keyInfo = typeof(TEntity).GetProperty(keyExpression.Member.Name);
            PropertyInfo value1Info = typeof(TEntity).GetProperty(value1Expression.Member.Name);
            PropertyInfo value2Info = typeof(TEntity).GetProperty(value2Expression.Member.Name);
            PropertyInfo value3Info = typeof(TEntity).GetProperty(value3Expression.Member.Name);

            ArrayList<TKey, TValue1, TValue2, TValue3> arrays = new ArrayList<TKey, TValue1, TValue2, TValue3>();

            foreach (TEntity entity in list)
            {
                TKey key_ = (TKey)keyInfo.GetValue(entity);
                TValue1 value_1 = (TValue1)value1Info.GetValue(entity);
                TValue2 value_2 = (TValue2)value2Info.GetValue(entity);
                TValue3 value_3 = (TValue3)value3Info.GetValue(entity);
                arrays.Add(new Array<TKey, TValue1, TValue2, TValue3>(key_, value_1, value_2, value_3));
            }

            return arrays;
        }
        public static ArrayList<TKey, TValue1, TValue2, TValue3, TValue4> ToArrayList<TEntity, TKey, TValue1, TValue2, TValue3, TValue4>(this IEnumerable<TEntity> list,
            Expression<Func<TEntity, TKey>> key, Expression<Func<TEntity, TValue1>> value1,
            Expression<Func<TEntity, TValue2>> value2,
            Expression<Func<TEntity, TValue3>> value3, 
            Expression<Func<TEntity, TValue4>> value4)
            where TEntity : class
        {
            var keyExpression = key.Body as MemberExpression;
            var value1Expression = value1.Body as MemberExpression;
            var value2Expression = value2.Body as MemberExpression;
            var value3Expression = value3.Body as MemberExpression;
            var value4Expression = value4.Body as MemberExpression;

            PropertyInfo keyInfo = typeof(TEntity).GetProperty(keyExpression.Member.Name);
            PropertyInfo value1Info = typeof(TEntity).GetProperty(value1Expression.Member.Name);
            PropertyInfo value2Info = typeof(TEntity).GetProperty(value2Expression.Member.Name);
            PropertyInfo value3Info = typeof(TEntity).GetProperty(value3Expression.Member.Name);
            PropertyInfo value4Info = typeof(TEntity).GetProperty(value4Expression.Member.Name);

            ArrayList<TKey, TValue1, TValue2, TValue3, TValue4> arrays = new ArrayList<TKey, TValue1, TValue2, TValue3, TValue4>();

            foreach (TEntity entity in list)
            {
                TKey key_ = (TKey)keyInfo.GetValue(entity);
                TValue1 value_1 = (TValue1)value1Info.GetValue(entity);
                TValue2 value_2 = (TValue2)value2Info.GetValue(entity);
                TValue3 value_3 = (TValue3)value3Info.GetValue(entity);
                TValue4 value_4 = (TValue4)value4Info.GetValue(entity);
                arrays.Add(new Array<TKey, TValue1, TValue2, TValue3, TValue4>(key_, value_1, value_2, value_3, value_4));
            }

            return arrays;
        }
        public static ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> ToArrayList<TEntity, TKey, TValue1, TValue2, TValue3, TValue4, TValue5>(this IEnumerable<TEntity> list,
            Expression<Func<TEntity, TKey>> key, Expression<Func<TEntity, TValue1>> value1,
            Expression<Func<TEntity, TValue2>> value2,
            Expression<Func<TEntity, TValue3>> value3,
            Expression<Func<TEntity, TValue4>> value4,
            Expression<Func<TEntity, TValue5>> value5)
            where TEntity : class
        {
            var keyExpression = key.Body as MemberExpression;
            var value1Expression = value1.Body as MemberExpression;
            var value2Expression = value2.Body as MemberExpression;
            var value3Expression = value3.Body as MemberExpression;
            var value4Expression = value4.Body as MemberExpression;
            var value5Expression = value5.Body as MemberExpression;

            PropertyInfo keyInfo = typeof(TEntity).GetProperty(keyExpression.Member.Name);
            PropertyInfo value1Info = typeof(TEntity).GetProperty(value1Expression.Member.Name);
            PropertyInfo value2Info = typeof(TEntity).GetProperty(value2Expression.Member.Name);
            PropertyInfo value3Info = typeof(TEntity).GetProperty(value3Expression.Member.Name);
            PropertyInfo value4Info = typeof(TEntity).GetProperty(value4Expression.Member.Name);
            PropertyInfo value5Info = typeof(TEntity).GetProperty(value5Expression.Member.Name);

            ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> arrays = new ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>();

            foreach (TEntity entity in list)
            {
                TKey key_ = (TKey)keyInfo.GetValue(entity);
                TValue1 value_1 = (TValue1)value1Info.GetValue(entity);
                TValue2 value_2 = (TValue2)value2Info.GetValue(entity);
                TValue3 value_3 = (TValue3)value3Info.GetValue(entity);
                TValue4 value_4 = (TValue4)value4Info.GetValue(entity);
                TValue5 value_5 = (TValue5)value5Info.GetValue(entity);
                arrays.Add(new Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>(key_, value_1, value_2, value_3, value_4, value_5));
            }

            return arrays;
        }
        public static ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6> ToArrayList<TEntity, TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6>(this IEnumerable<TEntity> list,
            Expression<Func<TEntity, TKey>> key, Expression<Func<TEntity, TValue1>> value1,
            Expression<Func<TEntity, TValue2>> value2,
            Expression<Func<TEntity, TValue3>> value3,
            Expression<Func<TEntity, TValue4>> value4,
            Expression<Func<TEntity, TValue5>> value5,
            Expression<Func<TEntity, TValue6>> value6)
            where TEntity : class
        {
            var keyExpression = key.Body as MemberExpression;
            var value1Expression = value1.Body as MemberExpression;
            var value2Expression = value2.Body as MemberExpression;
            var value3Expression = value3.Body as MemberExpression;
            var value4Expression = value4.Body as MemberExpression;
            var value5Expression = value5.Body as MemberExpression;
            var value6Expression = value6.Body as MemberExpression;

            PropertyInfo keyInfo = typeof(TEntity).GetProperty(keyExpression.Member.Name);
            PropertyInfo value1Info = typeof(TEntity).GetProperty(value1Expression.Member.Name);
            PropertyInfo value2Info = typeof(TEntity).GetProperty(value2Expression.Member.Name);
            PropertyInfo value3Info = typeof(TEntity).GetProperty(value3Expression.Member.Name);
            PropertyInfo value4Info = typeof(TEntity).GetProperty(value4Expression.Member.Name);
            PropertyInfo value5Info = typeof(TEntity).GetProperty(value5Expression.Member.Name);
            PropertyInfo value6Info = typeof(TEntity).GetProperty(value6Expression.Member.Name);

            ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6> arrays = new ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6>();

            foreach (TEntity entity in list)
            {
                TKey key_ = (TKey)keyInfo.GetValue(entity);
                TValue1 value_1 = (TValue1)value1Info.GetValue(entity);
                TValue2 value_2 = (TValue2)value2Info.GetValue(entity);
                TValue3 value_3 = (TValue3)value3Info.GetValue(entity);
                TValue4 value_4 = (TValue4)value4Info.GetValue(entity);
                TValue5 value_5 = (TValue5)value5Info.GetValue(entity);
                TValue6 value_6 = (TValue6)value6Info.GetValue(entity);
                arrays.Add(new Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6>(key_, value_1, value_2, value_3, value_4, value_5, value_6));
            }

            return arrays;
        }
        public static ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7> ToArrayList<TEntity, TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7>(this IEnumerable<TEntity> list,
            Expression<Func<TEntity, TKey>> key, Expression<Func<TEntity, TValue1>> value1,
            Expression<Func<TEntity, TValue2>> value2,
            Expression<Func<TEntity, TValue3>> value3,
            Expression<Func<TEntity, TValue4>> value4,
            Expression<Func<TEntity, TValue5>> value5,
            Expression<Func<TEntity, TValue6>> value6,
            Expression<Func<TEntity, TValue7>> value7)
            where TEntity : class
        {
            var keyExpression = key.Body as MemberExpression;
            var value1Expression = value1.Body as MemberExpression;
            var value2Expression = value2.Body as MemberExpression;
            var value3Expression = value3.Body as MemberExpression;
            var value4Expression = value4.Body as MemberExpression;
            var value5Expression = value5.Body as MemberExpression;
            var value6Expression = value6.Body as MemberExpression;
            var value7Expression = value7.Body as MemberExpression;

            PropertyInfo keyInfo = typeof(TEntity).GetProperty(keyExpression.Member.Name);
            PropertyInfo value1Info = typeof(TEntity).GetProperty(value1Expression.Member.Name);
            PropertyInfo value2Info = typeof(TEntity).GetProperty(value2Expression.Member.Name);
            PropertyInfo value3Info = typeof(TEntity).GetProperty(value3Expression.Member.Name);
            PropertyInfo value4Info = typeof(TEntity).GetProperty(value4Expression.Member.Name);
            PropertyInfo value5Info = typeof(TEntity).GetProperty(value5Expression.Member.Name);
            PropertyInfo value6Info = typeof(TEntity).GetProperty(value6Expression.Member.Name);
            PropertyInfo value7Info = typeof(TEntity).GetProperty(value7Expression.Member.Name);

            ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7> arrays = new ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7>();

            foreach (TEntity entity in list)
            {
                TKey key_ = (TKey)keyInfo.GetValue(entity);
                TValue1 value_1 = (TValue1)value1Info.GetValue(entity);
                TValue2 value_2 = (TValue2)value2Info.GetValue(entity);
                TValue3 value_3 = (TValue3)value3Info.GetValue(entity);
                TValue4 value_4 = (TValue4)value4Info.GetValue(entity);
                TValue5 value_5 = (TValue5)value5Info.GetValue(entity);
                TValue6 value_6 = (TValue6)value6Info.GetValue(entity);
                TValue7 value_7 = (TValue7)value7Info.GetValue(entity);
                arrays.Add(new Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7>(key_, value_1, value_2, value_3, value_4, value_5, value_6, value_7));
            }

            return arrays;
        }        
    }    
}
