using System;
using CLAP;
using Icm.TagManager.Domain;

namespace Icm.TagManager.CommandLine
{
    internal class Application
    {
        private readonly IService _service;

        public Application(IService service)
        {
            _service = service;
        }

        [Verb]
        public void Add(string path, string[] tags)
        {
            _service.AddTagsToFileAsync(path, tags).Wait();
        }

        [Verb]
        public void Show(string path)
        {
            var metadata = _service.GetMetadataAsync(path).Result;

            Console.WriteLine("Tags: {0}", string.Join(", ", metadata.Tags));
        }

        [Verb]
        public void Remove(string path, string[] tags)
        {
            _service.RemoveTagsFromFileAsync(path, tags).Wait();
        }
    }
}