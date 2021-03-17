using System;

namespace ArtistDatabase
{
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