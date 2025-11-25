using Domains.Entities;
using Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication3.Infrastructure.Configurations;

public class PostConfiguration:IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasConversion(p=> p.Value, p=> PostId.From(p)).IsRequired().HasColumnName("id");
        builder.Property(p => p.Title).HasConversion(p=> p.Value, p=>new PostText(p)).IsRequired().HasMaxLength(250).HasColumnName("title");
        builder.Property(p =>p.AuthorId).HasConversion(id => id.Value, id => UserId.From(id)).IsRequired().HasColumnName("author_id");
        builder.HasMany(p => p.Comments).WithOne().HasForeignKey("post_id");
        builder.Navigation(x =>x.Comments).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Ignore(p => p.LikedUsers);
        builder.Ignore(p => p.DislikedUsers);
        builder.Ignore(p => p.LikesCount);
        builder.Ignore(p => p.DislikesCount);
    }
}