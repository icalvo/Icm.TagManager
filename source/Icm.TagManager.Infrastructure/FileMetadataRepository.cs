using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Icm.TagManager.Domain;

namespace Icm.TagManager.Infrastructure
{
    public class FileMetadataRepository : IMetadataRepository
    {
        private readonly string _databaseFile;

        public FileMetadataRepository()
        {
            _databaseFile = "tagdb.txt";
        }

        public async Task<FileMetadata> GetByPathAsync(string path)
        {
            File.AppendText(_databaseFile).Close();
            using (var sr = File.OpenText(_databaseFile))
            {
                string line = await sr.ReadLineAsync();
                while (line != null)
                {
                    var split = line.Split('|');
                    var readPath = split[0].Trim();
                    if (readPath == path)
                    {
                        return new FileMetadata(path, split[1].Split(',').Select(x => x.Trim()));
                    }

                    line = await sr.ReadLineAsync();
                }

                return new FileMetadata(path);
            }
        }

        public async Task SaveAsync(string path, FileMetadata metadata)
        {
            using (var sr = File.AppendText(_databaseFile))
            {
                await sr.WriteLineAsync(metadata.Path + " | " + string.Join(", ", metadata.Tags));
            }
        }

        public Task WipeAllAsync()
        {
            var fi = new FileInfo(_databaseFile);

            if (fi.Exists)
            {
                fi.Delete();
            }

            return Task.FromResult(false);
        }
    }
}