using System;

namespace ArtistDatabase
{
        public class terminalControl{
        MongoCRUD db = new MongoCRUD("ArtistAddressBook");
        public bool Operating(){
            switch(Console.ReadLine()){
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
                case "Delete":
                    Delete();
                    return true;
                case "Exit":
                    return false;
                case "Clear":
                Console.Clear();
                    return true;
                default:
                    Console.WriteLine("Not a valid command try again.");
                    return true;
            }
        }
        public void Create(){
            Console.WriteLine("Write firstname, lastname, birthdate, addresse,");
            Console.WriteLine("and location on driver of resume or null of");
            Console.WriteLine("artist you want to create.");
            Console.WriteLine("seperate each element with a comma and space");
            string recToCreate = Console.ReadLine();
            string[] recInput = recToCreate.Split(", ");
            string[] textInput;
            if(System.IO.File.Exists(recInput[4])){
                textInput = System.IO.File.ReadAllLines($@"{recInput[4]}");
            }else{
                textInput = null;
            }
            //Creates a new Artist called halfdan via the class Artismodel
            ArtistModel artist = new ArtistModel{
                Firstname = recInput[0],
                Lastname = recInput[1],
                Birthdate = recInput[2],
                Addresses = recInput[3],
                Resume = new FileReader{
                    //find the path where you saved the .txt and insert it
                    // location for my resume D:\HalfdanTheVillain\ArtistDatabase\HalfdanResume.txt
                    text = textInput
                }
            };

            db.InsertRecord("Artist", artist);
        }
        public void Read(){
            //collect all records from the artist table
            var recs = db.ReadRecords<ArtistModel>("Artist");
            //foreach record in recs print the name and birthday and address if it is registeret
            foreach (var rec in recs){
                Console.WriteLine($"{rec.Id} \n{rec.Firstname} \n{rec.Lastname} \n{rec.Birthdate}");
                //if the artist has a address registeret print it
                if(rec.Addresses != null){
                    Console.WriteLine(rec.Addresses);
                }
                Console.WriteLine();
            }
            //Console.ReadLine();
        }
        public void ReadOneRec(){
            Console.WriteLine("First write which table you want, then id of the artist you want");
            Console.WriteLine("seperate each element with a comma and space");
            string recToRead = Console.ReadLine();
            string[] recInput = recToRead.Split(", ");
            //Turn string to guid
            Guid inputGuid = Guid.Parse(recInput[1]);
            var displayRec = db.ReadOneRecord<ArtistModel>(recInput[0], inputGuid);
            Console.WriteLine($"{displayRec.Firstname} \n{displayRec.Lastname} \n{displayRec.Birthdate} \n{displayRec.Addresses}");
        }
        public void Delete(){
            Console.WriteLine("Write from which table you want to delete and the id of the record");
            Console.WriteLine("seperate each element with a comma and space");
            string recToDelete = Console.ReadLine();
            string[] recInput = recToDelete.Split(", ");
            Guid inputGuid = Guid.Parse(recInput[1]);
            db.DeleteRecord<ArtistModel>(recInput[0], inputGuid);
        }
        public void Help(){
            Console.WriteLine("Write ReadAll, to read all records");
            Console.WriteLine("Write ReadOne, to read one record");
            Console.WriteLine("Write Create, to create a new record");
            Console.WriteLine("Write Delete, to delete a record");
            Console.WriteLine("Write Exit, to exit");
            //Console.ReadLine();
        }
    }
}