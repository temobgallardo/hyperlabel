using System.ComponentModel;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Text.Method;
using Android.Widget;
using Hyperlabel;
using Hyperlabel.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Hyperlabel.HtmlLabel), typeof(HtmlLabelRenderer))]
namespace Hyperlabel.Droid
{
  public class HtmlLabelRenderer : LabelRenderer
  {
    public TextView _view;

    public HtmlLabelRenderer(Context context) : base(context)
    {
      _view = this.Control;
    }

    protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
    {
      base.OnElementChanged(e);

      if (_view == null)
      {
        _view = (FormsTextView)CreateNativeControl();
        this.SetNativeControl(_view);
      }

      UpdateText();
    }

    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      base.OnElementPropertyChanged(sender, e);
    }

    private void UpdateText()
    {
      if (base.Element.TextType == TextType.Html)
      {
        if (Build.VERSION.SdkInt >= BuildVersionCodes.N)
        {
          base.Control.SetText(Html.FromHtml(base.Element.Text ?? string.Empty, FromHtmlOptions.ModeCompact), TextView.BufferType.Spannable);
        }
        else
        {
          base.Control.SetText(Html.FromHtml(base.Element.Text ?? string.Empty), TextView.BufferType.Spannable);
        }

        this.Control.MovementMethod = LinkMovementMethod.Instance;
      }
      else
      {
        _view.Text = base.Element.UpdateFormsText(base.Element.Text, base.Element.TextTransform);
      }
    }
  }
}