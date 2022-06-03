using System.Collections.Generic;
using System.Reflection;

namespace Vaetech.Collections.Generic.Expressions
{
	public class Property
	{
		public Property() { }
		public Property(PropertyInfo info) => SetProperty(info);

		public string Name { get; set; }
		public PropertyInfo Info { get; set; }
		public List<Method> Methods { get; set; } = new List<Method>();

		public void SetProperty(PropertyInfo info) => (Name, Info) = (info.Name, info);
		public void SetProperty(string name, PropertyInfo info) => (Name, Info) = (name, info);
		public Property SetMethods(List<Method> methods)
		{
			this.Methods = methods;
			return this;
		}

		~Property() => Methods = null;
	}
}
