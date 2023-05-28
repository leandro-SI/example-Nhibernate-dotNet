using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace ExNhibernate.API.Dependency
{
    public static class NHibernateDependency
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(NHibernateDependency).Assembly.ExportedTypes);
            HbmMapping entityMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();

            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<MsSql2012Dialect>();
                c.ConnectionString = connectionString;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.SchemaAction = SchemaAutoAction.Update;
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
            });

            configuration.AddMapping(entityMapping);

            var sessionFactory = configuration.BuildSessionFactory();
            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            return services;

        }
    }
}
