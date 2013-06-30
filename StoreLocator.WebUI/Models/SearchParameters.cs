using System;

namespace StoreLocator.WebUI
{
	public class SearchParameters
	{
		public SearchParameters ()
		{
		}

		public double OriginLatitude { get; set; }
		public double OriginLongitude { get; set; }
		public double Radius { get; set; }
	}
}

