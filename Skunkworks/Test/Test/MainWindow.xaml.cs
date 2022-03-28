using CSE.Client.Client.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = txt_UserName.Text;
            string password = txt_UserPassword.Text;
            string alias = txt_UserAlias.Text;
            RegistrationClient rc = new RegistrationClient();
            string pageXAML = await rc.OnRegister(username, password, alias);

            //Xamarin.Forms.Forms.Init()
            ContentPage page = new ContentPage().LoadFromXaml(pageXAML);
            //await Navigation.PushAsync(page);
        }
    }
}
