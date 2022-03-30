using System;
using ClientProxy;
using ClientProxy.Enums;
using ClientProxy.Models;
using CSE.Client.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

namespace DearVoid
{
    public partial class MainPage : Page, IObserver<NavigationIntent>
    {
        private const int ConsoleScrollOffset = 50;
        private const string RadioButtonNameTemplate = "ChoiceRadioButton";

        public Proxy proxy;

        public MainPage()
		{
			InitializeComponent();
        }

        public object CurrentPage { get; set; }

        protected override async void OnNavigatedTo(NavigationEventArgs eventArgs)
        {
            base.OnNavigatedTo(eventArgs);

            var user = eventArgs.Parameter as User;
            proxy = new Proxy();
            proxy.Subscribe(this);

            var startupPage = await proxy.RegisterAsync(user);
            ContentFrame.Content = XamlReader.Load(startupPage);
        }

        public void OnNext(NavigationIntent value)
        {
            switch (value.NavigationType)
            {
                case NavigationType.Forward 
                    when string.IsNullOrEmpty(value.XamlPage):
                    {
                        ContentFrame.GoForward();
                        break;
                    }

                case NavigationType.Backward:
                    {
                        ContentFrame.GoBack();
                        break;
                    }

                case NavigationType.Forward:
                    {
                        ContentFrame.ForwardStack.Add(new PageStackEntry(
                            typeof(FramePage),
                            XamlReader.Load(value.XamlPage.ToString()),
                            null));
                        ForwardToEnd();
                        break;
                    }
            }
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnCompleted()
        {
            
        }

        private void OnSwitchForward(object _, RoutedEventArgs __)
        {
            proxy.SwitchForward();
        }

        private void OnSwitchBackward(object _, RoutedEventArgs __)
        {
            proxy.SwitchBackward();
        }

        private void ForwardToEnd()
        {
            while (ContentFrame.CanGoForward)
            {
                ContentFrame.GoForward();
            }
        }

    }
}