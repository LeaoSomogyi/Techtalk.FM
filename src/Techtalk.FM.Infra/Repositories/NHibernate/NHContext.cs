﻿using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;
using FluentNHibernate.Mapping.Providers;
using NHibernate;
using System;
using System.Collections.Generic;
using Techtalk.FM.Infra.Repositories.NHibernate.Mappings;

namespace Techtalk.FM.Infra.Repositories.NHibernate
{
    public class NHContext
    {
        #region "  Private Properties  "

        private List<Type> _mappings = new List<Type>();

        private string _connString { get; set; }

        private string _defaultSchema { get; set; }

        private string _providerName { get; set; }

        private static readonly object _lock = new object();

        private static ISessionFactory _sessionFactory;

        #endregion

        #region "  Public Properties  "

        public List<Type> Mappings { get => _mappings; set {; } }

        #endregion

        #region "  Constructors  "

        public NHContext(string connStringName, string defaultSchema, string providerName)
        {
            _connString = connStringName;
            _defaultSchema = defaultSchema;
            _providerName = providerName;

            LoadMappings();
        }

        #endregion

        #region "  Public Methods  "

        public void Add<T>() where T : class, new()
        {
            if (!typeof(IMappingProvider).IsAssignableFrom(typeof(T))
                && !typeof(IIndeterminateSubclassMappingProvider).IsAssignableFrom(typeof(T))
                && !typeof(IFilterDefinition).IsAssignableFrom(typeof(T)))
            {
                throw new Exception($"{typeof(T).Name} not is a FluentNHibernate ClassMap.");
            }

            _mappings.Add(typeof(T));
        }

        public void LoadMappings()
        {
            Add<UserMap>();
            Add<BookMap>();
        }

        public ISessionFactory Configure()
        {
            lock (_lock)
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = Fluently.Configure().Database(DefineProvider())
                        .Mappings(c =>
                        {
                            c.FluentMappings.Conventions.Setup(x => x.Add(AutoImport.Never()));
                            Mappings.ForEach(m => c.FluentMappings.Add(m));
                        })
                        .BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        #endregion

        #region "  Private Methods  "

        private IPersistenceConfigurer DefineProvider()
        {
            switch (_providerName)
            {
                case "postgresql":
                    var postgreSQLConfig = PostgreSQLConfiguration.PostgreSQL82
                        .ConnectionString(_connString)
                        .DefaultSchema(_defaultSchema);

                    return postgreSQLConfig;

                case "sqlserver":
                    var sqlServerConfig = MsSqlConfiguration.MsSql2012
                       .ConnectionString(_connString)
                       .DefaultSchema(_defaultSchema);

                    return sqlServerConfig;

                case "sqlite":
                    var sqLiteConfig = SQLiteConfiguration.Standard
                       .ConnectionString(_connString)
                       .InMemory()
                       .DefaultSchema(_defaultSchema);

                    return sqLiteConfig;

                case "firebird":
                    var firebirdConfig = new FirebirdConfiguration()
                        .ConnectionString(_connString)
                        .DefaultSchema(_defaultSchema);

                    return firebirdConfig;

                default:
                    throw new ArgumentException("Unable to define database provider");
            }
        }

        #endregion
    }
}
