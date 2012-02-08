using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace XbrlTextBlock
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlTextReader reader = new XmlTextReader("goog-20111231.xml");

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && (!reader.Name.StartsWith("us-gaap")) && reader.Name.Contains("TextBlock"))
                {
                    Console.WriteLine("Tag definition: " + reader.Name);

                    var xbrlText = reader.ReadElementContentAsString();

                    Console.WriteLine(" ");
                    Console.WriteLine("Tag content:");
                    Console.WriteLine("*** " + xbrlText + " ***");
                    Console.WriteLine(" ");
                }
            }

            Console.ReadLine();
        }
    }
}
