using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//only for generating DB schema
using log4net;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using FluentNHibernate.Automapping;

using XpertSquare.Data.NH.Mapping;
using XpertSquare.Core.Model;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateDbTables();
            Console.ReadLine();
        }

        private static void GenerateDbTables()
        {

            Configuration cfg = new Configuration();
            cfg.Configure();

            ISessionFactory factory = Fluently.Configure(cfg)
                 .Mappings(x => x.FluentMappings.AddFromAssemblyOf<QuestionMap>())
                 .ExposeConfiguration(c=>
                     {
                         new SchemaExport(cfg)
                         .Execute(true,true,false);
                     })
                 .BuildSessionFactory();

            /*

            var connString = "Data Source=Media25Office;User ID=sa;Password=marius123;persist security info=False;initial catalog=XpertHub;";
            //var connString = "Data Source=mssql300.ixwebhosting.com;User ID=media25_admin;Password=marius123;persist security info=False;initial catalog=media25_PhotoGallery;";
            Fluently.Configure()
                .Database(MsSqlConfiguration
                .MsSql2008
                .ShowSql()
                .ConnectionString(x => x.Is(connString)))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<QuestionMap>())
                .ExposeConfiguration(config => config.SetProperty(NHibernate.Cfg.Environment.ProxyFactoryFactoryClass, "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle"))
                .ExposeConfiguration(cfg =>
                {
                    new SchemaExport(cfg)
                    .Execute(true, true, false);
                })
                .BuildSessionFactory();
             */
        }
    }
}
