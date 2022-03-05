using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Vaetech.Collections.Generic.Extensions
{
    public static class ArrayListExtensions
    {        
        public static ArrayList<TKey, TValue1> ToArrayList<TEntity, TKey, TValue1>(this IEnumerable<TEntity> list, 
            Expression<Func<TEntity, TKey>> key, 
            Expression<Func<TEntity, TValue1>> value1)
            where TEntity : class
        {
            var keyExpression = key.Body as MemberExpression;
            var value1Expression = value1.Body as MemberExpression;

            PropertyInfo keyInfo = typeof(TEntity).GetProperty(keyExpression.Member.Name);
            PropertyInfo value1Info = typeof(TEntity).GetProperty(value1Expression.Member.Name);

            ArrayList<TKey,TValue1> arrays = new ArrayList<TKey, TValue1>();

            foreach (TEntity entity in list)
            {
                TKey key_ = (TKey)keyInfo.GetValue(entity);
                TValue1 value_ = (TValue1)value1Info.GetValue(entity);
                arrays.Add(new Array<TKey, TValue1>(key_,value_));
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
