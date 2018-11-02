using ETModel;

namespace ETHotfix
{
	public interface IFUIFactory
	{
		ETTask<FUI> Create();
		void Remove();
	}
}