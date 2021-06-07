using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Zero1Five.Categories;

namespace Zero1Five
{
    public class Zero1FiveTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly ICategoryRepository categoryRepository;

        public Zero1FiveTestDataSeedContributor(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */
            await SeedCategories(context);
        }

        private async Task SeedCategories(DataSeedContext context)
        {
            var names = new[] { "Development", "Marketing", "Cooking" };

            var rollDice = new Random();

            List<Category> categories = new();

            for (var x = 0; x < names.Length; x++)
            {
                var name = names[rollDice.Next(0, names.Length)];
                categories.Add(Category.Create(Guid.NewGuid(), name, $"{name} description"));
            }
            await categoryRepository.InsertManyAsync(categories, true);
        }
    }
}