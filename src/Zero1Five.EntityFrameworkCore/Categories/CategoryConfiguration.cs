using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Zero1Five;
using Zero1Five.Gigs;

namespace Zero1Five.Categories
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(Zero1FiveConsts.DbTablePrefix + "Categories" + Zero1FiveConsts.DbSchema);
            builder.ConfigureByConvention();
            
            builder.Property(n => n.Name)
            .IsRequired()
            .HasMaxLength(CategoryConsts.NameMaxlength);

            builder.Property(n => n.Description)
            .IsRequired()
            .HasMaxLength(CategoryConsts.DescriptionMaxlength);

            builder
                .HasMany<Gig>()
                .WithOne()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}