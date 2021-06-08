using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Zero1Five.Products;

namespace Zero1Five.Gigs
{
    public class GigConfiguration : IEntityTypeConfiguration<Gig>
    {
        public void Configure(EntityTypeBuilder<Gig> builder)
        {
            builder.ToTable(Zero1FiveConsts.DbTablePrefix + "Gigs" + Zero1FiveConsts.DbSchema);

            builder.ConfigureByConvention();

            builder.Property(x => x.Title).IsRequired().HasMaxLength(GigConsts.TitleMaxLength);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(GigConsts.DescriptionMaxLength);

            builder.Property(x => x.CoverImage).IsRequired().HasMaxLength(GigConsts.CoverImageMaxLength);

            builder.HasMany<Product>(x => x.Products).WithOne().HasForeignKey(d => d.GigId);
        }
    }
}