using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.ShopBtnOnClick)]
	public class ShopBtnOnClick_CreateShopUI: AEvent
	{
		public override void Run()
		{
			RunAsync().NoAwait();
		}

		public async ETVoid RunAsync()
		{
			// 使用工厂创建一个Shop UI
			FUI ui = await FUIShopFactory.Create();
		}
	}
}
