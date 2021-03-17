//CRUD = Create Read Update Delete
using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ArtistDatabase
{
    public class MongoCRUD
        {
        private IMongoDatabase db;
        public MongoCRUD(string database){
            //create client for connecting to database
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }
        //inserting a record in the table in the databse
        public void InsertRecord<T>(string table, T record){
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }
        public List<T> ReadRecords<T>(string table){
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }
        //find on record in the table by searching for its id
        public T ReadOneRecord<T>(string table, Guid id){
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        public void UpsertRecord<T>(string table, Guid id, T record){
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record,
                new UpdateOptions{ IsUpsert = true});
        }

        public void DeleteRecord<T>(string table, Guid id){
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
    }
}