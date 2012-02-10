using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace XbrlTextBlock
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new XmlTextReader("goog-20111231.xml");
            using (FileStream fs = File.Create("XbrlTextBlock.csv"))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && (!reader.Name.StartsWith("us-gaap")) && reader.Name.Contains("TextBlock"))
                    {
                        Console.WriteLine("Tag definition: " + reader.Name);

                        writer.WriteLine(String.Format("\"{0}\",", reader.Name));

                        var xbrlText = reader.ReadElementContentAsString();
                        var textBlock = xbrlText.Replace(",", ";");

                        String result = Regex.Replace(textBlock, @"<[^>]*>", String.Empty);
                        var final = result.Replace("&#xA0;", " ");

                        var header = final.Substring(0, 50);

                        Console.WriteLine(" ");
                        Console.WriteLine("Tag content:");
                        Console.WriteLine("*** " + final + " ***");
                        Console.WriteLine(" ");

                        writer.WriteLine(String.Format("\"{0}\"", header));
                        writer.WriteLine(String.Format("\"{0}\"", final));
                    }
                }
                writer.Flush();
                writer.Close();
            }

            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}
