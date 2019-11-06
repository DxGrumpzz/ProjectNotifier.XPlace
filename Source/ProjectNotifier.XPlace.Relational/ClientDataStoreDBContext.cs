namespace ProjectNotifier.XPlace.Relational
{
    using Microsoft.EntityFrameworkCore;
    using ProjectNotifier.XPlace.Core;

    /// <summary>
    /// A C# representation of a local SQL lite database
    /// </summary>
    public class ClientDataStoreDBContext : DbContext
    {
        /// <summary>
        /// A table of <see cref="LoginCredentialsDataModel"/>
        /// </summary>
        public DbSet<LoginCredentialsDataModel> LoginCredentials { get; set; }

        /// <summary>
        /// A table for the user's application settings
        /// </summary>
        public DbSet<AppSettingsDataModel> ClientAppSettings { get; set; }


        public ClientDataStoreDBContext(DbContextOptions<ClientDataStoreDBContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary key for login credentials
            modelBuilder.Entity<LoginCredentialsDataModel>()
                .HasKey(key => key.DataModelID);

            // Set primary key for client app settings
            modelBuilder.Entity<AppSettingsDataModel>()
                .HasKey(key => key.RowID);
        }
    };
};
