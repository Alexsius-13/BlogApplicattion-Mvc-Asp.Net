using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			var adminRoleId = "e8cc7b89-cdb6-43de-baaa-64812a819e4d";
			var superAdminRoleId = "ef640bb0-d245-4c14-b87e-841c89b3ee77";
			var userRoleId = "30ce4504-c706-4f88-b23c-a658c61c10b6";

			//Seed Data
			var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Name = "Admin",
					NormalizedName = "Admin",
					Id = adminRoleId,
					ConcurrencyStamp = adminRoleId
				},
				new IdentityRole
				{
					Name = "SuperAdmin",
					NormalizedName = "SuperAdmin",
					Id = superAdminRoleId,
					ConcurrencyStamp = superAdminRoleId
				},

				new IdentityRole
				{
					Name = "User",
					NormalizedName = "User",
					Id = userRoleId,
					ConcurrencyStamp = userRoleId
				}
			};

			builder.Entity<IdentityRole>().HasData(roles);

			var superAdminId = "2c461a98-26dc-475d-b0db-8ecd9dd015c7";
			var superAdminUser = new IdentityUser
			{
				UserName = "superadmin@bloggie.com",
				Email = "superadmin@bloggie.com",
				NormalizedEmail = "superadmin@bloggi.com".ToUpper(),
				NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
				Id = superAdminId
			};

			superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
				.HashPassword(superAdminUser, "Superadmin@123");

			builder.Entity<IdentityUser>().HasData(superAdminUser);

			var superAdminRoles = new List<IdentityUserRole<string>>
			{
				new IdentityUserRole<string>
				{
					RoleId = adminRoleId,
					UserId = superAdminUser.Id,
				},
				new IdentityUserRole<string>
				{
					RoleId = superAdminRoleId,
					UserId = superAdminUser.Id,
				},
				new IdentityUserRole<string>
				{
					RoleId = userRoleId,
					UserId = superAdminUser.Id,
				}
			};

			builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
		}
	}
}
