using CLAP;
using Icm.TagManager.Domain;
using Icm.TagManager.Infrastructure;

namespace Icm.TagManager.CommandLine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var metadataRepository = new MongoMetadataRepository("mongodb://localhost", "tagmanager");
            var service = new Service(metadataRepository);
            var application = new Application(service);
            Parser.RunConsole(args, application);
        }
    }
}