namespace ETModel
{
	public interface IFUIFactory
	{
		ETTask<FUI> Create();
		void Remove();
	}
}