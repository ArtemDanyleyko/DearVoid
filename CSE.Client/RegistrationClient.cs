using CSE.Client.Common;
using CSE.Server;
using CSE.Client.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CSE.Client.Enums;

namespace CSE.Client
{
    public class RegistrationClient : IObserver<string>
    {
        public RegistrationClient()
        {
            XamlPages = new ObservableCollection<string>();
            Pages = new ObservableCollection<ResponsePage>();
        }

        public ObservableCollection<string> XamlPages { get; }

        public ObservableCollection<ResponsePage> Pages { get; }

        public async Task OnRegister(User user)
        {
            try
            {
                DummyServer.Instance.Subscribe(this);
                var welcomePageContent = DummyServer.Instance.Start(user.UserName);
                AddNewPage(welcomePageContent);

            }
            catch (Exception ex)
            {
                SharedUI.DisplayError("Error", ex.Message, "OK");
            }
        }

        public void RefreshClient()
        {
            //TODO: if need - rewrite and use or delete
            //var page = DummyServer.Instance.LoadNextPage();
            //AddNewPage(page);
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
            AddNewPage(value);
        }

        private void AddNewPage(string value)
        {
            XamlPages.Add(value);
            Pages.Add(new ResponsePage(
                Pages,
                value,
                PageStatus.NotAnswered));
        }
    }
}
