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
            if (tags == null) throw new ArgumentNullException("tags");
            Path = path;
            _tags = new HashSet<string>(tags.Select(x => x.ToLowerInvariant()));
        }

        public Guid Id { get; set; }

        public IEnumerable<string> Tags
        {
            get { return _tags; }
        }

        public string Path
        {
            get { return _path; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "Path cannot be null");
                }

                _path = value;
            }
        }

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
