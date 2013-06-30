using System;

namespace StoreLocator.WebUI.Extensions
{
  public static class DoubleExtensions
  {
    public static double ToRadians(this double angle)
    {
      return Math.PI * angle / 180.0;
    }
  }
}

