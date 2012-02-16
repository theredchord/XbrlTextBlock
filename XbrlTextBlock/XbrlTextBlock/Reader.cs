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
        public void ReadXml(string xmlFile, string fileId, FileStream fs, StreamWriter writer)
        {
            var xmlDocument = XDocument.Load(xmlFile);
            var elements = xmlDocument.Descendants();

            string keywords = "(Acquisition|Affiliates|Agreement|BusinessOverview|BusinessSegment|Contingencies|Litigation|Customer|Employee|Intangible|LegalMatters|LegalProceedings|MajorCustomer|Merger|Transaction|Royalty)";

            foreach (var el in elements)
            {
                if (el.Name.LocalName.ToString().Contains("TextBlock"))
                {
                    if (Regex.IsMatch(el.Name.LocalName, keywords))
                    {
                        var index = el.Name.LocalName.ToString().IndexOf("TextBlock");
                        var keyword = el.Name.LocalName.ToString().Substring(0, index);

                        Console.WriteLine("Tag definition: " + el.Name.LocalName.ToString());

                        //Console.WriteLine(" ");
                        //Console.WriteLine("Tag content:");
                        //Console.WriteLine("*** " + el.Value + " ***");
                        //Console.WriteLine(" ");

                        //FileId displayed with tag name
                        //writer.WriteLine(String.Format("\"{0}\",\"{1}\"", fileId, el.Name.LocalName));

                        //FileId displayed with tag content
                        //writer.WriteLine(String.Format("\"{0}\",\"{1}\"", fileId, el.Value.ToString()));

                        using (FileStream fs1 = File.Create(fileId + "_" + keyword + ".html"))
                        using (StreamWriter writer1 = new StreamWriter(fs1))
                        {
                            writer1.WriteLine(el.Value.ToString());
                        }
                    }
                }
            }
            writer.WriteLine(",,");
            fs.Flush();
        }
    }
}
