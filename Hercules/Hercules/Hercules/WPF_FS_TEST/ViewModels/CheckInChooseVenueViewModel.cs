using System.Linq;
using System.Collections.ObjectModel;
using Touchality.FoursquareApi;
using System.Collections.Generic;
using WP7Square.Classes;

namespace WP7Square.ViewModels
{
    public class CheckInChooseVenueViewModel
    {
        private ObservableCollection<NearbyVenueViewModel> _items;

        public CheckInChooseVenueViewModel(IEnumerable<Venue> venues)
        {
            if (venues == null) return;
            var q = from c in venues
                    orderby c.Dist
                    select c;
            int count = 1;
            foreach (var item in q)
            {
                string distance = Calcs.GetDistanceText(item.distance);

                distance = string.Format("#{0} - {1}", count++, distance);

                this.Items.Add(new NearbyVenueViewModel
                                   {
                                       Id = item.id,
                                       Name = item.name,
                                       Address = item.address,
                                       Distance = distance,
                                       VenueLocation = LocationHelper.ToLocation(item.geolat, item.geolong),
                                   });
            }
        }

        public CheckInChooseVenueViewModel()
        {
            this.Items.Add(new NearbyVenueViewModel() { Name = "Loading..." });
        }

        public ObservableCollection<NearbyVenueViewModel> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<NearbyVenueViewModel>();
                }

                return _items;
            }

            set
            {
                _items = value;
            }
        }
    }
}
