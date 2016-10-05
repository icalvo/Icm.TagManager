using System;
using System.Collections.Generic;
using System.Linq;

namespace Icm.TagManager.Domain
{
    public class FileMetadata
    {
        private readonly HashSet<string> _tags;
        private string _path;

        public FileMetadata(string path) : this(path, Enumerable.Empty<string>())
        {
            Path = path;
        }

        public FileMetadata(string path, params string[] tags) : this(path, tags.AsEnumerable())
        {
        }

        public FileMetadata(string path, IEnumerable<string> tags)
        {
            if (tags == null) throw new ArgumentNullException(nameof(tags));
            Path = path;
            _tags = new HashSet<string>(tags.Select(x => x.ToLowerInvariant()));
        }
        
        public IEnumerable<string> Tags => _tags;

        public string Path
        {
            get { return _path; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Path cannot be null");
                }

                _path = value;
            }
        }

        public void AddTag(string tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            _tags.Add(tag.Trim().ToLowerInvariant());
        }

        public void RemoveTag(string tag)
        {
            _tags.Remove(tag);
        }
    }
}
