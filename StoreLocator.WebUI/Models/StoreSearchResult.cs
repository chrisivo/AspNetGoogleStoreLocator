using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using StoreLocator.WebUI.Extensions;

namespace StoreLocator.WebUI.Models
{
  [XmlType("marker")]
	public class StoreSearchResult : StoreLocation
  {
    // default parameter-less constructor required for serialisation
    public StoreSearchResult()
    {
    }

    [XmlAttribute("distance", typeof(double))]
    public double Distance { get; private set; }

    // A new constructor required by the SearchForStores static method
    private StoreSearchResult(StoreLocation result, SearchParameters searchParams)
    {
      this.StoreAddress = result.StoreAddress;
      this.StoreName = result.StoreName;
      this.Latitude = result.Latitude;
      this.Longitude = result.Longitude;
      this.Distance = calculateDistance(result, searchParams);
    }
 
    public static List<StoreSearchResult> SearchForStores(IEnumerable<StoreLocation> Stores, SearchParameters Parameters)
    {
      var searchResults = new List<StoreSearchResult>();

      Stores.Where(x => calculateDistance(x, Parameters) < Parameters.Radius)
					.ToList()
					.ForEach(o => searchResults.Add(new StoreSearchResult(o, Parameters)));

      return searchResults;
    }

    private static double calculateDistance(StoreLocation store, SearchParameters searchParams)
    {

      // Store these two in variables as they'll be required twice
      double originLatitudeRadians = searchParams.OriginLatitude.ToRadians();
      double storeLatitudeRadians = store.Latitude.ToRadians();

      double ret = (3959 * 
        Math.Acos(
          Math.Cos(originLatitudeRadians) * 
          Math.Cos(storeLatitudeRadians) * 
          Math.Cos(
            store.Longitude.ToRadians() - 
            searchParams.OriginLongitude.ToRadians()
          ) + 
          Math.Sin(originLatitudeRadians) * 
          Math.Sin(storeLatitudeRadians)
        )
       );
      return ret;
    }
  }
}