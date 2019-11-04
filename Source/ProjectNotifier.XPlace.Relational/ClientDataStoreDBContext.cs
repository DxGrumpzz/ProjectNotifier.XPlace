namespace ProjectNotifier.XPlace.Relational
{
    using Microsoft.EntityFrameworkCore;
    using ProjectNotifier.XPlace.Core;

    /// <summary>
    /// 
    /// </summary>
    public class ClientDataStoreDBContext : DbContext
    {

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
