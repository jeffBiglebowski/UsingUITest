using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;

using Xamarin.Forms;

namespace UsingUITest
{

    public class MyPage : MasterDetailPage
	{
		Label l;

		public MyPage ()
		{
			var b = new Button {
				Text = "Clicksg me",
				AutomationId = "MyButton"		// referenced in UITests
			};
			b.Clicked += (sender, e) => {
				l.Text = "Was clicked";
			};

			l = new Label { 
				Text = "Hello, Xamarin.Forms!",
				AutomationId = "MyLabel"			// referenced in UITests
			};

            var entry = new Entry()
            {
                AutomationId = "MyEntry"
            };

            entry.Focused += (s, e) =>
            {
                entry.Unfocus();
                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.Navigation.PushPopupAsync(new PopupPage()
                {
                    Content = new Label()
                    {
                        Text = "Text",
                        BackgroundColor = Color.Blue,
                        Opacity = 0.5,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand
                    }
                }));
            };

            Detail = new ContentPage()
            {
                AutomationId = "MyContentPage",
                Content = new StackLayout
                {
                    Padding = new Thickness(0, 20, 0, 0),
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Children = {
                        b, l, entry
                    }
                }
            };
            Master = new ContentPage()
            {
                Title = "Menu",
                Content = new StackLayout() { BackgroundColor = Color.Red }
            };
		}
	}

	/// <summary>
	/// Demo of setting control identifiers to use with Calabash for testing
	/// https://developer.xamarin.com/guides/xamarin-forms/deployment,_testing,_and_metrics/uitest-and-test-cloud/
	/// </summary>
	public class App : Application
	{
		public App ()
		{	
			MainPage = new MyPage ();
		}
	}

}

