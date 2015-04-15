using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Icm.TagManager.Infrastructure;
using Xunit;

namespace Icm.TagManager.Domain.Test
{
    public class FullTests
    {
        [Fact]
        public async Task UseCase()
        {
            IMetadataRepository repo = new MongoMetadataRepository("mongodb://localhost", "test_tagmanager");
            var service = new Service(repo);
            string tempPath = CreateTempFile();
            string[] expectedTags = {"tag test 1", "tag 2"};
            await service.AddTagsToFileAsync(tempPath, expectedTags);

            repo = new MongoMetadataRepository("mongodb://localhost", "test_tagmanager");
            service = new Service(repo);

            FileMetadata metadata = await service.GetMetadataAsync(tempPath);

            metadata.Tags.Should().BeEquivalentTo(expectedTags);
        }

        private string CreateTempFile()
        {
            return Path.GetTempFileName();
        }
    }
}
