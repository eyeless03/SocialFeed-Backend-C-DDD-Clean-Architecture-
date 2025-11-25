using Domains.Entities;
using Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication3.Infrastructure.Configurations;

public class UserConfiguration:IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(v => v.Value, v => UserId.From(v)).IsRequired().HasColumnName("author_id");
        builder.Property(c => c.Username).HasConversion(v => v.Value, v => new Username(v)).HasMaxLength(25).IsRequired();
        builder.Property(c => c.PasswordHash).HasConversion(v =>v.Value, v => PasswordHash.FromHashed(v)).IsRequired().HasColumnName("password");
    }
}