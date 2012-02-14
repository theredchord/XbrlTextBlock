using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace XbrlTextBlock
{
    public class Reader
    {
        public void ReadXml(string xmlFile, string fileId)
        {
            var xmlDocument = XDocument.Load(xmlFile);
            var elements = xmlDocument.Descendants();

            var spacer = new AddSpaces();

            using (FileStream fs = File.Create("XbrlTextBlock-" + fileId + ".csv"))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                foreach (var el in elements)
                {
                    if (el.Name.ToString().Contains("TextBlock"))
                    {
                        Console.WriteLine("Tag definition: " + el.Name.LocalName.ToString());

                        Console.WriteLine(" ");
                        Console.WriteLine("Tag content:");
                        Console.WriteLine("*** " + el.Value + " ***");
                        Console.WriteLine(" ");

                        var name = spacer.AddSpacesToSentence(el.Name.LocalName);

                        var result = el.Value
                            .Replace(",", ";")
                            .Replace("\"", "*");

                        writer.WriteLine(String.Format("\"{0}\",\"{1}\",", name, result));
                    }
                }
                fs.Flush();
            }
        }
    }
}
