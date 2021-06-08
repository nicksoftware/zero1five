using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Zero1Five.Categories;
using Zero1Five.Gigs;
using Zero1Five.Products;
using Zero1Five.TestBase;

namespace Zero1Five
{
    public class Zero1FiveTestDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IProductRepository productRepository;
        private readonly IGigRepository gigRepository;

        public Zero1FiveTestDataSeedContributor(ICategoryRepository categoryRepository,
        IProductRepository productRepository,
        IGigRepository gigRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.gigRepository = gigRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            /* Seed additional test data... */
           
            await SeedCategoriesAsync(context);
        }

        private async Task SeedCategoriesAsync(DataSeedContext context)
        {
            var names = new[] { "Development", "Marketing", "Cooking" };

            var rollDice = new Random();

            var categories = names
                .Select(t => names[rollDice.Next(0, names.Length)])
                .Select(name => Category
                    .Create(Guid.NewGuid(), name, $"{name} description"))
                .ToList();

            await categoryRepository.InsertManyAsync(categories, true);
            
            List<Gig> gigs = new();
            for (var i = 1; i < 10; i++)
            {
                var id = Guid.NewGuid();
                var title = $"Gig {i}";
                var cover = $"cover{i}.jpg";
                var description = $"Gig {i} Description";
                if (i == 2) id = Guid.Parse(Zero1FiveTestData.GigId);
                var gig = Gig.Create(id, title, cover, description);
                gigs.Add(gig);
            }
            
            await gigRepository.InsertManyAsync(gigs, true);
            
            List<Product> products = new();
            
            for (var x = 0; x < 10; x++)
            {
                var name = $"Product {x}";
                var categoryId = categories[rollDice.Next(0, categories.Count)].Id;
                
                var gigId = Guid.Parse(Zero1FiveTestData.GigId);
                var product = Product.Create(Guid.NewGuid(), gigId, categoryId, name, $"image");
            
                product.Description = $"{name} Description";

                if (x == 1) product.IsPublished = true;
                
                products.Add(product);
            }
            await productRepository.InsertManyAsync(products, true);           // List<Product> products = new();
            
           
        }

    }
}