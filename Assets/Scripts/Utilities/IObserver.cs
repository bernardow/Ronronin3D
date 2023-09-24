using System.Collections;

namespace Utilities
{
    public interface IObserver
    {
        public void OnNotify();

        public IEnumerator Run();

        void Disable();

        void Enable();
    }
}
