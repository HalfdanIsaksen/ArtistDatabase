using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace ArtistDatabase
{
    class Program
    {
    MongoCRUD db = new MongoCRUD("ArtistAddressBook");
        static void Main(string[] args)
        {
            Console.WriteLine("hello world!");
        }
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
            var recs = db.ReadRecords<ArtistModel>("Artist");
            foreach (var rec in recs){
                Console.WriteLine($"{rec.Firstname}: {rec.Lastname}: {rec.Birthdate}:");
                
                if(rec.Addresses != null){
                    Console.WriteLine(rec.Addresses);
                }
                Console.WriteLine();
            }
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

//inserting a table in the databse with a record
        public void InsertRecord<T>(string table, T record){
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> ReadRecords<T>(string table){
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }
    }

    //creating a class for the basic information the databse needs to hold about an artist
    public class ArtistModel{
        /*[BsonId]
        public ObjectId Id{get; set;}*/
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
