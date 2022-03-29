using System;
using CSE.Client.Models;
using DearVoid.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DearVoid
{
    public partial class MainPage : Page
    {
        private const int ConsoleScrollOffset = 50;
        private const string RadioButtonNameTemplate = "ChoiceRadioButton";

        public MainViewModel ViewModel;

        public MainPage()
		{
			InitializeComponent();
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs eventArgs)
        {
            base.OnNavigatedTo(eventArgs);

            var user = eventArgs.Parameter as User;

            ViewModel = new MainViewModel(user);
            DataContext = ViewModel;
            ViewModel.CurrentPageChanged += OnPageChanged;
        }

        private void OnPageChanged(object _, EventArgs __)
        {
            if (ViewModel?.CurrentResponsePage is null)
            {
                return;
            }

            foreach (var answer in ViewModel.CurrentResponsePage.)

            for (var i = 0; i < ContentFrame.ChildCount; i++)
            {
                var radioButton = ContentFrame.Find($"{register}{nameof(RadioButton)}") as RadioButton;
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