using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace todo_api.Models
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard
                    .UsingFile("Banco.db")
                )
                .Mappings(
                    m => m.FluentMappings.AddFromAssemblyOf<Todo>()
                ).ExposeConfiguration(
                    cfg => new SchemaExport(cfg).Create(false, false)
                )
                .BuildSessionFactory();
            
            return sessionFactory.OpenSession();
        }
        
    }
}