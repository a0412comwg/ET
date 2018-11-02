using ETModel;
using FairyGUI;

namespace ETHotfix
{
	[ObjectSystem]
	public class FUIAwakeSystem : AwakeSystem<FUI, GObject>
	{
		public override void Awake(FUI self, GObject gObject)
		{
			self.GObject = gObject;
		}
	}
	
	public sealed class FUI: Entity
	{
		public GObject GObject;
	}
}