using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Data.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
	{
		public virtual void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.HasKey(sc => new { sc.Id });

			builder.Property(x => x.Id).HasColumnName("nRoleId").IsRequired();

			builder.Property(x => x.TypeRole).HasColumnName("sTypeRole").IsRequired();

			builder.Property(x => x.Descripcion).HasColumnName("sDescripcion").IsRequired();

			builder.HasData(
				new Role(1, "Gerente", "Cuenta Role para los gerentes que manejan empresas"),
				new Role(2, "Cliente", "Cuenta Role para los clientes que buscan trabajo"),
				new Role(3, "Administrador", "Cuenta Role para los administradores")
				);



		}
	}
}