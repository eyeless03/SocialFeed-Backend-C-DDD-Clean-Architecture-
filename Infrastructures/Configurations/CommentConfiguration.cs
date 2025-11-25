using Domains.Entities;
using Domains.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication3.Infrastructure.Configurations;

public class CommentConfiguration:IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");
        builder.HasKey(c => c.Id);
        builder.Property(c=> c.Id).HasConversion(c =>c.Value, c => CommentId.From(c)).IsRequired().HasColumnName("comment_id");
        builder.Property(c=> c.Text).HasConversion(c => c.Value, c => new CommentText(c)).IsRequired().HasColumnName("text");
        builder.Property(c => c.AuthorId).HasConversion(c => c.Value, c => UserId.From(c)).IsRequired().HasColumnName("author_id");
        builder.Ignore(c => c.LikedUsers);
        builder.Ignore(c => c.DislikedUsers);
        builder.Ignore(c => c.LikesCount);
        builder.Ignore(c => c.DislikesCount);
    }
}