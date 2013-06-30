using System;
using System.Web.Caching;

namespace StoreLocator.WebUI.Extensions
{
  public static class CacheExtensions
  {
    public static T GetOrStore<T>(this Cache cache, string key, Func<T> generator)
    {
      var result = cache[key];
      if (result == null)
      {
        result = generator();
        cache[key] = result;
      }
      return (T)result;
    }
  }
}

