using System.Collections.Generic;

namespace Utilities
{
    public class ObservableObject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void AddObserver(IObserver target) => _observers.Add(target);

        public void RemoveObserver(IObserver target)
        {
            if (_observers.Contains(target))
                _observers.Remove(target);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
                observer.OnNotify();
        }

        public void NotifySingleObserver(int index) => _observers[index].OnNotify();
    }
}
