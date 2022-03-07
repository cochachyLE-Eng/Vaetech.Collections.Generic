﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vaetech.Collections.Generic.Types;

namespace Vaetech.Collections.Generic
{
    public class ArrayList<TKey, TValue1, TValue2> : IList<Array<TKey, TValue1, TValue2>>
    {
        #region Variables
        private List<Array<TKey, TValue1, TValue2>> arrays = new List<Array<TKey, TValue1, TValue2>>();        
        #endregion

        #region Constructor 
        public ArrayList() { }
        public ArrayList(Array<TKey, TValue1, TValue2> array) => this.Add(array);
        public ArrayList(IEnumerable<Array<TKey, TValue1, TValue2>> list) 
        {
            Range range = GetRangeAdd(list.Count());
            this.arrays = new List<Array<TKey, TValue1, TValue2>>(list);
            OnDataSourceChanged(this.arrays, this.Count, range, ArrayListMethodType.AddRange);
        }        
        #endregion

        #region Events
        public Events.ArrayListEventHandler<TKey, TValue1, TValue2> DataSourceChangeHandler;
        #endregion

        #region Properties
        public virtual int Count => this.arrays.Count;
        public virtual bool IsReadOnly => false;
        public virtual Array<TKey, TValue1, TValue2> this[int index]
        {
            get => arrays[index];
            set => arrays[index] = value;
        }
        public virtual Array<TKey, TValue1, TValue2> this[TKey key]
        {
            get => this.SingleOrDefault(p => p.Key.Equals(key));
            set 
            {
                if (!arrays.Exists(p => p.Key.Equals(key)))
                    this.Add(new Array<TKey, TValue1, TValue2>(key, value.Value1, value.Value2));
                else
                {
                    Array<TKey, TValue1, TValue2> array = this.Single(p => p.Key.Equals(key));
                    array.Value1 = value.Value1;
                    array.Value2 = value.Value2;
                }
            }
        }
        
        public virtual IEnumerable<TKey> Identifiers => this.arrays.Select(p => p.Key);
        #endregion

        #region Methods
        private Range GetRangeAdd(int count) => new Range(position: this.arrays.Any() ? this.Count - 1 : 0, count);
        public virtual void Add(TKey key, TValue1 value1, TValue2 value2) => this.Add(new Array<TKey, TValue1, TValue2>(key, value1, value2));
        public virtual void Add(Array<TKey, TValue1, TValue2> item) 
        {
            Range range = GetRangeAdd(1);
            this.arrays.Add(item);            
            OnDataSourceChanged(item, this.Count, range, ArrayListMethodType.Add);
        }        
        public virtual void Clear()
        {
            Range range = new Range(0, this.Count);
            this.arrays.Clear(); 
            OnDataSourceChanged((ArrayList<TKey,TValue1, TValue2>)null, this.Count, range, ArrayListMethodType.Clear);
        }        
        public virtual bool Contains(Array<TKey,TValue1, TValue2> parameter) => this.arrays.Contains(parameter);
        public virtual void CopyTo(Array<TKey, TValue1, TValue2>[] parametersArray, int arrayIndex) => this.arrays.CopyTo(parametersArray, arrayIndex);
        public virtual bool Remove(Array<TKey, TValue1, TValue2> parameter)
        {
            Range range = new Range(this.arrays.IndexOf(parameter), 1);
            bool success = this.arrays.Remove(parameter);
            OnDataSourceChanged(parameter, this.Count, range, ArrayListMethodType.Remove);
            return success;
        }
        public virtual int IndexOf(Array<TKey,TValue1, TValue2> parameter) => this.arrays.IndexOf(parameter);
        public virtual void Insert(int index, Array<TKey, TValue1, TValue2> parameter) 
        {
            Range range = new Range(index,1);
            this.arrays.Insert(index, parameter);
            OnDataSourceChanged(parameter, this.Count, range, ArrayListMethodType.Insert);
        }
        public virtual void RemoveAt(int index)
        {
            Range range = new Range(index, 1);
            Array<TKey, TValue1, TValue2> parameter = this.Count-1 >= index ? this.arrays[index]: null;
            this.arrays.RemoveAt(index);
            OnDataSourceChanged(parameter, this.Count, range, ArrayListMethodType.RemoveAt);
        }

        public virtual IEnumerator<Array<TKey,TValue1, TValue2>> GetEnumerator() => this.arrays.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.arrays.GetEnumerator();        

        private void OnDataSourceChanged(Array<TKey, TValue1, TValue2> array, int count, Range range, ArrayListMethodType methodType)
            => DataSourceChangeHandler?.Invoke(this, new Events.ArrayListEventArgs<TKey, TValue1, TValue2>(array, count, range, methodType));
        private void OnDataSourceChanged(IList<Array<TKey, TValue1, TValue2>> list, int count, Range range, ArrayListMethodType methodType)
            => DataSourceChangeHandler?.Invoke(this, new Events.ArrayListEventArgs<TKey, TValue1, TValue2>(list, count, range, methodType));
        private void OnDataSourceChanged(ArrayList<TKey, TValue1, TValue2> list, int count, Range range, ArrayListMethodType methodType)
            => DataSourceChangeHandler?.Invoke(this, new Events.ArrayListEventArgs<TKey, TValue1, TValue2>(list, count, range, methodType));
        #endregion
    }
}