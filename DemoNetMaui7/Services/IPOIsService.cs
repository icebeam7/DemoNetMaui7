namespace DemoNetMaui7.Services
{
	public interface IPOIsService
	{
		Task<List<T>> GetItems<T>();
	}
}
