namespace Common
{
    public interface Factory
    {
        T GetInstance<T>();
    }
}