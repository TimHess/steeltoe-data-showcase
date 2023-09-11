using Imani.Solutions.Core.API.Util;
using steeltoe.data.showcase.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.IO;
using System;

namespace steeltoe.data.showcase.Repository
{
    public class SampleContext : DbContext
    {
        private ISettings settings = new ConfigSettings();
        private string connectionString;
        
        private const string defaultSchemaName = "Sample";

        private readonly string schemaName;

        public SampleContext(DbContextOptions options) : base(options)
        {
            this.schemaName = settings.GetProperty("SCHEMA_NAME",defaultSchemaName);
            this.connectionString = settings.GetProperty("ConnectionString","");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options){
            
            var exeName = Path.GetFileName(Assembly.GetEntryAssembly().Location);
            Console.WriteLine("Running with " + exeName);

            if("testhost.dll".Equals(exeName))
                return; //do not Postgres connections

            this.connectionString = settings.GetProperty("ConnectionString");

            options.UseNpgsql(connectionString,
            x => x.MigrationsHistoryTable("__EFMigrationsHistory", schemaName));
        }
            

        protected override void OnModelCreating(ModelBuilder builder){

            builder.HasDefaultSchema(schemaName);    //To all the tables.

            builder.Entity<Account>()
            .HasKey(b => b.Id).HasName("data_id");

            builder.Entity<Account>().Property(b => b.Id)
            .UseIdentityColumn();


            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Account> Account { get; set; }

        public static void Migrate(DatabaseFacade database)
        {
             database.Migrate();
        }
    }

}
