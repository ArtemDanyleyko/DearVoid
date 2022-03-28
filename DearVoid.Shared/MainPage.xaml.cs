using System;
using System.Collections.Generic;
using CSE.Client.Client.User;
using DearVoid.NavigationParams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Xamarin.Forms;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DearVoid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page, IObserver<string>
    {
        private RegistrationClient _registrationClient;

        private Dictionary<string, Action> _buttonsActionsRegistry;

        public MainPage()
        {
            this.InitializeComponent();
            Background = Colors.Red;

            _registrationClient = new RegistrationClient();

            _buttonsActionsRegistry = new Dictionary<string, Action>()
            {
                ["Forward"] = Forward,
                ["Backward"] = Backward
            };
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var username = txt_UserName.Text;
            var password = txt_UserPassword.Text;
            var alias = txt_UserAlias.Text;

            var xamlPageString = await _registrationClient.OnRegister(username, password, alias, this);
            OnNext(xamlPageString);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            System.Diagnostics.Debug.WriteLine(error);
        }

        public void OnNext(string value)
        {
            var page = XamlReader.Load(value);
            var navigationParams = new DummyPageNavigationParams(page, _buttonsActionsRegistry);
            Frame.Navigate(typeof(DummyPage), navigationParams);
        }

        private void Forward()
        {
            var xamlPageString = _registrationClient.GetNextPage();
            OnNext(xamlPageString);
        }

        private void Backward()
        {
            if (!Frame.CanGoBack)
            {
                return;
            }

            Frame.GoBack();
        }
    }
}