using System;
using System.Threading.Tasks;
using Icm.TagManager.Domain;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Icm.TagManager.Infrastructure
{
    public class MongoMetadataRepository : IMetadataRepository
    {
        private readonly IMongoCollection<FileMetadata> _collection;

        static MongoMetadataRepository()
        {
            BsonClassMap.RegisterClassMap<FileMetadata>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(x => x.Path);
                cm.MapProperty(x => x.Tags);
                cm.MapCreator(x => new FileMetadata(x.Path, x.Tags));
            });
        }

        public MongoMetadataRepository(string connectionString, string databaseName)
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            _collection = database.GetCollection<FileMetadata>("filemetadata");
        }
        public async Task<FileMetadata> GetByPathAsync(string path)
        {
            FileMetadata metadata = null;

            try
            {
                metadata = await _collection.Find(x => x.Path == path).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (metadata == null)
            {
                return new FileMetadata(path);
            }

            return metadata;
        }

        public async Task SaveAsync(string path, FileMetadata metadata)
        {
            await _collection.ReplaceOneAsync(x => x.Path == path, metadata, new UpdateOptions {IsUpsert = true});
        }

        public Task WipeAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
