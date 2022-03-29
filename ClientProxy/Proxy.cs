using ClientProxy.Models;
using CSE.Client;
using CSE.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;

namespace ClientProxy
{
    public class Proxy : IObserver<string>, IObservable<NavigationIntent>, IObservable<string>
    {
        private readonly RegistrationClient registrationClient;
        private readonly Dictionary<IObserver<NavigationIntent>, IDisposable> _observers;

        private int currentPosition; 
        private Dictionary<int, string> pagesStatuses;

        public Proxy(User user)
        {
            registrationClient = new RegistrationClient();
            _observers = new Dictionary<IObserver<NavigationIntent>, IDisposable>();

            _ = registrationClient.OnRegister(user, this);
        }

        public void SwitchForward()
        {
            if (pagesStatuses.Count - 1 == currentPosition)
            {
                registrationClient.GetNextPage();
                return;
            }

            _observers.Keys.ToList().ForEach(item => item.OnNext(NavigationIntent.Forward()));
        }

        public void SwitchBackward()
        {
            _observers.Keys.ToList().ForEach(item => item.OnNext(NavigationIntent.Backward()));
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
            _observers.Keys.ToList().ForEach(item => item.OnNext(NavigationIntent.Forward(value)));
            pagesStatuses.Add(pagesStatuses.Count, null);
            currentPosition = pagesStatuses.Count - 1;
        }

        public IDisposable Subscribe(IObserver<NavigationIntent> observer)
        {
            if (_observers.TryGetValue(observer, out var disposable))
            {
                return disposable;
            }

            disposable = Disposable.Create(() => _observers.Remove(observer));
            _observers.Add(observer, disposable);
            return disposable;
        }

        public void SetAnswer(string answer)
        {
            
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            
        }
    }
}
