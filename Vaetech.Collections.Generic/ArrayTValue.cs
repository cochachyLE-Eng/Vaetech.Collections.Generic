namespace Vaetech.Collections.Generic
{    
    public class Array<TKey, TValue1>
    {
        public Array() { }
        public Array(TKey key, TValue1 value1)
        {
            Key = key;
            Value1 = value1;
        }
        public Array(long index, TKey key, TValue1 value1)
        {
            Index = index;
            Key = key;
            Value1 = value1;
        }
        public long Index { get; private set; } = 0;
        public TKey Key { get; set; }
        public TValue1 Value1 { get; set; }
    }
    public class Array<TKey, TValue1, TValue2> : Array<TKey, TValue1>
    {
        public Array(TKey key, TValue1 value1, TValue2 value2) : base(key, value1)
        {
            Key = key;
            Value2 = value2;
        }
        public TValue2 Value2 { get; set; }
    }
    public class Array<TKey, TValue1, TValue2, TValue3> : Array<TKey, TValue1, TValue2>
    {
        public Array(TKey key, TValue1 value1, TValue2 value2, TValue3 value3) : base(key, value1, value2)
        {
            Key = key;
            Value3 = value3;
        }
        public TValue3 Value3 { get; set; }
    }
    public class Array<TKey, TValue1, TValue2, TValue3, TValue4> : Array<TKey, TValue1, TValue2, TValue3>
    {
        public Array(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4) : base(key, value1, value2, value3)
        {
            Key = key;
            Value4 = value4;
        }
        public TValue4 Value4 { get; set; }
    }
    public class Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> : Array<TKey, TValue1, TValue2, TValue3, TValue4>
    {
        public Array(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5) : base(key, value1, value2, value3, value4)
        {
            Key = key;
            Value5 = value5;
        }
        public TValue5 Value5 { get; set; }
    }
    public class Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6> : Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>
    {
        public Array(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5, TValue6 value6) : base(key, value1, value2, value3, value4, value5)
        {
            Key = key;
            Value6 = value6;
        }
        public TValue6 Value6 { get; set; }
    }
    public class Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6, TValue7> : Array<TKey, TValue1, TValue2, TValue3, TValue4, TValue5, TValue6>
    {
        public Array(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5, TValue6 value6, TValue7 value7) : base(key, value1, value2, value3, value4, value5, value6)
        {
            Key = key;
            Value7 = value7;
        }
        public TValue7 Value7 { get; set; }
    }
}
