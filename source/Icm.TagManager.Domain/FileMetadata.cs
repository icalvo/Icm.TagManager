using System;
using System.Collections.Generic;
using System.Linq;

namespace Icm.TagManager.Domain
{
    public class FileMetadata
    {
        private readonly HashSet<string> _tags;

        public FileMetadata(string path)
        {
            Path = path;
            _tags = new HashSet<string>();
        }

        public FileMetadata(string path, IEnumerable<string> tags)
        {
            Path = path;
            _tags = new HashSet<string>(tags.Select(x => x.ToLowerInvariant()));
        }

        public Guid Id { get; set; }

        public IEnumerable<string> Tags
        {
            get { return _tags; }
        }

        public string Path { get; private set; }

        public void AddTag(string tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException("tag");
            }

            _tags.Add(tag.ToLowerInvariant());
        }

        public void RemoveTag(string tag)
        {
            _tags.Remove(tag);
        }
    }
}
