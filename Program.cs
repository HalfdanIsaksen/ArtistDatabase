using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel;

namespace ArtistDatabase
{
    class Program
    {
    //MongoCRUD db = new MongoCRUD("ArtistAddressBook");
        static void Main(string[] args)
        {
            terminalControl tc = new terminalControl();
            bool showOperations = true;
            while(showOperations){
                showOperations = tc.Operating();
            }
        }
        /*
        public void Create(){
            //Creates a new Artist called halfdan via the class Artismodel
            ArtistModel halfdan = new ArtistModel{
                Firstname = "Halfdan",
                Lastname = "Isaksen",
                Birthdate = "30/01/1998",
                Addresses = "Julius Bloms Gade 13",
                Resume = new FileReader{
                    //find the path where you saved the .txt and insert it
                    text = System.IO.File.ReadAllLines(@"D:\HalfdanTheVillain\ArtistDatabase\HalfdanResume.txt")
                }
            };

            db.InsertRecord("Artist", halfdan);
        }
        public void Read(){
            //collect all records from the artist table
            var recs = db.ReadRecords<ArtistModel>("Artist");
            //foreach record in recs print the name and birthday and address if it is registeret
            foreach (var rec in recs){
                Console.WriteLine($"{rec.Id}: {rec.Firstname}: {rec.Lastname}: {rec.Birthdate}:");
                //if the artist has a address registeret print it
                if(rec.Addresses != null){
                    Console.WriteLine(rec.Addresses);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        public void ReadOneRec(){
            Console.WriteLine("First write which table you want, then id of the artist you want");
            Console.WriteLine("seperate each element with a comma and space");
            string recToRead = Console.ReadLine();
            string[] recInput = recToRead.Split(", ");
            //Turn string to guid
            Guid inputGuid = Guid.Parse(recInput[1]);
            db.ReadOneRecord<ArtistModel>(recInput[0], inputGuid);
        }
        public bool Operating(){
            switch(Console.ReadLine()){
                case "Start":
                    Start();
                    return true;
                case "Help":
                    Help();
                    return true;
                case "ReadAll":
                    Read();
                    return true;
                case "ReadOne":
                    ReadOneRec();
                    return true;
                case "Create":
                    Create();
                    return true;
                default:
                    Console.WriteLine("Not a valid command try again.");
                    return true;
            }
        }

        public void Start(){

        }
        public void Help(){
            Console.WriteLine("Write ReadAll, to read all records");
            Console.WriteLine("Write ReadOne, to read one record");
            Console.WriteLine("Write Create, to create a new record");
            Console.WriteLine("");
            Console.ReadLine();
        }
        
        public static Type GetType(string typeName){
            var type = Type.GetType(typeName);
            if(type != null) return type;
            return null;
        }
        //this converts a string to a type specified, not exactly what i wanted
        public static T GetTFromString<T>(string typeString){
            var typeReturned = TypeDescriptor.GetConverter(typeof(T));
            return (T)(typeReturned.ConvertFromInvariantString(typeString));
        }*/
    }
    public class terminalControl{
        MongoCRUD db = new MongoCRUD("ArtistAddressBook");
        public void Create(){
            Console.WriteLine("Write firstname, lastname, birthdate, addresse,");
            Console.WriteLine("and location on driver of resume or null of");
            Console.WriteLine("artist you want to create.");
            Console.WriteLine("seperate each element with a comma and space");
            string recToCreate = Console.ReadLine();
            string[] recInput = recToCreate.Split(", ");
            //Creates a new Artist called halfdan via the class Artismodel
            ArtistModel artist = new ArtistModel{
                Firstname = recInput[0],
                Lastname = recInput[1],
                Birthdate = recInput[2],
                Addresses = recInput[3],
                Resume = new FileReader{
                    //find the path where you saved the .txt and insert it
                    // location fdor my resume D:\HalfdanTheVillain\ArtistDatabase\HalfdanResume.txt
                    text = System.IO.File.ReadAllLines($@"{recInput[4]}")
                }
            };

            db.InsertRecord("Artist", artist);
        }
        public void Read(){
            //collect all records from the artist table
            var recs = db.ReadRecords<ArtistModel>("Artist");
            //foreach record in recs print the name and birthday and address if it is registeret
            foreach (var rec in recs){
                Console.WriteLine($"{rec.Id}: {rec.Firstname}: {rec.Lastname}: {rec.Birthdate}:");
                //if the artist has a address registeret print it
                if(rec.Addresses != null){
                    Console.WriteLine(rec.Addresses);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        public void ReadOneRec(){
            Console.WriteLine("First write which table you want, then id of the artist you want");
            Console.WriteLine("seperate each element with a comma and space");
            string recToRead = Console.ReadLine();
            string[] recInput = recToRead.Split(", ");
            //Turn string to guid
            Guid inputGuid = Guid.Parse(recInput[1]);
            db.ReadOneRecord<ArtistModel>(recInput[0], inputGuid);
        }
        public bool Operating(){
            switch(Console.ReadLine()){
                case "Start":
                    Start();
                    return true;
                case "Help":
                    Help();
                    return true;
                case "ReadAll":
                    Read();
                    return true;
                case "ReadOne":
                    ReadOneRec();
                    return true;
                case "Create":
                    Create();
                    return true;
                case "Exit":
                    return false;
                default:
                    Console.WriteLine("Not a valid command try again.");
                    return true;
            }
        }

        public void Start(){

        }
        public void Help(){
            Console.WriteLine("Write ReadAll, to read all records");
            Console.WriteLine("Write ReadOne, to read one record");
            Console.WriteLine("Write Create, to create a new record");
            Console.WriteLine("");
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
            var filter = Builders<T>.Filter.Eq("id", id);
            collection.DeleteOne(filter);
        }
    }

    //creating a class for the basic information the databse needs to hold about an artist
    public class ArtistModel{
        [BsonId]
        public Guid Id{get; set;}
        public string Firstname{get; set;}
        public string Lastname{get; set;}
        public string Birthdate{get; set;}
        public string Addresses{get; set;}
        //if im corret i can initalize the class by calling Artistmodel.resume = new filereader() later
        public FileReader Resume{get; set;}
    }

    public class FileReader{
        //FileReader lets you upload a .txt file and then converts it to a string array
        //Husk at det sted .txt filen skal findes kan gøres som et console.readline med placeringen af tekstfilen på computeren
        public string[] text{get; set;}
        //Prints the resume in the terminal
        public void PrintResume(){
            foreach (string line in text){
                Console.WriteLine("\t" + line);
            }
        }
    }
}
