using CSE.Client.Common;
using CSE.Server;
using CSE.Client.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CSE.Client
{
    public class RegistrationClient : IObserver<string>
    {
        public RegistrationClient()
        {
            Pages = new ObservableCollection<string>();
        }

        public ObservableCollection<string> Pages { get; }

        public async Task OnRegister(User user)
        {
            try
            {
                DummyServer.Instance.Subscribe(this);
                var welcomePageContent = DummyServer.Instance.Start(user.UserName);
                Pages.Add(welcomePageContent);

            }
            catch (Exception ex)
            {
                SharedUI.DisplayError("Error", ex.Message, "OK");
            }
        }

        public void GetNextPage()
        {
            var page = DummyServer.Instance.LoadNextPage();
            Pages.Add(page);
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
            Pages.Add(value);
        }
    }
}
