using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.LoginFinish)]
	public class LoginFinish_CreateLobbyUI: AEvent
	{
		public override void Run()
		{
			RunAsync().NoAwait();
		}

		public async ETVoid RunAsync()
		{
			FUIComponent fuiComponent = Game.Scene.GetComponent<FUIComponent>();
			// 使用工厂创建一个Lobby UI
			await fuiComponent.Create(FUIType.Lobby);
			
			// 创建Lobby UI完成 
			Game.EventSystem.Run(EventIdType.CreateLobbyUIFinish);
		}
	}
}
