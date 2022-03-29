using CSE.Client;
using CSE.Client.Enums;
using CSE.Client.Models;
using DearVoid.ViewModels.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Markup;

namespace DearVoid.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly RegistrationClient registrationClient;

        public event EventHandler CurrentPageChanged;

        public MainViewModel(User user)
        {
            registrationClient = new RegistrationClient();

            _ = registrationClient.OnRegister(user);
            Pages = registrationClient.Pages;
            Pages.CollectionChanged += OnReceivedNewPage;
            CurrentResponsePage = Pages[0];
        }

        public ObservableCollection<ResponsePage> Pages { get; }

        public bool CanSwitchPageForward => CurrentResponsePage.Index < Pages.Last().Index;

        public bool CanSwitchPageBackward => CurrentResponsePage.Index > 0;

        private ResponsePage currentResponsePage;
        public ResponsePage CurrentResponsePage
        {
            get => currentResponsePage;
            set 
            {
                SetProperty(ref currentResponsePage, value);
                CurrentPage = XamlReader.Load(currentResponsePage.XamlPage);
                CurrentPageChanged?.Invoke(null, EventArgs.Empty);
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

        public void SwitchPageByIndex(int index)
        {
            if (!CanSwitchPageForward)
            {
                return;
            }

            CurrentResponsePage = Pages[index];
            RaiseProperties(nameof(CanSwitchPageForward), nameof(CanSwitchPageBackward));
        }

        public void SwitchPageForward()
        {
            if (!CanSwitchPageForward)
            {
                return;
            }

            CurrentResponsePage = Pages[Pages.IndexOf(CurrentResponsePage) + 1];
            RaiseProperties(nameof(CanSwitchPageForward), nameof(CanSwitchPageBackward));
        }

        public void SwitchPageBackward()
        {
            if (!CanSwitchPageBackward)
            {
                return;
            }

            CurrentResponsePage = Pages[Pages.IndexOf(CurrentResponsePage) - 1];
            RaiseProperties(nameof(CanSwitchPageForward), nameof(CanSwitchPageBackward));
        }

        public void ChangePageState()
        {
            CurrentResponsePage.PageStatus = PageStatus.PendingSend;
        }

        private void OnReceivedNewPage(object _, System.Collections.Specialized.NotifyCollectionChangedEventArgs __)
        {
            ConsoleText += $"{Environment.NewLine} New page received, total page {registrationClient.Pages.Count}";
            RaiseProperties(nameof(CanSwitchPageForward), nameof(CanSwitchPageBackward));
        }
    }
}
