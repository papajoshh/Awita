namespace Runtime.Domain
{
    public class Child
    {
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