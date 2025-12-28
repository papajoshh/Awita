using System;

namespace Runtime.Infrastructure
{
    public class Extractor
    {
        public bool IsExtracting { get; private set; } = false;
        public event Action OnExtractingFinished;
        public bool CloudAppeared { get; private set; }
        private float timeOn = 0f;
        
        public void Toogle()
        {
            IsExtracting = !IsExtracting;
            if (!IsExtracting) timeOn = 0f;
        }

        public void Update(float delta)
        {
            timeOn += delta;
            if (!(timeOn >= 15f)) return;
            CloudAppeared = true;
            OnExtractingFinished?.Invoke();
        }
    }
}