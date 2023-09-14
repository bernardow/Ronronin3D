namespace Utilities
{
    public interface IObserver
    {
        public void OnNotify();

        void Disable();

        void Enable();
    }
}
