namespace Runtime.Infrastructure
{
    public class Router
    {
        public bool IsConnected { get; private set; } = false;
        public void Connect() => IsConnected = true;
    }
}