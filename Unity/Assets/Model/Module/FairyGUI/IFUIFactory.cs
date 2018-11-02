namespace ETModel
{
	public interface IFUIFactory
	{
		ETTask<FUI> Create(string type);
		void Remove(string type);
	}
}