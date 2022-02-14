using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace Hyperlabel.Test
{
  public class HyperLinksbelTest
  {
    private Fixture _fixture = new Fixture();

    [Theory]
    [InlineData("", "")]
    [InlineData(null, "")]
    [InlineData(null, null)]
    [InlineData("", null)]
    public void GetTextNamesAndHyperlinks_Should_Throw(string text, string linkNames)
    {
      var sut = new HyperLinksbel();

      Assert.Throws<ArgumentNullException>(() => sut.GetTextNamesAndHyperlinks(text, linkNames));
    }

    [Theory]
    [InlineData("Learn more at &#10; https://aka.ms/xamarin-quickstart and https://google.com. Xamarin.Forms Forums at https://social.msdn.microsoft.com/Forums/en-US/d27815ce-d60a-43ab-a2b6-0ca3e00167fe/make-multiple-links-in-labels-clickable?forum=xamarinforms#latest Google at https://www.google.com/", "Xamarin quickstart, , Make multiple links clickable on Label ,Google")]
    public void GetTextNamesAndHyperlinks_Should_Separate_Text_LinksNames(string text, string linkNames)
    {
      var result = new List<StringSection>()
      {
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text="Learn"
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text="more"
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text="at"
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text="&#10;"
        },
        new StringSection()
        {
          HasHyperlink = true,
          Link = "https://aka.ms/xamarin-quickstart",
          Text="Xamarin quickstart"
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text="and"
        },
        new StringSection()
        {
          HasHyperlink = true,
          Link = "https://google.com.",
          Text = "https://google.com."
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text="Xamarin.Forms"
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text="Forums"
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text="at"
        },
        new StringSection()
        {
          HasHyperlink = true,
          Link = "https://social.msdn.microsoft.com/Forums/en-US/d27815ce-d60a-43ab-a2b6-0ca3e00167fe/make-multiple-links-in-labels-clickable?forum=xamarinforms#latest",
          Text="Make multiple links clickable on Label"
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text="Google"
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text="at"
        },
        new StringSection()
        {
          HasHyperlink = true,
          Link = "https://www.google.com/",
          Text="Google"
        }
      };

      var sut = new HyperLinksbel();

      var expected = sut.GetTextNamesAndHyperlinks(text, linkNames);

      result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("https://google.com. https://google.com. Hola", "Google, ,A")]
    public void GetTextNamesAndHyperlinks_Text_Should_Be_Link_If_Text_Null_Empty(string text, string linkNames)
    {
      var expected = new List<StringSection>()
      {
        new StringSection()
        {
          HasHyperlink = true,
          Link = "https://google.com.",
          Text = "Google"
        },
        new StringSection()
        {
          HasHyperlink = true,
          Link = "https://google.com.",
          Text = "https://google.com."
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link = "",
          Text = "Hola"
        }
      };

      var sut = new HyperLinksbel();

      var actual = sut.GetTextNamesAndHyperlinks(text, linkNames);

      actual.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("htps://google Hola a", ",A")]
    public void GetTextNamesAndHyperlinks_Should_Not_Have_Link_When_Not_Valid(string text, string linkNames)
    {
      var result = new List<StringSection>()
      {
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text = "htps://google"
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text = "Hola"
        },
        new StringSection()
        {
          HasHyperlink = false,
          Link= string.Empty,
          Text = "a"
        }
      };

      var sut = new HyperLinksbel();

      var expected = sut.GetTextNamesAndHyperlinks(text, linkNames);

      result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetTextNamesAndHyperlinks_Test()
    {
      var expectedText = _fixture.Create<string>();
      var expectedLinks = _fixture.Create<string>();

      var sut = new HyperLinksbel();

      var expected = sut.GetTextNamesAndHyperlinks(expectedText, expectedLinks);

      Assert.NotEmpty(expected);
    }

    [Fact]
    public void SetupHyperlinksAndNames_Should_Throw()
    {
      var sut = new HyperLinksbel();
      Assert.Throws<ArgumentNullException>(() => sut.SetupHyperlinksAndNames(null));
    }

    [Fact]
    public void SetupHyperlinksAndNames_Should_ManageEmpty()
    {
      var empty = new List<StringSection>();

      var sut = new HyperLinksbel();

      var result = sut.SetupHyperlinksAndNames(empty);

      Assert.Empty(result.Spans);
    }

    [Fact]
    public void SetupHyperlinksAndNames_Should_Create_Spans()
    {
      var fixture = new Fixture();
      var input = fixture.Create<List<StringSection>>();

      var sut = new HyperLinksbel();

      var expected = sut.SetupHyperlinksAndNames(input);

      Assert.NotEmpty(expected.Spans);
    }

    [Fact]
    public void SetupHyperlinksAndNames_Should_Create_Spans_With_Nulls()
    {
      var input = new List<StringSection>(){
        new StringSection()
        {
          Text = "a",
          HasHyperlink = false
        },
        new StringSection()
        {
          Link = "a",
          HasHyperlink = true
        },
        new StringSection()
        {
          Text= "b",
          Link = "a",
          HasHyperlink = true
        }
      };

      var sut = new HyperLinksbel();

      var expected = sut.SetupHyperlinksAndNames(input);

      Assert.NotEmpty(expected.Spans);
    }

    [Theory]
    [InlineData(null, "b")]
    public void SetupSpan_Should_Create_Only_Text_Span(string link, string name)
    {
      var sut = new HyperLinksbel();

      var actual = sut.SetupSpan(link, name, isUrl: false);

      Assert.NotNull(actual);
      Assert.Equal(name + " ", actual.Text);
      Assert.Empty(actual.GestureRecognizers);
    }

    [Theory]
    [InlineData("a", "b")]
    [InlineData("a", null)]
    public void SetupSpan_Should_Be_Customized_For_Hyperlink(string link, string name)
    {
      var sut = new HyperLinksbel();

      var actual = sut.SetupSpan(link, name, isUrl: true);

      Assert.NotNull(actual);
      var expectedText = (name ?? link) + " ";
      Assert.Equal(expectedText, actual.Text);
      Assert.Equal(Color.DeepSkyBlue, actual.TextColor);
      Assert.Equal(TextDecorations.Underline, actual.TextDecorations);
      Assert.NotEmpty(actual.GestureRecognizers);
    }

    [Theory]
    [InlineData(null, null)]
    public void SetupSpan_Should_Throw_Exception(string link, string name)
    {
      var sut = new HyperLinksbel();

      Assert.Throws<ArgumentNullException>(() => sut.SetupSpan(link, name));
    }

    [Theory]
    [InlineData("a")]
    [InlineData("b")]
    public void GetANameForLink_Should_Get_A_Name(string splitedText)
    {
      var expected = _fixture.Create<string>();
      var sut = new HyperLinksbel();
      int j = 0;
      var names = new string[] { expected, "C", "D" };

      string actual = sut.GetANameForLink(splitedText, names, ref j);

      Assert.NotEmpty(actual);
      Assert.NotNull(actual);
      Assert.Equal(expected, actual);
      Assert.Equal(1, j);
    }
  }
}