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
            MongoCRUD db = new MongoCRUD("ArtistAddressBook"); 
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

    //creating a class for the basic information the databse needs to hold about an artist
    public class ArtistModel{
        public string Firstname{get; set;}
        public string Lastname{get; set;}
        public string Birthdate{get; set;}
        public string Addresses{get; set;}
        //if im corret i can initalize the class by calling Artistmodel.resume = new filereader() later
        public FileReader Resume{get; set;}
    }

    public class FileReader{
        //FileReader is gonna be a .txt file you can upload
        //important that when a file is going to be loaded and passed as
        //a string and you want the .txt to print in console that the file is
        //stored in a string array with each line as an index in the arrray

        //Husk at det sted .txt filen skal findes kan gøres som et console.readline med placeringen af tekstfilen på computeren
        private string[] text;
        public FileReader(string[] inputText){
            text = inputText;
        }
        //Prints the resume in the terminal
        public void PrintResume(){
            foreach (string line in text){
                Console.WriteLine("\t" + line);
            }
        }
    }
}
