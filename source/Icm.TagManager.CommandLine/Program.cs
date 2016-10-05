using Icm.TagManager.Domain;
using Icm.TagManager.Infrastructure;

namespace Icm.TagManager.CommandLine
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            IMetadataRepository metadataRepository;
            // metadataRepository = new MongoMetadataRepository("mongodb://localhost", "tagmanager");
            metadataRepository = new FileMetadataRepository();
            var normalizer = new FileSystemPathNormalizer();
            var service = new Service(metadataRepository, normalizer);
            var application = new Application(service);

            CLAP.MethodInvoker.Invoker = new AsyncMethodInvoker();
            return CLAP.Parser.Run(args, application);
        }
    }
}