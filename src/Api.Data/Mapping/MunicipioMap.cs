using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using src.Api.Domain.Entities;

namespace src.Api.Data.Mapping
{
    public class MunicipioMap : IEntityTypeConfiguration<MunicipioEntity>
    {
        public void Configure(EntityTypeBuilder<MunicipioEntity> builder)
        {
            builder.ToTable("municipio");

            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.CodIBGE);

            builder.HasOne(u => u.Uf)
                   .WithMany(m => m.Municipios); 
        }
    }
}