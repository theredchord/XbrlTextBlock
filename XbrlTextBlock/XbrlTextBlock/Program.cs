using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XbrlTextBlock
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlTextReader reader = new XmlTextReader("goog-20111231.xml");

            while (reader.Read())
            {
                switch(reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if(reader.Name.Contains("TextBlock") && (!reader.Name.StartsWith("us-gaap")))
                        {
                            Console.WriteLine(reader.Name);
                        }
                        break;
                    case XmlNodeType.Text:
                        if (reader.Name.Contains("TextBlock") && (!reader.Name.StartsWith("us-gaap")))
                        {
                            Console.WriteLine(reader.Value);
                        }
                        break;
                }
            }

            Console.ReadLine();
        }
    }
}
