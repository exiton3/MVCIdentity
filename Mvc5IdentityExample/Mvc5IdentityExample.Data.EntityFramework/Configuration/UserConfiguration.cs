using Mvc5IdentityExample.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Mvc5IdentityExample.Data.EntityFramework.Configuration
{
    internal class UserConfiguration : EntityTypeConfiguration<UserN>
    {
        internal UserConfiguration()
        {
            ToTable("UserN");

            HasKey(x => x.UserId)
                .Property(x => x.UserId)
                .HasColumnName("UserId")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            Property(x => x.PasswordHash)
                .HasColumnName("PasswordHash")
                .HasColumnType("nvarchar")
                .IsMaxLength()
                .IsOptional();

            Property(x => x.SecurityStamp)
                .HasColumnName("SecurityStamp")
                .HasColumnType("nvarchar")
                .IsMaxLength()
                .IsOptional();

            Property(x => x.UserName)
                .HasColumnName("UserName")
                .HasColumnType("nvarchar")
                .HasMaxLength(256)
                .IsRequired();

            HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .Map(x =>
                {
                    x.ToTable("UserRole");
                    x.MapLeftKey("UserId");
                    x.MapRightKey("RoleId");
                });

            HasMany(x => x.Claims)
                .WithRequired(x => x.UserN)
                .HasForeignKey(x => x.UserId);

            HasMany(x => x.Logins)
                .WithRequired(x => x.UserN)
                .HasForeignKey(x => x.UserId);
        }
    }
}