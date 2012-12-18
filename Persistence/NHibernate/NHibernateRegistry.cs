namespace Common.Persistence.NHibernate
{
    using StructureMap.Configuration.DSL;

    using global::NHibernate;

    public class NHibernateRegistry : Registry
    {
        public NHibernateRegistry() 
        {
            // This is the interesting stuff
            // The session factory is slow to create, because it compiles all the mappings
            // We only want it done once, then use it to quickly create sessions.
            // You'll notice the Singleton scope here. 
            // It's created in the NHibernateSessionFactory class the first time it's requested
            // The StructureMap keeps it around
            this.For<ISessionFactory>()
                .Singleton()
                 .Use(x => x.GetInstance<ISessionFactoryBuilder>().GetSessionFactory());

            // The Session is how we interact with NHibernate in our code.
            // You'll see it's created by taking the instance of our SessionFactory, and calling OpenSession on it
            // The InstanceScope.Hybrid (Changed to non obsolete HybridHttpOrThreadLocalScoped) means that in a web app, 
            // we keep the same session around for the life of a webrequest
            // When there is no HttpContext to store it against, it gets stored against the thread context
            // This lets us use the same configuration in integration tests.
            this.For<ISession>()
                .HybridHttpOrThreadLocalScoped()
                .Use(x => x.GetInstance<ISessionFactory>().OpenSession());            
        }
    }
}