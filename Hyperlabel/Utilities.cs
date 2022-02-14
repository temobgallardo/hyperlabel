using System;

namespace Hyperlabel
{
  public class Utilities
  {
    public static bool IsUrl(string input)
    {
      var res = Uri.TryCreate(input, UriKind.Absolute, out var uriResult) &&
(uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
      return res;
    }
  }
}