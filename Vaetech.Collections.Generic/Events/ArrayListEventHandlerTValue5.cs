using System;
using System.Collections.Generic;
using Vaetech.Collections.Generic.Types;

namespace Vaetech.Collections.Generic.Events
{        
    public delegate void ArrayListEventHandler<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>(object sender, ArrayListEventArgs<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> e);
    public class ArrayListEventArgs<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> : EventArgs
    {        
        public IList<Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>> Arrays { get; set; }
        public long Count { get; set; }
        public Range Range { get; set; }
        public ArrayListMethodType MethodType { get; set; }
        public ArrayListEventArgs(Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> array, int count, Range range, ArrayListMethodType methodType)
        {
            Arrays = new ArrayList<TKey,TValue1, TValue2, TValue3, TValue4, TValue5>(array);
            Count = count;
            Range = range;
            MethodType = methodType;
        }
        public ArrayListEventArgs(IList<Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>> array, int count, Range range, ArrayListMethodType methodType)
        {
            Arrays = new ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>(array);
            Count = count;
            Range = range;
            MethodType = methodType;
        }
        public ArrayListEventArgs(ArrayList<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> arrays, int count, Range range, ArrayListMethodType methodType) {
            Arrays = arrays;
            Count = count;
            Range = range;
            MethodType = methodType;
        }
    }
}
