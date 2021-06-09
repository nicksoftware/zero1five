using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Zero1Five.Categories;
using Zero1Five.Gigs;

namespace Zero1Five.Products
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(Zero1FiveConsts.DbTablePrefix + "Products" + Zero1FiveConsts.DbSchema);
            builder.ConfigureByConvention();
            builder.Property(a => a.Title).IsRequired().HasMaxLength(ProductConsts.TitleMaxLength);

            builder.Property(a => a.Description).IsRequired().HasMaxLength(ProductConsts.DescriptionMaxLength);

            builder.Property(a => a.CoverImage).IsRequired().HasMaxLength(ProductConsts.CoverMaxLength);

            builder.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId).IsRequired();
            builder.HasOne<Gig>().WithMany().HasForeignKey(x => x.GigId).IsRequired();

        }
    }
}