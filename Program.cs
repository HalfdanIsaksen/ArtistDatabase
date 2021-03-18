using System;

namespace ArtistDatabase
{
    class Program
    {
    //MongoCRUD db = new MongoCRUD("ArtistAddressBook");
        static void Main(string[] args)
        {
            Console.WriteLine("Write Help to get a list of commands");
            terminalControl tc = new terminalControl();
            bool showOperations = true;
            while(showOperations){
                showOperations = tc.Operating();
            }
        }
    }
}
