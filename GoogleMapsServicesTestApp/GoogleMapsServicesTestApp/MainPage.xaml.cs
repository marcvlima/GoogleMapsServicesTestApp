using GoogleMapsServicesClient.NETStandard;
using GoogleMapsServicesClient.NETStandard.Geocode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GoogleMapsServicesTestApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            Search.Clicked += (sender, args) =>
            {
                GeocodeClient client = new GeocodeClient("https://maps.googleapis.com/maps/api/", "AIzaSyBepGH1lMmdhpQ9CIpq6ip6dep3ACzdFMs", "BR");

                var result = client.GetLocationByAddress(new GeocodeRequestInfo() { CityName = this.CityName.Text, PlaceNumber = int.Parse(this.LocationNumber.Text), PublicPlaceName = this.PublicPlaceName.Text });
                if (result.Status == GeocodeResult.StatusTypes.OK)
                {
                    Result.IsEnabled = true;

                    var mainResult = result.Results.FirstOrDefault();
                    Result.Text = mainResult.FormattedAddress;

                    var viewMapButton = new Button();
                    viewMapButton.Text = "View on Map";
                    viewMapButton.Clicked += (viewMapSender, viewMapArgs) =>
                    {
                        Navigation.PushAsync(new MapsPagexaml(mainResult.Geometry.Location));
                    };

                    ResultsPanel.Children.Add(viewMapButton);

                    foreach (var addressComponent in mainResult.AddressComponents)
                    {
                        Label addressNameLabel = new Label();
                        addressNameLabel.Text = addressComponent.LongName;

                        var typesString = "(";
                        var types = addressComponent.Types.ToList();
                        for (int i = 0; i < types.Count; i++)
                        {
                            typesString += types[i];
                            if (i < types.Count - 1)
                                typesString += " ";
                        }
                        typesString += ")";
                        Label addressTypesLabel = new Label();
                        addressTypesLabel.Text = typesString;

                        StackLayout stack = new StackLayout();
                        stack.Orientation = StackOrientation.Horizontal;
                        stack.Children.Add(addressNameLabel);
                        stack.Children.Add(addressTypesLabel);

                        ResultsPanel.Children.Add(stack);
                    }
                }
            };
        }
	}
}
