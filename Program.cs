using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ArtistDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello world!");
            MongoCRUD db = new MongoCRUD("Artist"); 
            Console.ReadLine();
        }
    }
    public class MongoCRUD
        {
        private IMongoDatabase db;
        public MongoCRUD(string database){
            //create client for connecting to database
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record){
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }
    }
}
