using Xamarin.Forms;

namespace Hyperlabel
{
  public partial class MainPage : ContentPage
  {
    public string Html { get; set; }
    public string Welcome { get; set; }
    public string BigHtml { get; set; }

    public MainPage()
    {
      InitializeComponent();

      Html = "<p><a href = \"https://dfsm-webportal-ge4.ausvdc02.pcf.dell.com/users\" target = \"_blank\">https://dfsm-webportal-ge4.ausvdc02.pcf.dell.com</a></p>";
      Welcome = $"Welcome to Xamarin.Forms!";
      BigHtml = "<p>Hyperlink** on portal <a href=\"https://www.dell.com\" target=\"_blank\">dell portal</a> </p><p>idem <a href=\"https://www.theguardian.com\" target=\"_blank\">The Guardian</a></p><p>Not hyperlink on portal https://www.dell.com</p><p>idem https://www.reddit.com</p>";

      BindingContext = this;
    }
  }
}