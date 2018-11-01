using System;

namespace ETModel
{
	[AttributeUsage(AttributeTargets.Class)]
	public class FUIFactoryAttribute: BaseAttribute
	{
		public string Type { get; }

		public FUIFactoryAttribute(string type)
		{
			this.Type = type;
		}
	}
}