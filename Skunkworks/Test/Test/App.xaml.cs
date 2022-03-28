using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Xamarin.Forms;

namespace Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		public App()
		{
			var nav = new NavigationPage();
			nav.BarBackgroundColor = Color.Transparent;
			nav.BarTextColor = Color.White;


			var listpage = new ContentPage();
			listpage.Content = new ListView
			{
				ItemsSource = new List<ContentPage> {
					new StackLayoutPageXaml(),
					new StackLayoutPageCode(),
					new RelativeLayoutPageXaml(),
					new RelativeLayoutPageCode(),
					new GridPageXaml(),
					new GridPageCode(),
					new AbsoluteLayoutPageXaml(),
					new AbsoluteLayoutPageCode()
				},
				ItemTemplate = new DataTemplate(typeof(CustomCell))
			};
			((ListView)listpage.Content).ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				nav.PushAsync((ContentPage)e.SelectedItem);
			};
			nav.PushAsync(listpage);

			// The root page of your application
			Main = nav;
		}
	}
}
