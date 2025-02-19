using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace WebApi.Infraestructure.Data
{
	public class AppDbContext : DbContext
    {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Permission> Permissions { get; set; }
		public DbSet<PermissionType> PermissionTypes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Permission>()
				.HasOne(p => p.PermissionTypeNavigation)
				.WithMany()
				.HasForeignKey(p => p.PermissionType);
		}
	}
}
