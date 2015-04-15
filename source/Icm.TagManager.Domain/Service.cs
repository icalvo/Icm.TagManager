using System.Threading.Tasks;

namespace Icm.TagManager.Domain
{
    public class Service : IService
    {
        private readonly IMetadataRepository _repository;
        private readonly IPathNormalizer _normalizer;

        public Service(IMetadataRepository repository, IPathNormalizer normalizer)
        {
            _repository = repository;
            _normalizer = normalizer;
        }

        public async Task AddTagsToFileAsync(string path, params string[] tags)
        {
            var normalizedPath = _normalizer.Normalize(path);
            var metadata = await _repository.GetByPathAsync(normalizedPath);

            foreach (var tag in tags)
            {
                metadata.AddTag(tag);
            }

            await _repository.SaveAsync(normalizedPath, metadata);
        }

        public async Task RemoveTagsFromFileAsync(string path, params string[] tags)
        {
            var normalizedPath = _normalizer.Normalize(path);
            var metadata = await _repository.GetByPathAsync(normalizedPath);

            foreach (var tag in tags)
            {
                metadata.RemoveTag(tag);
            }

            await _repository.SaveAsync(normalizedPath, metadata);
        }

        public async Task<FileMetadata> GetMetadataAsync(string path)
        {
            var normalizedPath = _normalizer.Normalize(path);
            return await _repository.GetByPathAsync(normalizedPath);
        }
    }
}