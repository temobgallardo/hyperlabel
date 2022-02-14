using Xamarin.Forms;

namespace Hyperlabel
{
  public partial class MainPage : ContentPage
  {
    public string Html { get; set; }

    public MainPage()
    {
      InitializeComponent();

      Html = "<p><a href = \"https://dfsm-webportal-ge4.ausvdc02.pcf.dell.com/users\" target = \"_blank\">https://dfsm-webportal-ge4.ausvdc02.pcf.dell.com</a></p>";

      BindingContext = this;
    }
  }
}