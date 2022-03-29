using System;
using System.Threading.Tasks;
using CSE.Client.Common;
using CSE.Client.Models;
using CSE.Server;

namespace CSE.Client
{
    public class RegistrationClient
    {
        public async Task<string> OnRegister(User user, IObserver<string> observer)
        {
            try
            {
                DummyServer.Instance.Subscribe(observer);
                var welcomePageContent = DummyServer.Instance.Start(user.UserName);
                return welcomePageContent;
            }
            catch (Exception ex)
            {
                SharedUI.DisplayError("Error", ex.Message, "OK");
                return string.Empty;
            }
        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {
            System.Diagnostics.Debug.WriteLine(error);
        }

        public void GetNextPage()
        {
            DummyServer.Instance.GetNextPage();
        }
            
    }
}
