using DemoNetMaui7.Models;

namespace DemoNetMaui7.Services
{
	public interface IGoogleService
	{
		Task<GoogleDirection> GetDirections(Location origin, Location destination);
	}
}
