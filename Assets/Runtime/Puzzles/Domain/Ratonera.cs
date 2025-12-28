namespace Runtime.Infrastructure
{
    public class Ratonera
    {
        public bool IsOpen { get; private set; } = false;
        
        public void Open() => IsOpen = true;
    }
}