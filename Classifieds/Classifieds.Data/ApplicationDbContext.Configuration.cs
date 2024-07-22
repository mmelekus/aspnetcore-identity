using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Classifieds.Data;

public partial class ApplicationDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            SqlConnectionStringBuilder builder = new();

            builder.DataSource = "tcp:127.0.0.1,1433";
            builder.InitialCatalog = "Classifieds_db";
            builder.TrustServerCertificate = true;
            builder.MultipleActiveResultSets = true;
            builder.ConnectTimeout = 3;
            builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR"); // "SA";
            builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD"); // "s3cret-Ninja";
            
            optionsBuilder.UseSqlServer(builder.ConnectionString);
        }
    }
}