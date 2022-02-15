using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hyperlabel
{
  public class HyperLinksbel : Label
  {
    private readonly ICommand _linkTapGesture = new Command<string>(async (url) => await Launcher.OpenAsync(new Uri(url)));

    public static BindableProperty LinksTextProperty = BindableProperty
      .Create(propertyName: nameof(LinksText)
      , returnType: typeof(string)
      , declaringType: typeof(HyperLinksbel)
      , propertyChanged: OnLinksTextPropertyChanged);

    public string LinksText
    {
      get => GetValue(LinksTextProperty) as string;
      set => SetValue(LinksTextProperty, value);
    }

    public static BindableProperty LinksNamesProperty = BindableProperty
      .Create(propertyName: nameof(LinksNames)
      , returnType: typeof(string)
      , declaringType: typeof(HyperLinksbel)
      , propertyChanged: OnLinksTextPropertyChanged);

    public string LinksNames
    {
      get => GetValue(LinksNamesProperty) as string;
      set => SetValue(LinksNamesProperty, value);
    }

    public void SetFormattedText()
    {
      var linksAndNames = GetTextNamesAndHyperlinks(LinksText, LinksNames);
      this.FormattedText = SetupHyperlinksAndNames(linksAndNames);
    }

    public List<StringSection> GetTextNamesAndHyperlinks(string text, string linkNames)
    {
      if (string.IsNullOrEmpty(text))
      {
        throw new ArgumentNullException(nameof(text));
      }

      var splitText = text.Split(' ');
      var isLinks = splitText.Select(t => Utilities.IsUrl(t)).ToList();
      var linksAndEmpties = splitText.Select(t => Utilities.IsUrl(t) ? t.Trim() : string.Empty).ToList();
      var sections = new List<StringSection>(splitText.Length);

      var names = linkNames?.Split(',');

      for (int i = 0, j = 0; i < splitText.Length; i++)
      {
        string name = isLinks[i] ? GetANameForLink(splitText[i].Trim(), names, ref j) : splitText[i];

        sections.Add(new StringSection
        {
          Text = name?.Trim(),
          Link = linksAndEmpties[i]?.Trim(),
          HasHyperlink = isLinks[i]
        });
      }

      return sections;
    }

    public string GetANameForLink(string splitedText, string[] names, ref int j)
    {
      // not a hyperlink
      if (names is null || j >= names.Length)
        return splitedText;

      var name = names[j++].Trim();
      return string.IsNullOrEmpty(name) ? splitedText : name;
    }

    public bool HasNameForLinks(string names)
    {
      if (string.IsNullOrEmpty(names))
        return false;

      return names.Split(',').Length > 0;
    }

    public FormattedString SetupHyperlinks(string linksString)
    {
      if (string.IsNullOrEmpty(linksString))
        throw new ArgumentNullException(nameof(linksString));

      var formattedString = new FormattedString();

      var splitText = linksString.Split(' ');

      if (splitText.Length == 1)
      {
        Span span = SetupSpan(splitText[0], command: _linkTapGesture, isUrl: Utilities.IsUrl(splitText[0]));
        formattedString.Spans.Add(span);
        return formattedString;
      }

      foreach (var s in splitText)
      {
        Span span = SetupSpan(s, command: _linkTapGesture, isUrl: Utilities.IsUrl(splitText[0]));
        formattedString.Spans.Add(span);
      };

      return formattedString;
    }

    public Span SetupSpan(string link, string name = null, ICommand command = null, bool isUrl = false)
    {
      if (link is null && name is null)
        throw new ArgumentNullException(nameof(link) + " and " + nameof(name));

      var rightName = name ?? link;
      var span = new Span { Text = $"{name ?? link} " };

      if (isUrl)
      {
        span.CustomizeHyperlink(link, command);
      }

      return span;
    }

    public FormattedString SetupHyperlinksAndNames(List<StringSection> namedLinks)
    {
      if (namedLinks is null)
        throw new ArgumentNullException(nameof(namedLinks));

      var formattedString = new FormattedString();

      foreach (var nl in namedLinks)
      {
        Span span = SetupSpan(nl.Link, nl.Text, _linkTapGesture, nl.HasHyperlink);
        formattedString.Spans.Add(span);
      };

      return formattedString;
    }

    private static void OnLinksTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
      var linksLabel = bindable as HyperLinksbel;
      linksLabel.SetFormattedText();
    }
  }

  public class StringSection
  {
    public string Text { get; set; }

    public string Link { get; set; }

    public bool HasHyperlink { get; set; }
  }
}