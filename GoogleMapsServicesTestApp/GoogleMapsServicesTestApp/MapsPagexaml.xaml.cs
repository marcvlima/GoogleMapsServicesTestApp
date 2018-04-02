using GoogleMapsServicesClient.NETStandard.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace GoogleMapsServicesTestApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapsPagexaml : ContentPage
	{
		public MapsPagexaml (Location location)
		{
			InitializeComponent ();
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(location.Lat, location.Lng), Distance.FromMiles(1)));

            var position = new Position(location.Lat, location.Lng); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "My Place",
                Address = "custom detail info"
            };
            MyMap.Pins.Add(pin);
        }
	}
}