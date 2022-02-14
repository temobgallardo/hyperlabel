﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hyperlabel
{
  public class HtmlLabelConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      var formatted = new FormattedString();

      foreach (var item in ProcessString((string)value))
        formatted.Spans.Add(CreateSpan(item));

      return formatted;
    }

    private Span CreateSpan(StringSection section)
    {
      var span = new Span()
      {
        Text = section.Text
      };

      if (Utilities.IsUrl(section.Link))
      {
        span.CustomizeHyperlink(section.Link, _navigationCommand);
      }

      return span;
    }

    public IList<StringSection> ProcessString(string rawText)
    {
      const string spanPattern = @"(<a.*?>.*?</a>)";

      MatchCollection collection = Regex.Matches(rawText, spanPattern, RegexOptions.Singleline);

      var sections = new List<StringSection>();

      var lastIndex = 0;

      foreach (Match item in collection)
      {
        var foundText = item.Value;
        sections.Add(new StringSection() { Text = rawText.Substring(lastIndex, item.Index) });
        lastIndex += item.Index + item.Length;

        // Get HTML href
        var html = new StringSection()
        {
          Link = Regex.Match(item.Value, "(?<=href=\\\")[\\S]+(?=\\\")").Value,
          Text = Regex.Replace(item.Value, "<.*?>", string.Empty)
        };

        sections.Add(html);
      }

      sections.Add(new StringSection() { Text = rawText.Substring(lastIndex) });

      return sections;
    }

    private ICommand _navigationCommand = new Command<string>(async (url) =>
    {
      await Launcher.OpenAsync(new Uri(url));
    });

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}