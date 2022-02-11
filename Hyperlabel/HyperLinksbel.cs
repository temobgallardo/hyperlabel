using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hyperlabel
{
  public class HyperLinksbel : Label
  {
    public static BindableProperty LinksTextProperty = BindableProperty
      .Create(propertyName: nameof(LinksText)
      , returnType: typeof(string)
      , declaringType: typeof(HyperLinksbel)
      , propertyChanged: OnLinksTextPropertyChanged);

    private readonly ICommand _linkTapGesture = new Command<string>(async (url) => await Launcher.OpenAsync(new Uri(url)));

    public string LinksText
    {
      get => GetValue(LinksTextProperty) as string;
      set => SetValue(LinksTextProperty, value);
    }

    private void SetFormattedText()
    {
      var formattedString = new FormattedString();

      if (!string.IsNullOrEmpty(this.LinksText))
      {
        var splitText = this.LinksText.Split(' ');

        foreach (string textPart in splitText)
        {
          var span = new Span { Text = $"{textPart} " };

          if (this.IsUrl(textPart)) // a link
          {
            span.TextColor = Color.DeepSkyBlue;
            span.TextDecorations = TextDecorations.Underline;
            span.GestureRecognizers.Add(new TapGestureRecognizer
            {
              Command = _linkTapGesture,
              CommandParameter = textPart
            });
          }

          formattedString.Spans.Add(span);
        }
      }

      this.FormattedText = formattedString;
    }

    private bool IsUrl(string input) => Uri.TryCreate(input, UriKind.Absolute, out var uriResult) &&
        (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

    private static void OnLinksTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
      var linksLabel = bindable as HyperLinksbel;
      linksLabel.SetFormattedText();
    }
  }
}