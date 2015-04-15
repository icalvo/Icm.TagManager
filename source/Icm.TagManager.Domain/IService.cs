using System.Threading.Tasks;

namespace Icm.TagManager.Domain
{
    public interface IService
    {
        Task AddTagsToFileAsync(string path, params string[] tags);
        Task<FileMetadata> GetMetadataAsync(string path);
        Task RemoveTagsFromFileAsync(string path, params string[] tags);
    }
}