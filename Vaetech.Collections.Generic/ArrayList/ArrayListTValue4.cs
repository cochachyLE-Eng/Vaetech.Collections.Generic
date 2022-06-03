using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vaetech.Collections.Generic.Types;

namespace Vaetech.Collections.Generic
{
    public class ArrayList<TKey, TValue1, TValue2, TValue3, TValue4> : IList<Array<TKey, TValue1, TValue2, TValue3, TValue4>>
    {
        #region Variables
        private List<Array<TKey, TValue1, TValue2, TValue3, TValue4>> arrays = new List<Array<TKey, TValue1, TValue2, TValue3, TValue4>>();        
        #endregion

        #region Constructor 
        public ArrayList() { }
        public ArrayList(Array<TKey, TValue1, TValue2, TValue3, TValue4> array) => this.Add(array);
        public ArrayList(IEnumerable<Array<TKey, TValue1, TValue2, TValue3, TValue4>> list) 
        {
            Range range = GetRangeAdd(list.Count());
            this.arrays = new List<Array<TKey, TValue1, TValue2, TValue3, TValue4>>(list);
            OnDataSourceChanged(this.arrays, this.Count, range, ArrayListMethodType.AddRange);
        }        
        #endregion

        #region Events
        public Events.ArrayListEventHandler<TKey, TValue1, TValue2, TValue3, TValue4> DataSourceChangeHandler;
        #endregion

        #region Properties
        public virtual int Count => this.arrays.Count;
        public virtual bool IsReadOnly => false;
        public virtual Array<TKey, TValue1, TValue2, TValue3, TValue4> this[int index]
        {
            get => arrays[index];
            set => arrays[index] = value;
        }
        public virtual Array<TKey, TValue1, TValue2, TValue3, TValue4> this[TKey key]
        {
            get => this.SingleOrDefault(p => p.Key.Equals(key));
            set
            {
                if (!arrays.Exists(p => p.Key.Equals(key)))
                    this.Add(new Array<TKey, TValue1, TValue2, TValue3, TValue4>(key, value.Value1, value.Value2, value.Value3, value.Value4));
                else
                {
                    Array<TKey, TValue1, TValue2, TValue3, TValue4> array = this.Single(p => p.Key.Equals(key));
                    array.Value1 = value.Value1;
                    array.Value2 = value.Value2;
                    array.Value3 = value.Value3;
                    array.Value4 = value.Value4;
                }
            }
        }
        public virtual IEnumerable<TKey> Identifiers => this.arrays.Select(p => p.Key);
        #endregion

        #region Methods
        private Range GetRangeAdd(int count) => new Range(position: this.arrays.Any() ? this.Count - 1 : 0, count);
        public virtual void Add(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4) => this.Add(new Array<TKey, TValue1, TValue2, TValue3, TValue4>(key, value1, value2, value3, value4));
        public virtual void Add(Array<TKey, TValue1, TValue2, TValue3, TValue4> item) 
        {
            Range range = GetRangeAdd(1);
            this.arrays.Add(item);            
            OnDataSourceChanged(item, this.Count, range, ArrayListMethodType.Add);
        }
        public virtual void AddOrReplace(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4)
            => this.AddOrReplace(new Array<TKey, TValue1, TValue2, TValue3, TValue4>(key, value1, value2, value3, value4));
        public virtual void AddOrReplace(Array<TKey, TValue1, TValue2, TValue3, TValue4> item)
        {
            Range range = GetRangeAdd(1);
            if (!this.arrays.Exists(c => c.Key.Equals(item.Key)))
                this.arrays.Add(item);
            else
            {
                var itemsingle = this.arrays.Single(c => c.Key.Equals(item.Key));
                itemsingle.Value1 = item.Value1;
                itemsingle.Value2 = item.Value2;
                itemsingle.Value3 = item.Value3;
                itemsingle.Value4 = item.Value4;
            }
            OnDataSourceChanged(item, this.Count, range, ArrayListMethodType.Add);
        }
        public virtual void Clear()
        {
            Range range = new Range(0, this.Count);
            this.arrays.Clear(); 
            OnDataSourceChanged((ArrayList<TKey,TValue1, TValue2, TValue3, TValue4>)null, this.Count, range, ArrayListMethodType.Clear);
        }        
        public virtual bool Contains(Array<TKey,TValue1, TValue2, TValue3, TValue4> parameter) => this.arrays.Contains(parameter);
        public virtual void CopyTo(Array<TKey, TValue1, TValue2, TValue3, TValue4>[] parametersArray, int arrayIndex) => this.arrays.CopyTo(parametersArray, arrayIndex);
        public virtual bool Remove(Array<TKey, TValue1, TValue2, TValue3, TValue4> parameter)
        {
            Range range = new Range(this.arrays.IndexOf(parameter), 1);
            bool success = this.arrays.Remove(parameter);
            OnDataSourceChanged(parameter, this.Count, range, ArrayListMethodType.Remove);
            return success;
        }
        public virtual int IndexOf(Array<TKey,TValue1, TValue2, TValue3, TValue4> parameter) => this.arrays.IndexOf(parameter);
        public virtual void Insert(int index, Array<TKey, TValue1, TValue2, TValue3, TValue4> parameter) 
        {
            Range range = new Range(index,1);
            this.arrays.Insert(index, parameter);
            OnDataSourceChanged(parameter, this.Count, range, ArrayListMethodType.Insert);
        }
        public virtual void RemoveAt(int index)
        {
            Range range = new Range(index, 1);
            Array<TKey, TValue1, TValue2, TValue3, TValue4> parameter = this.Count-1 >= index ? this.arrays[index]: null;
            this.arrays.RemoveAt(index);
            OnDataSourceChanged(parameter, this.Count, range, ArrayListMethodType.RemoveAt);
        }

        public virtual IEnumerator<Array<TKey,TValue1, TValue2, TValue3, TValue4>> GetEnumerator() => this.arrays.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.arrays.GetEnumerator();        

        private void OnDataSourceChanged(Array<TKey, TValue1, TValue2, TValue3, TValue4> array, int count, Range range, ArrayListMethodType methodType)
            => DataSourceChangeHandler?.Invoke(this, new Events.ArrayListEventArgs<TKey, TValue1, TValue2, TValue3, TValue4>(array, count, range, methodType));
        private void OnDataSourceChanged(IList<Array<TKey, TValue1, TValue2, TValue3, TValue4>> list, int count, Range range, ArrayListMethodType methodType)
            => DataSourceChangeHandler?.Invoke(this, new Events.ArrayListEventArgs<TKey, TValue1, TValue2, TValue3, TValue4>(list, count, range, methodType));
        private void OnDataSourceChanged(ArrayList<TKey, TValue1, TValue2, TValue3, TValue4> list, int count, Range range, ArrayListMethodType methodType)
            => DataSourceChangeHandler?.Invoke(this, new Events.ArrayListEventArgs<TKey, TValue1, TValue2, TValue3, TValue4>(list, count, range, methodType));
        #endregion
    }
}
