using System.ComponentModel;
using Android.Content;
using Android.Text.Method;
using Hyperlabel.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Hyperlabel.HtmlLabel), typeof(HtmlLabelRenderer))]
namespace Hyperlabel.Droid
{
  public class HtmlLabelRenderer : LabelRenderer
  {
    public HtmlLabelRenderer(Context context) : base(context)
    {
    }

    protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
    {
      base.OnElementChanged(e);

      UpdateText();
    }

    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.OnElementPropertyChanged(sender, e);

      if (e.PropertyName == Label.TextColorProperty.PropertyName || e.PropertyName == Label.TextTransformProperty.PropertyName)
      {
        UpdateText();
      }
    }

    private void UpdateText()
    {
      if (base.Element.TextType == TextType.Html)
      {
        base.Control.MovementMethod = LinkMovementMethod.Instance;
      }
    }
  }
}