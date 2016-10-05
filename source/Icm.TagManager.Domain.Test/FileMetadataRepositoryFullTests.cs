using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Icm.TagManager.Infrastructure;
using Xunit;

namespace Icm.TagManager.Domain.Test
{
    public class MetadataRepositoryFullTests
    {
        [Fact]
        public async Task File()
        {
            await UseCase(() => new FileMetadataRepository());
        }

        private static async Task UseCase(Func<IMetadataRepository> repoBuilder)
        {
            IMetadataRepository repo = repoBuilder();

            IPathNormalizer normalizer = new FileSystemPathNormalizer();
            var service = new Service(repo, normalizer);
            string tempPath = CreateTempFile();
            string[] expectedTags = {"tag test 1", "tag 2"};
            await service.AddTagsToFileAsync(tempPath, expectedTags);

            repo = repoBuilder();
            service = new Service(repo, normalizer);

            FileMetadata metadata = await service.GetMetadataAsync(tempPath);

            metadata.Tags.Should().BeEquivalentTo(expectedTags);
        }

        private static string CreateTempFile()
        {
            return Path.GetTempFileName();
        }
    }
}
