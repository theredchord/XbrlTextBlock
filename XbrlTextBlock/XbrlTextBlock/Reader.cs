using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace XbrlTextBlock
{
    public class Reader
    {
        public void ReadXml(string xmlFile, int fileId)
        {
            var reader = new XmlTextReader(xmlFile);
            using (FileStream fs = File.Create("XbrlTextBlock" + fileId + ".csv"))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                while (reader.Read())
                {
                    //Checks for specific tags and writes tag contents to new file
                    if (reader.NodeType == XmlNodeType.Element && reader.Name.Contains("TextBlock") && (reader.Name.Contains("Acquisitions") || reader.Name.Contains("BusinessCombination") || reader.Name.Contains("Contingencies") || reader.Name.Contains("RelatedParty")))
                    {
                        Console.WriteLine("Tag definition: " + reader.Name);

                        writer.WriteLine(String.Format("\"{0}\",", reader.Name));

                        var xbrlText = reader.ReadElementContentAsString();
                        var textBlock = xbrlText.Replace(",", ";");

                        String result = Regex.Replace(textBlock, @"<[^>]*>", String.Empty);
                        var trimResult = result.Trim();
                        var final = trimResult.Replace("&#xA0;", " ");
                        var final2 = final.Replace("&#160;", " ");

                        Console.WriteLine(" ");
                        Console.WriteLine("Tag content:");
                        Console.WriteLine("*** " + final2 + " ***");
                        Console.WriteLine(" ");

                        writer.WriteLine(String.Format("\"{0}\"", final2));
                    }
                }
                fs.Flush();
            }
        }
    }
}
