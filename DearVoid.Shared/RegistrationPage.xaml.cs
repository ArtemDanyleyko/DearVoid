using CSE.Client.Models;
using System;
using Windows.UI;
using Windows.UI.Xaml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DearVoid
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistrationPage : Windows.UI.Xaml.Controls.Page
    {
        public RegistrationPage()
        {
            this.InitializeComponent();

            Background = Colors.Red;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var user = new User(
                UserName.Text,
                UserPassword.Text,
                UserAlias.Text);

            Frame.Navigate(typeof(MainPage), user);
        }

        public void OnError(Exception error)
        {
            System.Diagnostics.Debug.WriteLine(error);
        }
    }
}