using NHibernate;

namespace Common.Persistence.NHibernate
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory GetSessionFactory();
    }
}
