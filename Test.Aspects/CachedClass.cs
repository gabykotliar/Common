using Common.Aspects;

namespace Test.Aspects
{
    public class CachedClass
    {
        private int counter = 0;

        [CacheAspect(SlidingExpiryWindow = "60")]
        public int Get()
        {
            return counter++;
        }
    }
}