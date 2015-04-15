using System.Threading.Tasks;

namespace Icm.TagManager.Domain
{
    public class Service : IService
    {
        private readonly IMetadataRepository _repository;

        public Service(IMetadataRepository repository)
        {
            _repository = repository;
        }

        public async Task AddTagsToFileAsync(string path, params string[] tags)
        {
            FileMetadata metadata = await _repository.GetByPathAsync(path);

            foreach (string tag in tags)
            {
                metadata.AddTag(tag);
            }

            await _repository.SaveAsync(path, metadata);
        }

        public async Task RemoveTagsFromFileAsync(string path, params string[] tags)
        {
            FileMetadata metadata = await _repository.GetByPathAsync(path);

            foreach (string tag in tags)
            {
                metadata.RemoveTag(tag);
            }

            await _repository.SaveAsync(path, metadata);
        }

        public async Task<FileMetadata> GetMetadataAsync(string tempPath)
        {
            return await _repository.GetByPathAsync(tempPath);
        }
    }
}