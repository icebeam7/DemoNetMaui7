namespace DemoNetMaui7.Helpers
{
	public class GoogleDirectionsHelper
	{
		public static IEnumerable<Location> ConvertPointsToPositions(string encodedPoints)
		{
			char[] polylineChars = encodedPoints.ToCharArray();
			int index = 0;

			int currentLat = 0;
			int currentLng = 0;
			int next5bits;
			int sum;
			int shifter;

			while (index < polylineChars.Length)
			{
				// calculate next latitude
				sum = 0;
				shifter = 0;
				do
				{
					next5bits = (int)polylineChars[index++] - 63;
					sum |= (next5bits & 31) << shifter;
					shifter += 5;
				} while (next5bits >= 32 && index < polylineChars.Length);

				if (index >= polylineChars.Length)
					break;

				currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

				//calculate next longitude
				sum = 0;
				shifter = 0;
				do
				{
					next5bits = (int)polylineChars[index++] - 63;
					sum |= (next5bits & 31) << shifter;
					shifter += 5;
				} while (next5bits >= 32 && index < polylineChars.Length);

				if (index >= polylineChars.Length && next5bits >= 32)
					break;

				currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

				var latitude = (double)currentLat;
				var longitude = (double)currentLng;

				yield return new Location(latitude / 1E5, longitude / 1E5);
			}
		}
	}
}
