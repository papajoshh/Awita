using System;

namespace Runtime.Domain
{
    public class Child
    {
        public bool FirstLevelHidrationCompleted => levelOfHidration >= 1;
        public bool SecondLevelHidrationCompleted => levelOfHidration >= 4;
        public bool ThirdLevelHidrationCompleted => levelOfHidration >= 12;
        private int levelOfHidration;

        public event Action OnHidrate;
        public static Child NewBorn()
        {
            return new Child()
            {
                levelOfHidration = 0
            };
        }

        public void Hidrate()
        {
            levelOfHidration++;
            OnHidrate?.Invoke();
        }
    }
}