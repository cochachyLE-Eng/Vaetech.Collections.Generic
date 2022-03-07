using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Vaetech.Collections.Generic
{
    public partial class Parameters : IList<Parameters.Parameter>
    {
        #region Variables
        private List<Parameter> parameters = new List<Parameter>();
        public static ArrayList<Type, DbType> TypeMap = new ArrayList<Type, DbType>();
        #endregion

        #region Constructor 
        public Parameters() => FillTypeMap();
        public Parameters(Parameter parameter) { this.Add(parameter); FillTypeMap(); }
        public Parameters(IEnumerable<Parameter> list) { this.parameters = new List<Parameter>(list); FillTypeMap(); }
        #endregion

        #region Properties
        public virtual int Count => this.parameters.Count;
        public virtual bool IsReadOnly => false;
        public virtual Parameter this[int index]
        {
            get => parameters[index];
            set => parameters[index] = value;
        }
        public virtual Parameter this[string name] {
            get => this.SingleOrDefault(p => p.Name.Equals(name));
            set
            {
                if (!parameters.Exists(p => p.Name.Equals(name)))
                    this.Add(value);
                else
                {
                    var param = this.Single(p => p.Name.Equals(name));
                    param.Name = value.Name;
                    param.Value = value.Value;
                    param.DbType = value.DbType ?? (value != null ? Parameters.TypeMap[value.Value.GetType()] : (DbType?) null);
                    param.Direction = value.Direction ?? ParameterDirection.Input;
                    param.Size = value.Size ?? -1;
                    param.Precision = value.Precision;
                    param.Scale = value.Scale;
                }
            }
        } 
        public virtual IEnumerable<string> Identifiers => this.parameters.Select(p => p.Name);
        #endregion

        #region Methods        
        private static string Clean(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                switch (name[0])
                {
                    case '@':
                    case ':':
                    case '?':
                        return name.Substring(1);
                }
            }
            return name;
        }
        public void Add(string name, object value, DbType? dbType, ParameterDirection? direction, int? size)
        {
            this[name] = new Parameter
            {
                Name = name,
                Value = value,
                DbType = dbType ?? (value != null ? Parameters.TypeMap[value.GetType()] : (DbType?) null),
                Direction = direction ?? ParameterDirection.Input,
                Size = size ?? -1
            };
        }
        public void Add(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
        {
            this[name] = new Parameter
            {
                Name = name,
                Value = value,
                DbType = dbType ?? (value != null ? Parameters.TypeMap[value.GetType()] : (DbType?) null),
                Direction = direction ?? ParameterDirection.Input,
                Size = size ?? -1,
                Precision = precision,
                Scale = scale
            };
        }
        public void Add(Parameter parameter)
        {
            this.parameters.Add(new Parameter
            {
                Name = parameter.Name,
                Value = parameter.Value,
                DbType = parameter.DbType ?? (parameter.Value != null ? Parameters.TypeMap[parameter.Value.GetType()] : (DbType?) null),
                Direction = parameter.Direction ?? ParameterDirection.Input,
                Size = parameter.Size ?? -1,
                Precision = parameter.Precision,
                Scale = parameter.Scale
            });
        }
        public virtual void Clear() => this.parameters.Clear();
        public virtual bool Contains(Parameter parameter) => this.parameters.Contains(parameter);
        public virtual void CopyTo(Parameter[] parametersArray, int arrayIndex) => this.parameters.CopyTo(parametersArray, arrayIndex);
        public virtual bool Remove(Parameter parameter) => this.parameters.Remove(parameter);
        public virtual int IndexOf(Parameter parameter) => this.parameters.IndexOf(parameter);
        public virtual void Insert(int index, Parameter parameter) => this.parameters.Insert(index, parameter);
        public virtual void RemoveAt(int index) => this.parameters.RemoveAt(index);
        public virtual IEnumerator<Parameter> GetEnumerator() => this.parameters.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.parameters.GetEnumerator();
        #endregion

        #region DataTypes        
        private void FillTypeMap()
        {
            TypeMap[typeof(byte)] = DbType.Byte;
            TypeMap[typeof(sbyte)] = DbType.SByte;
            TypeMap[typeof(short)] = DbType.Int16;
            TypeMap[typeof(ushort)] = DbType.UInt16;
            TypeMap[typeof(int)] = DbType.Int32;
            TypeMap[typeof(uint)] = DbType.UInt32;
            TypeMap[typeof(long)] = DbType.Int64;
            TypeMap[typeof(ulong)] = DbType.UInt64;
            TypeMap[typeof(float)] = DbType.Single;
            TypeMap[typeof(double)] = DbType.Double;
            TypeMap[typeof(decimal)] = DbType.Decimal;
            TypeMap[typeof(bool)] = DbType.Boolean;
            TypeMap[typeof(string)] = DbType.String;
            TypeMap[typeof(char)] = DbType.StringFixedLength;
            TypeMap[typeof(Guid)] = DbType.Guid;
            TypeMap[typeof(DateTime)] = DbType.DateTime;
            TypeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            TypeMap[typeof(byte[])] = DbType.Binary;
            TypeMap[typeof(byte?)] = DbType.Byte;
            TypeMap[typeof(sbyte?)] = DbType.SByte;
            TypeMap[typeof(short?)] = DbType.Int16;
            TypeMap[typeof(ushort?)] = DbType.UInt16;
            TypeMap[typeof(int?)] = DbType.Int32;
            TypeMap[typeof(uint?)] = DbType.UInt32;
            TypeMap[typeof(long?)] = DbType.Int64;
            TypeMap[typeof(ulong?)] = DbType.UInt64;
            TypeMap[typeof(float?)] = DbType.Single;
            TypeMap[typeof(double?)] = DbType.Double;
            TypeMap[typeof(decimal?)] = DbType.Decimal;
            TypeMap[typeof(bool?)] = DbType.Boolean;
            TypeMap[typeof(char?)] = DbType.StringFixedLength;
            TypeMap[typeof(Guid?)] = DbType.Guid;
            TypeMap[typeof(DateTime?)] = DbType.DateTime;
            TypeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;
        }
        #endregion
        ~Parameters()
        {
            TypeMap = null;
        }
    }
}
