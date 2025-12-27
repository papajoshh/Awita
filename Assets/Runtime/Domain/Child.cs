namespace Runtime.Domain
{
    public class Child
    {
        public bool FirstLevelHidrationCompleted => levelOfHidration >= 1;
        public bool SecondLevelHidrationCompleted => levelOfHidration >= 4;
        private int levelOfHidration;
        
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
        }
    }
}