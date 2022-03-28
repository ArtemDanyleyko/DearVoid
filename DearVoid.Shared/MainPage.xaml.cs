using CSE.Client.Models;
using DearVoid.ViewModels;
using Windows.UI.Xaml;

namespace DearVoid
{
    public partial class MainPage : Windows.UI.Xaml.Controls.Page
    {
        private const int ConsoleScrollOffset = 50;

        public MainViewModel ViewModel;

        public MainPage()
		{
			InitializeComponent();
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var user = e.Parameter as User;

            ViewModel = new MainViewModel(user);
            DataContext = ViewModel;
        }

        private void OnSwitchForward(object _, RoutedEventArgs __)
        {
            ViewModel.SwitchPageForward();
        }

        private void OnSwitchBackward(object _, RoutedEventArgs __)
        {
            ViewModel.SwitchPageBackward();
        }
    }
}