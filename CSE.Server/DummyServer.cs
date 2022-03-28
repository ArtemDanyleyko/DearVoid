using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;

namespace CSE.Server
{
    public class DummyServer : IObservable<string>
    {
        private const string TemplatedMessagePageName = "TemplatedMessagePage";
        private const string WelcomePageName = "WelcomePage";

        private readonly Dictionary<IObserver<string>, IDisposable> _observers;

        private readonly string[] _pagesPaths;

        private CancellationTokenSource _cancellationTokenSource;

        private DummyServer()
        {
            _observers = new Dictionary<IObserver<string>, IDisposable>();

            var currentAssembly = this.GetType().Assembly;
            _pagesPaths = currentAssembly.GetManifestResourceNames();
        }

        public static DummyServer Instance { get; } = new DummyServer();

        public string Start(string userName)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            _ = NotifyAsync(_cancellationTokenSource.Token);

            var welcomePageContentTemplate = LoadPageTemplate(WelcomePageName);
            return string.Format(welcomePageContentTemplate, userName);

            async Task NotifyAsync(CancellationToken token)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                await Task.Delay(TimeSpan.FromSeconds(10));

                if (token.IsCancellationRequested)
                {
                    return;
                }

                _observers.Keys.ToList().ForEach(item => item.OnNext(LoadNextPage()));

                await NotifyAsync(token);
            }
        }

        public string LoadNextPage()
        {
            var templatedMessagePageContentTemplate = LoadPageTemplate(TemplatedMessagePageName);
            var templatedMessagePageContent = string.Format(
                templatedMessagePageContentTemplate,
                $"Here will be some dynamic title ({DateTime.Now:u})",
                $"Here will be some dynamic description ({DateTime.Now:u})");

            return templatedMessagePageContent;
        }

        private string LoadPageTemplate(string templateName)
        {
            var filePath = _pagesPaths.FirstOrDefault(path => path.Contains(templateName));
            var stream = GetType().Assembly.GetManifestResourceStream(filePath);
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            };
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            if (_observers.TryGetValue(observer, out var disposable))
            {
                return disposable;
            }

            disposable = Disposable.Create(() => _observers.Remove(observer));
            _observers.Add(observer, disposable);
            return disposable;
        }
    }
}