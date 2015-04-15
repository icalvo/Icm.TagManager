using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Icm.TagManager.Domain.Test
{
    public class ServiceTests
    {
        [Fact]
        public async Task AddTagsToFileAsync_UsesNormalizer()
        {
            var fileMetadata = new FileMetadata("x");
            var repository = new Mock<IMetadataRepository>();
            var normalizer = new Mock<IPathNormalizer>();
            repository.Setup(x => x.GetByPathAsync("NORMALIZED a")).ReturnsAsync(fileMetadata);
            normalizer.Setup(x => x.Normalize("a")).Returns("NORMALIZED a").Verifiable();
            Service service = new Service(repository.Object, normalizer.Object);

            await service.AddTagsToFileAsync("a", "tag1", "tag2");

            repository.Verify(x => x.GetByPathAsync("NORMALIZED a"));
            repository.Verify(x => x.SaveAsync("NORMALIZED a", fileMetadata));
        }
    }
}