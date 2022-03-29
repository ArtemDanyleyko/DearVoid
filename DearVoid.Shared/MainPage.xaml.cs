using CSE.Client.Models;
using DearVoid.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DearVoid
{
    public partial class MainPage : Page
    {
        private const int ConsoleScrollOffset = 50;

        public MainViewModel ViewModel;

        private List<string> radioButtonsRegistry;

        public MainPage()
		{
			InitializeComponent();

            radioButtonsRegistry = new List<string>()
            {
                "FirstChoice",
                "SecondChoice",
                "ThirdChoice"
            };
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs eventArgs)
        {
            base.OnNavigatedTo(eventArgs);

            var user = eventArgs.Parameter as User;

            ViewModel = new MainViewModel(user);
            DataContext = ViewModel;
            ViewModel.currentPageChanged += SubscribeRadioButtons;
        }

        private void SubscribeRadioButtons(object _, EventArgs __)
        {
            foreach (var register in radioButtonsRegistry)
            {
                var radioButton = ContentFrame.FindName($"{register}{nameof(RadioButton)}") as RadioButton;
                if (radioButton is null)
                {
                    continue;
                }

                radioButton.Checked += (o, e) =>
                {
                    OnCheckedChanged();
                };
            }
        }

        private void OnSwitchForward(object _, RoutedEventArgs __)
        {
            ViewModel.SwitchPageForward();
        }

        private void OnCheckedChanged()
        {
            ViewModel.ChangePageState();
        }

        private void OnSwitchBackward(object _, RoutedEventArgs __)
        {
            ViewModel.SwitchPageBackward();
        }
    }
}