using System;
using MongoDB.Bson.Serialization.Attributes;
namespace ArtistDatabase
{
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
}