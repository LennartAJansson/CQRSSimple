namespace CQRS.Data
{
    using CQRS.Model;

    using Microsoft.EntityFrameworkCore;

    public class WeatherForecastsContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public WeatherForecastsContext(DbContextOptions<WeatherForecastsContext> options)
            : base(options)
        { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.Entity<Operation>()
        //        .HasOne<WeatherForecast>()
        //        .WithMany()
        //        .HasForeignKey(o => o.WeatherForecastId)
        //        .OnDelete(DeleteBehavior.NoAction)
        //        .HasConstraintName("FK_Operations_WeatherForecasts_WeatherForecastId");
    }
}
