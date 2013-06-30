using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using StoreLocator.WebUI.Models;
using System.Xml.Serialization;
using System.IO;
using StoreLocator.WebUI.Extensions;

namespace StoreLocator.WebUI.Controllers
{
  public class HomeController : Controller
  { 
    public ActionResult Index()
    {
      return View();
    }

    private const double LAT_DEFAULT = 37.402653;
    private const double LNG_DEFAULT = -122.079354;
    private const double RADIUS_DEFAULT = 10;


		public ViewResult SearchResults(double? lat, double? lng, double? radius)
		{

      if (!lat.HasValue)
        lat = LAT_DEFAULT;

      if (!lng.HasValue)
        lng = LNG_DEFAULT;

      if (!radius.HasValue)
        radius = RADIUS_DEFAULT;

			var allStores = obtainStoreData();
			var searchResults = StoreSearchResult.SearchForStores(allStores, 
        new SearchParameters()
        {
  				Radius = radius.Value,
  				OriginLatitude = lat.Value,
  				OriginLongitude = lng.Value
  			}
      );

			var modelToSend = new ListOfStores<StoreSearchResult>() 
      { 
        Results = searchResults 
      };

      return new XmlResult<ListOfStores<StoreSearchResult>>() 
      { 
        Data = modelToSend
      };
		}

		private List<StoreLocation> obtainStoreData()
		{
        var stores = HttpRuntime.Cache.GetOrStore<List<StoreLocation>>("stores", () => {
        var serial = new XmlSerializer(typeof(ListOfStores<StoreLocation>));
        var path = Server.MapPath("~/Content/siteMarkers.xml");
        var reader = new StreamReader(path);
        var list = (ListOfStores<StoreLocation>)serial.Deserialize(reader);
        return list.Results;
      });

      return stores;
//			return new[]
//			{
//				new StoreLocation { StoreName = "Frankie Johnnie & Luigo Too", StoreAddress = "939 W El Camino Real, Mountain View, CA", Latitude = 37.386339, Longitude = -122.085823 },
//				new StoreLocation { StoreName = "Amici's East Coast Pizzeria", StoreAddress = "790 Castro St, Mountain View, CA", Latitude = 37.38714, Longitude = -122.083235 },
//				new StoreLocation { StoreName = "Kapp's Pizza Bar & Grill", StoreAddress = "191 Castro St, Mountain View, CA", Latitude = 37.393885, Longitude = -122.078916 },
//				new StoreLocation { StoreName = "Round Table Pizza: Mountain View", StoreAddress = "570 N Shoreline Blvd, Mountain View, CA", Latitude = 37.402653, Longitude = -122.079354 },
//				new StoreLocation { StoreName = "Tony & Alba's Pizza & Pasta", StoreAddress = "619 Escuela Ave, Mountain View, CA", Latitude = 37.394011, Longitude = -122.095528 },
//				new StoreLocation { StoreName = "Oregano's Wood-Fired Pizza", StoreAddress = "4546 El Camino Real, Los Altos, CA", Latitude=  37.401724,Longitude = -122.114646 }
//			}.ToList();
		}
	}
}
