using System.IO;
using Icm.TagManager.Domain;

namespace Icm.TagManager.Infrastructure
{
    public class FileSystemPathNormalizer : IPathNormalizer {
        public string Normalize(string path)
        {
            return Path.GetFullPath(path);
        }
    }
}