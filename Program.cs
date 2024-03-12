/*
    Chloe Nibali - 000913397
    Lab 6 - Serialization and RAF
    Mar 12, 2024
    CPRG 211 E, OOP 2

*/

namespace SerializationLab
{

    internal class Program
    {
        static void Main(string[] args)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, @"..\..\..\events.txt");
            string jsonPath = Path.Combine(basePath, @"..\..\..\events.json");
            string streamPath = Path.Combine(basePath, @"..\..\..\stream.txt");


            //Task 1: Create Events class with number and location attributes
            // -Done

            //Task 2: Create an object with numner = 1 and location = Calgary
            Console.WriteLine(":::Creating first event:::");
            Events e1 = new Events(1,"Calgary");

            //Task 3: Seialize event to file called events.txt
            // I used Xml for this task
            Events.XmlSerializeEvent(filePath, e1);

            //Task 4: deserialize and display to console
            Events.XmlDeserializeEvent(filePath);

            //Task 5: Create 4 OBJ and serialize to Json File, Then deserialize and display to console
            Console.WriteLine(":::Creating 4 additional Evnet Objects:::");
            Events e2 = new Events(2, "Wyoming");
            Events e3 = new Events(3, "Rio");
            Events e4 = new Events(4, "International Space Station");
            Events e5 = new Events(5, "Behind Denny's");

            Console.WriteLine(":::Adding new objects to a list:::");
            List<Events> eventList = new List<Events>([e2,e3,e4,e5]);

            Events.SerializeListJson(jsonPath, eventList);

            Events.DeserializeListJson(jsonPath);

            //Task 6: Create method called ReadFromFile, in which you write "Hackathon" to the file, then read back
            // the first, middle and last letters using StreamWriter and Seek method
            Events.ReadFromFile(streamPath,"Hackathon");
            Events.ReadFromFile(streamPath, "Serialization");

            
        }
    }
}
