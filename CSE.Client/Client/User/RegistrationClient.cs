using CSE.Client.Common;
using CSE.Server;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CSE.Client.Client.User
{
    public class RegistrationClient
    {
        private CallServer _callServer;

        public RegistrationClient()
        {
            _callServer = new CallServer();
        }

        public async Task<string> OnRegister(string userName, string password, string alias, IObserver<string> observer)
        {
            try
            {
                DummyServer.Instance.Subscribe(observer);
                var welcomePageContent = DummyServer.Instance.Start(userName);
                return welcomePageContent;

            }
            catch (Exception ex)
            {
                SharedUI.DisplayError("Error", ex.Message, "OK");
                return string.Empty;
            }
        }

        public string GetNextPage() =>
            DummyServer.Instance.LoadNextPage();
    }
}
