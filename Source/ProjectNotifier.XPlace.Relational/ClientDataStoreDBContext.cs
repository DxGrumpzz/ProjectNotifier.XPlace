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


        public ClientDataStoreDBContext(DbContextOptions<ClientDataStoreDBContext> options) :
            base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary key
            modelBuilder.Entity<LoginCredentialsDataModel>()
                .HasKey(key => key.DataModelID);
        }
    };
};
