using Xamarin.Forms;
using Xunit;

namespace Hyperlabel.Test.Forms
{
  public class HtmlLabelTest
  {
    public HtmlLabelTest()
    {
      Device.PlatformServices = new MockPlatformServices();
    }

    [Fact]
    public void HtmlLabel_Should_Have_Html_Text()
    {
      var expected = "<p>Hyperlink** on portal <a href=\"https://www.dell.com\" target=\"_blank\">dell portal</a> </p><p>idem <a href=\"https://www.theguardian.com\" target=\"_blank\">The Guardian</a></p><p>Not hyperlink on portal https://www.dell.com</p><p>idem https://www.reddit.com</p>";
      var htmlLabel = new HtmlLabel
      {
        Text = expected,
        TextType = TextType.Html
      };

      Assert.Equal(expected, htmlLabel.Text);
      Assert.Equal(TextType.Html, htmlLabel.TextType); 
      Assert.Matches("<(\"[^\"]*\"|'[^']*'|[^'\">])*>", htmlLabel.Text);
    }

    [Fact]
    public void HtmlLabel_Should_Have_Normal_Text()
    {
      var expected = "Foo";
      var htmlLabel = new HtmlLabel
      {
        Text = expected,
        TextType = TextType.Text
      };

      Assert.Equal(expected, htmlLabel.Text);
      Assert.Equal(TextType.Text, htmlLabel.TextType);
    }

    [Fact]
    public void HtmlLabel_Should_Have_Normal_Even_On_Html_Type()
    {
      var expected = "Foo";
      var htmlLabel = new HtmlLabel
      {
        Text = expected,
        TextType = TextType.Html
      };

      Assert.Equal(expected, htmlLabel.Text);
      Assert.Equal(TextType.Html, htmlLabel.TextType);
      Assert.DoesNotMatch("<(\"[^\"]*\"|'[^']*'|[^'\">])*>", htmlLabel.Text);
    }
  }
}
