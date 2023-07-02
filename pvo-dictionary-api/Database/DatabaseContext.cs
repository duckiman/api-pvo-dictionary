using pvo_dictionary_api.Models;
using pvo_dictionary_api.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Npgsql;
using System.Collections.Generic;
using System;

namespace pvo_dictionary_api.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        #region Dictionary
        public DbSet<Dictionary> dictionaries { get; set; }
        #endregion

        #region User
        public DbSet<User> users { get; set; }
        #endregion

        #region Example
        public DbSet<Example> examples { get; set; }
        #endregion

        #region Example_relationship
        public DbSet<ExampleRelationship> example_relationships { get; set; }
        #endregion

        #region Example_link
        public DbSet<ExampleLink> example_links { get; set; }
        #endregion

        #region Mode
        public DbSet<Mode> modes { get; set; }
        #endregion

        #region Tone
        public DbSet<Tone> tones { get; set; }
        #endregion

        #region Dialect
        public DbSet<Dialect> dialects { get; set; }
        #endregion

        #region Register
        public DbSet<Register> registers { get; set; }
        #endregion

        #region Nuance
        public DbSet<Nuance> nuance { get; set; }
        #endregion

        #region UserSetting
        public DbSet<UserSetting> user_settings { get; set; }
        #endregion

        #region AuditLog
        public DbSet<AuditLog> audit_logs { get; set; }
        #endregion

        #region Concept
        public DbSet<Concept> concepts { get; set; }
        #endregion

        #region ConceptLink
        public DbSet<ConceptLink> concept_links { get; set; }
        #endregion

        #region ConceptRelationship
        public DbSet<ConceptRelationship> concept_relationships { get; set; }
        #endregion


        public static void UpdateDatabase(DatabaseContext context)
        {
            context.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var sqlConnection = "Server=localhost;Port=3306;Database=pvo_dictionary;Uid=root;Pwd=1234$;MaximumPoolSize=500;";
                optionsBuilder.UseMySql(sqlConnection,
                    MySqlServerVersion.LatestSupportedServerVersion);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            new UserSeeder(modelBuilder).SeedData();
            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}