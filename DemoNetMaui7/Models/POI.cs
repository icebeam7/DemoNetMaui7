namespace DemoNetMaui7.Models
{
	public class POI
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string State { get; set; }
		public int CountryId { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public bool Active { get; set; }
		public Location GeoPosition => new Location(Latitude, Longitude);
		public string FullAddress => $"{Address}, {City}, {PostalCode}, {State}";

		public POI(Location location, string name)
		{
			this.Latitude = location.Latitude;
			this.Longitude = location.Longitude;
			this.Name = name;
		}

		public POI()
		{

		}
	}
}
