using System.Data;

namespace Vaetech.Collections.Generic
{
    public partial class Parameters
    {
        public sealed class Parameter
        {
            public Parameter() { }
            public Parameter(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
            {
                Name = name;
                Value = value;
                DbType = dbType;
                Direction = direction;
                Size = size;
                Precision = precision;
                Scale = scale;
            }
            public string Name { get; set; }
            public object Value { get; set; }
            public DbType? DbType { get; set; }
            public ParameterDirection? Direction { get; set; }
            public int? Size { get; set; }
            public byte? Precision { get; set; }
            public byte? Scale { get; set; }
        }
    }
}
