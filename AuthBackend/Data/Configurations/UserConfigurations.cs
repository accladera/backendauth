using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Data.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
	{
		public virtual void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Id).HasColumnName("nUserId").IsRequired();

			builder.Property(x => x.UserName).HasColumnName("sUserName").IsRequired();

			builder.Property(x => x.Email).HasColumnName("sEmail").IsRequired();


			//builder.Property(x => x.UserName).HasColumnName("sUserName");
			//builder.Property(x => x.Email).HasColumnName("sEmail");
			builder.Property(x => x.Hash).HasColumnName("sHash").IsRequired();
			builder.Property(x => x.ResetPassword).HasColumnName("bResetPassword").IsRequired();
			builder.Property(x => x.RoleId).HasColumnName("nRoleId").IsRequired();
			builder.Property(x => x.BirthDate).HasColumnName("dBirthDate").IsRequired();

		}
	}
}