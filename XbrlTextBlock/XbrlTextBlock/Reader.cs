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
        public void ReadXml(string xmlFile, string fileId)
        {
            var reader = new XmlTextReader(xmlFile);

            using (FileStream fs = File.Create("XbrlTextBlock-" + fileId + ".csv"))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                while (reader.Read())
                {
                    //Checks for specific tags and writes tag contents to new file
                    if (reader.NodeType == XmlNodeType.Element && reader.Name.Contains("TextBlock") && (reader.Name.Contains("Acquisitions") 
                        || reader.Name.Contains("BusinessCombination") || reader.Name.Contains("Contingencies") || reader.Name.Contains("RelatedParty")))
                    {
                        if (!String.IsNullOrEmpty(reader.Name))
                        {
                            Console.WriteLine("Tag definition: " + reader.Name);
                            writer.WriteLine(String.Format("\"{0}\",", reader.Name));
                        }

                        var xbrlText = reader.ReadElementContentAsString();
                        var textBlock = xbrlText.Replace(",", ";");

                        String result = Regex.Replace(textBlock, @"<[^>]*>", String.Empty);
                        var trimResult = result.Trim();
                        var final = trimResult.Replace("&#xA0;", " ")
                            .Replace("&#160;", " ")
                            .Replace("&nbsp;", " ")
                            .Replace("&#x2013;", " ");

                        if (!String.IsNullOrEmpty(final))
                        {
                            Console.WriteLine(" ");
                            Console.WriteLine("Tag content:");
                            Console.WriteLine("*** " + final + " ***");
                            Console.WriteLine(" ");

                            writer.WriteLine(String.Format("\"{0}\"", final));
                        }
                    }
                }
                fs.Flush();
            }
        }
    }
}
