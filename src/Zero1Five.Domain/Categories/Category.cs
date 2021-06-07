using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Zero1Five.Categories
{
    public class Category : BasicAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        protected Category() { }
        private Category(Guid id, string name, string description) : base(id)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name)); ;
            Description = Check.NotNullOrWhiteSpace(description, nameof(description));
        }

        internal static Category Create(Guid id, string name, string description)
        {
            return new Category(id, name, description);
        }
    }
}