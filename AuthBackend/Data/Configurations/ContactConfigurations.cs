using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Data.Configurations
{
    public class ContactConfigurations : IEntityTypeConfiguration<Contact>
	{
		public virtual void Configure(EntityTypeBuilder<Contact> builder)
		{


			builder
			   .HasOne<User>(s => s.Follower)
			   .WithMany(g => g.Followers)
			   .HasForeignKey(s => s.FollowerId);


			builder
			   .HasOne<User>(s => s.Following)
			   .WithMany(g => g.Followings)
			   .HasForeignKey(s => s.FollowingId);


			builder.Property(x => x.Id).HasColumnName("nContactId").IsRequired();

			builder.Property(x => x.FollowerId).HasColumnName("nFollowerId").IsRequired();

			builder.Property(x => x.FollowingId).HasColumnName("nFollowingId").IsRequired();

			builder.Property(x => x.Accept).HasColumnName("bAccept").IsRequired();
		}
	}
}