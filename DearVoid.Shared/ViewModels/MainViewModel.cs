using CSE.Client;
using CSE.Client.Models;
using DearVoid.ViewModels.Abstract;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Markup;

namespace DearVoid.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly RegistrationClient registrationClient;

        public MainViewModel(User user)
        {
            registrationClient = new RegistrationClient();

            _ = registrationClient.OnRegister(user);
            Pages = registrationClient.Pages;
            Pages.CollectionChanged += OnReceivedNewPage;
            CurrentStringPage = Pages[0];
        }


        public ObservableCollection<string> Pages { get; }

        public bool CanSwitchPageForward => Pages.IndexOf(currentStringPage) < Pages.Count - 1;

        public bool CanSwitchPageBackward => Pages.IndexOf(currentStringPage) > 0;

        private string currentStringPage;
        public string CurrentStringPage
        {
            get => currentStringPage;
            set 
            {
                SetProperty(ref currentStringPage, value);
                CurrentPage = XamlReader.Load(currentStringPage);
            } 
        }

        private object currentPage;
        public object CurrentPage
        {
            get => currentPage;
            set => SetProperty(ref currentPage, value);
        }

        private string consoleText;
        public string ConsoleText
        {
            get => consoleText;
            set
            {
                SetProperty(ref consoleText, value);
            }
        }

        public void SwitchPageForward()
        {
            if (!CanSwitchPageForward)
            {
                return;
            }

            CurrentStringPage = Pages[Pages.IndexOf(CurrentStringPage) + 1];
            RaiseProperty(nameof(CanSwitchPageForward));
            RaiseProperty(nameof(CanSwitchPageBackward));
        }

        public void SwitchPageBackward()
        {
            if (!CanSwitchPageBackward)
            {
                return;
            }

            CurrentStringPage = Pages[Pages.IndexOf(CurrentStringPage) - 1];
            RaiseProperty(nameof(CanSwitchPageForward));
            RaiseProperty(nameof(CanSwitchPageBackward));
        }

        private void OnReceivedNewPage(object _, System.Collections.Specialized.NotifyCollectionChangedEventArgs __)
        {
            ConsoleText += $"{Environment.NewLine} New page received, total page {registrationClient.Pages.Count}";

            RaiseProperty(nameof(CanSwitchPageForward));
            RaiseProperty(nameof(CanSwitchPageBackward));
        }
    }
}
