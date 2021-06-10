using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Zero1Five.Gigs;

namespace Zero1Five.EntityFrameworkCore.Gigs
{
    public class GigRepositoryTests:Zero1FiveEntityFrameworkCoreTestBase
    {
        private readonly IGigRepository _gigRepository;
        public GigRepositoryTests()
        {
            _gigRepository = GetRequiredService<IGigRepository>();
        }
        [Fact]
        public async Task  GetListAsync_Should_QueryList()
        {
            var result =await _gigRepository.GetListAsync();
            
            result.Count.ShouldBeGreaterThan(0);
        }
    }
}