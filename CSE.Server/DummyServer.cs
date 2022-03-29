using CSE.Server.Models;
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

        private readonly Dictionary<IObserver<string>, IDisposable> observers;
        private readonly string[] pagesPaths;

        private CancellationTokenSource cancellationTokenSource;
        private List<TestQuestion> questions;

        private DummyServer()
        {
            observers = new Dictionary<IObserver<string>, IDisposable>();
            questions = new List<TestQuestion>()
            {
                new TestQuestion(
                    "How many angles are in a triangle", 
                    new List<Anwer>() 
                    { 
                        new Anwer(true, "3"), 
                        new Anwer(false, "4"),  
                        new Anwer(false, "5")
                    }),
                new TestQuestion(
                    "How many corners are in a quadrilateral",
                    new List<Anwer>()
                    {
                        new Anwer(false, "3"),
                        new Anwer(true, "4"),
                        new Anwer(false, "5")
                    }),
                new TestQuestion(
                    "How many corners are in a pentagon",
                    new List<Anwer>()
                    {
                        new Anwer(true, "This is confidential information"),
                        new Anwer(false, "4"),
                        new Anwer(true, "5")
                    }),
            };

            var currentAssembly = this.GetType().Assembly;
            pagesPaths = currentAssembly.GetManifestResourceNames();
        }

        public static DummyServer Instance { get; } = new DummyServer();

        public string Start(string userName)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();

            _ = NotifyAsync(cancellationTokenSource.Token);

            var welcomePageContentTemplate = LoadPageTemplate(WelcomePageName);
            return string.Format(welcomePageContentTemplate, userName);

            async Task NotifyAsync(CancellationToken token)
            {
                for (var i = 0; i < questions.Count; i++)
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

                    observers.Keys.ToList().ForEach(item => item.OnNext(LoadNextPage(i)));
                }
            }
        }

        public string LoadNextPage(int index)
        {
            var templatedMessagePageContentTemplate = LoadPageTemplate(TemplatedMessagePageName);
            var templatedMessagePageContent = string.Format(
                templatedMessagePageContentTemplate,
                index.ToString(),
                questions[index].Question,
                questions[index].Answers[0].AnwerContent,
                questions[index].Answers[1].AnwerContent,
                questions[index].Answers[2].AnwerContent);

            return templatedMessagePageContent;
        }

        private string LoadPageTemplate(string templateName)
        {
            var filePath = pagesPaths.FirstOrDefault(path => path.Contains(templateName));
            var stream = GetType().Assembly.GetManifestResourceStream(filePath);
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            };
        }

        public void Stop()
        {
            cancellationTokenSource?.Cancel();
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            if (observers.TryGetValue(observer, out var disposable))
            {
                return disposable;
            }

            disposable = Disposable.Create(() => observers.Remove(observer));
            observers.Add(observer, disposable);
            return disposable;
        }
    }
}