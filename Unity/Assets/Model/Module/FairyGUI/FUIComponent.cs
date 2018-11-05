using System;
using System.Collections.Generic;
using FairyGUI;

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

	/// <summary>
	/// 管理所有顶层UI, 顶层UI都是GRoot的孩子
	/// </summary>
	public class FUIComponent: Component
	{
		private readonly Dictionary<string, IFUIFactory> uiTypes = new Dictionary<string, IFUIFactory>();

		public FUI Root;

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();

			this.uiTypes.Clear();
			
			this.Root.RemoveChildren();
		}

		public void Awake()
		{
			this.Root = ComponentFactory.Create<FUI, GObject>(GRoot.inst);
			
			this.uiTypes.Clear();
            
			List<Type> types = Game.EventSystem.GetTypes(typeof (FUIFactoryAttribute));

			foreach (Type type in types)
			{
				object[] attrs = type.GetCustomAttributes(typeof (FUIFactoryAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

				FUIFactoryAttribute attribute = attrs[0] as FUIFactoryAttribute;
				if (this.uiTypes.ContainsKey(attribute.Type))
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
				this.uiTypes.Add(attribute.Type, factory);
			}
		}

		public async ETTask<FUI> Create(string name)
		{
			try
			{
				FUI ui = await this.uiTypes[name].Create();
				this.Root.Add(ui);
				return ui;
			}
			catch (Exception e)
			{
				throw new Exception($"{name} UI Create 错误: {e}");
			}
		}
		
		public void Remove(string name)
		{
			try
			{
				this.Root.Remove(name);
				this.uiTypes[name].Remove();
			}
			catch (Exception e)
			{
				throw new Exception($"{name} UI Remove 错误: {e}");
			}
		}
		
		public FUI Get(string name)
		{
			try
			{
				FUI ui = this.Root.Get(name);
				return ui;
			}
			catch (Exception e)
			{
				throw new Exception($"{name} UI Get 错误: {e}");
			}
		}
	}
}