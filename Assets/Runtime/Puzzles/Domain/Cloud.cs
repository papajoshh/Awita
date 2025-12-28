using System;

namespace Runtime.Infrastructure
{
    public class Cloud
    {
        public bool IsRaining { get; private set; } = false;
        public void StartRaining()
        {
            IsRaining = true;
            OnRainStarted?.Invoke();
        }

        public event Action OnRainStarted;
    }
}