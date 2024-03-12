using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SerializationLab
{
    public class Events
    {
        
        public int? EventNumber { get; set; }
        public string? Location { get; set; }
        public Events(int num, string location)
        {
            this.EventNumber = num;
            this.Location = location;
        }
        public Events()
        {
            
        }

        public static void XmlSerializeEvent(string path,Events e)
        {
            Console.WriteLine("::::::::::Serializing event with Xml::::::::::");
            XmlSerializer serializer = new XmlSerializer(typeof(Events));
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, e);
            }
            Console.WriteLine("::::::::::Event Serilized::::::::::\n");

            // The above works fine for regular XML, now I want to try BXML instead
            //string bxmlString;
            //using (var stream = new MemoryStream())
            //{
            //    // serialize OBJ to BXML
            //    var writer = XmlDictionaryWriter.CreateBinaryWriter(stream);
            //    var serializer = new DataContractSerializer(typeof(Events));
            //    serializer.WriteObject(writer, e);
            //    writer.Flush();
            //    bxmlString = Convert.ToBase64String(stream.ToArray());             
            //}
            //File.WriteAllText(path, bxmlString);

        }

        public static void XmlDeserializeEvent(string path)
        {
            Console.WriteLine("::::::::::Deserializing Xml event::::::::::");
            XmlSerializer serializer = new XmlSerializer(typeof(Events));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                Events? e1 = (Events?)serializer.Deserialize(fs);
                Console.WriteLine(e1);
            }
            Console.WriteLine("::::::::::Deserialization Completed::::::::::\n");

            //// Now with BXML
            //string bxmlString = File.ReadAllText(path);
            //Events deserializedEvent;
            //using (var stream = new MemoryStream(Convert.FromBase64String(bxmlString)))
            //{
            //    var reader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max);
            //    var serializer = new DataContractSerializer(typeof(Events));
            //    deserializedEvent = (Events)serializer.ReadObject(reader);
            //}
            //Console.WriteLine(deserializedEvent);
        }

        public static void SerializeListJson(string path, List<Events> eventsList) 
        {
            Console.WriteLine(":::Serializing Events List using JSON:::");
            string jsonString = JsonSerializer.Serialize(eventsList);
            File.WriteAllText(path, jsonString);
            Console.WriteLine(":::Serialization Completed:::\n");
        }

        public static void DeserializeListJson(string path)
        {
            Console.WriteLine(":::Deserializing JSON file:::");
            string deserializedJsonString = File.ReadAllText(path);
            List<Events>? deseilaizedEventsList = JsonSerializer.Deserialize<List<Events>>(deserializedJsonString);
            if (deseilaizedEventsList != null)
            {
                foreach (Events e in deseilaizedEventsList)
                {
                    Console.WriteLine(e);
                }
            }
            Console.WriteLine(":::Deseralization Complete:::\n");
        }

        public static void ReadFromFile(string path, string word)
        {
            // write "Hackathon" to the file
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(word);
            }
            Console.WriteLine($"In the word '{word}'...");

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                //Find first character (set pointer position in file)
                fs.Seek(0, SeekOrigin.Begin);
                //pull and represent as byte
                int first = fs.ReadByte();
                //cast byte into char
                char firstChar = (char)first;
                // write character to console
                Console.WriteLine($"The first character is: '{firstChar}'");

                //repeat for middle and last
                fs.Seek(fs.Length / 2, SeekOrigin.Begin);
                int middle = fs.ReadByte();
                char middleChar = (char)middle;
                Console.WriteLine($"The middle character is: '{middleChar}'");

                fs.Seek(-1, SeekOrigin.End);
                int last = fs.ReadByte();
                char lastChar = (char)last;
                Console.WriteLine($"The last character is: '{lastChar}'\n");

            }
        }

        public override string ToString()
        {
            // overrides the ToString method
            return $"Event #: {EventNumber}\n" +
                $"Location: {Location}\n";
        }
    }
}
