﻿using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SayWhat.MongoDAL.Dictionary
{
    public class DictionaryRepo: IMongoRepo
    {
        public const string DictionaryCollectionName = "dictionary";
        public const string WordFieldBsonName = "w";
        
        private readonly IMongoDatabase _db;

        public DictionaryRepo(IMongoDatabase db) => _db = db;
        
        public Task Add(DictionaryWord word) => Collection.InsertOneAsync(word);
        public Task<long> GetCount() => Collection.CountDocumentsAsync(new BsonDocument());

        public Task<DictionaryWord> GetOrDefault(string origin) => 
            Collection
            .Find(Builders<DictionaryWord>.Filter.Eq(WordFieldBsonName, origin))
            .FirstOrDefaultAsync(); 
        
        private IMongoCollection<DictionaryWord> Collection 
            => _db.GetCollection<DictionaryWord>(DictionaryCollectionName);

        public async Task UpdateDb()
        {
            await Collection.Indexes.DropAllAsync();
            var keys = Builders<DictionaryWord>.IndexKeys.Ascending(WordFieldBsonName);
            var indexOptions = new CreateIndexOptions<DictionaryWord>
            {
                Unique = true ,
            };
            var model = new CreateIndexModel<DictionaryWord>(keys, indexOptions);
            await Collection.Indexes.CreateOneAsync(model);
            
            /*var options = new CreateIndexOptions { Unique = true };
            var field = new StringFieldDefinition<DictionaryWord>(WordFieldBsonName);
            var indexDefinition = new IndexKeysDefinitionBuilder<DictionaryWord>().Ascending(field);
            var indexModel = new CreateIndexModel<DictionaryWord>(indexDefinition,options);

            return Collection.Indexes.CreateOneAsync(indexModel);*/
        }
    }
}