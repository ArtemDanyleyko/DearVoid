using System;
using System.Collections.Generic;
using System.Linq;
using ClientProxy;
using ClientProxy.Enums;
using ClientProxy.Models;
using CSE.Client.Enums;
using CSE.Client.Models;
using DearVoid.NavigationParams;
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

        private Dictionary<string, Action<int>> _radioButtonsActionsRegistry;

        public Proxy proxy;

        public MainPage()
		{
			InitializeComponent();
            _radioButtonsActionsRegistry = new Dictionary<string, Action<int>>()
            {
                ["ChoiceRadioButton0"] = ChangepageState,
                ["ChoiceRadioButton1"] = ChangepageState,
                ["ChoiceRadioButton2"] = ChangepageState
            };
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
                        var navigateParam = new DummyPageNavigationParams(
                            XamlReader.Load(value.XamlPage.ToString()),
                            _radioButtonsActionsRegistry,
                            PageStatesListBox.Items.Count);

                        ContentFrame.ForwardStack.Add(new PageStackEntry(
                            typeof(FramePage),
                            navigateParam,
                            null));

                        ForwardToEnd();
                      
                        var text = $"Page number {PageStatesListBox.Items.Count + 1}. State: {PageStatus.NotAnswered}";
                        PageStatesListBox.Items.Add(new TextBlock() { Text = text });
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

        private async void OnStart(object button, RoutedEventArgs __)
        {
            proxy.StartTest();
            StartButton.Visibility = Visibility.Collapsed;
            NavigationStackPanel.Visibility = Visibility.Visible;
        }

        private void OnSwitchForward(object _, RoutedEventArgs __)
        {
            proxy.SwitchForward();
        }

        private void OnSwitchBackward(object _, RoutedEventArgs __)
        {
            proxy.SwitchBackward();
        }

        private void ChangepageState(int index)
        {
            if(PageStatesListBox.Items.Count <= index)
            {
                return;
            }

            var statusItem = PageStatesListBox.Items[index] as TextBlock;
            var startRemoveIndex = statusItem.Text.IndexOf("State: ");
            statusItem.Text = statusItem.Text.Remove(startRemoveIndex);
            statusItem.Text += $"{PageStatus.PendingSend}";
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