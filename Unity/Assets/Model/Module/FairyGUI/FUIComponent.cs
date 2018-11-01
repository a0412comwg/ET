using System;
using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
	[ObjectSystem]
	public class FUIComponentAwakeSystem : AwakeSystem<FUIComponent>
	{
		public override void Awake(FUIComponent self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class FUIComponentLoadSystem : LoadSystem<FUIComponent>
	{
		public override void Load(FUIComponent self)
		{
			self.Load();
		}
	}

	/// <summary>
	/// 管理所有UI
	/// </summary>
	public class FUIComponent: Component
	{
		private readonly Dictionary<string, IFUIFactory> UiTypes = new Dictionary<string, IFUIFactory>();
		private readonly Dictionary<string, FUI> uis = new Dictionary<string, FUI>();

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();

			foreach (string type in uis.Keys.ToArray())
			{
				FUI ui;
				if (!uis.TryGetValue(type, out ui))
				{
					continue;
				}
				uis.Remove(type);
				ui.Dispose();
			}

			this.uis.Clear();
			this.UiTypes.Clear();
		}

		public void Awake()
		{
			this.Load();
		}

		public void Load()
		{
			this.UiTypes.Clear();
            
			List<Type> types = Game.EventSystem.GetTypes(typeof(FUIFactoryAttribute));

			foreach (Type type in types)
			{
				object[] attrs = type.GetCustomAttributes(typeof (FUIFactoryAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

				FUIFactoryAttribute attribute = attrs[0] as FUIFactoryAttribute;
				if (UiTypes.ContainsKey(attribute.Type))
				{
                    Log.Debug($"已经存在同类FUI Factory: {attribute.Type}");
					throw new Exception($"已经存在同类FUI Factory: {attribute.Type}");
				}
				object o = Activator.CreateInstance(type);
				IFUIFactory factory = o as IFUIFactory;
				if (factory == null)
				{
					Log.Error($"{o.GetType().FullName} 没有继承 IFUIFactory");
					continue;
				}
				this.UiTypes.Add(attribute.Type, factory);
			}
		}

		public FUI Create(string type)
		{
			try
			{
				FUI ui = UiTypes[type].Create(type);
				uis.Add(type, ui);
				return ui;
			}
			catch (Exception e)
			{
				throw new Exception($"{type} UI 错误: {e}");
			}
		}

		public void Add(string type, FUI ui)
		{
			this.uis.Add(type, ui);
		}

		public void Remove(string type)
		{
			FUI ui;
			if (!uis.TryGetValue(type, out ui))
			{
				return;
			}
            uis.Remove(type);
			ui.Dispose();
		}

		public void RemoveAll()
		{
			foreach (string type in this.uis.Keys.ToArray())
			{
				FUI ui;
				if (!this.uis.TryGetValue(type, out ui))
				{
					continue;
                }
                this.uis.Remove(type);
				ui.Dispose();
			}
		}

		public FUI Get(string type)
		{
			FUI ui;
			this.uis.TryGetValue(type, out ui);
			return ui;
		}

		public List<string> GetUITypeList()
		{
			return new List<string>(this.uis.Keys);
		}
	}
}