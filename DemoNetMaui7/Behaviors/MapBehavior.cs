using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using DemoNetMaui7.Helpers;
using DemoNetMaui7.Models;
using MauiMap = Microsoft.Maui.Controls.Maps.Map;

namespace DemoNetMaui7.Behaviors
{
	public class MapBehavior : BindableBehavior<MauiMap>
	{
		private MauiMap map;

		public static readonly BindableProperty IsReadyProperty =
			BindableProperty.CreateAttached("IsReady",
				typeof(bool),
				typeof(MapBehavior),
				default(bool),
				BindingMode.Default,
				null,
				OnIsReadyChanged);

		public bool IsReady
		{
			get => (bool)GetValue(IsReadyProperty);
			set { SetValue(IsReadyProperty, value); }
		}

		private static void OnIsReadyChanged(BindableObject view, object oldValue, object newValue)
		{
			SetInitialPosition(view as MapBehavior, (bool)newValue);
		}

		private static void SetInitialPosition(MapBehavior mapBehavior, bool isReady)
		{
			if (mapBehavior != null && isReady)
			{
				mapBehavior.ChangePosition();
				mapBehavior.HighlightPosition();
			}
		}

		public static readonly BindableProperty POIsProperty =
			BindableProperty.CreateAttached(nameof(POIs),
				typeof(IEnumerable<POI>),
				typeof(MapBehavior),
				default(IEnumerable<POI>),
				BindingMode.Default,
				null,
				OnPOIsChanged);

		public IEnumerable<POI> POIs
		{
			get => (IEnumerable<POI>)GetValue(POIsProperty);
			set { SetValue(POIsProperty, value); }
		}

		private static void OnPOIsChanged(BindableObject view, object oldValue, object newValue)
		{
			SetInitialPosition(view as MapBehavior, true);
		}

		public static readonly BindableProperty GoogleDirectionsProperty =
			BindableProperty.CreateAttached("GoogleDirections",
				typeof(GoogleDirection),
				typeof(MapBehavior),
				default(GoogleDirection),
				BindingMode.Default,
				null,
				OnGoogleDirectionsChanged);

		public GoogleDirection GoogleDirections
		{
			get => (GoogleDirection)GetValue(GoogleDirectionsProperty);
			set { SetValue(GoogleDirectionsProperty, value); }
		}

		private static void OnGoogleDirectionsChanged(BindableObject view, object oldValue, object newValue)
		{
			var mapBehavior = view as MapBehavior;

			if (mapBehavior != null)
			{
				if (newValue != null)
					mapBehavior.DrawRoute();
			}
		}

		private void DrawRoute()
		{
			if (!IsReady || POIs == null || !POIs.Any())
				return;

			map.MapElements.Clear();

			var encodedPoints = GoogleDirections.Routes.First().OverviewPolyline.Points;
			var positions = GoogleDirectionsHelper.ConvertPointsToPositions(encodedPoints);

			var polyline = new Polyline();
			polyline.StrokeColor = Colors.Black;
			polyline.StrokeWidth = 7;

			foreach (var p in positions)
				polyline.Geopath.Add(p);

			map.MapElements.Add(polyline);

			var firstPoint = polyline.Geopath[0];

			map.MoveToRegion(MapSpan.FromCenterAndRadius(
				new Location(firstPoint.Latitude, firstPoint.Longitude),
				Distance.FromMiles(0.50f)));

			var pin = new Pin
			{
				Type = PinType.SearchResult,
				Location = new Location(firstPoint.Latitude, firstPoint.Longitude),
				Label = "Start"
			};

			map.Pins.Add(pin);
		}

		private void ChangePosition()
		{
			if (POIs == null || !POIs.Any())
				return;

			var firstCoord = POIs.First();
			var position = firstCoord.GeoPosition;
			var distance = Distance.FromKilometers(1);

			map.MoveToRegion(MapSpan.FromCenterAndRadius(position, distance));
		}

		private void HighlightPosition()
		{
			if (POIs == null || !POIs.Any())
				return;

			var firstCoord = POIs.First();
			var position = firstCoord.GeoPosition;
			var distance = Distance.FromMeters(100);

			var highlightCircle = new Circle()
			{
				Center = position,
				Radius = distance,
				StrokeColor = Color.FromArgb("#88FF0000"),
				StrokeWidth = 8,
				FillColor = Color.FromArgb("#88FFC0CB")
			};

			map.MapElements.Add(highlightCircle);
		}

		protected override void OnAttachedTo(MauiMap bindable)
		{
			base.OnAttachedTo(bindable);
			map = bindable;
		}

		protected override void OnDetachingFrom(MauiMap bindable)
		{
			base.OnDetachingFrom(bindable);
			map = null;
		}
	}
}
