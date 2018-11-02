using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.InitSceneStart)]
	public class InitSceneStart_CreateLoginFUI: AEvent
	{
		public override void Run()
		{
			RunAsync().NoAwait();
		}

		public async ETVoid RunAsync()
		{
			FUIComponent fuiComponent = Game.Scene.GetComponent<FUIComponent>();
			// 使用工厂创建一个Login UI
			FUI loginUI = await fuiComponent.Create(FUIType.Login);
			// login ui应该挂在顶层UI上，其它的UI未必挂在顶层UI，视情况而定
			fuiComponent.Root.Add(loginUI);
		}
	}
}
