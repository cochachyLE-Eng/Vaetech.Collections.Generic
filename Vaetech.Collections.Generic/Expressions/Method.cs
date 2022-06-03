using System.Collections.Generic;
using System.Reflection;

namespace Vaetech.Collections.Generic.Expressions
{
	public class Method
	{
		public Method() { }
		public Method(MethodInfo info) => SetMethod(info);
		public Method(MethodInfo info, object[] @params) => SetMethod(info, @params);

		public string Name { get; set; }
		public MethodInfo Info { get; set; }
		public object[] Parameters { get; set; } = new object[] { };

		public void SetMethod(MethodInfo info) => (Name, Info, Parameters) = (info.Name, info, new object[] { });
		public void SetMethod(MethodInfo info, object[] @params) => (Name, Info, Parameters) = (info.Name, info, @params);
		public void SetMethod(string name, MethodInfo info) => (Name, Info, Parameters) = (name, info, new object[] { });
		public void SetMethod(string name, MethodInfo info, object[] @params) => (Name, Info, Parameters) = (name, info, @params);

		~Method() => Parameters = null;
	}
}
