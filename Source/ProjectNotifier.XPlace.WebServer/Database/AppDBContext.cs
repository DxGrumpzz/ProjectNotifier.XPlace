namespace ProjectNotifier.XPlace.WebServer
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ProjectNotifier.XPlace.Core;

    /// <summary>
    /// The server's database access class
    /// </summary>
    public class AppDBContext : IdentityDbContext<AppUserModel>
    {

        DbSet<UserProjectPreference> UserProjectPreferences { get; set; }



        public AppDBContext(DbContextOptions options) : 
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region UserProjectPreferences

            // Set primary key
            builder.Entity<UserProjectPreference>()
            .HasKey(key => key.RowID);

            #endregion


            #region AppUserModel

            // Map user preferences to a 1 to many relationship
            builder.Entity<AppUserModel>()
            .HasMany(property => property.UserProjectPreferences)
            .WithOne(p => p.User);


            #endregion
        }
    };
};