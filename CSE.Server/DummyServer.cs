using CSE.Server.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSE.Server
{
    public class DummyServer : IObservable<string>
    {
        private const string TemplatedMessagePageName = "TemplatedMessagePage";
        private const string WelcomePageName = "WelcomePage";
        private const string SelectabeRadioButtonTemplate = @"
            <RadioButton
                x:Name=""ChoiceRadioButton{0}""
                Margin=""5""
                CheckedChanged=""OnCheckedChanged""
                Content=""{1}""
                GroupName=""First Group"" />""";

        private readonly Dictionary<IObserver<string>, IDisposable> _observers;
        private readonly string[] _pagesPaths;
        private readonly Question[] _questions;

        private int _currentIndex;
        private CancellationTokenSource _cancellationTokenSource;

        private DummyServer()
        {
            _observers = new Dictionary<IObserver<string>, IDisposable>();
            _questions = Questionnaire.GetMockQuestionnaire();

            var currentAssembly = this.GetType().Assembly;
            _pagesPaths = currentAssembly.GetManifestResourceNames();
        }

        public static DummyServer Instance { get; } = new DummyServer();

        public void StartTimer()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            _ = NotifyAsync(_cancellationTokenSource.Token);

            async Task NotifyAsync(CancellationToken token)
            {
                while (_currentIndex < _questions.Length)
                {
                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    await Task.Delay(TimeSpan.FromSeconds(15));

                    if (token.IsCancellationRequested)
                    {
                        return;
                    }

                    if (_currentIndex == _questions.Length)
                    {
                        return;
                    }

                    _observers.Keys.ToList().ForEach(item => item.OnNext(LoadNextPage(_currentIndex)));
                    _currentIndex++;
                }
            }
        }

        public string Start(string userName)
            => string.Format(LoadPageTemplate(WelcomePageName), userName);

        public void GetNextPage()
        {
            if (_currentIndex == _questions.Length)
            {
                return;
            }

            _observers.Keys.ToList().ForEach(item => item.OnNext(LoadNextPage(_currentIndex)));
            _currentIndex++;
        }

        private string LoadNextPage(int index)
        {
            var question = Questionnaire.GetMockQuestionnaire()[index];
            var templatedMessagePageContentTemplate = LoadPageTemplate(TemplatedMessagePageName);

            var radioButtons = new StringBuilder();
            for(int i = 0; i < question.Answers.Length; i++)
            {
                radioButtons.Append(string.Format(
                    SelectabeRadioButtonTemplate,
                    i,
                    question.Answers[i]));
            }

            var pageNumber = index + 1;
            var templatedMessagePageContent = string.Format(
                templatedMessagePageContentTemplate,
                pageNumber,
                question.Text,
                radioButtons);

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