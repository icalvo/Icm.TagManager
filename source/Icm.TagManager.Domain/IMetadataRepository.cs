using System.Threading.Tasks;

namespace Icm.TagManager.Domain
{
    public interface IMetadataRepository
    {
        Task<FileMetadata> GetByPathAsync(string path);

        Task SaveAsync(string path, FileMetadata metadata);
    }
}