using Common.Aspects;

namespace Test.Aspects
{
    public class CachedClass
    {
        private int counter = 0;
        
        public int Get()
        {
            return counter++;
        }
    }
}