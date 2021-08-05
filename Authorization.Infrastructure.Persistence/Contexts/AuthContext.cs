using Authorization.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure.Persistence.Contexts
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> contextOptions)
            : base(contextOptions)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<RoleScope> RoleScopes { get; set; }
        public DbSet<Scope> Scopes { get; set; }
        public DbSet<ConfirmationRequest> ConfirmationRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().ToTable("role");
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().Property(x => x.Id).HasColumnName("r_id");
            modelBuilder.Entity<Role>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Role>().Property(x => x.Name).HasColumnName("name");

            modelBuilder.Entity<User>().ToTable("user");
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).HasColumnName("u_id");
            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
            modelBuilder.Entity<User>().Property(x => x.PasswordHash).HasColumnName("password");
            modelBuilder.Entity<User>().Property(x => x.Salt).HasColumnName("salt");
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasColumnName("first_name");
            modelBuilder.Entity<User>().Property(x => x.LastName).HasColumnName("last_name");
            modelBuilder.Entity<User>().Property(x => x.Active).HasColumnName("active");
            modelBuilder.Entity<User>().Property(x => x.CreatedAt).HasColumnName("created_at");
            modelBuilder.Entity<User>().Property(x => x.RemovedAt).HasColumnName("removed_at");
            modelBuilder.Entity<User>().HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey("r_id");

            modelBuilder.Entity<Client>().ToTable("client");
            modelBuilder.Entity<Client>().HasKey(x => x.Id);
            modelBuilder.Entity<Client>().Property(x => x.Id).HasColumnName("c_id");
            modelBuilder.Entity<Client>().HasIndex(x => x.ClientSecret).IsUnique();
            modelBuilder.Entity<Client>().Property(x => x.ClientSecret).HasColumnName("secret");
            modelBuilder.Entity<Client>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Client>().Property(x => x.Name).HasColumnName("name");
            modelBuilder.Entity<Client>().Property(x => x.CreatedAt).HasColumnName("created_at");

            modelBuilder.Entity<RefreshToken>().ToTable("refresh_token");
            modelBuilder.Entity<RefreshToken>().HasKey(x => x.Id);
            modelBuilder.Entity<RefreshToken>().Property(x => x.Id).HasColumnName("rt_id");
            modelBuilder.Entity<RefreshToken>().Property(x => x.Token).HasColumnName("refresh_token");
            modelBuilder.Entity<RefreshToken>().Property(x => x.ExpireAt).HasColumnName("expire_at");
            modelBuilder.Entity<RefreshToken>().Property(x => x.CreatedAt).HasColumnName("created_at");
            modelBuilder.Entity<RefreshToken>().Property(x => x.RevokedAt).HasColumnName("revoked_at");
            modelBuilder.Entity<RefreshToken>().Property(x => x.RevokeReason).HasColumnName("revoke_reason");
            modelBuilder.Entity<RefreshToken>().HasOne(x => x.Client).WithMany().HasForeignKey("c_id");
            modelBuilder.Entity<RefreshToken>().HasOne(x => x.User).WithMany().HasForeignKey("u_id");

            modelBuilder.Entity<RoleScope>().ToTable("role_scope");
            modelBuilder.Entity<RoleScope>().HasKey(x => new { x.RoleId, x.ScopeId });
            modelBuilder.Entity<RoleScope>().Property(x => x.RoleId).HasColumnName("r_id");
            modelBuilder.Entity<RoleScope>().Property(x => x.ScopeId).HasColumnName("s_id");
            modelBuilder.Entity<RoleScope>().HasOne(x => x.Role).WithMany(x => x.RoleScopes).HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<RoleScope>().HasOne(x => x.Scope).WithMany(x => x.RoleScopes).HasForeignKey(x => x.ScopeId);

            modelBuilder.Entity<Scope>().ToTable("scope");
            modelBuilder.Entity<Scope>().HasKey(x => x.Id);
            modelBuilder.Entity<Scope>().Property(x => x.Id).HasColumnName("s_id");

            modelBuilder.Entity<ConfirmationRequest>().ToTable("confirmation_request");
            modelBuilder.Entity<ConfirmationRequest>().HasKey(x => x.Id);
            modelBuilder.Entity<ConfirmationRequest>().Property(x => x.Id).HasColumnName("cr_id");
            modelBuilder.Entity<ConfirmationRequest>().HasIndex(x => x.Key).IsUnique();
            modelBuilder.Entity<ConfirmationRequest>().Property(x => x.Key).HasColumnName("key");
            modelBuilder.Entity<ConfirmationRequest>().Property(x => x.CreatedAt).HasColumnName("created_at");
            modelBuilder.Entity<ConfirmationRequest>().Property(x => x.ExpiredAt).HasColumnName("expired_at");
            modelBuilder.Entity<ConfirmationRequest>().Property(x => x.Confirmed).HasColumnName("confirmed");
            modelBuilder.Entity<ConfirmationRequest>().Property(x => x.RequestType).HasColumnName("request_type");
            modelBuilder.Entity<ConfirmationRequest>().HasOne(x => x.User).WithMany(x => x.ConfirmationRequests).HasForeignKey("u_id");
        }
    }
}
