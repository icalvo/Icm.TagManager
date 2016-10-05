using System;
using System.Threading.Tasks;
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

        [Verb(Description = "Add new tag to file")]
        public async Task Add(
            [Description("Path of the file to which the tags will be added")]string path,
            [Description("List of tags")]string[] tags)
        {
            await _service.AddTagsToFileAsync(path, tags);
        }

        [Verb]
        public async Task Show(string path)
        {
            var metadata = await _service.GetMetadataAsync(path);

            Console.WriteLine("Tags: {0}", string.Join(", ", metadata.Tags));
        }

        [Verb]
        public async Task Remove(string path, string[] tags)
        {
            await _service.RemoveTagsFromFileAsync(path, tags);
        }

        [Error]
        public void HandleError(ExceptionContext context)
        {
            var aggregate = context.Exception as AggregateException;
            if (aggregate != null)
            {
                foreach (var exception in aggregate.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            else
            {
                Console.WriteLine(context.Exception.Message);
            }
        }
    }
}