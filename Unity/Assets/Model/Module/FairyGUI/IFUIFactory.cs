namespace ETModel
{
	public interface IFUIFactory
	{
		FUI Create(string type);
		void Remove(string type);
	}
}