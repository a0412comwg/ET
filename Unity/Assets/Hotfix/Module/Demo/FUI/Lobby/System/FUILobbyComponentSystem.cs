using ETModel;

namespace ETHotfix
{
    [ObjectSystem]
    public class FUILobbyComponentSystem : AwakeSystem<FUILobbyComponent>
    {
        public override void Awake(FUILobbyComponent self)
        {
            FUI lobby = self.GetParent<FUI>();
            lobby.Get("EnterMapBtn").GObject.asButton.onClick.Add(() => EnterMapBtnOnClick(self));
        }

        public static void EnterMapBtnOnClick(FUILobbyComponent self)
        {
            LogicHelper.EnterMapAsync().NoAwait();
        }
    }
}