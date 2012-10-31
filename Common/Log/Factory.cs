namespace Common.Log
{
    public interface Factory
    {
        T GetInstance<T>();
    }
}