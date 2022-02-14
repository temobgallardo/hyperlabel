using System.Windows.Input;
using Xamarin.Forms;

namespace Hyperlabel
{
  public static class SpanExtensions
  {
    public static void CustomizeHyperlink(this Span span, string textPart, ICommand tapCommand)
    {
      span.TextColor = Color.DeepSkyBlue;
      span.TextDecorations = TextDecorations.Underline;
      span.GestureRecognizers.Add(new TapGestureRecognizer
      {
        Command = tapCommand,
        CommandParameter = textPart
      });
    }
  }
}