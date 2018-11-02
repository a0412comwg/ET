using ETModel;

namespace ETHotfix
{
	public interface IFUIFactory
	{
		ETTask<FUI> Create(string type);
		void Remove(string type);
	}
}