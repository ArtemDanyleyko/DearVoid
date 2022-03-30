using System;
using System.Threading.Tasks;
using CSE.Client.Common;
using CSE.Client.Models;
using CSE.Server;

namespace CSE.Client
{
    public class RegistrationClient
    {
        public async Task OnRegister(User user, IObserver<string> observer)
        {
            try
            {
                DummyServer.Instance.Subscribe(observer);
                DummyServer.Instance.Start(user.UserName);
            }
            catch (Exception ex)
            {
                SharedUI.DisplayError("Error", ex.Message, "OK");
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
